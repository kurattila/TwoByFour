using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TwoByFour.MultiplicationTable;

namespace TwoByFour.Pages
{
    public class ResultsModel : PageModel
    {
        public class SingleResultEvaluation
        {
            public string Result { get; set; }
            public bool IsCorrect { get; set; }
        }

        ITrainingCourse _course;

        public ResultsModel(ITrainingCourse course)
        {
            _course = course;
        }

        [BindProperty]
        public List<SingleResultEvaluation> ResultEvaluations { get; set; }

        public void OnGet()
        {
            ResultEvaluations = new List<SingleResultEvaluation>();
            foreach (var result in _course.AlreadySeenChallenges)
            {
                var singleResultEval = new SingleResultEvaluation
                {
                    Result = result.TextualResult,
                    IsCorrect = true
                };
                ResultEvaluations.Add(singleResultEval);
            }
        }

        public IActionResult OnPost()
        {
            for (var evalIndex = 0; evalIndex < ResultEvaluations.Count; evalIndex++)
            {
                if (!ResultEvaluations[evalIndex].IsCorrect)
                    _course.SetNeedsMoreLearning(_course.AlreadySeenChallenges[evalIndex]);
            }

            _course.StartNewSession();
            return RedirectToPage("/Results");
        }
    }
}
