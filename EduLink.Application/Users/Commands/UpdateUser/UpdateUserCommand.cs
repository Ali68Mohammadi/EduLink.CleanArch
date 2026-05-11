using MediatR;
using System.Text.Json.Serialization;

namespace EduLink.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand :IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? PhoneNumber { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
    }
}
