using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class QuestionAndAnswerOfAssessmentEtc : IEntityTypeConfiguration<QuestionAndAnswerOfAssessment>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<QuestionAndAnswerOfAssessment> builder)
        {
            builder.ToTable("QuestionsAndAnswersOfAssessment", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}