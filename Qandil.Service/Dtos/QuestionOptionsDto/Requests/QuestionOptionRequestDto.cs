using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Dtos.QuestionOptionsDto.Requests
{
    public  class QuestionOptionRequestDto
    {
        public string Text { get; set; }
        public int? Value { get; set; }
        public int Order { get; set; }
        public Guid DiagnosisQuestionId { get; set; }
    }
}
