using Application.DTOs.AuthDtos;

namespace Application.Responses
{
    public class RegisterUserResponses
    {
        public ReturnDataDto? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
