using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ValidationApi.Ef
{
    public partial class CONN_STRContext : DbContext
    {
        public CONN_STRContext()
        {
        }

        public CONN_STRContext(DbContextOptions<CONN_STRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StrConexao> StrConexaos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        
                optionsBuilder.UseSqlServer(File.ReadAllText(Helpers.Util.ConnectionString("CONN_STR")));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StrConexao>(entity =>
            {
                entity.HasKey(e => e.Chave)
                    .HasName("PK__STR_CONE__B1BCC6AF0D1BFA59");

                entity.ToTable("STR_CONEXAO");

                entity.Property(e => e.Chave)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CHAVE");

                entity.Property(e => e.Valor)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("VALOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
