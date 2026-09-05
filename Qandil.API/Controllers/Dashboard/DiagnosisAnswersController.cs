using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.DisgnosisAnswer;
using Qandil.API.Dtos.Responses.DiagnosisAnswers;
using Qandil.Core.Common;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.Dtos.DiagnisisAnswerDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisAnswersController(IAnswerService _answerService) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpPost("cards/save")]
        public async Task<IActionResult> SaveAndEvaluateCard([FromBody] EvaluateCardRequest request)
        {
            var dto = new EvaluateCardRequestDto
            {
                DiagnosisId = request.DiagnosisId,
                CardType = request.CardType,
                Answers = request.Answers.Select(a => new AnswerRequestDto
                {
                    QuestionId = a.QuestionId,
                    ScoreValue = a.ScoreValue,
                    Notes = a.Notes
                }).ToList()
            };

            var result = await _answerService.SaveAndEvaluateCardAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل حفظ إجابات البطاقة",
                    MessageEn = "Failed to save card answers"
                });

            var response = new CardResultResponse
            {
                CardType = result.Value.CardType,
                DisplayName = result.Value.DisplayName,
                SubTitleScores = result.Value.SubTitleScores,
                TotalScore = result.Value.TotalScore,
                EvaluationMessage = result.Value.EvaluationMessage
            };

            return Ok(new ApiResponse<CardResultResponse>
            {
                Success = true,
                MessageAr = "تم حفظ الإجابات وحساب النتيجة بنجاح",
                MessageEn = "Answers saved and evaluated successfully",
                Data = response
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpPost("diagnosis/save")]
        public async Task<IActionResult> SaveDiagnosisSubTitleAnswers([FromBody] SaveDiagnosisSubTitleAnswersRequest request)
        {
            var dto = new SaveDiagnosisSubTitleAnswersRequestDto
            {
                DiagnosisId = request.DiagnosisId,
                SubTitle = request.SubTitle,
                Answers = request.Answers.Select(a => new AnswerRequestDto
                {
                    QuestionId = a.QuestionId,
                    BooleanValue = a.BooleanValue,
                    TextValue = a.TextValue,
                    SelectedOption = a.SelectedOption,
                    Notes = a.Notes
                }).ToList()
            };

            var result = await _answerService.SaveDiagnosisSubTitleAnswersAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل حفظ إجابات المجموعة",
                    MessageEn = "Failed to save diagnosis answers"
                });

            return Ok(new ApiResponse<string>
            {
                Success = true,
                MessageAr = "تم حفظ الإجابات بنجاح",
                MessageEn = "Answers saved successfully"
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpPut("answer")]
        public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] UpdateAnswerRequest request)
        {
            var dto = new UpdateAnswerRequestDto
            {
                ScoreValue = request.ScoreValue,
                BooleanValue = request.BooleanValue,
                TextValue = request.TextValue,
                SelectedOption = request.SelectedOption,
                Notes = request.Notes
            };

            var result = await _answerService.UpdateAnswerAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل تعديل الإجابة",
                    MessageEn = "Failed to update answer"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم تعديل الإجابة بنجاح",
                MessageEn = "Answer updated successfully",
                Data = true
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpGet("cards/results/{diagnosisId:guid}")]
        public async Task<IActionResult> GetCardResults(Guid diagnosisId)
        {
            var result = await _answerService.GetCardResultsAsync(diagnosisId);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل جلب نتائج البطاقات",
                    MessageEn = "Failed to retrieve card results"
                });

            var response = result.Value.Select(r => new CardResultResponse
            {
                CardType = r.CardType,
                DisplayName = r.DisplayName,
                SubTitleScores = r.SubTitleScores,
                TotalScore = r.TotalScore,
                EvaluationMessage = r.EvaluationMessage
            }).ToList();

            return Ok(new ApiResponse<List<CardResultResponse>>
            {
                Success = true,
                MessageAr = "تم جلب نتائج البطاقات بنجاح",
                MessageEn = "Card results retrieved successfully",
                Data = response
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpGet("cards/details/{diagnosisId:guid}/{cardType}")]
        public async Task<IActionResult> GetCardAnswerDetails(Guid diagnosisId, CardType cardType)
        {
            var result = await _answerService.GetCardAnswerDetailsAsync(diagnosisId, cardType);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل جلب تفاصيل البطاقة",
                    MessageEn = "Failed to retrieve card details"
                });

            var response = new CardDetailsResponse
            {
                CardType = result.Value.CardType,
                DisplayName = result.Value.DisplayName,
                Answers = result.Value.Answers.Select(a => new CardAnswerDetailResponse
                {
                    AnswerId = a?.AnswerId,
                    QuestionId=a.QuistionId,
                    QuestionText = a.QuestionText,
                    SubTitle = a.SubTitle,
                    ScoreValue = a.ScoreValue,
                    DisplayAnswer = a.DisplayAnswer
                }).ToList()
            };

            return Ok(new ApiResponse<CardDetailsResponse>
            {
                Success = true,
                MessageAr = "تم جلب تفاصيل البطاقة بنجاح",
                MessageEn = "Card details retrieved successfully",
                Data = response
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpGet("diagnosis/results/{diagnosisId:guid}")]
        public async Task<IActionResult> GetDiagnosisQuestionsResults(Guid diagnosisId)
        {
            var result = await _answerService.GetDiagnosisQuestionsResultsAsync(diagnosisId);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل جلب نتائج أسئلة التشخيص",
                    MessageEn = "Failed to retrieve diagnosis results"
                });

            var response = result.Value.Select(g => new DiagnosisSubTitleResultResponse
            {
                SubTitle = g.SubTitle,
                Answers = g.Answers.Select(a => new DiagnosisAnswerDetailResponse
                {
                    AnswerId = a.AnswerId,
                    QuestionId = a.QuestionId,
                    QuestionText = a.QuestionText,
                    Type = a.Type,
                    BooleanValue = a.BooleanValue,
                    TextValue = a.TextValue,
                    SelectedOptionText = a.SelectedOptionText
                }).ToList()
            }).ToList();

            return Ok(new ApiResponse<List<DiagnosisSubTitleResultResponse>>
            {
                Success = true,
                MessageAr = "تم جلب نتائج أسئلة التشخيص بنجاح",
                MessageEn = "Diagnosis results retrieved successfully",
                Data = response
            });
        }
        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpDelete("answer/{diagnosisId:guid}/{questionId:guid}")]
        public async Task<IActionResult> DeleteAnswerByQuestion(Guid diagnosisId, Guid questionId)
        {
            var result = await _answerService.DeleteAnswerByQuestionAsync(diagnosisId, questionId);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل حذف الإجابة",
                    MessageEn = "Failed to delete answer"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الإجابة بنجاح",
                MessageEn = "Answer deleted successfully",
                Data = true
            });
        }
    }
}