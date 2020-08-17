using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuizAsp.Models
{
    public class UserAnswer
    {
        public string Answer { get; set; }
        public int UserAnswerInt
        {
            get
            {
                bool success = int.TryParse(Answer, out int parsedValue);
                return success ? parsedValue : 0;
            }
        }

    }
}
