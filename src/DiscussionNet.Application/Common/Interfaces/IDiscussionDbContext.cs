using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Comment = DiscussionNet.Domain.Entities.Comment;

namespace DiscussionNet.Application.Common.Interfaces
{
    public interface IDiscussionDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
        DbSet<Topic> Topics { get; set; }
        //DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<CommentReaction> CommentReactions { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
