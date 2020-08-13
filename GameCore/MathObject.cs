using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuizAsp.GameCore
{
        public struct MathObject
    {
        public MathObjectType Type;
        public object Item;

        public MathObject(int number)
        {
            Type = MathObjectType.Integer;
            Item = number;
        }

        public MathObject(Operator op)
        {
            Type = MathObjectType.Operator;
            Item = op;
        }

        public override string ToString()
        {
            if (Type == MathObjectType.Integer)
            {
                return ((int)Item).ToString();
            }
            else // (Type == MathObjectType.Operator)
            {
                char[] symbols = { '+', '-', '*', '/', '√', '^' };
                return symbols[(int)Item].ToString();
            }
        }
    }
}