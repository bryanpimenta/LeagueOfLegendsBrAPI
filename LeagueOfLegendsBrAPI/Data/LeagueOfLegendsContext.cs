using Microsoft.EntityFrameworkCore;
using LeagueOfLegendsBrAPI.Models;

namespace LeagueOfLegendsBrAPI.Data
{
    public class LeagueOfLegendsContext : DbContext
    {
        public LeagueOfLegendsContext(DbContextOptions<LeagueOfLegendsContext> options)
            : base(options)
        {
        }

        public DbSet<Champion> Champion { get; set; }
        public DbSet<ChampionSkin> ChampionSkins { get; set; }
        public DbSet<ChampionInfo> ChampionInfos { get; set; }
        public DbSet<ChampionStats> ChampionStats { get; set; }
        public DbSet<ChampionSpell> ChampionSpells { get; set; }
        public DbSet<ChampionPassive> ChampionPassives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>()
                .HasMany(c => c.Skins)
                .WithOne(s => s.Champion)
                .HasForeignKey(s => s.ChampionId);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Info)
                .WithOne(i => i.Champion)
                .HasForeignKey<ChampionInfo>(i => i.ChampionId);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Stats)
                .WithOne(s => s.Champion)
                .HasForeignKey<ChampionStats>(s => s.ChampionId);

            modelBuilder.Entity<Champion>()
                .HasMany(c => c.Spells)
                .WithOne(s => s.Champion)
                .HasForeignKey(s => s.ChampionId);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Passive)
                .WithOne(p => p.Champion)
                .HasForeignKey<ChampionPassive>(p => p.ChampionId);
        }
    }
}
