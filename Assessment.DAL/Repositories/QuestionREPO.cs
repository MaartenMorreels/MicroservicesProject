using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class QuestionRepo : BaseRepo<Question>, IQuestionRepo
    {
        #region Public Constructors

        public QuestionRepo(AssessmentContext context) : base(context)
        {
        }

        public IEnumerable<Question> GetAllQuestionsByQuestionaryId(int questionaryId)
        {
            return Context.Questions.Where(x => x.QuestionaryId == questionaryId);
        }

        #endregion Public Constructors
        
    }
}