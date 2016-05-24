using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouSurvey.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }

        public int QuestionId { get; set; }

        public string Description { get; set; }
    }
}
