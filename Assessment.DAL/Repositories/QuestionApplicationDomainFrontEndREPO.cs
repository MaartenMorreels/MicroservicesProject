using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assessment.DAL.Repositories
{
    public class QuestionApplicationDomainFrontEndRepo : BaseRepo<QuestionApplicationDomainFrontEnd>, IQuestionApplicationDomainFrontEndRepo
    {
        #region Public Constructors
        public QuestionApplicationDomainFrontEndRepo(AssessmentContext context) : base(context)
        {
        }
        #endregion Public Constructors
    }
}
