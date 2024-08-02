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
        public DbSet<ChampionSkin> ChampionSkin { get; set; }
        public DbSet<ChampionInfo> ChampionInfo { get; set; }
        public DbSet<ChampionStats> ChampionStats { get; set; }
        public DbSet<ChampionSpell> ChampionSpell { get; set; }
        public DbSet<ChampionPassive> ChampionPassive { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>()
                .HasKey(c => c.Key);

            modelBuilder.Entity<ChampionSkin>()
                .HasKey(cs => cs.Id);

            modelBuilder.Entity<ChampionSkin>()
                .HasOne(cs => cs.Champion)
                .WithMany(c => c.Skins)
                .HasForeignKey(cs => cs.Champion_id)
                .HasPrincipalKey(c => c.Key);

            modelBuilder.Entity<ChampionInfo>()
                .HasKey(ci => ci.Champion_id);

            modelBuilder.Entity<ChampionStats>()
                .HasKey(cs => cs.Champion_id);

            modelBuilder.Entity<ChampionSpell>()
                .HasOne(cs => cs.Champion)
                .WithMany(c => c.Spells)
                .HasForeignKey(cs => cs.Champion_id)
                .HasPrincipalKey(c => c.Key);

            modelBuilder.Entity<ChampionPassive>()
                .HasKey(cp => cp.Champion_id);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Info)
                .WithOne(i => i.Champion)
                .HasForeignKey<ChampionInfo>(i => i.Champion_id);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Stats)
                .WithOne(s => s.Champion)
                .HasForeignKey<ChampionStats>(s => s.Champion_id);

            modelBuilder.Entity<Champion>()
                .HasMany(c => c.Spells)
                .WithOne(s => s.Champion)
                .HasForeignKey(s => s.Champion_id);

            modelBuilder.Entity<Champion>()
                .HasOne(c => c.Passive)
                .WithOne(p => p.Champion)
                .HasForeignKey<ChampionPassive>(p => p.Champion_id);
        }

    }
}
