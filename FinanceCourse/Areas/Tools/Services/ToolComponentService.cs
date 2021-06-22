using FinanceCourse.Areas.Tools.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Services
{
    public class ToolComponentService
    {
        public List<Type> Tools { get; private set; }

        public ToolComponentService()
        {
            Tools = new List<Type> {
                typeof(QuizComponent),
                typeof(PainterComponent),
                typeof(LifeWheelComponent),
                typeof(ROIComponent),
                typeof(MoneyTimeValueComponent),
                typeof(PaperTradingComponent)
            };
        }

        public RenderFragment RenderTool(Type t) => builder =>
        {
            builder.OpenComponent(0, t);
            builder.CloseComponent();
        };

        public RenderFragment RenderTool(int tId) => RenderTool(Tools[tId]);

        public int GetToolId(Type type)
        {
            return Tools.IndexOf(type);
        }
    }
}
