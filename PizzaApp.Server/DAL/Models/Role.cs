using PizzaApp.Server.Enums;
using PizzaApp.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApp.Server.DAL.Models
{
    public class Role
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("user_role")]
        public UserRoles UserRole { get; set; }

        public ICollection<User> User { get; set; } = new HashSet<User>();
    }
}
