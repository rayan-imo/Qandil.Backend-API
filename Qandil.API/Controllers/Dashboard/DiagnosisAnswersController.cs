using Microsoft.AspNetCore.Mvc;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisAnswersController(IAnswerService _answerService) : ControllerBase
    {

    }
}
