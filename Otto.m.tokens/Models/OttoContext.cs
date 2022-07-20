using Microsoft.EntityFrameworkCore;

namespace Otto.m.tokens.Models
{
    public partial class OttoContext : DbContext
    {
        public OttoContext()
        {
        }

        public OttoContext(DbContextOptions<OttoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MToken> MTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var server = Environment.GetEnvironmentVariable("SERVER");
                var db = Environment.GetEnvironmentVariable("DB");
                var user = Environment.GetEnvironmentVariable("USER");
                var pass = Environment.GetEnvironmentVariable("PASS");

                Console.WriteLine($"SERVER: {server}");
                Console.WriteLine($"DB: {db}");
                Console.WriteLine($"USER: {user}");
                Console.WriteLine($"PASS: {pass}");

                var connectionString = $"Server={server};Database={db};User Id={user};Password={pass};";


                optionsBuilder.UseSqlServer(connectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MToken>(entity =>
            {
                entity.ToTable("m_tokens");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(100)
                    .HasColumnName("access_token");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.ExpiresAt)
                    .HasColumnType("datetime")
                    .HasColumnName("expires_at");

                entity.Property(e => e.MUserId).HasColumnName("m_user_id");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasColumnName("modified");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(50)
                    .HasColumnName("refresh_token");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type")
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
