using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YouSurvey.Models;

namespace YouSurvey.Controllers
{
    public class SurveyCreatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets list of all surveys created by the current user
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            var surveys = new List<Survey>();
            var survey1 = new Survey();
            survey1.Id = "s1";
            survey1.Name = "Survey 1";

            var q1 = new Question();
            q1.Id = "q1";
            q1.Description = "How was the overall experience with the demo?";
            q1.Choices.Add(new Choice()
            {
                Id = "c1",
                Description = "Satisfactory"
            });
            q1.Choices.Add(new Choice()
            {
                Id = "c2",
                Description = "Could have been better"
            });
            q1.Choices.Add(new Choice()
            {
                Id = "c3",
                Description = "Didn't get chance to observe"
            });
            q1.Choices.Add(new Choice()
            {
                Id = "c4",
                Description = "Very good"
            });
            q1.Choices.Add(new Choice()
            {
                Id = "c5",
                Description = "Good"
            });
            survey1.Questions.Add(q1);

            var q2 = new Question();
            q2.Id = "q2";
            q2.Description = "How was the instructor's engagment?";
            q2.Choices.Add(new Choice()
            {
                Id = "c1",
                Description = "Satisfactory"
            });
            q2.Choices.Add(new Choice()
            {
                Id = "c2",
                Description = "Could have been better"
            });
            q2.Choices.Add(new Choice()
            {
                Id = "c3",
                Description = "Didn't get chance to observe"
            });
            q2.Choices.Add(new Choice()
            {
                Id = "c4",
                Description = "Very good"
            });
            q2.Choices.Add(new Choice()
            {
                Id = "c5",
                Description = "Good"
            });
            survey1.Questions.Add(q2);

            surveys.Add(survey1);
            return Ok(surveys);
        }

        /// <summary>
        /// Gets the view to enable creating a new survey
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new survey
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // add survey to store

            return RedirectToAction("Details");
        }

        /// <summary>
        /// Gets the view showing details of a survey and its questions.
        /// Provides links to create choices for the survey
        /// </summary>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Gets the view to enable creating a new question and choices
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateQuestion()
        {
            return View();
        }

        /// <summary>
        /// Creates a question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public IActionResult CreateQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // add question to a survey
            // update survey to store

            return RedirectToAction("Details");
        }

        /// <summary>
        /// Shows the details of a question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult QuestionDetails(int id)
        {
            return View();
        }

        /// <summary>
        /// Indicates completion of survey creation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Completed(int id)
        {
            // save the surveys to the completed list of surveys

            return RedirectToAction("Index");
        }
    }
}
