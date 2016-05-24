using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouSurvey.Models
{
    public class Question
    {
        public string QuestionId { get; set; }

        public string SurveyId { get; set; }

        public string Description { get; set; }

        public List<Choice> Choices { get; set; } = new List<Choice>();

        public bool AllowMultipleAnswers { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
