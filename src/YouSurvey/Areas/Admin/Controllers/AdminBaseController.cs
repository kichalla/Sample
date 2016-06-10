using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouSurvey.Areas.Admin.Controllers
{
    [Authorize("IsAdmin")]
    public class AdminBaseController : Controller
    {
    }
}
