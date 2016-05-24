using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouSurvey.Models
{
    public class Choice
    {
        public string ChoiceId { get; set; }

        public string QuestionId { get; set; }

        public string Description { get; set; }
    }
}
