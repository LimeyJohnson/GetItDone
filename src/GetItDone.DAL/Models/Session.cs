using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetItDone.DAL.Models
{
    public class Session
    {
        
        [Key]
        public Guid ID { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Expires { get; set; }
        public virtual User SessionUser { get; set; }
    }
}
