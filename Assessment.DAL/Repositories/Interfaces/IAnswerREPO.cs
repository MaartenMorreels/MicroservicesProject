using Assessment.DAL.Entities;
using System.Collections.Generic;

namespace Assessment.DAL.Repositories.Interfaces
{
    public interface IAnswerRepo : IBaseRepo<Answer>
    {
        IEnumerable<Answer> GetAllAnswersByQuestionId(int questionId);
    }
}