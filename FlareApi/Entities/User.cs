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
        public const int NameLenght = 150;
        public const int LastNameLenght = 150;
        public const int PasswordLenght = 64;
        public const int HashLenght = 128;

    #endregion

        [Key]
        [MaxLength(UenLenght)]
        public string Uen { get; set; }

        [MaxLength(NameLenght)]
        public string Name { get; set; }

        [MaxLength(LastNameLenght)]
        public string LastName { get; set; }
        
        [MaxLength(HashLenght)]
        public string Password { get; set; }
        
        public string RoleId { get; set; }
        
        public virtual Role RoleNav { get; set; } = null!;
        
        public int DepartmentId { get; set; }
        
        public virtual Department DepartmentNav { get; set; } = null!;

        public bool Active { get; set; } = true;

        public DateTime? ReactivationDate { get; set; }

        public int FailedAttempts { get; set; }

    }
}