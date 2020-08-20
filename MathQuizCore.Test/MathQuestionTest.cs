using MathQuizCore;
using MathQuizCore.Test.Factories;
using MathQuizCore.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MathQuizCore.Test
{
    [TestClass]
    public class MathQuestionTest
    {
        #region Variables
        private readonly MathInterpreterMock interpreterMock; // real/actual/expected answer
        private MathQuestion targetObj;                       // the one being tested
        #endregion

        #region Constructor, BeforeEach
        public MathQuestionTest()
        {
            interpreterMock = MathInterpreterFactory.GetMathInterpreter1();
        }

        [TestInitialize]
        public void BeforeEach()
        {
            targetObj = new MathQuestion(interpreterMock.Target);
        }
        #endregion

        [TestMethod]
        public void MathQuestion_GenerateRandom_ShouldCreate5Questions()
        {
            // Arrange
            List<MathQuestion> questionList;
            // Act
            questionList = MathQuestion.GenerateRandom(5, Enums.DifficultyLevel.Medium);
            // Assert
            Assert.AreEqual(5, questionList.Count);
        }

        [TestMethod]
        public void MathQuestion_GenerateRandom_ShouldCreate3Questions()
        {
            // Arrange
            List<MathQuestion> questionList;
            // Act
            questionList = MathQuestion.GenerateRandom(3, Enums.DifficultyLevel.Hard);
            // Assert
            Assert.AreEqual(3, questionList.Count);
        }

        [TestMethod]
        public void MathQuestion_GetQuestion_ShouldGiveCorrectOutput()
        {
            // Arrange
            string expected = interpreterMock.QuestionAsString;
            // Act
            string result = targetObj.GetQuestion();
            // Assert
            Assert.IsTrue(result.StartsWith(expected));
        }

        [TestMethod]
        public void MathQuestion_GetQuestion_ShouldGiveCorrectOutputWithPrefix()
        {
            // Arrange
            int questionNumber = 13;
            // Act
            string result = targetObj.GetQuestion(questionNumber);
            // Assert
            Assert.IsTrue(result.StartsWith($"Question {questionNumber}: {interpreterMock.QuestionAsString}"));
        }

        [TestMethod()]
        public void MathQuestion_IsItCorrect_ShouldBeTrue()
        {
            // Act
            targetObj.UserResponse = interpreterMock.NumberAnswer; // user is always correct lets say
            bool answerIsCorrect = targetObj.IsItCorrect();
            // Assert
            Assert.IsTrue(answerIsCorrect);            
        }

        [TestMethod()]
        public void MathQuestion_IsItCorrect_ShouldBeTrue2()
        {
            // Act
            bool answerIsCorrect = targetObj.IsItCorrect(interpreterMock.NumberAnswer);
            // Assert
            Assert.IsTrue(answerIsCorrect);
        }
    }
}
