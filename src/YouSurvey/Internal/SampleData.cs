using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouSurvey.Data;
using YouSurvey.Models;

namespace YouSurvey.Internal
{
    public static class SampleData
    {
        const string defaultAdminUserName = "DefaultAdminUserName";
        const string defaultAdminPassword = "DefaultAdminPassword";

        public static async Task InitializeYouSurveyDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = true)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await InsertTestData(serviceProvider);

                    if (createUsers)
                    {
                        await CreateUsers(serviceProvider);
                    }
                }
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            var surveys = GetSurveys();

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                foreach (var survey in surveys)
                {
                    db.Surveys.Add(survey);
                }

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Create an administrator and some users
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateUsers(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // create Admin user
            await CreateUser(
                userManager,
                configuration[defaultAdminUserName],
                configuration[defaultAdminPassword],
                new[]
                {
                    new Claim("IsAdmin", "Yes"),
                    new Claim("ManageAllSurveys", "Allowed")
                });

            // create regular users
            await CreateUser(
                userManager,
                "james@james.com",
                "James1!",
                new Claim[] { });
            await CreateUser(
                userManager,
                "john@john.com",
                "John1!",
                new Claim[] { });
            await CreateUser(
                userManager,
                "mike@mike.com",
                "Mike1!",
                new Claim[] { });
        }

        private static async Task CreateUser(
            UserManager<ApplicationUser> userManager,
            string userName,
            string password,
            Claim[] claims)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = userName };
                await userManager.CreateAsync(user, password);
                await userManager.AddClaimsAsync(user, claims);
            }
        }

        private static List<Survey> GetSurveys()
        {
            var surveys = new List<Survey>();

            var survey1 = new Survey();
            survey1.Name = "ASP.NET 5";
            survey1.Description = "A demo on ASP.NET 5";
            survey1.CreatedBy = "james@james.com";
            survey1.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey1.LastUpdatedBy = survey1.CreatedBy;
            survey1.LastUpdatedDate = survey1.CreatedDate;

            var survey1Question1 = new Question();
            survey1Question1.Description = "How well did the instructor engage you during the demo?";
            survey1Question1.AllowMultipleAnswers = false;
            survey1Question1.CreatedBy = "james@james.com";
            survey1Question1.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey1Question1.LastUpdatedBy = survey1Question1.CreatedBy;
            survey1Question1.LastUpdatedDate = survey1Question1.CreatedDate;
            survey1Question1.Choices.Add(new Choice() { Description = "Very good" });
            survey1Question1.Choices.Add(new Choice() { Description = "Good" });
            survey1Question1.Choices.Add(new Choice() { Description = "Could be better" });
            survey1Question1.Choices.Add(new Choice() { Description = "Bad" });

            var survey1Question2 = new Question();
            survey1Question2.Description = "Overall do you feel this course helped you learn something new?";
            survey1Question2.CreatedBy = "james@james.com";
            survey1Question2.AllowMultipleAnswers = false;
            survey1Question2.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey1Question2.LastUpdatedBy = survey1Question2.CreatedBy;
            survey1Question2.LastUpdatedDate = survey1Question2.CreatedDate;
            survey1Question2.Choices.Add(new Choice() { Description = "Yes" });
            survey1Question2.Choices.Add(new Choice() { Description = "No" });

            var survey1Question3 = new Question();
            survey1Question3.Description = "Would you recommend this course to your colleague?";
            survey1Question3.CreatedBy = "james@james.com";
            survey1Question3.AllowMultipleAnswers = false;
            survey1Question3.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey1Question3.LastUpdatedBy = survey1Question3.CreatedBy;
            survey1Question3.LastUpdatedDate = survey1Question3.CreatedDate;
            survey1Question3.Choices.Add(new Choice() { Description = "Yes" });
            survey1Question3.Choices.Add(new Choice() { Description = "No" });

            var survey1Question4 = new Question();
            survey1Question4.Description = "Which of the following topics that you really liked?";
            survey1Question4.CreatedBy = "james@james.com";
            survey1Question4.AllowMultipleAnswers = true;
            survey1Question4.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey1Question4.LastUpdatedBy = survey1Question4.CreatedBy;
            survey1Question4.LastUpdatedDate = survey1Question4.CreatedDate;
            survey1Question4.Choices.Add(new Choice() { Description = "Logging" });
            survey1Question4.Choices.Add(new Choice() { Description = "Dependency Injection" });
            survey1Question4.Choices.Add(new Choice() { Description = "Mvc" });
            survey1Question4.Choices.Add(new Choice() { Description = "Debugging into sources" });

            survey1.Questions.Add(survey1Question1);
            survey1.Questions.Add(survey1Question2);
            survey1.Questions.Add(survey1Question3);
            survey1.Questions.Add(survey1Question4);

            var survey2 = new Survey();
            survey2.Name = "TMobile Center Visit";
            survey2.Description = "Your recent visit to the TMobile center";
            survey2.CreatedBy = "john@john.com";
            survey2.CreatedDate = DateTime.UtcNow.AddDays(-8);
            survey2.LastUpdatedBy = survey2.CreatedBy;
            survey2.LastUpdatedDate = survey2.CreatedDate;

            var survey2Question1 = new Question();
            survey2Question1.Description = "How well did the representative engage you during the demo?";
            survey2Question1.AllowMultipleAnswers = false;
            survey2Question1.CreatedBy = "john@john.com";
            survey2Question1.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey2Question1.LastUpdatedBy = survey2Question1.CreatedBy;
            survey2Question1.LastUpdatedDate = survey2Question1.CreatedDate;
            survey2Question1.Choices.Add(new Choice() { Description = "Very good" });
            survey2Question1.Choices.Add(new Choice() { Description = "Good" });
            survey2Question1.Choices.Add(new Choice() { Description = "Could be better" });
            survey2Question1.Choices.Add(new Choice() { Description = "Bad" });

            var survey2Question2 = new Question();
            survey2Question2.Description = "Did you have to wait for a long time before someone could help you?";
            survey2Question2.CreatedBy = "john@john.com";
            survey2Question2.AllowMultipleAnswers = false;
            survey2Question2.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey2Question2.LastUpdatedBy = survey2Question2.CreatedBy;
            survey2Question2.LastUpdatedDate = survey2Question2.CreatedDate;
            survey2Question2.Choices.Add(new Choice() { Description = "Yes" });
            survey2Question2.Choices.Add(new Choice() { Description = "No" });

            var survey2Question3 = new Question();
            survey2Question3.Description = "Would you recommend TMobile to your friends?";
            survey2Question3.CreatedBy = "john@john.com";
            survey2Question3.AllowMultipleAnswers = false;
            survey2Question3.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey2Question3.LastUpdatedBy = survey2Question3.CreatedBy;
            survey2Question3.LastUpdatedDate = survey2Question3.CreatedDate;
            survey2Question3.Choices.Add(new Choice() { Description = "Yes" });
            survey2Question3.Choices.Add(new Choice() { Description = "No" });

            survey2.Questions.Add(survey2Question1);
            survey2.Questions.Add(survey2Question2);
            survey2.Questions.Add(survey2Question3);

            var survey3 = new Survey();
            survey3.Name = "Smart Thermostat";
            survey3.Description = "Your experience with our Smart Thermostat";
            survey3.CreatedBy = "mike@mike.com";
            survey3.CreatedDate = DateTime.UtcNow.AddDays(-8);
            survey3.LastUpdatedBy = survey3.CreatedBy;
            survey3.LastUpdatedDate = survey3.CreatedDate;

            var survey3Question1 = new Question();
            survey3Question1.Description = "How easy was it to assembly and install the thermostat?";
            survey3Question1.AllowMultipleAnswers = false;
            survey3Question1.CreatedBy = "mike@mike.com";
            survey3Question1.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey3Question1.LastUpdatedBy = survey3Question1.CreatedBy;
            survey3Question1.LastUpdatedDate = survey3Question1.CreatedDate;
            survey3Question1.Choices.Add(new Choice() { Description = "Very easy" });
            survey3Question1.Choices.Add(new Choice() { Description = "Easy" });
            survey3Question1.Choices.Add(new Choice() { Description = "Could be better" });
            survey3Question1.Choices.Add(new Choice() { Description = "Bad" });

            var survey3Question2 = new Question();
            survey3Question2.Description = "Was the information on the display of the thermsotat helpful for you?";
            survey3Question2.CreatedBy = "mike@mike.com";
            survey3Question2.AllowMultipleAnswers = false;
            survey3Question2.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey3Question2.LastUpdatedBy = survey3Question2.CreatedBy;
            survey3Question2.LastUpdatedDate = survey3Question2.CreatedDate;
            survey3Question2.Choices.Add(new Choice() { Description = "Yes" });
            survey3Question2.Choices.Add(new Choice() { Description = "No" });

            var survey3Question3 = new Question();
            survey3Question3.Description = "Would you recommend this thermostat to your friends?";
            survey3Question3.CreatedBy = "mike@mike.com";
            survey3Question3.AllowMultipleAnswers = false;
            survey3Question3.CreatedDate = DateTime.UtcNow.AddDays(-10);
            survey3Question3.LastUpdatedBy = survey3Question3.CreatedBy;
            survey3Question3.LastUpdatedDate = survey3Question3.CreatedDate;
            survey3Question3.Choices.Add(new Choice() { Description = "Yes" });
            survey3Question3.Choices.Add(new Choice() { Description = "No" });

            survey3.Questions.Add(survey3Question1);
            survey3.Questions.Add(survey3Question2);
            survey3.Questions.Add(survey3Question3);

            surveys.Add(survey1);
            surveys.Add(survey2);
            surveys.Add(survey3);

            return surveys;
        }
    }
}
