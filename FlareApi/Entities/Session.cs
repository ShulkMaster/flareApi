using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(Session))]
    public class Session
    {
        public const int GuiLenght = 36;

        // please change GuiLenght here as well
        [Column(TypeName = "CHAR(36)")]
        public Guid Id { get; set; }

        public DateTime Expiration { get; set; }

        public string Uen { get; set; } = string.Empty;

        [ForeignKey(nameof(Entities.User.Uen))]
        public virtual User User { get; set; } = null!;
    }
}