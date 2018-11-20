using System;
using System.Collections.Generic;
using System.Text;
using Assessment.DAL.Entities;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionaryFactory
    {
        private int _questionaryId = 1;
        private int _questionId = 1;
        private int _questionAndAnswerOfAssesmentId = 1;
        private string _description = "This is a first questionary.";
        private List<Question> _questions = new List<Question>();


        public Questionary Build()
        {
            _questionId = _questionId + 1;
            return new Questionary()
            {
                Id = _questionaryId,
                Description = _description,
                Questions = _questions
            };
        }

        public QuestionaryFactory WithQuestionaryId(int questionaryId)
        {
            this._questionaryId = questionaryId;
            return this;
        }

        public QuestionaryFactory WithDescription(string description)
        {
            this._description = description;
            return this;
        }
        

        public QuestionaryFactory WithQuestionId(int questionId)
        {
            this._questionId = questionId;
            return this;
        }

        public QuestionaryFactory WithQuestionAndAnwserOfAssesmentId(int questionAndAnswerOfAssesmentId)
        {
            this._questionAndAnswerOfAssesmentId = questionAndAnswerOfAssesmentId;
            return this;
        }

        public QuestionaryFactory WithQuestions(List<Question> questions)
        {
            this._questions = questions;
            return this;
        }
    }
}
