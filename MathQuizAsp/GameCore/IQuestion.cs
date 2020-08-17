using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuizAsp.GameCore
{
    public interface IQuestion<TAnswer>
    {
        string GetQuestion();
        string GetQuestion(int questionNumber);
        TAnswer Answer { get; }
        TAnswer UserResponse { get; set; }
        bool IsItCorrect();
        bool IsItCorrect(TAnswer userGuess);
    }
}