using System;
using System.Collections.Generic;
using System.Text;
using Assessment.DAL.Entities;

namespace Assessment.DAL.Tests.Factories
{
    public class AssessmentFactory
    {
        private int _assessmentId = 1;
        private uint _QuestionOfAssessment1Id = 1;
        private uint _QuestionOfAssessment2Id = 2;
        private int _AnswersOfAssessmentOwnerID = 1;
        private int _SubjectOfAssessmentOwnerID = 1;
        private int _AssessmentStateId = 0;
        private int _ownerId = 1;
        private string _feedback;


        private List<QuestionAndAnswerOfAssessment> _listQuestionOfAssessment = new List<QuestionAndAnswerOfAssessment>();

        public Entities.Assessment Build()
        {
            return new Entities.Assessment()
            {
                Id = _assessmentId,
                AssessmentIdentifier = Guid.NewGuid(),
                Feedback = _feedback, //"This is some feedback for the first assessment of the first test.",
                OwnerId = _ownerId,
                AssessmentStateId = _AssessmentStateId,
                ListOfQuestionAndAnswerOfAssessment = _listQuestionOfAssessment
            };
        }

        public AssessmentFactory WithAssessmentId(int assessmentId)
        {
            this._assessmentId = assessmentId;
            return this;
        }

        public AssessmentFactory WithAssessmentFeedback(string feedback)
        {
            this._feedback = feedback;
            return this;
        }
        public AssessmentFactory WithQuestionsOfAssessment(List<QuestionAndAnswerOfAssessmentFactory> list)
        {
            foreach (var item in list)
            {
                this._listQuestionOfAssessment.Add(item.Build());
            }
            return this;
        }

        public AssessmentFactory WithAssesmentOwnerId(int ownerId)
        {
            this._ownerId = ownerId;
            return this;
        }

        public AssessmentFactory WithAssessmentState(int AssessmentStateId)
        {
            this._AssessmentStateId = AssessmentStateId;
            return this;
        }
    }
}
