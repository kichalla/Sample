using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouSurvey.Data;
using YouSurvey.Models;

namespace YouSurvey.Controllers
{
    [Authorize]
    public class SurveyCreatorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SurveyCreatorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
            var currentUser = HttpContext.User;
            return Ok(_dbContext.Surveys.Where(survey => survey.CreatedBy == currentUser.Identity.Name));
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
