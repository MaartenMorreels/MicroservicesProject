using System.Collections.Generic;
using Assessment.DAL.Entities;

namespace Assessment.DAL.Repositories.Interfaces
{
    public interface IQuestionRepo : IBaseRepo<Question>
    {
        IEnumerable<Question> GetAllQuestionsByQuestionaryId(int questionaryId);
    }
}