using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionApplicationLanguageFactory
    {
        private int _questionApplicationLanguageId = 1;
        private int _questionCompositionId = 1;
        private int _applicationLanguageId = 1;

        public QuestionApplicationLanguage Build()
        {
            return new QuestionApplicationLanguage()
            {
                Id = _questionApplicationLanguageId,
                QuestionCompositionId = _questionCompositionId,
                ApplicationLanguageId = _applicationLanguageId

            };
        }
        public QuestionApplicationLanguageFactory WithQuestionApplicationLanguageId(int questionApplicationLanguageId)
        {
            this._questionApplicationLanguageId = questionApplicationLanguageId;
            return this;
        }
        public QuestionApplicationLanguageFactory WithQuestionCompositionId(int questioncompositionId)
        {
            this._questionCompositionId = questioncompositionId;
            return this;
        }
        public QuestionApplicationLanguageFactory WithApplicationLanguageId(int applicationLanguageId)
        {
            this._applicationLanguageId = applicationLanguageId;
            return this;
        }

    }
}
