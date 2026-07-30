using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.DiagnosisQuestions;
using Qandil.API.Dtos.Responses.DiagnosisQuestions;
using Qandil.Core.Common;
using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IDiagnosisQuestionService _questionService) : ControllerBase
    {
        [HttpGet("Card")]
        public async Task<ActionResult<ApiResponse<List<CardQuestionResponse>>>> GetQustionByCardName(string cardName)
        {
            var result = await _questionService.GetQuestionsByCardName(cardName);


            if (!result.IsSuccess)

                return BadRequest(new ApiResponse<string>

                {
                    Success = false,
                    MessageAr = "لا يوجد اسئلة لهذه البطاقة",
                    MessageEn = "there are no question for this card",
                });

            var questionResponses = result.Value.Select(CardQuestionResponse.Transform).ToList();

            return Ok(new ApiResponse<List<CardQuestionResponse>>
            {
                Success = true,
                MessageAr = "تم جلب أسئلة البطاقة بنجاح",
                MessageEn = "Card questions were successfully fetched",
                Data = questionResponses

            });

        }

        [HttpGet("Diagnosis")]
        public async Task<ActionResult<ApiResponse<List<DiagnosisQuestionResponse>>>> GetDiagnosisQuestions()
        {
            var result = await _questionService.GetDiagnosisQuestions();


            if (!result.IsSuccess)

                return BadRequest(new ApiResponse<List<string>>
                {
                    Success = false,
                    MessageAr = "لا يوجد اسئلة تشخيص ",
                    MessageEn = "there are no diagnosis questions ",
                });
            var questionResponses = result.Value.Select(DiagnosisQuestionResponse.Transform).ToList();
            return Ok(new ApiResponse<List<DiagnosisQuestionResponse>>
            {
                Success = true,
                MessageAr = "تم جلب أسئلة التشخيص بنجاح",
                MessageEn = "diagnosis questions are successfully fetched",
                Data = questionResponses
            });

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var result = await _questionService.GetQuestionByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "السؤال غير موجود",
                    MessageEn = "question is not exist "

                });
            var questionEntity = result.Value;

            if (questionEntity.CardName != null)

                return Ok(new ApiResponse<DiagnosisQuestionResponse>
                {
                    Success = true,
                    MessageAr = "تم جلب السؤال المطلوب بنجاح ",
                    MessageEn = "question is successfully fetched",
                    Data = DiagnosisQuestionResponse.Transform(questionEntity)

                });

            return Ok(new ApiResponse<DiagnosisQuestionResponse>
            {
                Success = true,
                MessageAr = "تم جلب السؤال المطلوب بنجاح ",
                MessageEn = "question is successfully fetched",
                Data = DiagnosisQuestionResponse.Transform(questionEntity)

            });


        }


        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] DiagnosisQuestionRequest dto)
        {
            var questionDto = new QuestionRequestDto
            {
                CardName = dto.CardName,
                MainTitle = dto.MainTitle,
                SubTitle = dto.SubTitle,
                QuestionText = dto.QuestionText,
                Options = dto.Options,
                Type = dto.Type,
                Order = dto.Order,
            };

            var result = await _questionService.AddQuestionAsync(questionDto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية اضافة سؤال",
                    MessageEn = "Failed to add Question"
                });

            var questionEntity = result.Value;

            if (questionDto.CardName == null)

                return Ok(new ApiResponse<DiagnosisQuestionResponse> 
                {
                    
                    Success = true, 
                    MessageAr="تم اضافة سؤال تشخيص",
                    MessageEn= "Question added successfully",
                    Data = DiagnosisQuestionResponse.Transform(questionEntity),

                });
            return Ok(new ApiResponse<CardQuestionResponse>
            {
                Success = true,
                MessageAr = "تم اضافة سؤال لليطاقة",
                MessageEn = "Question card added successfully",
                Data = CardQuestionResponse.Transform(questionEntity),

            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (!result.IsSuccess)

                return NotFound(new ApiResponse<string> 
                {
                    Success = false, 
                    MessageAr = "فشل حذف السؤال",
                    MessageEn= "Failed to delete question"

                });

            return Ok(new ApiResponse<bool> 
            { 
                Success = true,
                MessageAr = "تم حذف السؤال بنجاح",
                MessageEn = "Question is deleted successfully",
                Data = result.Value
            });
        }
    }
}

