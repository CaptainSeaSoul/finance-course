using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Models
{
    public class ROIModel : ToolModel
    {
        [ValidateComplexType]
        public List<FormPackage> FormModel { get; set; }

        public override int ToolId { get => 3; }

        public ROIModel()
        {
            if (!String.IsNullOrEmpty(ToolDataJson))
                FormModel = JsonConvert.DeserializeObject<List<FormPackage>>(ToolDataJson);
            else
            {
                FormModel = new();
                FormModel.Add(new()
                {
                    Name = "Example task name",
                    Cost = 20000,
                    HoursTimeCost = 40,
                    Difficulty = 8,
                    EstimatedReturn = 30000,
                    Confidence = 0.7,
                    StartDate = new(2022, 8, 21),
                    EndDate = new(2023, 4, 13),
                    Intengibles = "It will make a great impact on ecology and make me feel content"
                });
                FormModel.Add(new()
                {
                    Name = "Finish the homework",
                    Cost = 0,
                    HoursTimeCost = 8,
                    Difficulty = 8,
                    EstimatedReturn = 0,
                    Confidence = 0.9,
                    StartDate = new(2022, 8, 21),
                    EndDate = new(2022, 8, 22),
                    Intengibles = "I'll get a good grade"
                });
                FormModel.Add(new()
                {
                    Name = "Invest in Tesla",
                    Cost = 500,
                    HoursTimeCost = 3,
                    Difficulty = 3,
                    EstimatedReturn = 800,
                    Confidence = 0.6,
                    StartDate = new(2022, 6, 11),
                    EndDate = new(2023, 6, 22)
                });
            }
        }

        public ROIModel(ToolModel parentModel)
        {
            Id = parentModel.Id;
            ToolDataJson = parentModel.ToolDataJson;

            FormModel = JsonConvert.DeserializeObject<List<FormPackage>>(ToolDataJson);
        }

        public override void SaveJson()
        {
            ToolDataJson = JsonConvert.SerializeObject(FormModel);
            base.ToolId = ToolId;
        }

        public class FormPackage
        {
            [StringLength(256, MinimumLength = 3)]
            [Required]
            public string Name { get; set; }
            public Double Cost { get; set; }
            public int HoursTimeCost { get; set; }
            [Range(0, 10)]
            public int Difficulty { get; set; }
            public Double EstimatedReturn { get; set; }
            [Range(0, 1.0)]
            public Double Confidence { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Intengibles { get; set; }
            public Double ReturnConfidence
            {
                get
                {
                    return EstimatedReturn * Confidence;
                }
            }
            public Double RealReturn { get; set; }

            public FormPackage(FormPackage another)
            {
                Initialize(another);
            }

            private void Initialize(FormPackage another)
            {
                Name = another.Name;
                Cost = another.Cost;
                HoursTimeCost = another.HoursTimeCost;
                Difficulty = another.Difficulty;
                EstimatedReturn = another.EstimatedReturn;
                Confidence = another.Confidence;
                StartDate = another.StartDate;
                EndDate = another.EndDate;
                Intengibles = another.Intengibles;
                RealReturn = another.RealReturn;
            }

            public void Reset()
            {
                Initialize(new());
            }

            public FormPackage() { }
        }
    }
}
