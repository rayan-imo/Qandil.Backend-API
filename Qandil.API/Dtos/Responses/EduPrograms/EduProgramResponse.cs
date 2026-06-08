using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.EduPrograms
{
    public class EduProgramResponse
    {
        public string Name { get; set; }
        public int SessionNumber { get; set; }
        public string SessionDuration { get; set; }
        
       public static EduProgramResponse Transform (EduProgram program)
        {
            return new EduProgramResponse 
            { 
               Name= program.Name,
               SessionDuration= program.SessionDuration,
               SessionNumber= program.SessionNumber,

            };

        }

    }
}
