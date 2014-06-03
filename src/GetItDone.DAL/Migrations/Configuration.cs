using GetItDone.DAL.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace GetItDone.DAL.Migrations
{
    

    internal sealed class Configuration : DbMigrationsConfiguration<GetItDone.DAL.GetItDoneContext>
    {
        public Configuration()
        {
#if DEBUG
            AutomaticMigrationsEnabled = false;
#else
            AutomaticMigrationsEnabled = true;
#endif


        }

        protected override void Seed(GetItDone.DAL.GetItDoneContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            ////
           

                
        }
    }
}
