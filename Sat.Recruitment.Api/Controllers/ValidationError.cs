using System.Collections.Generic;

namespace Sat.Recruitment.Api.Controllers
{
    public class ValidationError
    {
        public ValidationError()
        {
            this.Title = "One or more validation errors occurred.";
            this.Errors = new List<string>();
        }
        public string Title { get; set; }
        public List<string> Errors { get; set; }
    }
}