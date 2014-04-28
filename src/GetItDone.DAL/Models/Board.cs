using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetItDone.DAL.Models
{
    public class Board
    {
        [Key]
        public int BoardID { get; set; }
        [Required]
        public string Name { get; set; }
        public string ColorCode { get; set; }
        [Required]
        [JsonIgnore]
        public virtual User Owner { get; set; }

    }
}
