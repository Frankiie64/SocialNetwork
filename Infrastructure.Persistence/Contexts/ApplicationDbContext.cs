using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Helper;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Common;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedSocial.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserVM user;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            user = _httpContextAccessor.HttpContext.Session.Get<UserVM>("user");

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = user.FirstName;
                        break;
                    case EntityState.Added:
                        entry.Entity.Creadted = DateTime.Now;
                        entry.Entity.CreatedBy = user.FirstName;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> users { get; set; }
        public DbSet<Publications> publications { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Friends> friends { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region Table
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Publications>().ToTable("publications");
            modelBuilder.Entity<Comments>().ToTable("comments");
            modelBuilder.Entity<Friends>().ToTable("friends");

            #endregion

            #region constraint

            //Primary Key

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Publications>().HasKey(x => x.Id);
            modelBuilder.Entity<Comments>().HasKey(x => x.Id);
            modelBuilder.Entity<Friends>().HasKey(x => x.Id);


            //Relationships
            modelBuilder.Entity<User>()
            .HasMany<Publications>(user => user.Publications)
            .WithOne(publication => publication.Owner)
            .HasForeignKey(publication => publication.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            modelBuilder.Entity<Publications>()
          .HasMany<Comments>(publication => publication.Coments)
          .WithOne(coments => coments.Publication)
          .HasForeignKey(coments => coments.PublicationId)
          .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
           .HasMany<Comments>(user => user.comments)
           .WithOne(comment => comment.Owner)
           .HasForeignKey(comment => comment.UserId)
           .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

                modelBuilder.Entity<User>()
            .HasMany<Friends>(user => user.Friends)
            .WithOne(friend => friend.User)
            .HasForeignKey(friend => friend.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            #endregion

            #region "Validation Required"

            #region Users

            modelBuilder.Entity<User>()
            .Property(users => users.FirstName)
            .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
          .Property(users => users.LastName)
          .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
          .Property(users => users.Username)
          .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
          .Property(users => users.Password)
          .IsRequired();

            modelBuilder.Entity<User>()
          .Property(users => users.Phone)
          .IsRequired();

            modelBuilder.Entity<User>()
          .Property(users => users.Email)
          .IsRequired();


            #endregion

            #region Publications

            modelBuilder.Entity<Publications>()
            .Property(publications => publications.Title)
            .IsRequired();

            #endregion

            #region Comments

            modelBuilder.Entity<Comments>()
            .Property(comment => comment.comment)
            .IsRequired();

            #endregion

            #region Friends

            modelBuilder.Entity<Friends>()
          .Property(friend => friend.UserId)
          .IsRequired();

                modelBuilder.Entity<Friends>()
           .Property(friend => friend.FriendId)
           .IsRequired();

            #endregion

            #endregion
        }
    }
}

