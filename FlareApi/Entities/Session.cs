using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlareApi.Entities
{
    [Table(nameof(Session))]
    public class Session
    {
        public Guid Id { get; set; }
        
        public DateTime Expiration { get; set; }
        
        [ForeignKey(nameof(User.Uen))]
        public string Uen { get; set; } = string.Empty;

        public User UserNav { get; set; } = null!;
    }
}