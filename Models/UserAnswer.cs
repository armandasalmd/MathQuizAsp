using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathQuizAsp.Models
{
    public class UserAnswerDTO
    {
        public string UserAnswer { get; set; }
        public int UserAnswerInt {
            get
            {
                bool success = int.TryParse(UserAnswer, out int parsedValue);
                return success ? parsedValue : 0;
            }
        }
    }
}