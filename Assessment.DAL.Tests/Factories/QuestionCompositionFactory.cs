using System;
using Assessment.DAL.Entities;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionCompositionFactory
    {
        private int _questionCompositionId = 1;
        private int _applicationBackEndId = 1;
        private int _applicationFrontEndId = 1;
        private int _applicationLanguageId = 1;

        public QuestionComposition Build()
        {
            _questionCompositionId = _questionCompositionId;
            return new QuestionComposition()
            {
                Id = _questionCompositionId,
                ApplicationBackEndId = _applicationBackEndId,
                ApplicationFrontEndId = _applicationFrontEndId,
                ApplicationLanguageId = _applicationLanguageId
            };
        }

        public QuestionCompositionFactory WithQuestionCompositionId(int questioncompositionId)
        {
            this._questionCompositionId = questioncompositionId;
            return this;
        }

        public QuestionCompositionFactory WithApplicationLanguageId(int applicationLanguageId)
        {
            this._applicationLanguageId = applicationLanguageId;
            return this;
        }
        public QuestionCompositionFactory WithApplicationFrontEndId(int applicationFrontEndId)
        {
            this._applicationFrontEndId = applicationFrontEndId;
            return this;
        }
        public QuestionCompositionFactory WithApplicationBackEndId(int applicationBackEndId)
        {
            this._applicationBackEndId = applicationBackEndId;
            return this;
        }
    }
}
