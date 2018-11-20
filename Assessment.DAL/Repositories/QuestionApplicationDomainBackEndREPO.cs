using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Repositories
{
    public class QuestionApplicationDomainBackEndRepo : BaseRepo<QuestionApplicationDomainBackEnd>, IQuestionApplicationDomainBackEndRepo
    {
        public QuestionApplicationDomainBackEndRepo(AssessmentContext context) : base(context)
        {
        }
    }
}
