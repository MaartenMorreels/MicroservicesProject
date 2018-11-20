using System;
using System.Collections.Generic;
using System.Text;
using Assessment.DAL.Entities;
using Assessment.DAL.Helper;
using Assessment.DAL.Tests.Factories;

namespace Assessment.DAL.Tests
{
    public class QuestionFactory
    {
        private int _questionId = 1;
        private int _answerID = 1;
        private bool _correct = false;
        private int _ownerId = 2;
        private string _text1 = "This is a fist answer for the first question.";
        private string _text2 = "This is a second answer for the first question.";
        private int _aoa1Id=15;
        private int _aoa3Id = 130;
        private int _aoa2Id = 100;
        private int _questionaryId = 1;
        private int _questionAndAnswerOfAssesmentId = 1;
        private int _assesmentId = 22222;
        private List<Answer> _answers = new List<Answer>();
        private List<QuestionAndAnswerOfAssessment> _questionsOfAssessment = new List<QuestionAndAnswerOfAssessment>();

        public Question Build()
        {
            _aoa1Id = _aoa1Id + 1;
            _aoa2Id = _aoa2Id + 1;
            _aoa3Id = _aoa3Id + 1;
            return new Question()
            {
                Id = _questionId,
                AllowedTime = 15,
                QuestionDifficultyId = 31,
                QuestionTypeId = 3,
                QuestionPhrase = "This is a first question.",
                QuestionaryId = _questionaryId,
                Answers = _answers,
                QuestionAndAnswerOfAssessment = _questionsOfAssessment
            };
        }

        public QuestionFactory WithQuestionId(int questionId)
        {
            this._questionId = questionId;
            return this;
        }

        public QuestionFactory WithAnswers(List<Answer> answers)
        {
            this._answers = answers;
            return this;
        }

        public QuestionFactory withQuestionaryId(int questionaryId)
        {
            this._questionaryId = questionaryId;
            return this;
        }

        public QuestionFactory WithQuestionAndAnswerOfAssesmentId(int questionAndAnswerOfAssesmentId)
        {
            this._questionAndAnswerOfAssesmentId = questionAndAnswerOfAssesmentId;
            return this;
        }

        public  QuestionFactory withQuestionAndAnswerOFAssesment(List<QuestionAndAnswerOfAssessment> questionsOfAssessment)
        {
            this._questionsOfAssessment = questionsOfAssessment;
            return this;

        }
        
    }
}
