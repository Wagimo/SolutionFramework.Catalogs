using Microsoft.EntityFrameworkCore;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.RegisterEntityConfigurations<SqlServerContext>();
        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<ApproverMatrix> AppoverMatrix { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ContractTemplate> ContractTemplate { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Duration> Duration { get; set; }
        public DbSet<DurationDetail> DurationDetail { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<HolyDay> HolyDay { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<OptionList> OptionList { get; set; }
        public DbSet<OptionListDetail> OptionListDetail { get; set; }
        public DbSet<Provider> Provider { get; set; }
    }
}
