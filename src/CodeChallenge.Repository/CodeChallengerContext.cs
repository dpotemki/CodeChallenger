using CodeChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeChallenge.Repository
{
    public class CodeChallengerContext : DbContext
    {
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeDescription> ChallengeDescriptions { get; set; }
        public DbSet<ChallengeSolution> ChallengeSolutions { get; set; }
        public DbSet<ChallengeCodeTemplate> ChallengeCodeTemplates { get; set; }
        public DbSet<TestCase> TestCases { get; set; }

        public CodeChallengerContext(DbContextOptions<CodeChallengerContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.Descriptions)
                .WithOne(d => d.Challenge)
                .HasForeignKey(d => d.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.Solutions)
                .WithOne(s => s.Challenge)
                .HasForeignKey(s => s.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.CodeTemplates)
                .WithOne(ct => ct.Challenge)
                .HasForeignKey(ct => ct.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.TestCases)
                .WithOne(tc => tc.Challenge)
                .HasForeignKey(tc => tc.ChallengeId);
        }
    }

}
