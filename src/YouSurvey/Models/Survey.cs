using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouSurvey.Models
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
