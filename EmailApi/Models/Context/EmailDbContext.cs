using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailApi.Models.Context
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {

        }
        public DbSet<User> users{set;get;}
        public DbSet<User1> user1s{set;get;}
        public DbSet<FileModel> fileModels{set;get;}
        public DbSet<UploadedFile> uploadedFiles{set;get;}
        public DbSet<Authenticates> authenticates{set;get;}
        public DbSet<Details> details{set;get;}
                public DbSet<Userlogin> userlogins { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x=>x.userId).ValueGeneratedNever();
            modelBuilder.Entity<User>().HasIndex(x=> new {x.Email}).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x=> new {x.MobileNo}).IsUnique();
            modelBuilder.Entity<Authenticates>().Property(x=>x.userid).ValueGeneratedNever();
            modelBuilder.Entity<Details>().Property(x=>x.userId).ValueGeneratedNever();
                        modelBuilder.Entity<Userlogin>().Property(x => x.Id).ValueGeneratedNever();

        }
    }
}