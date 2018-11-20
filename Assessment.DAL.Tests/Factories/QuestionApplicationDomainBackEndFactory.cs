using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionApplicationDomainBackEndFactory
    {
        private int _questionApplicationDomainBackEndId = 1;
        private int _questionCompositionId = 1;
        private int _applicationDomainBackEndId = 1;

        public QuestionApplicationDomainBackEnd Build()
        {
            _questionApplicationDomainBackEndId = _questionApplicationDomainBackEndId ;
            return new QuestionApplicationDomainBackEnd
            {
                Id = _questionApplicationDomainBackEndId,
                QuestionCompositionId = _questionCompositionId,
                ApplicationDomainBackEndId = _applicationDomainBackEndId
            };
        }

        public QuestionApplicationDomainBackEndFactory WithQuestionApplicationDomainBackEndId(int questionApplicationDomainBackEndId)
        {
            this._questionApplicationDomainBackEndId = questionApplicationDomainBackEndId;
            return this;
        }

        public QuestionApplicationDomainBackEndFactory WithQuestionCompositionId(int questionCompositionId)
        {
            this._questionCompositionId = questionCompositionId;
            return this;
        }

        public QuestionApplicationDomainBackEndFactory WithApplicationDomainBackEndId(int applicationDomainBackEndId)
        {
            this._applicationDomainBackEndId = applicationDomainBackEndId;
            return this;
        }

    }
}
