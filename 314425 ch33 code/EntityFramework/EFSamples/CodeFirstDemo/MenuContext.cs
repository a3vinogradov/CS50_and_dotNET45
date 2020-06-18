using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo
{
  public class MenuContext : DbContext
  {
    private const string connectionString = @"server=dark\sqlexpress;database=WroxMenus;trusted_connection=true";
    public MenuContext()
      : base(connectionString)
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Menu>().Property(m => m.Price).HasColumnType("money");
      modelBuilder.Entity<Menu>().Property(m => m.Day).HasColumnType("date");
      modelBuilder.Entity<Menu>().Property(m => m.Text).HasMaxLength(40).IsRequired();
      modelBuilder.Entity<Menu>().HasRequired(m => m.MenuCard).WithMany(c => c.Menus).HasForeignKey(m => m.MenuCardId);
      modelBuilder.Entity<MenuCard>().Property(c => c.Text).HasMaxLength(30).IsRequired();
      modelBuilder.Entity<MenuCard>().HasMany(c => c.Menus).WithRequired().WillCascadeOnDelete();
    }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuCard> MenuCards { get; set; }
  }
}
