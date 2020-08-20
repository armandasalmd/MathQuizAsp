using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MathQuizCore.Test
{
    [TestClass]
    public class MathInterpreterTest
    {

        private MathInterpreter targetObj;


        [TestInitialize]
        public void BeforeEach()
        {
            List<MathObject> mathObjects = new List<MathObject>()
            {
                new MathObject()
            };

            //objToTest = new MathInterpreter(mathObjects);
        }

        [TestMethod]
        public void MathInterpreter_SetMathObjects()
        {

        }
    }
}
