using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Persistence.Context
{
    public class ContactDbContextFactory : DesignTimeDbContextFactoryBase<ForumDbContext>
    {
        protected override ForumDbContext CreateNewInstance(DbContextOptions<ForumDbContext> options)
        {
            return new ForumDbContext(options);
        }
    }
}
