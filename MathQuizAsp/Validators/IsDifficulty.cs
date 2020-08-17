using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MathQuizAsp.GameCore;

namespace MathQuizAsp.Validators
{
    public class IsDifficulty : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            string[] difficulties = Enum.GetNames(typeof(DifficultyLevel));
            return difficulties.Contains((string)value);
        }
    }
}