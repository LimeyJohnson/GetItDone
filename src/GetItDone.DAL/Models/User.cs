﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetItDone.DAL.Models
{
    public class User
    {
        public User()
        {
            Joined = DateTime.Now;
        }
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Joined { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }


        #region Playlist

        public virtual List<PlayList> PlayLists { get; set; }

        #endregion

        public virtual List<Board> Boards
        {
            get;
            set;
        }

    }

}
