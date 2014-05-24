using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetItDone.DAL.Models
{
   
    public class UserBoard
    {
        [Key, Column(Order=0)]
        public int UserID { get; set; }
        [Key, Column(Order = 1)]
        public int BoardID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("BoardID")]
        public Board Board { get; set; }
    }
}
