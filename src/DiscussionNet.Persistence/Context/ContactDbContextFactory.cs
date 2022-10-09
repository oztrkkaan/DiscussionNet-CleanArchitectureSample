using Microsoft.EntityFrameworkCore;

namespace DiscussionNet.Persistence.Context
{
    public class ContactDbContextFactory : DesignTimeDbContextFactoryBase<DiscussionDbContext>
    {
        protected override DiscussionDbContext CreateNewInstance(DbContextOptions<DiscussionDbContext> options)
        {
            return new DiscussionDbContext(options);
        }
    }
}
