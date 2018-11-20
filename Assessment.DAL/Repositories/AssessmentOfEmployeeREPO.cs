using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class AssessmentOfEmployeeREPO : BaseRepo<AssessmentOfEmployee>, IAssessmentOfEmployeeREPO
    {
        #region Public Constructors

        public AssessmentOfEmployeeREPO(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}
