using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Dtos.TestSubjectDto.Request
{
    public class TestSubjectRequestDto
    {
        public Guid TestId { get; set; }
        public Guid SubjectId { get; set; }
        public double MaxMark { get; set; }
    }
}
