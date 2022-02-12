using Microsoft.EntityFrameworkCore;
using SalesWebMvc.ViewModels;
# nullable disable
namespace SalesWebMvc.Data;
public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Department { get; set; }
    public DbSet<Seller> Seller { get; set; }
    public DbSet<SalesRecord> SalesRecord { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Department
        modelBuilder.Entity<Department>(b =>
        {
            //Columns
            b.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn); 
            b.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();

            //Keys
            b.HasKey(x => x.Id);

            //Table Name
            b.ToTable("Department");

        });
        // Seller
        modelBuilder.Entity<Seller>(b =>
        {
            //Columns
            b.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            b.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            b.Property(x => x.Email).HasColumnType("nvarchar(50)").IsRequired();
            b.Property(x => x.BirthDate).HasColumnType("datetime2").IsRequired();
            b.Property(x => x.BaseSalary).HasColumnType("decimal").IsRequired();
            b.Property(x => x.DepartmentId).HasColumnType("int").IsRequired();

            //Keys
            b.HasKey(x => x.Id);

            //Table Name
            b.ToTable("Seller");

            //Relations
            b.HasOne(x => x.Department).WithMany(x => x.Sellers).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.ClientSetNull);


        });
        // SalesRecord
        modelBuilder.Entity<SalesRecord>(b =>
        {
            //Columns
            b.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn); 
            b.Property(x => x.Date).HasColumnType("datetime2").IsRequired();
            b.Property(x => x.Amount).HasColumnType("decimal").IsRequired();
            b.Property(x => x.Status).HasColumnType("int").IsRequired();
            b.Property(x => x.SellerId).HasColumnType("int").IsRequired();

            //Keys
            b.HasKey(x => x.Id);

            //Table Name
            b.ToTable("SalesRecord");

            //Relations
            b.HasOne(x => x.Seller).WithMany(x => x.Sales).HasForeignKey(x => x.SellerId).OnDelete(DeleteBehavior.ClientSetNull);


        });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<SalesWebMvc.ViewModels.SalesRecordViewModel> SalesRecordViewModel { get; set; }

    public DbSet<SalesWebMvc.ViewModels.DepartmentViewModel> DepartmentsViewModel { get; set; }
}
