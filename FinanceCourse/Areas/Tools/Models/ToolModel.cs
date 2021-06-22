using FinanceCourse.Areas.Tools.Services;
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual int ToolId { get; set; }

        public string ToolDataJson { get; set; }

        public virtual void SaveJson()
        {

        }
    }
}
