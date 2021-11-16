using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(Role))]
    public class Role
    {
    #region Constants

        public const int NameLenght = 50;
        public const int Regular = 1;
        public const int Admin = 2;

    #endregion

        public static readonly int[] AccessLevels = { Regular, Admin };
        public static readonly string[] Roles = { "Regular", "Admin" };

        [Key]
        [MaxLength(NameLenght)]
        public string Name { get; set; } = string.Empty;

        public int AccessLevel { get; set; }
    }
}