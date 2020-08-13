using MathQuizAsp.GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuizAsp.Models
{
    public class GameViewModel
    {
        // Game menu settings
        public string Difficulty { get; }
        public int TotalQuestions { get; }
        public int CorrectUserAnswers { get; set; }
        public int Accuracy
        {
            get => Convert.ToInt32(Math.Round((double)CorrectUserAnswers / TotalQuestions * 100.0));
        }

        public int QuestionsRemaining { get => TotalQuestions - CurrentQuestionId; }

        public List<MathQuestion> AllQuestions { get; set; }

        public int CurrentQuestionId { get; set; }
        public MathQuestion CurrentQuestion { get => AllQuestions[CurrentQuestionId]; }

        public bool IsAnsweringMode { get; set; }
        public string UserAnswer { get; set; }

        public bool IsGameFinished
        {
            get => AllQuestions.Count == CurrentQuestionId;
        }

        public GameViewModel(string difficulty, int totalQuestions)
        {
            Difficulty = difficulty;
            TotalQuestions = totalQuestions;
            CurrentQuestionId = 0;
            AllQuestions = MathQuestion.GenerateRandom(totalQuestions,
                        (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), difficulty));
            IsAnsweringMode = true;
            UserAnswer = string.Empty;
        }

        public bool CheckAnswer(int userGuess)
        {
            bool isUserCorrect = userGuess == CurrentQuestion.Answer;
            if (isUserCorrect)
            {
                CorrectUserAnswers++;
            }
            IsAnsweringMode = false;
            UserAnswer = userGuess.ToString();
            
            return isUserCorrect;
        }

        public void NextQuestion()
        {
            CurrentQuestionId++;
            IsAnsweringMode = true;
            UserAnswer = string.Empty;
        }
    }
}