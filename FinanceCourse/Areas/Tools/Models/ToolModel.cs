using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Models
{
    public class ToolModel
    {
        private string _toolData;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ToolId { get; set; }

        [NotMapped]
        public JObject ToolData
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_toolData) ? "{}" : _toolData);
            }
            set
            {
                _toolData = value.ToString();
            }
        }
    }
}
