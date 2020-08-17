using System;
using System.Collections.Generic;
using System.Text;

namespace MathQuizCore.Interfaces
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
