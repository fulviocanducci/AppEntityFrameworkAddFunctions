using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using System;
namespace AppEntityFrameworkAddFunctions.Models
{
    public sealed class Database: DbContext
    {
        public DbSet<People> People { get; set; }

        public Database()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SqlExpress;Database=DataBase;User Id=sa;Password=senha;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region People_Configuration
            modelBuilder.Entity<People>(o =>
            {
                o.ToTable("People");

                o.HasKey(x => x.Id);
                o.Property(x => x.Id)
                    .UseSqlServerIdentityColumn();

                o.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                o.Property(x => x.Created)
                    .IsRequired();

                o.Property(x => x.IsActive)
                    .IsRequired();                    
            });
            #endregion

            #region Functions
            modelBuilder.HasDbFunction(typeof(Functions).GetMethod(nameof(Functions.Day)))
                .HasName(nameof(Functions.Day))
                .HasTranslation(args =>
                {
                    return new SqlFunctionExpression("DAY", typeof(int), args);
                });
            modelBuilder.HasDbFunction(typeof(Functions).GetMethod(nameof(Functions.Month)))
                .HasName(nameof(Functions.Month))
                .HasTranslation(args =>
                {
                    return new SqlFunctionExpression("MONTH", typeof(int), args);
                });
            modelBuilder.HasDbFunction(typeof(Functions).GetMethod(nameof(Functions.Year)))
                .HasName(nameof(Functions.Year))
                .HasTranslation(args =>
                {
                    return new SqlFunctionExpression("YEAR", typeof(int), args);
                });

            modelBuilder.HasDbFunction(typeof(Functions).GetMethod(nameof(Functions.GetDate)))
                .HasName(nameof(Functions.GetDate))
                .HasTranslation(args =>
                {
                    return new SqlFunctionExpression("GETDATE", typeof(DateTime), args);
                });

            modelBuilder.HasDbFunction(typeof(Functions).GetMethod(nameof(Functions.ShortDate)))
                .HasName(nameof(Functions.ShortDate))                
                .HasTranslation(args =>
                {
                    return new SqlFunctionExpression("DBO.SHORTDATE", typeof(DateTime), args);
                });
            #endregion
        }
    }
}
