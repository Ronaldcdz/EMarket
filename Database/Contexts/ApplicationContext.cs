using EMarket.Core.Domain.Common;
using EMarket.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api

            #region tables
            modelBuilder.Entity<Advert>().ToTable("Adverts");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion


            #region "primary keys"
            modelBuilder.Entity<Advert>().HasKey(advert => advert.Id);
            modelBuilder.Entity<Category>().HasKey(category => category.Id);
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Category>()
                .HasMany<Advert>(category => category.Adverts)
                .WithOne(advert => advert.Category)
                .HasForeignKey(advert => advert.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Advert>(user => user.Adverts)
                .WithOne(advert => advert.User)
                .HasForeignKey(advert => advert.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region "property configurations"


            #region adverts
            modelBuilder.Entity<Advert>()
                .Property(advert => advert.Name)
                .IsRequired();

            modelBuilder.Entity<Advert>()
               .Property(advert => advert.Description)
               .IsRequired();


            modelBuilder.Entity<Advert>()
                .Property(advert => advert.ImagePath);

            modelBuilder.Entity<Advert>()
                .Property(advert => advert.Price)
                .IsRequired();

            modelBuilder.Entity<Advert>()
                .Property(advert => advert.CategoryId)
                .IsRequired();



            #endregion

            #region category
            modelBuilder.Entity<Category>()
                .Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(120);

            modelBuilder.Entity<Category>()
                .Property(category => category.Description)
                .IsRequired()
                .HasMaxLength(120);

            #endregion

            #region users
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(user => user.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
               .Property(user => user.Email)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(user => user.Phone)
               .IsRequired();


            #endregion



            #endregion
        }
    }
}
