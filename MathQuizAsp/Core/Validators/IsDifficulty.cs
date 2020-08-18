using MathQuizCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MathQuizData.Core.Validators
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
