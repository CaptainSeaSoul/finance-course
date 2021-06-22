using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Services
{
    public class SavePaintingInvokeHelper
    {
        private Func<string, Task> func;

        public SavePaintingInvokeHelper(Func<string, Task> func)
        {
            this.func = func;
        }

        [JSInvokable("FinanceCourse")]
        public async Task SavePaintingInvokeCallerAsync(string jsonPainting)
        {
            await func(jsonPainting);
        }
    }
}
