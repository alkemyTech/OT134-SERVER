using System.Collections.Generic;

namespace OngProject.Core.Helper
{
    public class ManagerResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
