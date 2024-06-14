using Microsoft.EntityFrameworkCore;
using APBD_kol2.Models;


public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Characters> Characters { get; set; }
        public DbSet<Backpacks> Backpacks { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Character_Titles> CharacterTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Backpacks>().HasKey(b => new { b.CharacterId, b.ItemId });
            modelBuilder.Entity<Character_Titles>().HasKey(ct => new { ct.CharacterId, ct.TitleId });

            modelBuilder.Entity<Backpacks>().HasData(new List<Backpacks>
            {
                new Backpacks
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 10
                },
                new Backpacks
                {
                    CharacterId = 1,
                    ItemId = 2,
                    Amount = 5
                },
                new Backpacks
                {
                    CharacterId = 2,
                    ItemId = 3,
                    Amount = 1
                },
            });

            modelBuilder.Entity<Characters>().HasData(new List<Characters>
            {
                new Characters
                {
                    Id = 1,
                    FirstName = "Kacper",
                    LastName = "Mazur",
                    CurrentWeight = 10,
                    MaxWeight = 1060
                },
                new Characters
                {
                    Id = 2,
                    FirstName = "Grzegorz",
                    LastName = "Olszewski",
                    CurrentWeight = 5,
                    MaxWeight = 25
                },
                new Characters
                {
                    Id = 3,
                    FirstName = "Michal",
                    LastName = "Pazio",
                    CurrentWeight = 1,
                    MaxWeight = 977
                },
            });

            modelBuilder.Entity<Character_Titles>().HasData(new List<Character_Titles>
            {
                new Character_Titles
                {
                    CharacterId = 1,
                    TitleId = 1,
                    AcquiredAt = DateTime.Now
                },
                new Character_Titles
                {
                    CharacterId = 2,
                    TitleId = 2,
                    AcquiredAt = DateTime.Now
                },
                new Character_Titles
                {
                    CharacterId = 3,
                    TitleId = 3,
                    AcquiredAt = DateTime.Now
                },
            });

            modelBuilder.Entity<Items>().HasData(new List<Items>
            {
                new Items
                {
                    Id = 1,
                    Name = "Banan",
                    Weight = 5
                },
                new Items
                {
                    Id = 2,
                    Name = "AK47",
                    Weight = 10
                },
                new Items
                {
                    Id = 3,
                    Name = "Papier",
                    Weight = 1
                },
            });

            modelBuilder.Entity<Titles>().HasData(new List<Titles>
            {
                new Titles
                {
                    Id = 1,
                    Name = "Mag"
                },
                new Titles
                {
                    Id = 2,
                    Name = "Raper"
                },
                new Titles
                {
                    Id = 3,
                    Name = "Informatyk"
                },
            });
        }
    }
