using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Domain.Entities;
using Eskisehirspor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Eskisehirspor.Persistence.Context
{
    public class ForumDbContext : DbContext, IForumDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Domain.Entities.Thread> Threads { get; set; }
        public DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        //public DbSet<ThreadReaction> ThreadReactions { get; set; }

        public ForumDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.SoftDelete();
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Database.BeginTransactionAsync();
        }
    }
}
