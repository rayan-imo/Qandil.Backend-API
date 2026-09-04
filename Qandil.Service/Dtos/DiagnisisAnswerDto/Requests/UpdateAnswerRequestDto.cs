using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Dtos.DiagnisisAnswerDto.Requests
{
    public class UpdateAnswerRequestDto
    {
        public int? ScoreValue { get; set; }
        public bool? BooleanValue { get; set; }
        public string TextValue { get; set; }
        public string SelectedOption { get; set; }
        public string Notes { get; set; }
    }
}
