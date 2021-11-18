using System;
using FlareApi.Entities;

namespace FlareApi.Api.V1.Request
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }

        public Gender? Gender { get; set; }

        public int DepartmentId { get; set; }
    }

    public class CreateUserRequest : UpdateUserRequest
    {
        public string Uen { get; set; } = string.Empty;
    }
}