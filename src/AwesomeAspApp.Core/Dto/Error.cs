using System.Collections.Generic;

namespace AwesomeAspApp.Core.Dto
{
    public class Error
    {
        public Error(string type, string message, int code, IReadOnlyDictionary<string, string>? fields = null)
        {
            Type = type;
            Message = message;
            Code = code;
            Fields = fields;
        }

        public Error()
        {
        }

        public string Type { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public IReadOnlyDictionary<string, string>? Fields { get; set; }
    }
}
