using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;

public partial class MeMawKnowsBestCrimeDataContext : DbContext
{
    public MeMawKnowsBestCrimeDataContext()
    {
    }

    public MeMawKnowsBestCrimeDataContext(DbContextOptions<MeMawKnowsBestCrimeDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MeMawKnowBestThreeWeightedCrimeDatum> MeMawKnowBestThreeWeightedCrimeData { get; set; }
    public virtual DbSet<MeMawKnowBest_StateName> MeMawKnowBest_StateNames { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost; database= MeMawBestKnowBest;  Trusted_Connection=true; TrustServerCertificate=True; encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeMawKnowBestThreeWeightedCrimeDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MeMawKNowBest_Three_Weighted_CrimeData");
            entity.Property(e => e.StateCityIdentity).HasColumnName("CitySize");
            entity.Property(e => e.StateCityIdentity).HasColumnName("State_City_Identity");
            entity.Property(e => e.WRelAggravatedAssaultWeighted).HasColumnName("W_Rel_AggravatedAssault_Weighted");
            entity.Property(e => e.WRelAssaultOffensesWeighted).HasColumnName("W_Rel_AssaultOffenses_Weighted");
            entity.Property(e => e.WRelCrimesAgainstPersonsWeighted).HasColumnName("W_Rel_CrimesAgainstPersons_Weighted");
            entity.Property(e => e.WRelCrimesAgainstPropertyWeighted).HasColumnName("W_Rel_CrimesAgainstProperty_Weighted");
            entity.Property(e => e.WRelCrimesAgainstSocietyWeighted).HasColumnName("W_Rel_CrimesAgainstSociety_Weighted");
            entity.Property(e => e.WRelTotalMajorCrimeWeighted).HasColumnName("W_Rel_TotalMajorCrime_Weighted");
            entity.Property(e => e.WRelTotalOffenseWeighted).HasColumnName("W_Rel_TotalOffense_Weighted");
        });


        modelBuilder.Entity<MeMawKnowBest_StateName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MeMawKnowBest_StateName");
            entity.Property(e => e.States).HasColumnName("States");

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
