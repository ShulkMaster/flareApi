using System;
using FlareApi.Entities;

namespace FlareApi.Api.V1.Request
{
    public class CreateUserRequest
    {
        public string Uen { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }

        public Gender? Gender { get; set; }

        public int DepartmentId { get; set; }
    }
}