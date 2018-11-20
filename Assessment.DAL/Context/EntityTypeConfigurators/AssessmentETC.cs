using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class AssessmentEtc : IEntityTypeConfiguration<Entities.Assessment>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Entities.Assessment> builder)
        {
            builder.ToTable("Assessment", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}