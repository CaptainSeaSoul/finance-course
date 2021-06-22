using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpaca.Markets;
using FinanceCourse.Areas.Tools.Services;
using Newtonsoft.Json;

namespace FinanceCourse.Areas.Tools.Models
{
    public class PaperTradingModel : ToolModel
    {
        public override int ToolId { get => 5; }

        private static readonly Double _defaultBalance = 100000;

        public Double Balance { get; set; } = _defaultBalance;

        public SortedDictionary<string, List<StockTransaction>> Transactions { get; private set; } = new();

        public SortedDictionary<string, StockTransaction> CurrentPossesion { get; private set; } = new();

        private TradingService _tradingService = null;

        public PaperTradingModel() { }

        public PaperTradingModel(TradingService tradingService)
        {
            _tradingService = tradingService;

            if (!(ToolDataJson == null || ToolDataJson.Length == 0))
                DeserializeJson(ToolDataJson);
        }

        public PaperTradingModel(ToolModel parentModel, TradingService tradingService)
        {
            _tradingService = tradingService;

            DeserializeJson(parentModel.ToolDataJson);

            Id = parentModel.Id;
            ToolDataJson = parentModel.ToolDataJson;
        }

        private void DeserializeJson(string toolDataJson)
        {
            var box = JsonConvert.DeserializeObject<
                Tuple<Double,
                SortedDictionary<string, List<StockTransaction>>,
                SortedDictionary<string, StockTransaction>>>(toolDataJson);

            Balance = box.Item1;
            Transactions = box.Item2;
            CurrentPossesion = box.Item3;
        }

        // updates the prices or add a new symbol
        public async Task CalculateCurrentPossesionAsync()
        {
            var dict = await _tradingService.ListAssetsSymbolPriceAsync();

            foreach (var pair in dict)
                if (CurrentPossesion.ContainsKey(pair.Key))
                    CurrentPossesion[pair.Key].Price = pair.Value;
                else
                    CurrentPossesion.Add(pair.Key, new(pair.Value, 0));
        }

        public async Task<List<double>> CalculateNetWorthGraphAsync() 
        {
            List<double> netWorth = new(30);

            if (_tradingService == null)
            {
                for (int i = 0; i < 30; i++)
                    netWorth.Add(_defaultBalance);

                return netWorth;
            }

            DateTime date = DateTime.UtcNow.Date;

            SortedDictionary<string, int> SymbolAmount = new();

            var Last30DaysPrices = await _tradingService.GetLast30DaysStockPriceAsync(date);

            double balance = _defaultBalance;

            date = date.AddDays(-29);
            // Calculate net worth for each day of the graph (30 days)
            for (int i = 0; i < 30; i++, date = date.AddDays(1))
            {
                // subtract all purchases made this day from the balance
                foreach (var symbol in Transactions)
                {
                    if (!SymbolAmount.ContainsKey(symbol.Key))
                        SymbolAmount[symbol.Key] = 0;

                    if (i == 0)
                    {
                        for (int j = 0; symbol.Value[j].TransactionDateUtc < date; j++)
                        {
                            SymbolAmount[symbol.Key] += symbol.Value[j].Amount;
                            balance -= symbol.Value[j].Summ();
                        }
                    }
                    else
                    {
                        var SymbolCurrentDateTransactions = symbol.Value.Where((t) => t.TransactionDateUtc == date);

                        foreach (var t in SymbolCurrentDateTransactions)
                        {
                            SymbolAmount[symbol.Key] += t.Amount;
                            balance -= t.Summ();
                        }
                    }
                }

                netWorth.Add(balance);

                // add stock value to the net worth at the end of the day
                foreach (var symbol in SymbolAmount)
                {
                    double price = Last30DaysPrices[symbol.Key][i];
                    netWorth[i] += symbol.Value * price;
                }
            }


            return netWorth;
        }

        public void AddTransaction(Double price, string symbol, int amount)
        {
            // Adding transaction to history
            if (!Transactions.ContainsKey(symbol))
                Transactions[symbol] = new();

            Transactions[symbol].Add(new(price, amount));

            // Changing current state
            if (CurrentPossesion.ContainsKey(symbol))
            {
                CurrentPossesion[symbol].Amount += amount;
                CurrentPossesion[symbol].Price = price;
            }
            else
                CurrentPossesion[symbol] = new(price, amount);

            Balance -= price * amount;
        }

        public override void SaveJson()
        {
            base.ToolId = ToolId;

            Tuple<Double,
                SortedDictionary<string, List<StockTransaction>>,
                SortedDictionary<string, StockTransaction>> box = new(Balance, Transactions, CurrentPossesion);
            ToolDataJson = JsonConvert.SerializeObject(box);
        }
    }

    public class StockTransaction
    {
        public Double Price { get; set; }
        public int Amount { get; set; } // positive if gain, negative if sell
        public DateTime? TransactionTimeUtc { get; set; }
        public DateTime? TransactionDateUtc { get => TransactionTimeUtc?.Date; }

        public StockTransaction()
        {
            Price = 0;
            Amount = 0;
            TransactionTimeUtc = null;
        }

        public StockTransaction(Double price, int amount)
        {
            Price = price;
            Amount = amount;
            TransactionTimeUtc = DateTime.UtcNow;
        }

        public Double Summ() => Price * Amount;
    }
}
