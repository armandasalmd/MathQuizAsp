using MathQuizAsp.GameCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathQuizAsp.Models
{
    public class GameViewModel
    {
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

        [Required]
        [MinLength(1, ErrorMessage = "So what's your guess?")]
        public string UserAnswer { get; set; }

        public bool IsGameFinished
        {
            get => AllQuestions.Count == CurrentQuestionId;
        }

        public long FinishTillTime { get; set; }

        public GameViewModel(string difficulty, int totalQuestions)
        {
            Difficulty = difficulty;
            TotalQuestions = totalQuestions;
            CurrentQuestionId = 0;
            AllQuestions = MathQuestion.GenerateRandom(totalQuestions,
                        (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), difficulty));
            IsAnsweringMode = true;
            UserAnswer = string.Empty;
            FinishTillTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (totalQuestions * 5000 * 1); // 5s per question
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

        public void ForceFinish()
        {
            CurrentQuestionId = TotalQuestions;
            IsAnsweringMode = false;
            UserAnswer = string.Empty;
        }
    }
}