using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Repositories
{
    public class QuestionApplicationLanguageREPO : BaseRepo<QuestionApplicationLanguage>,IQuestionApplicationLanguageREPO
    {

        #region Public Constructors
        public QuestionApplicationLanguageREPO(AssessmentContext context) : base(context)
        {
        }

        #endregion Public Constructors
    }
}
