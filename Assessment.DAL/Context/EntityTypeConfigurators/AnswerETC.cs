using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class AnswerEtc : IEntityTypeConfiguration<Answer>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}