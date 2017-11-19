using Microsoft.AspNetCore.Mvc.RazorPages;
using TwoByFour.MultiplicationTable;

namespace TwoByFour.Pages
{
    public class NeedsMoreLearningModel : PageModel
    {
        private readonly ITrainingCourse _course;

        public NeedsMoreLearningModel(ITrainingCourse course)
        {
            _course = course;
        }

        public Multiplication[] Items => _course.NeedMoreLearningChallenges;
    }
}