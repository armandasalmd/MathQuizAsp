using MathQuizAsp.Validators;
using System.ComponentModel.DataAnnotations;

namespace MathQuizAsp.Models
{
    public class GameSettings
    {
        [Display(Name = "Select difficulty:")]
        [IsDifficulty(ErrorMessage = "Incorrect difficulty")]
        [Required(ErrorMessage = "Select difficulty level")]
        public string Difficulty { get; set; }

        [Display(Name = "Select question count:")]
        [Required(ErrorMessage = "Select question count")]
        public string QuestionCount { get; set; }

    }
}