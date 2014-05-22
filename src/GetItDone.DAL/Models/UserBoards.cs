using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetItDone.DAL.Models
{
   
    public class UserBoard
    {
        [Key]
        public int UserBoardID { get; set; }

        public User User { get; set; }

        public Board Board { get; set; }
    }
}
