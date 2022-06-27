using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    public class UserRead
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Index { get; set; }

        public long UserId { get; set; }

        [StringLength(100)]
        public string PaperId { get; set; }

        // 阅读量
        public int reads { get; set; }

        public DateTime time { get; set; }
    }
}
