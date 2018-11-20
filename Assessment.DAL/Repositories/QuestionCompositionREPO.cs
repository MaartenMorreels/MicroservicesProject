using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class QuestionCompositionREPO : BaseRepo<QuestionComposition>, IQuestionCompositionREPO
    {
        #region Public Constructors

        public QuestionCompositionREPO(AssessmentContext context) : base(context)
        {
        }

        public IEnumerable<QuestionComposition> GetAllQuestionCompositionByQuestionId(int questionId)
        {
            return Context.Questions.Where(x => x.Id == questionId).Select(x=>x.QuestionComposition).FirstOrDefault();
        }

        #endregion Public Constructors
    }
}
