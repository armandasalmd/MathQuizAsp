using System;

namespace MathQuizXamarin.Models
{
    public class GameStatistics
    {
        public int TotalAnswers = 0;
        public int CorrectAnswers = 0;

        public double CorrectPercent {
            get
            {
                return Math.Round(CorrectAnswers / (double)TotalAnswers * 100);
            }
        }
    }
}
