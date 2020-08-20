using MathQuizCore.Test.Mocks;
using System.Collections.Generic;

namespace MathQuizCore.Test.Factories
{
    public class MathInterpreterFactory
    {

        public static MathInterpreterMock GetMathInterpreter1()
        {
            List<MathObject> objs = new List<MathObject>()
            {
                new MathObject(6),
                new MathObject(Enums.Operator.Add),
                new MathObject(8),
                new MathObject(Enums.Operator.Multiply),
                new MathObject(2),
            };
            return new MathInterpreterMock()
            {
                Target = new MathInterpreter(objs),
                QuestionAsString = "6 + 8 * 2",
                NumberAnswer = 22
            };
        }

    }
}
