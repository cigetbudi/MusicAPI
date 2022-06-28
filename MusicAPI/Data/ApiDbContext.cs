﻿using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Song> Songs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    Id = 1,
                    Title = "Matahariku",
                    Language = "Indonesia",
                    Duration = "4:00"
                },
                new Song
                {
                    Id = 2,
                    Title = "Voyeur",
                    Language = "English",
                    Duration = "4:33"
                }
                );
        }
    }
}
