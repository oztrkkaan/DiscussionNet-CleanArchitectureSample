﻿using DiscussionNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Thread = DiscussionNet.Domain.Entities.Thread;

namespace DiscussionNet.Application.Common.Interfaces
{
    public interface IForumDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserEmailVerification> UserEmailVerifications { get; set; }
        DbSet<Topic> Topics { get; set; }
        //DbSet<Tag> Tags { get; set; }
        DbSet<Thread> Threads { get; set; }
        DbSet<ThreadReaction> ThreadReactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}