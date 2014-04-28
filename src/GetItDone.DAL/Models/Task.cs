using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetItDone.DAL.Models
{
    public class Task
    {
        public Task()
        {

            Created = DateTime.Now;
        }
        [Key]
        public int TaskID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Details { get; set; }
        
        [Required]
        [JsonIgnore]
        public virtual User Owner { get; set; }
        
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        
        [Column(TypeName = "datetime2")]
        public Nullable<DateTime> Due { get; set; }
        
        [Column(TypeName = "datetime2")]
        public Nullable<DateTime> Moved{ get; set; }
        
        public Nullable<int> Priority { get; set; }
       
        //How long the task is going to take in min
        public int Duration { get; set; }

        public Board Board { get; set; }

        public void ChangeBoard(Board newBoard)
        {
            this.Moved = DateTime.Now;
            this.Board = newBoard;
        }
    }
}
