﻿using Microsoft.EntityFrameworkCore;
using APBD_kol2.Models;


public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<CharacterTitle> CharacterTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Backpack>().HasKey(b => new { b.CharacterId, b.ItemId });
            modelBuilder.Entity<CharacterTitle>().HasKey(ct => new { ct.CharacterId, ct.TitleId });

            modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 10
                },
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 2,
                    Amount = 5
                },
                new Backpack
                {
                    CharacterId = 2,
                    ItemId = 3,
                    Amount = 1
                },
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character
                {
                    Id = 1,
                    FirstName = "Kacper",
                    LastName = "Mazur",
                    CurrentWeight = 10,
                    MaxWeight = 1060
                },
                new Character
                {
                    Id = 2,
                    FirstName = "Grzegorz",
                    LastName = "Olszewski",
                    CurrentWeight = 5,
                    MaxWeight = 25
                },
                new Character
                {
                    Id = 3,
                    FirstName = "Michal",
                    LastName = "Pazio",
                    CurrentWeight = 1,
                    MaxWeight = 977
                },
            });

            modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
            {
                new CharacterTitle
                {
                    CharacterId = 1,
                    TitleId = 1,
                    AcquiredAt = DateTime.Now
                },
                new CharacterTitle
                {
                    CharacterId = 2,
                    TitleId = 2,
                    AcquiredAt = DateTime.Now
                },
                new CharacterTitle
                {
                    CharacterId = 3,
                    TitleId = 3,
                    AcquiredAt = DateTime.Now
                },
            });

            modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Banan",
                    Weight = 5
                },
                new Item
                {
                    Id = 2,
                    Name = "AK47",
                    Weight = 10
                },
                new Item
                {
                    Id = 3,
                    Name = "Papier",
                    Weight = 1
                },
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title
                {
                    Id = 1,
                    Name = "Mag"
                },
                new Title
                {
                    Id = 2,
                    Name = "Raper"
                },
                new Title
                {
                    Id = 3,
                    Name = "Informatyk"
                },
            });
        }
    }
