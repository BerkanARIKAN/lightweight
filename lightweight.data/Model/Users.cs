using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lightweight.data.Model
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public bool status { get; set; }
        [NotMapped]
        public string token { get; set; }

        public string role { get; set; }
    }
}