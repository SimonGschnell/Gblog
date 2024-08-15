using Microsoft.EntityFrameworkCore;

namespace Gblog.Context
{
    public class PostContext: DbContext
    {

        public PostContext(DbContextOptions<PostContext> options) : base(options){
            
        }

        public DbSet<Models.Post> Post { get; set; }
    }
}
