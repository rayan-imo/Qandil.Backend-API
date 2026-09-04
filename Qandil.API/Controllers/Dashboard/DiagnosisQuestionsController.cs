using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.DiagnosisQuestions;
using Qandil.API.Dtos.Responses.DiagnosisQuestions;
using Qandil.Core.Common;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Requests;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisQuestionsController(IDiagnosisQuestionService _diagnosisQuestionService) : ControllerBase
    {
       
            [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
            [HttpGet("diagnosis-questions")]
            public async Task<IActionResult> GetAllDiagnosisQuestions()
            {
                var result = await _diagnosisQuestionService.GetAllDiagnosisQuestionsAsync();

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = "فشل جلب أسئلة التشخيص",
                        MessageEn = "Failed to retrieve diagnosis questions"
                    });

                var response = result.Value.Select(g => new DiagnosisQuestionResponse
                {
                    SubTitle = g.SubTitle,
                    Questions = g.Questions.Select(q => new DiagnosisQuestionItemResponse
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        Type = q.Type,
                        Order = q.Order,
                        Options = q.Options.Select(o => new DiagnosisOptionResponse
                        {
                            Id = o.Id,
                            Text = o.Text
                        }).ToList()
                    }).ToList()
                }).ToList();

                return Ok(new ApiResponse<List<DiagnosisQuestionResponse>>
                {
                    Success = true,
                    MessageAr = "تم جلب أسئلة التشخيص بنجاح",
                    MessageEn = "Diagnosis questions retrieved successfully",
                    Data = response
                });
            }

            [Authorize(Roles = "Admin,SuperAdmin")]
            [HttpPost("diagnosis")]
            public async Task<IActionResult> AddDiagnosisQuestion([FromBody] DiagnosisQuestionRequest request)
            {
                var dto = new DiagnosisQuestionRequestDto
                {
                    SubTitle = request.SubTitle,
                    QuestionText = request.QuestionText,
                    Type = request.Type,
                    Order = request.Order,
                    Options = request.Options?.Select(o => new DiagnosisQuestionOptionRequestDto
                    {
                        Text = o.Text,
                        Order = o.Order
                    }).ToList()
                };

                var result = await _diagnosisQuestionService.AddDiagnosisQuestionAsync(dto);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "فشلت عملية إضافة السؤال",
                        MessageEn = "Failed to add question"
                    });

            return Ok(new ApiResponse<DiagnosisQuestionItemResponse>
            {
                Success = true,
                MessageAr = "تمت إضافة السؤال بنجاح",
                MessageEn = "Question added successfully",
                Data = DiagnosisQuestionItemResponse.Transform(result.Value)



            });
            }

            [Authorize(Roles = "Admin,SuperAdmin")]
            [HttpPut("diagnosis/{id:guid}")]
            public async Task<IActionResult> UpdateDiagnosisQuestion(Guid id, [FromBody] DiagnosisQuestionRequest request)
            {
                var dto = new DiagnosisQuestionRequestDto
                {
                    SubTitle = request.SubTitle,
                    QuestionText = request.QuestionText,
                    Type = request.Type,
                    Order = request.Order,
                    Options = request.Options?.Select(o => new DiagnosisQuestionOptionRequestDto
                    {
                        Text = o.Text,
                        Order = o.Order
                    }).ToList()
                };

                var result = await _diagnosisQuestionService.UpdateDiagnosisQuestionAsync(id, dto);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "فشل تحديث السؤال",
                        MessageEn = "Failed to update question"
                    });

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    MessageAr = "تم تحديث السؤال بنجاح",
                    MessageEn = "Question updated successfully"
                });
            }

            [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
            [HttpGet("cards")]
            public async Task<IActionResult> GetAllCardQuestions()
            {
                var result = await _diagnosisQuestionService.GetAllCardQuestionsAsync();

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = "فشل جلب أسئلة البطاقات",
                        MessageEn = "Failed to retrieve card questions"
                    });

                var response = result.Value.Select(c => new CardQuestionsResponse
                {
                    CardType = c.CardType,
                    DisplayName = c.DisplayName,
                    SubTitleGroups = c.SubTitleGroups.Select(sg => new SubTitleGroupResponse
                    {
                        SubTitle = sg.SubTitle,
                        Questions = sg.Questions.Select(q => new CardQuestionItemResponse
                        {
                            Id = q.Id,
                            QuestionText = q.QuestionText,
                            Type = q.Type,
                            ScoreInputType = q.ScoreInputType,
                            MinValue = q.MinValue,
                            MaxValue = q.MaxValue
                        }).ToList()
                    }).ToList()
                }).ToList();

                return Ok(new ApiResponse<List<CardQuestionsResponse>>
                {
                    Success = true,
                    MessageAr = "تم جلب أسئلة البطاقات بنجاح",
                    MessageEn = "Card questions retrieved successfully",
                    Data = response
                });
            }

            [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
            [HttpGet("cards/{cardType}")]
            public async Task<IActionResult> GetQuestionsByCardType(CardType cardType)
            {
                var result = await _diagnosisQuestionService.GetQuestionsByCardType(cardType);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "لا يوجد أسئلة لهذه البطاقة",
                        MessageEn = "No questions found for this card"
                    });

                var response = result.Value.Select(q => new CardQuestionItemResponse
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Type = q.Type,
                    ScoreInputType = q.ScoreInputType,
                    MinValue = q.MinValue,
                    MaxValue = q.MaxValue
                }).ToList();

                return Ok(new ApiResponse<List<CardQuestionItemResponse>>
                {
                    Success = true,
                    MessageAr = "تم جلب أسئلة البطاقة بنجاح",
                    MessageEn = "Card questions retrieved successfully",
                    Data = response
                });
            }

            [Authorize(Roles = "Admin,SuperAdmin")]
            [HttpPost("cards")]
            public async Task<IActionResult> AddCardQuestion([FromBody] CardQuestionRequest request)
            {
                var dto = new CardQuestionRequestDto
                {
                    CardType = request.CardType,
                    SubTitle = request.SubTitle,
                    QuestionText = request.QuestionText,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    Order = request.Order
                };

                var result = await _diagnosisQuestionService.AddCardQuestionAsync(dto);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "فشلت عملية إضافة سؤال البطاقة",
                        MessageEn = "Failed to add card question"
                    });

                return Ok(new ApiResponse<DiagnosisQuestionItemResponse>
                {
                    Success = true,
                    MessageAr = "تمت إضافة سؤال البطاقة بنجاح",
                    MessageEn = "Card question added successfully",
                    Data= DiagnosisQuestionItemResponse.Transform(result.Value)

                });
            }

            [Authorize(Roles = "Admin,SuperAdmin")]
            [HttpPut("cards/{id:guid}")]
            public async Task<IActionResult> UpdateCardQuestion(Guid id, [FromBody] CardQuestionRequest request)
            {
                var dto = new CardQuestionRequestDto
                {
                    CardType = request.CardType,
                    SubTitle = request.SubTitle,
                    QuestionText = request.QuestionText,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    Order = request.Order
                };

                var result = await _diagnosisQuestionService.UpdateCardQuestionAsync(id, dto);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "فشل تحديث سؤال البطاقة",
                        MessageEn = "Failed to update card question"
                    });

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    MessageAr = "تم تحديث سؤال البطاقة بنجاح",
                    MessageEn = "Card question updated successfully"
                });
            }

            [Authorize(Roles = "Admin,SuperAdmin")]
            [HttpDelete("{id:guid}")]
            public async Task<IActionResult> DeleteQuestion(Guid id)
            {
                var result = await _diagnosisQuestionService.DeleteQuestionAsync(id);

                if (!result.IsSuccess)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        MessageAr = result.Error ?? "فشل حذف السؤال",
                        MessageEn = "Failed to delete question"
                    });

                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    MessageAr = "تم حذف السؤال بنجاح",
                    MessageEn = "Question deleted successfully",
                    Data = true
                });
            }
     }
}
