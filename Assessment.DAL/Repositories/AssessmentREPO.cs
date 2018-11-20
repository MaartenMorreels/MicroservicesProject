using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class AssessmentRepo : BaseRepo<Entities.Assessment>, IAssessmentRepo
    {
        #region Public Constructors

        public AssessmentRepo(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}