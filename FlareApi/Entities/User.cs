using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(User))]
    public class User
    {
    #region Constants

        public const int UenLenght = 12;
        public const int UenMinLenght = 6;
        public const int NameLenght = 150;
        public const int LastNameLenght = 150;
        public const int PasswordLenght = 64;
        public const int HashLenght = 128;

    #endregion

        [Key]
        [MaxLength(UenLenght)]
        public string Uen { get; set; } = string.Empty;

        [MaxLength(NameLenght)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(LastNameLenght)]
        public string LastName { get; set; } = string.Empty;

            [MaxLength(HashLenght)]
        public string Password { get; set; } = string.Empty;
        
        public string RoleId { get; set; } = string.Empty;
        
        public virtual Role Role { get; set; } = null!;
        
        public int DepartmentId { get; set; }
        
        public virtual Department Department { get; set; } = null!;

        public bool Active { get; set; }

        public DateTime? ReactivationDate { get; set; }

        public int FailedAttempts { get; set; }

        public DateTime? Birthday { get; set; }

        public Gender? Gender { get; set; }
    }
}