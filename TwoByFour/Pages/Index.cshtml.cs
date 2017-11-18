using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoByFour.MultiplicationTable;

namespace TwoByFour.Pages
{
    public class IndexModel : PageModel
    {
        ITrainingCourse _course;
        IMultiplicationSmartFactory _multiplicationSmartFactory;

        public IndexModel(ITrainingCourse course, IMultiplicationSmartFactory multiplicationSmartFactory)
        {
            _course = course;
            _multiplicationSmartFactory = multiplicationSmartFactory;
        }

        public string ChallengeText { get; set; }

        private void NextChallenge()
        {
            var challenge = _multiplicationSmartFactory.CreateMultiplication();
            ChallengeText = challenge.TextualChallenge;
            _course.SetAlreadySeen(challenge);
        }

        public void OnGet()
        {
            NextChallenge();
        }

        public void OnPost()
        {
            NextChallenge();
        }
    }
}
