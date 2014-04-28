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
        /// <summary>
        /// If not null it will only show tasks that have a changed date less than today + Filter
        /// </summary>
        public Nullable<int> Filter { get; set; }

    }
}
