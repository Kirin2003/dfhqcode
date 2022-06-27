using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = "";

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; } = "";

        [StringLength(10)]
        public string Gender { get; set; } = "";

        [StringLength(50)]
        public string College { get; set; } = "";

        [StringLength(50)]
        public string Profession { get; set; } = "";

        [StringLength(200)]
        public string InterestingAreas { get; set; } = "";
    }
}
