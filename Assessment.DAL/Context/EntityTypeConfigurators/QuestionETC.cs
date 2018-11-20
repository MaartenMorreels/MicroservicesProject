using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class QuestionEtc : IEntityTypeConfiguration<QuestionComposition>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<QuestionComposition> builder)
        {
            builder.ToTable("Question",schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}