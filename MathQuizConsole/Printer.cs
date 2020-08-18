using MathQuizCore;
using System;
using System.IO;

namespace MathQuizConsole
{
    public class Printer : IDisposable
    {
        private readonly string SavePath;
        private const bool AppendMode = true;
        private readonly StreamWriter Writer;

        public Printer(string filename)
        {
            string currentPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            SavePath = Path.Combine(currentPath, filename);
            Writer = new StreamWriter(SavePath, AppendMode);
        }

        public Printer() : this("results.txt") { }

        public void Print(in Game game)
        {
            DateTime datestamp = DateTime.Now;
            Writer.WriteLine("========= Game results =========");
            Writer.WriteLine($"Timestamp: {datestamp}");
            Writer.WriteLine($"Difficulty: {game.GameDifficulty}");
            Writer.WriteLine($"Accuracy: {game.CorrectAnswers}/{game.TotalAnswers}\n");

            foreach (MathQuestion question in game.QuestionList)
            {
                Writer.Write($"{question.GetQuestion()}{question.UserResponse}");
                if (!question.IsItCorrect())
                    Writer.Write($" (Mistake. Correct answer {question.Answer})");
                Writer.WriteLine("");
            }
            Writer.WriteLine("\n\n");
        }

        public void Dispose()
        {
            Writer.Close();
            Writer.Dispose();
        }
    }
}
