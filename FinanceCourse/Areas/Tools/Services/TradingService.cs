using Alpaca.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Services
{
    public class TradingService
    {
        private AlpacaConfig _alpacaConfig;
        private IAlpacaDataClient DataClient { get; set; }
        private IAlpacaTradingClient TradingClient { get; set; }


        public TradingService(AlpacaConfig alpacaConfig)
        {
            _alpacaConfig = alpacaConfig;

            SecretKey secretKey = new(_alpacaConfig.Key_ID, _alpacaConfig.Secret_Key);

            DataClient = Environments.Paper
                .GetAlpacaDataClient(secretKey);

            TradingClient = Environments.Paper
                .GetAlpacaTradingClient(secretKey);
        }

        public async Task<List<double>> GetLast30DaysStockPriceAsync(string symbol, DateTime dateUtc)
        {
            dateUtc = dateUtc.Date;

            var bars = await DataClient.ListHistoricalBarsAsync(new(symbol, dateUtc.AddDays(-29), dateUtc, BarTimeFrame.Day));

            double[] res = new double[30];

            dateUtc = dateUtc.AddDays(-29);

            foreach(var bar in bars.Items)
                res[(bar.TimeUtc.Value.Date - dateUtc).Days] = (double) bar.Close;

            res[29] = await GetStockCurrentPriceAsync(symbol);

            for (int i = 1; i < 29; i++)
                if (res[i] == 0)
                    res[i] = res[i - 1];

            for (int i = 28; i >= 0; i--)
                if (res[i] == 0)
                    res[i] = res[i + 1];

            return res.ToList<double>();
        }

        public async Task<Dictionary<string, List<double>>> GetLast30DaysStockPriceAsync(DateTime dateUtc)
        {
            var symbols = await ListAssetsSymbolAsync();

            Dictionary<string, List<double>> res = new();

            foreach (var symbol in symbols)
                res[symbol] = await GetLast30DaysStockPriceAsync(symbol, dateUtc);

            return res;
        }

        public async Task<double> GetStockCurrentPriceAsync(string symbol)
        {
            return (Double)(await DataClient.GetLastTradeAsync(symbol)).Price;
        }

        public async Task<Dictionary<string, double>> ListAssetsSymbolPriceAsync()
        {
            var symbols = await ListAssetsSymbolAsync();

            List<Task<double>> tasks = new();

            foreach (var symbol in symbols)
                tasks.Add(GetStockCurrentPriceAsync(symbol));

            var res = await Task.WhenAll(tasks);

            Dictionary<string, double> assets = new();

            for (int i = 0; i < symbols.Count; i++)
                assets[symbols[i]] = res[i];

            return assets;
        }

        public async Task<List<string>> ListAssetsSymbolAsync()
        {
            var watchList = await TradingClient.GetWatchListByIdAsync(new(_alpacaConfig.Watch_List_ID));
            var assets = watchList.Assets;

            List<string> symbols = new();

            foreach (var asset in assets)
                symbols.Add(asset.Symbol);

            return symbols;
        }
    }

    public class AlpacaConfig
    {
        public string Key_ID { get; set; }
        public string Secret_Key { get; set; }
        public string Watch_List_ID { get; set; }
    }
}
