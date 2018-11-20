using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class QuestionaryRepo : BaseRepo<Questionary>, IQuestionaryRepo
    {
        #region Public Constructors

        public QuestionaryRepo(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}