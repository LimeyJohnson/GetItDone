using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using GetItDone.DAL.Models;

namespace GetItDone.DAL
{
    public class GetItDoneContext : DbContext
    {
        public GetItDoneContext(string connString)
            : base(connString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public GetItDoneContext() : this("GetItDone") { } 

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Board> Boards{ get; set; }
        public DbSet<TaskSchedule> Schedules { get; set; }

        public DbSet<UserBoard> UserBoards { get; set; }

    }

}
