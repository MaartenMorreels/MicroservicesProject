using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;

namespace Assessment.DAL.Repositories
{
    public class AssessmentOfCandidateREPO: BaseRepo<AssessmentOfCandidate>,IAssessmentOfCandidateRepo
    {
        #region Public Constructors

        public AssessmentOfCandidateREPO(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}
