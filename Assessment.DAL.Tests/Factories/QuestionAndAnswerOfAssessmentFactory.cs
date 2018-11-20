using Assessment.DAL.Entities;

namespace Assessment.DAL.Tests.Factories
{
    public class QuestionAndAnswerOfAssessmentFactory
    {
        #region Private Fields

        private int _answerId = 1;
        private int _aoaId = 10;
        private Entities.Assessment _assesment = new Entities.Assessment();
        private int _assesmentId = 1;
        private int _ownerId = 1;
        private int _questionAndAnswerOfAssessmentId = 1;
        private int _questionId = 1;
        private Question _question = new Question();
        #endregion Private Fields

        #region Public Methods

        public QuestionAndAnswerOfAssessment Build()
        {
            return new QuestionAndAnswerOfAssessment()
            {
                Id = _questionAndAnswerOfAssessmentId,
                OwnerId = _ownerId,
                QuestionId = _questionId,
                Assessment = _assesment,
                AssessmentId = _assesmentId,
                AnswerId = _answerId,
                QuestionStateId = 0,
                Question = _question,
            };
        }

        public QuestionAndAnswerOfAssessmentFactory WithAnswerId(int answerId)
        {
            _answerId = answerId;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithAssesment(Entities.Assessment assesment)
        {
            _assesment = assesment;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithAssesmentID(int assesmentId)
        {
            _assesmentId = assesmentId;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithOwnerId(int ownerId)
        {
            _ownerId = ownerId;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithQuestionAndAnswerOfAssessmentId(int questionAndAnswerOfAssessmentId)
        {
            _questionAndAnswerOfAssessmentId = questionAndAnswerOfAssessmentId;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithQuestionId(int questionId)
        {
            _questionId = questionId;
            return this;
        }

        public QuestionAndAnswerOfAssessmentFactory WithQuestion(Question question)
        {
            _question = question;
            return this;
        }
        #endregion Public Methods
    }
}