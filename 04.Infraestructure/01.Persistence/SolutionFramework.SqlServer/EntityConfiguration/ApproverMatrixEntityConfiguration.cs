using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.SqlServer.EntityConfiguration
{
    public class ApproverMatrixEntityConfiguration : IEntityTypeConfiguration<ApproverMatrix>
    {
        public void Configure(EntityTypeBuilder<ApproverMatrix> builder)
        {
            builder.ConfigurationBase<Guid, string, ApproverMatrix>();
            builder.ToTable("ApproverMatrix");

            builder.HasOne(x => x.Area)
                .WithMany(x => x.ApproverMatrix)
                .HasForeignKey(x => x.IdArea)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
