using MathQuizCore;
using MathQuizCore.Enums;
using MathQuizCore.Interfaces;
using System;
using System.Collections.Generic;

namespace MathQuizXamarin.Services
{
    public class MathQuestion : IQuestion<int>
    {
        public MathInterpreter Interpreter;
        private static Random Rand { get; set; } // Random number generator

        public int Answer
        {
            get
            {
                return Interpreter.Calculate();
            }
        }

        public int UserResponse { get; set; }

        public MathQuestion() : this(DifficultyLevel.Easy) { }

        /// <summary>
        /// Randomly generates a question
        /// </summary>
        public MathQuestion(DifficultyLevel level)
        {
            if (Rand == null)
                Rand = new Random(Environment.TickCount);
            Interpreter = MakeRandomInterpreter(level);
        }

        /// <summary>
        /// Manually create math question
        /// </summary>
        /// <param name="op">Operator to use</param>
        public MathQuestion(MathInterpreter interpreter)
        {
            if (Rand == null)
                Rand = new Random(Environment.TickCount);
            Interpreter = interpreter;
        }

        private MathInterpreter MakeRandomInterpreter(DifficultyLevel level)
        {
            int levelInt = (int)level;
            List<MathObject> mathObjects = new List<MathObject>();

            Operator op1 = (Operator)(Rand.Next(0, 6));

            /// <quote src="./Requirements/Requirements1.txt">
            /// Ensure division questions result in an integer answer
            /// </quote>
            if (op1 == Operator.Divide)
            {
                int num1 = Rand.Next(levelInt, levelInt * 15);
                int num2 = Rand.Next(levelInt, levelInt * 15);
                mathObjects.Add(new MathObject(num1 * num2));
                mathObjects.Add(new MathObject(op1));
                mathObjects.Add(new MathObject(num2));
            }
            else if (op1 == Operator.SqRoot)
            {
                int answerNumber = Rand.Next(levelInt, levelInt * 3);
                mathObjects.Add(new MathObject(op1)); // sq root operator
                mathObjects.Add(new MathObject(answerNumber * answerNumber));
            }
            else if (op1 == Operator.Power)
            {
                mathObjects.Add(new MathObject(Rand.Next(levelInt, levelInt * 4))); // base
                mathObjects.Add(new MathObject(op1)); // power operator
                mathObjects.Add(new MathObject(Rand.Next(levelInt, levelInt + 1))); // power
            }
            else // default operations + - *
            {
                mathObjects.Add(new MathObject(Rand.Next(levelInt, levelInt * 15)));
                mathObjects.Add(new MathObject(op1));
                mathObjects.Add(new MathObject(Rand.Next(levelInt, levelInt * 15)));
            }

            // GENERATING second operation
            // (30% * level) questions get 2nd operation
            if (Rand.Next(1, 100) <= 30 * levelInt)
            {
                Operator op2 = (Operator)(Rand.Next(0, 4)); // + - * / only
                mathObjects.Add(new MathObject(op2));
                mathObjects.Add(new MathObject(Rand.Next(levelInt, levelInt * 10)));
            }
            return new MathInterpreter(mathObjects);
        }

        public static List<MathQuestion> GenerateRandom(int count, DifficultyLevel difficultyLevel)
        {
            List<MathQuestion> questions = new List<MathQuestion>();
            for (int i = 0; i < count; i++)
                questions.Add(new MathQuestion(difficultyLevel));
            return questions;
        }

        #region Interface<IQuestion<int>> implementations

        public string GetQuestion(int questionNumber) => $"Question {questionNumber}: {GetQuestion()}";

        public string GetQuestion() => Interpreter.ToString();

        public bool IsItCorrect() => Answer == UserResponse;

        public bool IsItCorrect(int userGuess) => Answer == userGuess;
        #endregion
    }
}
