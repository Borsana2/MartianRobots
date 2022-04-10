using System.Data.Entity;

namespace MartianRobots.Infrastructure
{

    class MartianRobotsContext : DbContext
        {
            
            public MartianRobotsContext() : base("name=MartianRobotsDB") { }
            

            }

            public DbSet<Robot> Robots { get; set; }
            public DbSet<Grid> Grids { get; set; }
            public DbSet<Scent> Scents { get; set; }
            
        

        }
    
}
