using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MathQuizAsp.Models
{
    public class UserAnswer
    {
        public string Answer { get; set; }
        public int UserAnswerInt {
            get
            {
                bool success = int.TryParse(Answer, out int parsedValue);
                return success ? parsedValue : 0;
            }
        }

    }
}