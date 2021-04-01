using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Models
{
    public class QuizComponentModel : ToolModel
    {
        public class Answer  // for blazor to work see https://stackoverflow.com/questions/49365019/bind-list-of-objects-to-a-form
        {
            public AnswerOptions? AnswerOption { get; set; }
        }

        public List<Answer> Answers { get; set; }

        public QuizComponentModel()
        {
            if (ToolDataJson == null || ToolDataJson.Length == 0) {
                Answers = new List<Answer>(20);

                for (int i = 0; i < 20; i++)
                    Answers.Add(new Answer());
            }
            else
                Answers = JsonConvert.DeserializeObject<List<Answer>>(ToolDataJson);
        }

        public QuizComponentModel(ToolModel parentModel)
        {
            Id = parentModel.Id;
            ToolId = parentModel.ToolId;
            ToolDataJson = parentModel.ToolDataJson;

            Answers = JsonConvert.DeserializeObject<List<Answer>>(ToolDataJson);
        }

        public override void SaveJson()
        {
            ToolDataJson = JsonConvert.SerializeObject(Answers);
        }
    }

    public enum AnswerOptions
    {
        a,
        b,
        c,
        d,
        e
    }
}
