﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public int UserID { get; set; }
        [Required,JsonIgnore, ForeignKey("UserID")]
        public virtual User Creator { get; set; }
        /// <summary>
        /// If not null it will only show tasks that have a changed date less than today + Filter
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Nullable<int> Filter { get; set; }

        
        public virtual List<Task> Tasks { get; set; }
        public virtual List<User> BoardUsers { get; set; } 
    }
}
