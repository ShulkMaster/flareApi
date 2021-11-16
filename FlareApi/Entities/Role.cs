using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(Role))]
    public class Role
    {
    #region Constants

        public const int NameLenght = 50;
        public const int RegularLevel = 1;
        public const int AdminLevel = 2;
        public const string Regular = nameof(Regular);
        public const string Admin = nameof(Admin);

    #endregion

        public static readonly int[] AccessLevels = { RegularLevel, AdminLevel };
        public static readonly string[] Roles = { Regular, Admin };

        [Key]
        [MaxLength(NameLenght)]
        public string Name { get; set; } = string.Empty;

        public int AccessLevel { get; set; }
    }
}