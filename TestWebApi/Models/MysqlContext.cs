using Microsoft.EntityFrameworkCore;

namespace BackendCode.Models
{
    public class MySqlContext: DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }
        public DbSet<UserLike> UsersLikes { get; set; }
        public DbSet<UserRead> UserReads { get; set; }
    }
}
