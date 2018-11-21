﻿using Lessplastic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LessplasticWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventTowns> EventsTowns { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<UserEvents> UsersEvents { get; set; }

        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEvents>(x =>
            {
                x.HasKey(e => new { e.EventId, e.LessplasticUserId });
                
                x.HasOne(e => e.Event)
                 .WithMany(e => e.Participants)
                 .HasForeignKey(e => e.EventId)
                 .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(e => e.LessplasticUser)
                 .WithMany(e => e.Events)
                 .HasForeignKey(e => e.LessplasticUserId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<EventTowns>(x =>
            {
                x.HasKey(e => new { e.EventId, e.TownId });

                x.HasOne(e => e.Town)
                 .WithMany(e => e.Events)
                 .HasForeignKey(e => e.TownId)
                 .OnDelete(DeleteBehavior.Restrict);
                
                x.HasOne(e => e.Event)
                 .WithMany(e => e.Towns)
                 .HasForeignKey(e => e.EventId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }
    }
}