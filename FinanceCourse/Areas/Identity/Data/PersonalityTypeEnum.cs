using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Identity.Data
{
    public enum PersonalityTypeEnum
    {
        Hoarder,
        Spender,
        [Description("Money Monk")]
        Money_Monk,
        Avoider,
        Amasser,
        Worrier
    } 
}
