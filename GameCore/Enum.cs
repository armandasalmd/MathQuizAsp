using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuizAsp.GameCore
{
    public enum MathObjectType
    {
        Integer, Operator
    };

    public enum Operator
    {
        Add, Subtract, Multiply, Divide, SqRoot, Power
    };

    public enum DifficultyLevel
    {
        Easy = 1, Medium, Hard
    }

}