using System.ComponentModel.DataAnnotations;

namespace FlareApi.Entities
{
    public class Department
    {
    #region Constants
        
        public const int NameLenght = 150;

    #endregion
        
        [Key]
        public int Id { get; set; }

        [MaxLength(NameLenght)]
        public string Name { get; set; }
    }
}