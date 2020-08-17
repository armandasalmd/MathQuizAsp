using System;
using System.Collections.Generic;
using MathQuizCore.Enums;

namespace MathQuizCore
{
    public class MathInterpreter : List<MathObject>
    {
        #region Constructors
        public MathInterpreter(List<MathObject> mathObjects)
        {
            SetMathObjects(mathObjects);
        }
        #endregion

        #region Core
        public void SetMathObjects(List<MathObject> mathObjects)
        {
            Clear();
            AddRange(mathObjects);
        }

        private int FindMostSignificantOperatorIndex(List<MathObject> objs)
        {
            // Quick recap - math operators order:
            // 1. Parentheses (we dont use them)
            // 2. Powers/Roots
            // 3. Multiply/Divide
            // 4. Add/Subtract
            int operatorIndex = -1;
            int operatorType = -1;

            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].Type == MathObjectType.Operator)
                {
                    int operatorAsInt = (int)objs[i].Item;
                    // If new found operator is higher priority
                    if (operatorAsInt > operatorType)
                    {
                        operatorType = operatorAsInt;
                        operatorIndex = i;
                    }
                }
            }
            return operatorIndex;
        }

        public int Calculate()
        {
            // Flow:
            // 1. Find most significant operator
            // 2. Perform an action for numbers around it (except sq root)
            // 3. Replace 3 MathObjects out of the list and replace with a operation result
            const int attemptLimit = 100;
            int attemptIndex = 0;
            List<MathObject> objs = CloneMathObjects(this);

            while (objs.Count != 1 && attemptIndex <= attemptLimit)
            {
                int mostCrucialOperatorId = FindMostSignificantOperatorIndex(objs);

                int numberBeforeOperand = 0, numberAfterOperand;
                bool isSqRootOperation = (Operator)objs[mostCrucialOperatorId].Item == Operator.SqRoot;
                if (isSqRootOperation)
                {
                    numberAfterOperand = (int)objs[mostCrucialOperatorId + 1].Item;
                }
                else
                {
                    numberBeforeOperand = (int)objs[mostCrucialOperatorId - 1].Item;
                    numberAfterOperand = (int)objs[mostCrucialOperatorId + 1].Item;
                }
                int operationResult = MakeSimpleCalculation(
                    numberBeforeOperand,
                    numberAfterOperand,
                    (Operator)objs[mostCrucialOperatorId].Item
                );
                if (isSqRootOperation)
                {
                    objs.RemoveRange(mostCrucialOperatorId, 2);
                    objs.Insert(mostCrucialOperatorId, new MathObject(operationResult));
                }
                else
                {
                    objs.RemoveRange(mostCrucialOperatorId - 1, 3);
                    objs.Insert(mostCrucialOperatorId - 1, new MathObject(operationResult));
                }
                attemptIndex++;
            }
            if (attemptIndex == attemptLimit)
            {
                throw new Exception("Calculation exceeded attempts.");
            }
            else
            {
                return (int)objs[0].Item;
            }
        }
        #endregion

        public override string ToString()
        {
            string answer = string.Empty;
            foreach (MathObject item in this)
            {
                answer += item.ToString() + " ";
            }
            return answer + "= ";
        }

        #region Static methods
        public static int MakeSimpleCalculation(int num1, int num2, Operator _op)
        {
            switch (_op)
            {
                case Operator.Add: return num1 + num2;
                case Operator.Subtract: return num1 - num2;
                case Operator.Multiply: return num1 * num2;
                case Operator.Divide:
                    return num2 == 0 ? 0 : num1 / num2;
                case Operator.SqRoot: return (int)System.Math.Sqrt(num2);
                case Operator.Power: return (int)System.Math.Pow(num1, num2);
                default:
                    throw new Exception("Unsupported operator was used to create an answer");
            }
        }

        public static List<MathObject> CloneMathObjects(MathInterpreter interpreter)
        {
            List<MathObject> list = new List<MathObject>();
            foreach (MathObject item in interpreter)
            {
                list.Add(item);
            }
            return list;
        }
        #endregion
    }
}
