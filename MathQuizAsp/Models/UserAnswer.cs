using System.ComponentModel.DataAnnotations;

namespace MathQuizAsp.ViewModels
{
    public class UserAnswer
    {
        [Required]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "This is not an integer number!")]
        public string Answer { get; set; }
    }
}
