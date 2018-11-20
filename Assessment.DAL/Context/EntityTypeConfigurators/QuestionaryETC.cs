using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class QuestionaryEtc : IEntityTypeConfiguration<Questionary>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Questionary> builder)
        {
            builder.ToTable("Questionary", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
