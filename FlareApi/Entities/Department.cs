using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(Department))]
    public class Department
    {
    #region Constants
        
        public const int NameLenght = 150;

    #endregion
        
        [Key]
        public int Id { get; set; }

        [MaxLength(NameLenght)]
        public string Name { get; set; } = string.Empty;
    }
}