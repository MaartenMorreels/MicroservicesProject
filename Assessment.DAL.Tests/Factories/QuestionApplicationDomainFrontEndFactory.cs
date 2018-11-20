using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionApplicationDomainFrontEndFactory
    {
        private int _questionApplicationDomainFrontEndId = 1;
        private int _questionCompositionId = 1;
        private int _applicationDomainFrontEndId = 1;

        public QuestionApplicationDomainFrontEnd Build()
        {
            _questionApplicationDomainFrontEndId = _questionApplicationDomainFrontEndId;
            return new QuestionApplicationDomainFrontEnd
            {
                Id = _questionApplicationDomainFrontEndId,
                QuestionCompositionId = _questionCompositionId,
                ApplicationDomainFrontEndId = _applicationDomainFrontEndId
            };
        }

        public QuestionApplicationDomainFrontEndFactory WithQuestionApplicationDomainFrontEndId(int questionApplicationDomainFrontEndId)
        {
            this._questionApplicationDomainFrontEndId = questionApplicationDomainFrontEndId;
            return this;
        }

        public QuestionApplicationDomainFrontEndFactory WithQuestionCompositionId(int questionCompositionId)
        {
            this._questionCompositionId = questionCompositionId;
            return this;
        }

        public QuestionApplicationDomainFrontEndFactory WithApplicationDomainFrontEndId(int applicationDomainFrontEndId)
        {
            this._applicationDomainFrontEndId = applicationDomainFrontEndId;
            return this;
        }
    }
}
