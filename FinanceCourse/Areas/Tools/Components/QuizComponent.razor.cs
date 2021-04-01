using FinanceCourse.Areas.Identity.Data;
using FinanceCourse.Areas.Tools.Models;
using FinanceCourse.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCourse.Areas.Tools.Components
{
    public partial class QuizComponent : BlazorToolBase
    {
        private QuizComponentModel quizModel = new();
        private PersonalityTypeEnum? PersonalityType = null; 

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            PersonalityType = User.PersonalityType;

            if (User.ToolsStates.Where(ts => ts.ToolId == 0).Any())
                quizModel = new QuizComponentModel(User.ToolsStates.First(ts => ts.ToolId == 0));
        }

        private async void HandleValidSubmitAsync()  // TODO custom validation (so all are entered) https://blazor-university.com/forms/writing-custom-validation/
        {
            // save quizModel
            await SaveStateAsync(quizModel);

            // the calculate the personality and save it somewhere
            PersonalityType = CalculatePersonality();
            if (User == null)
                await GetCurrentUserAsync();

            User.PersonalityType = PersonalityType;

            _db.Update(User);
            await _db.SaveChangesAsync();

            // show the result
            StateHasChanged();
        }

        private PersonalityTypeEnum CalculatePersonality()
        {
            int H = 0;
            int S = 0;
            int M = 0;
            int V = 0;
            int A = 0;

            switch (quizModel.Answers[0].AnswerOption)
            {
                case AnswerOptions.a:
                    S++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    V++;
                    break;
                case AnswerOptions.d:
                    A++;
                    break;
                case AnswerOptions.e:
                    M++;
                    break;
            }
            switch (quizModel.Answers[1].AnswerOption)
            {
                case AnswerOptions.a:
                    M++;
                    break;
                case AnswerOptions.b:
                    S++;
                    break;
                case AnswerOptions.c:
                    A++;
                    break;
                case AnswerOptions.d:
                    H++;
                    break;
                case AnswerOptions.e:
                    V++;
                    break;
            }
            switch (quizModel.Answers[2].AnswerOption)
            {
                case AnswerOptions.a:
                    H++;
                    break;
                case AnswerOptions.b:
                    V++;
                    break;
                case AnswerOptions.c:
                    S++;
                    break;
                case AnswerOptions.d:
                    M++;
                    break;
                case AnswerOptions.e:
                    A++;
                    break;
            }
            switch (quizModel.Answers[3].AnswerOption)
            {
                case AnswerOptions.a:
                    A++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    M++;
                    break;
                case AnswerOptions.d:
                    S++;
                    break;
                case AnswerOptions.e:
                    V++;
                    break;
            }
            switch (quizModel.Answers[4].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    A++;
                    break;
                case AnswerOptions.c:
                    H++;
                    break;
                case AnswerOptions.d:
                    M++;
                    break;
                case AnswerOptions.e:
                    S++;
                    break;
            }
            switch (quizModel.Answers[5].AnswerOption)
            {
                case AnswerOptions.a:
                    A++;
                    break;
                case AnswerOptions.b:
                    V++;
                    break;
                case AnswerOptions.c:
                    H++;
                    break;
                case AnswerOptions.d:
                    S++;
                    break;
                case AnswerOptions.e:
                    M++;
                    break;
            }
            switch (quizModel.Answers[6].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    A++;
                    break;
                case AnswerOptions.c:
                    S++;
                    break;
                case AnswerOptions.d:
                    M++;
                    break;
                case AnswerOptions.e:
                    H++;
                    break;
            }
            switch (quizModel.Answers[7].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    A++;
                    break;
                case AnswerOptions.d:
                    S++;
                    break;
                case AnswerOptions.e:
                    M++;
                    break;
            }
            switch (quizModel.Answers[8].AnswerOption)
            {
                case AnswerOptions.a:
                    S++;
                    break;
                case AnswerOptions.b:
                    M++;
                    break;
                case AnswerOptions.c:
                    V++;
                    break;
                case AnswerOptions.d:
                    H++;
                    break;
                case AnswerOptions.e:
                    A++;
                    break;
            }
            switch (quizModel.Answers[9].AnswerOption)
            {
                case AnswerOptions.a:
                    M++;
                    break;
                case AnswerOptions.b:
                    S++;
                    break;
                case AnswerOptions.c:
                    A++;
                    break;
                case AnswerOptions.d:
                    V++;
                    break;
                case AnswerOptions.e:
                    H++;
                    break;
            }
            switch (quizModel.Answers[10].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    M++;
                    break;
                case AnswerOptions.c:
                    S++;
                    break;
                case AnswerOptions.d:
                    A++;
                    break;
                case AnswerOptions.e:
                    H++;
                    break;
            }
            switch (quizModel.Answers[11].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    M++;
                    break;
                case AnswerOptions.d:
                    S++;
                    break;
                case AnswerOptions.e:
                    A++;
                    break;
            }
            switch (quizModel.Answers[12].AnswerOption)
            {
                case AnswerOptions.a:
                    H++;
                    break;
                case AnswerOptions.b:
                    A++;
                    break;
                case AnswerOptions.c:
                    V++;
                    break;
                case AnswerOptions.d:
                    M++;
                    break;
                case AnswerOptions.e:
                    S++;
                    break;
            }
            switch (quizModel.Answers[13].AnswerOption)
            {
                case AnswerOptions.a:
                    A++;
                    break;
                case AnswerOptions.b:
                    V++;
                    break;
                case AnswerOptions.c:
                    H++;
                    break;
                case AnswerOptions.d:
                    S++;
                    break;
                case AnswerOptions.e:
                    M++;
                    break;
            }
            switch (quizModel.Answers[14].AnswerOption)
            {
                case AnswerOptions.a:
                    V++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    S++;
                    break;
                case AnswerOptions.d:
                    M++;
                    break;
                case AnswerOptions.e:
                    A++;
                    break;
            }
            switch (quizModel.Answers[15].AnswerOption)
            {
                case AnswerOptions.a:
                    H++;
                    break;
                case AnswerOptions.b:
                    S++;
                    break;
                case AnswerOptions.c:
                    M++;
                    break;
                case AnswerOptions.d:
                    V++;
                    break;
                case AnswerOptions.e:
                    A++;
                    break;
            }
            switch (quizModel.Answers[16].AnswerOption)
            {
                case AnswerOptions.a:
                    S++;
                    break;
                case AnswerOptions.b:
                    M++;
                    break;
                case AnswerOptions.c:
                    A++;
                    break;
                case AnswerOptions.d:
                    H++;
                    break;
                case AnswerOptions.e:
                    V++;
                    break;
            }
            switch (quizModel.Answers[17].AnswerOption)
            {
                case AnswerOptions.a:
                    M++;
                    break;
                case AnswerOptions.b:
                    A++;
                    break;
                case AnswerOptions.c:
                    H++;
                    break;
                case AnswerOptions.d:
                    V++;
                    break;
                case AnswerOptions.e:
                    S++;
                    break;
            }
            switch (quizModel.Answers[18].AnswerOption)
            {
                case AnswerOptions.a:
                    S++;
                    break;
                case AnswerOptions.b:
                    V++;
                    break;
                case AnswerOptions.c:
                    A++;
                    break;
                case AnswerOptions.d:
                    H++;
                    break;
                case AnswerOptions.e:
                    M++;
                    break;
            }
            switch (quizModel.Answers[19].AnswerOption)
            {
                case AnswerOptions.a:
                    M++;
                    break;
                case AnswerOptions.b:
                    H++;
                    break;
                case AnswerOptions.c:
                    V++;
                    break;
                case AnswerOptions.d:
                    A++;
                    break;
                case AnswerOptions.e:
                    S++;
                    break;
            }


            List<int> res = new List<int> { H, S, M, V, A };

            switch (res.Select((value, index) => new { Value = value, Index = index }).Aggregate((a, b) => (a.Value > b.Value) ? a : b).Index)
            {
                case 0:
                    return PersonalityTypeEnum.Hoarder;
                case 1:
                    return PersonalityTypeEnum.Spender;
                case 2:
                    return PersonalityTypeEnum.Money_Monk;
                case 3:
                    return PersonalityTypeEnum.Avoider;
                case 4:
                    return PersonalityTypeEnum.Amasser;
            }

            throw new ArithmeticException();
        }
    }
}
