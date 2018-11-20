using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.DAL.Repositories
{
    public class AnswerRepo : BaseRepo<Answer>, IAnswerRepo
    {
        #region Public Constructors

        public AnswerRepo(AssessmentContext context) : base(context)
        {
        }

        public IEnumerable<Answer> GetAllAnswersByQuestionId(int questionId)
        {
            return Context.Answers.Where(x => x.QuestionId == questionId);
        }

        #endregion Public Constructors
    }
}