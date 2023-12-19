using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PopastNaStajirovku2.Models
{
    public class User 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }   
        public string Email { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
        
       
    }
}
