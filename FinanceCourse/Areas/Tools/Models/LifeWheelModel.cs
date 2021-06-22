using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Models
{
    public class LifeWheelModel : ToolModel
    {
        [ValidateComplexType]
        public List<NameScorePair> LifeStats { get; set; }

        public override int ToolId { get => 2; }

        public LifeWheelModel()
        {
            if (ToolDataJson == null || ToolDataJson.Length == 0)
            {
                LifeStats = new();

                LifeStats.Add(new("Health", 0));
                LifeStats.Add(new("Finance", 0));
                LifeStats.Add(new("Friends", 0));
                LifeStats.Add(new("Relationship", 0));
            }
            else
                LifeStats = JsonConvert.DeserializeObject<List<NameScorePair>>(ToolDataJson);
        }

        public LifeWheelModel(ToolModel parentModel)
        {
            Id = parentModel.Id;
            ToolDataJson = parentModel.ToolDataJson;

            LifeStats = JsonConvert.DeserializeObject<List<NameScorePair>>(ToolDataJson);
        }

        public override void SaveJson()
        {
            ToolDataJson = JsonConvert.SerializeObject(LifeStats);
            base.ToolId = ToolId;
        }

        public class NameScorePair
        {
            [StringLength(20, MinimumLength = 3)]
            public string Name { get; set; }
            [Range(0, 10)]
            public int Score { get; set; }

            public NameScorePair(string key, int value)
            {
                Name = key;
                Score = value;
            }

            public NameScorePair(NameScorePair another)
            {
                Name = another.Name;
                Score = another.Score;
            }

            public NameScorePair() { }

            public void Reset()
            {
                Name = null;
                Score = 0;
            }
        }
    }
}
