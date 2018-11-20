using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class QuestionsAndAnswersOfAssessmentRepo : BaseRepo<QuestionAndAnswerOfAssessment>, IQuestionsAndAnswersOfAssessmentREPO
    {
        #region Public Constructors

        public QuestionsAndAnswersOfAssessmentRepo(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}
