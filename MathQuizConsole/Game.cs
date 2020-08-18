using MathQuizCore;
using MathQuizCore.Enums;
using MathQuizCore.Interfaces;
using System;
using System.Collections.Generic;

namespace MathQuizConsole
{
    public class Game
    {
        public List<IQuestion<int>> QuestionList { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswers { get; set; }
        public bool AskQuestionCountFromUser { get; set; }
        public DifficultyLevel GameDifficulty { get; set; }

        public Game(List<IQuestion<int>> questionList)
        {
            AskQuestionCountFromUser = false;
            QuestionList = questionList;
        }

        public Game(int questionCount)
        {
            AskQuestionCountFromUser = false;
            QuestionList = new List<IQuestion<int>>();
            QuestionList.AddRange(MathQuestion.GenerateRandom(questionCount, GameDifficulty));
        }

        public Game()
        {
            AskQuestionCountFromUser = true;
        }

        ~Game()
        {
            Console.WriteLine($"You scored {CorrectAnswers}/{TotalAnswers} answers correct");
            Console.WriteLine("Game has finished. Thank you for playing!\n\n");
        }

        private void AskQuestionCountAndGenerateIt()
        {
            Console.WriteLine("How many questions do you want?");
            int.TryParse(Console.ReadLine(), out int questionCountInt);
            Console.Clear();

            if (questionCountInt <= 0)
            {
                throw new Exception("Question count must be more than 0");
            }

            QuestionList = new List<IQuestion<int>>();
            QuestionList.AddRange(MathQuestion.GenerateRandom(questionCountInt, GameDifficulty));
        }

        private void AskForDifficultyLevel()
        {
            Console.WriteLine("Choose difficulty level:");
            int n = Enum.GetNames(typeof(DifficultyLevel)).Length; // enum size

            // Print a list of difficulties
            for (int i = 1; i < n + 1; i++)
            {
                Console.WriteLine($"{i}. {(DifficultyLevel)i}");
            }

            int.TryParse(Console.ReadLine(), out int userInputInt);
            if (userInputInt <= n && userInputInt > 0)
            {
                GameDifficulty = (DifficultyLevel)userInputInt;
            }
            else
            {
                throw new Exception("Selected difficulty does not exist.");
            }
        }

        public void OnStart()
        {
            AskForDifficultyLevel();
            if (AskQuestionCountFromUser)
                AskQuestionCountAndGenerateIt();

            int i = 0;
            foreach (IQuestion<int> question in QuestionList)
            {
                Console.Write(question.GetQuestion(i + 1));
                string userInputStr = Console.ReadLine();
                int.TryParse(userInputStr, out int userInputInt);
                question.UserResponse = userInputInt;
                if (question.IsItCorrect())
                {
                    CorrectAnswers++;
                    Console.WriteLine("That's right!\n");
                }
                else
                {
                    Console.WriteLine($"Nope, it's {question.Answer}\n");
                }
                TotalAnswers++;
                i++;
            }
            OnFinish();
        }

        public void OnFinish()
        {
            Console.WriteLine("Would you like to save your results? (Y/N)");
            char userChoice = Console.ReadLine().ToUpper()[0];
            if (userChoice == 'Y')
            {
                // Separation of concern. Game class should not handle printing itself:)
                using (Printer printer = new Printer("scores.txt"))
                {
                    printer.Print(this);
                }
            }
        }

    }
}
