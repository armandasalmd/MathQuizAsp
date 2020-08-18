using MathQuizCore.Enums;
using MathQuizData.Core.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathQuizAsp.Models
{
    public class GameSettings
    {
        public readonly string DefaultDifficulty = DifficultyLevel.Medium.ToString();
        public readonly IEnumerable<string> DifficultyOptions = Enum.GetNames(typeof(DifficultyLevel));

        [Display(Name = "Select difficulty:")]
        [IsDifficulty(ErrorMessage = "Incorrect difficulty")]
        [Required(ErrorMessage = "Select difficulty level")]
        public string Difficulty { get; set; }

        [Display(Name = "Select question count:")]
        [Required(ErrorMessage = "Select question count")]
        public string QuestionCount { get; set; }

    }
}
