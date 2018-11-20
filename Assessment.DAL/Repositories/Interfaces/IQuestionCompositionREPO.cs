using Assessment.DAL.Entities;
using System.Collections.Generic;

namespace Assessment.DAL.Repositories.Interfaces
{
    public interface IQuestionCompositionREPO : IBaseRepo<QuestionComposition>
    {
        IEnumerable<QuestionComposition> GetAllQuestionCompositionByQuestionId(int questionId);
    }
}
