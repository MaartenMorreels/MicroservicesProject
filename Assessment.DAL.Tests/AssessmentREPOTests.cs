using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class AssessmentRepoTests : AssessmentCoreTest
    {
        #region Private Fields

        // Repos
        private AssessmentRepo _assessmentRepo;

        // Ents
        private Entities.Assessment _assessment;
        private Entities.Assessment _assessmentAdded;
        private Entities.Assessment _assessmentInDb;
        private Entities.Assessment _assessmentToUpdate;

        // Lists
        private List<Entities.Assessment> _assessments = new List<Entities.Assessment>();

        //Ids
        private const int AssesmentID1 = 1;
        private const string AssesmentFeedback1 = "This is some test feedback for the first assessment.";
        private const int AssesmentOwnerID1 = 1;
        //Question1
        private const int AssesmentQuestionOwnerId1 = 1;
        //Question2
        private const int AssesmentQuestionOwnerId2 = 1;


        private const int AssesmentID2 = 2;
        private const string AssesmentFeedback2 = "This is some test feedback for the second assessment.";

        //NewAssesment
        private const int NewAssesmentID = 3;
        private const int NewAssesmentQuestionAnswerOwnerId = 1;

        //update
        private const string UpdateAssesmentFeedback = "This is the altered feedback for the assessment.";

        private int AssesmentAssessmentState0 = 0;
        private int AssesmentAssessmentState1 = 1;

        private const int Question1Id = 1;
        private const int Question2Id = 2;
        private const int Question3Id = 3;
        private const int AnswerId1 = 10;
        private const bool Correct1 = true;
        private const string AnswerText1 = "This is a first answer in the list.";
        private const int AnswerId2 = 20;
        private const bool Correct2 = false;
        private const string AnswerText2 = "This is a second answer in the list.";
        private const int AnswerId3 = 30;
        private const bool Correct3 = false;
        private const string AnswerText3 = "This is a thirt answer in the list.";
        private const int AnswerId4 = 40;
        private const bool Correct4 = true;
        private const string AnswerText4 = "This is a simple answer in the list.";

        #endregion Private Fields

        #region Public Constructors

        public override void Init()
        {
            var _questionaries = new List<Questionary>();

            var _answers = new List<Answer>()
            {
                new AnswerFactory().WithAnswerId(AnswerId1).WithAnswerCorrect(Correct1).WithText(AnswerText1).Build(),
                new AnswerFactory().WithAnswerId(AnswerId2).WithAnswerCorrect(Correct2).WithText(AnswerText2).Build()
            };
            var _answers2 = new List<Answer>()
            {
                new AnswerFactory().WithAnswerId(AnswerId3).WithAnswerCorrect(Correct3).WithText(AnswerText3).Build(),
                new AnswerFactory().WithAnswerId(AnswerId4).WithAnswerCorrect(Correct4).WithText(AnswerText4).Build()
            };


            var testAssessment = new AssessmentFactory().WithAssessmentId(AssesmentID1).WithAssessmentFeedback(AssesmentFeedback1).WithAssesmentOwnerId(AssesmentOwnerID1).WithAssessmentState(AssesmentAssessmentState0).Build();
            var testAssessment2 = new AssessmentFactory().WithAssessmentId(AssesmentID2).WithAssessmentFeedback(AssesmentFeedback2).WithAssesmentOwnerId(AssesmentOwnerID1).WithAssessmentState(AssesmentAssessmentState0).Build();

            _assessments.Add(testAssessment);
            _assessments.Add(testAssessment2);

            var QuestionsOfAssessment1 = new List<QuestionAndAnswerOfAssessment>()
            {
                new QuestionAndAnswerOfAssessmentFactory()
                    .WithAnswerId(AnswerId1)
                    .WithQuestionAndAnswerOfAssessmentId(124)
                    .WithOwnerId(AssesmentQuestionOwnerId1)
                    .WithAssesment(testAssessment).Build()

            };

            var QuestionsOfAssessment2 = new List<QuestionAndAnswerOfAssessment>()
            {
                new QuestionAndAnswerOfAssessmentFactory()
                    .WithAnswerId(AnswerId3)
                    .WithQuestionAndAnswerOfAssessmentId(1245)
                    .WithOwnerId(AssesmentQuestionOwnerId2)
                    .WithAssesment(testAssessment2).Build()
            };

            var _questions = new List<Question>()
            {
                new QuestionFactory().WithQuestionId(Question1Id).WithAnswers(_answers2).withQuestionAndAnswerOFAssesment(QuestionsOfAssessment2).Build(),
                new QuestionFactory().WithQuestionId(Question2Id).WithAnswers(_answers).withQuestionAndAnswerOFAssesment(QuestionsOfAssessment1).Build(),
                new QuestionFactory().WithQuestionId(Question3Id).Build(),
            };


            //New
            List<QuestionAndAnswerOfAssessmentFactory> NewQuestionsOfAssessment = new List<QuestionAndAnswerOfAssessmentFactory>()
            {
                new QuestionAndAnswerOfAssessmentFactory()
                    .WithAnswerId(AnswerId1)
                    .WithQuestion(new Question())
                    .WithOwnerId(NewAssesmentQuestionAnswerOwnerId)
            };


            _questionaries = new List<Questionary>()
            {
                new QuestionaryFactory().WithQuestionaryId(13).WithQuestions(_questions).Build(),
            };


            _assessment = new AssessmentFactory().WithAssessmentId(NewAssesmentID)
                .WithQuestionsOfAssessment(NewQuestionsOfAssessment).Build();
            InitializeDbSet(_questionaries);
        }

        public AssessmentRepoTests()
        {

        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void AnAssessmentCanBeAddedCorrectlyToTheDb()
        {

            _assessmentRepo = new AssessmentRepo(Context);
            _assessmentRepo.Add(_assessment);

            _assessmentInDb = Context.Assessments.First(x => x.Id == NewAssesmentID);

            #region Asserts

            // Compare some basic properties for this assessment object
            Assert.Equal(_assessmentInDb.Feedback, _assessment.Feedback);
            Assert.Equal(_assessmentInDb.OwnerId, _assessment.OwnerId);
            Assert.Equal(_assessmentInDb.AssessmentStateId, _assessment.AssessmentStateId);
            Assert.Equal(_assessmentInDb.Id, _assessment.Id);

            // Compare all properties for the QuestionsOfAssessment list for this assessment object
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.Count() is 1);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId == _assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId == _assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId == _assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().Id == _assessment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().Id);

            #endregion Asserts
        }

        [Fact]
        public void AnAssessmentAddedToTheDb_CanBeRetrievedFromTheDbById()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            //_assessmentInDb = _assessmentRepo.Find(x => x.Id == AssesmentID1).FirstOrDefault();
            _assessmentInDb = Context.Assessments.Where(x => x.Id == AssesmentID1).First();

            var origin = _assessments.Find(x => x.Id == AssesmentID1);

            #region Asserts

            // Compare some basic properties for this assessment object
            Assert.Equal(_assessmentInDb.Feedback, origin.Feedback);
            Assert.Equal(_assessmentInDb.OwnerId, origin.OwnerId);
            Assert.Equal(_assessmentInDb.AssessmentStateId, origin.AssessmentStateId);

            // Compare all properties for the QuestionsOfAssessment list for this assessment object
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.Count() is 1);
            Assert.Equal(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId, origin.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId);
            Assert.Equal(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId, origin.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId);
            Assert.Equal(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId, origin.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId);

            #endregion Asserts

        }

        [Fact]
        public void AnAssessmentCanBeUpdated_AndTheChangesAreAddedCorrectlyToTheDb()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            _assessmentToUpdate = Context.Assessments.First(x => x.Id == AssesmentID1);

            _assessmentToUpdate.Feedback = UpdateAssesmentFeedback;

            _assessmentRepo.Update(_assessmentToUpdate);
            Context.SaveChanges();

            _assessmentInDb = Context.Assessments.First(x => x.Id == AssesmentID1);
            var assesment = _assessments.First(x => x.Id == AssesmentID1);

            #region Asserts

            // Compare some basic properties for this assessment object
            Assert.Equal(_assessmentInDb.Feedback, assesment.Feedback);
            Assert.Equal(_assessmentInDb.OwnerId, assesment.OwnerId);
            Assert.Equal(_assessmentInDb.AssessmentStateId, assesment.AssessmentStateId);

            // Compare all properties for the QuestionsOfAssessment list for this assessment object
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.Count() is 1);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId == assesment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().OwnerId);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId == assesment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionId);
            Assert.True(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId == assesment.ListOfQuestionAndAnswerOfAssessment.FirstOrDefault().QuestionStateId);

            #endregion Asserts

        }

        [Fact]
        public void WhenIAskForAListOfAllAssessments_IRecieveTheListIExpect()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            IEnumerable<Entities.Assessment> AllAssesments = _assessmentRepo.GetAll();

            #region Asserts
            Assert.Equal(AllAssesments.Count(), 2);
            #endregion            
        }

        [Fact]
        public void WhenIAskToFindAllAssessmentsWhichMeetTheGivenCriteria_IReceiveTheListIExpect()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            List<Entities.Assessment> assessmentInDb_FeedbackContains_First = _assessmentRepo.Find(a => a.Feedback.Contains("first")).ToList();
            List<Entities.Assessment> assessment_FeedbackContains_First = _assessments.FindAll(a => a.Feedback.Contains("first"));
            // List of 1
            // Condition 2: OwnerId = 1
            List<Entities.Assessment> assessmentInDb_WithOwnerId1 = _assessmentRepo.Find(a => a.OwnerId is AssesmentOwnerID1).ToList();
            List<Entities.Assessment> assessment_WithOwnerId1 = _assessments.FindAll(a => a.OwnerId is AssesmentOwnerID1).ToList();
            // List of 1
            // Condition 3: AssessmentState = 0
            List<Entities.Assessment> assessmentInDb_WithAssessmentState0 = _assessmentRepo.Find(a => a.AssessmentStateId is 0).ToList();
            List<Entities.Assessment> assessment_WithAssessmentState0 = _assessments.FindAll(a => a.AssessmentStateId is 0).ToList();
            // List of 2
            // Condition 4: QuestionsOfAssessment.Count() > 1
            List<Entities.Assessment> assessmentInDb_CountOfQuestionsOfAssessmentIs1 = _assessmentRepo.Find(a => a.ListOfQuestionAndAnswerOfAssessment.Count() == 1).ToList(); // List of 1
            List<Entities.Assessment> assessment_CountOfQuestionsOfAssessmentIs1 = _assessments.FindAll(a => a.ListOfQuestionAndAnswerOfAssessment.Count() == 1).ToList(); // List of 1


            #region Asserts

            // Check condition 1
            Assert.True(assessmentInDb_FeedbackContains_First.Count is 1);
            Assert.Contains(assessmentInDb_FeedbackContains_First.FirstOrDefault().Feedback, assessment_FeedbackContains_First.FirstOrDefault().Feedback);
            Assert.True(assessmentInDb_FeedbackContains_First.FirstOrDefault().OwnerId == assessment_FeedbackContains_First.FirstOrDefault().OwnerId);
            Assert.True(assessmentInDb_FeedbackContains_First.FirstOrDefault().AssessmentStateId == assessment_FeedbackContains_First.FirstOrDefault().AssessmentStateId);
            Assert.True(assessmentInDb_FeedbackContains_First.FirstOrDefault().ListOfQuestionAndAnswerOfAssessment.Count() is 1);

            // Check condition 2
            Assert.True(assessmentInDb_WithOwnerId1.Count is 2);
            Assert.Contains(assessmentInDb_WithOwnerId1.FirstOrDefault().Feedback, assessment_WithOwnerId1.FirstOrDefault().Feedback);
            Assert.True(assessmentInDb_WithOwnerId1.FirstOrDefault().ListOfQuestionAndAnswerOfAssessment.Count() is 1);

            // Check condition 3
            Assert.True(assessmentInDb_WithAssessmentState0.Count is 2);
            Assert.True(assessmentInDb_WithAssessmentState0.FirstOrDefault().AssessmentStateId == assessment_WithAssessmentState0.FirstOrDefault().AssessmentStateId);
            Assert.True(assessmentInDb_WithAssessmentState0.LastOrDefault().AssessmentStateId == assessment_WithAssessmentState0.LastOrDefault().AssessmentStateId);
            Assert.Contains(assessmentInDb_WithAssessmentState0.FirstOrDefault().Feedback, assessment_WithAssessmentState0.FirstOrDefault().Feedback);
            Assert.Contains(assessmentInDb_WithAssessmentState0.LastOrDefault().Feedback, assessment_WithAssessmentState0.LastOrDefault().Feedback);

            // Check condition 4
            Assert.True(assessmentInDb_CountOfQuestionsOfAssessmentIs1.Count is 2);
            Assert.Contains(assessmentInDb_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().Feedback, assessment_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().Feedback);
            Assert.True(assessmentInDb_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().OwnerId == assessment_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().OwnerId);
            Assert.True(assessmentInDb_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().AssessmentStateId == assessment_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().AssessmentStateId);
            Assert.True(assessmentInDb_CountOfQuestionsOfAssessmentIs1.FirstOrDefault().ListOfQuestionAndAnswerOfAssessment.Count() is 1);

            #endregion Asserts

        }

        [Fact]
        public void WhenIRetrieveAnAssessmentWithChildsInDbThatIsNotNull_TheChildsCanNotBeNull()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            _assessmentInDb = _assessmentRepo.Find(a => a.Id == AssesmentID1).First();

            #region Asserts

            // Check if childs are not null
            Assert.NotNull(_assessmentInDb);
            Assert.NotNull(_assessmentInDb.ListOfQuestionAndAnswerOfAssessment);

            #endregion Asserts

        }

        [Fact]
        public void WhenIAskToFindAQuestionWithMeetTheGivinCriteria_IRecieveAQuestion()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var question = Context.Questions.Where(x => x.Id == Question1Id);

            #region Asserts
            Assert.Equal(question.FirstOrDefault().Answers.Count(), 2);
            Assert.Equal(question.FirstOrDefault().Answers.FirstOrDefault().Id, AnswerId3);
            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAQuestionWithMeetTheGivinCriteria_IRecieveAQuestionWithNoAnswers()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var question = Context.Questions.Where(x => x.Id == Question3Id);

            #region Asserts
            Assert.Equal(question.FirstOrDefault().Answers.Count(), 0);
            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAAnswerWithMeetTheGivingCriteria_IRecieveAAnswer()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var answer = Context.Answers.Where(x => x.Id == AnswerId1);

            #region Asserts
            Assert.Equal(answer.FirstOrDefault().Id, AnswerId1);
            Assert.Equal(answer.FirstOrDefault().Text, AnswerText1);
            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAAnswerWithMeetTheGivingCriteria_IRecieveAAnswerWithAQuestion()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var answer = Context.Answers.Where(x => x.Id == AnswerId1);

            #region Asserts
            Assert.Equal(answer.FirstOrDefault().Id, AnswerId1);
            Assert.Equal(answer.FirstOrDefault().Text, AnswerText1);
            Assert.Equal(answer.FirstOrDefault().QuestionId, Question2Id);
            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAAssesmentWithASpecificAnswerId_IWantRecieveAAnswerThatIsCorrect()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var assesment = Context.QuestionsAndAnswersOfAssessment.Where(x => x.AssessmentId == AssesmentID1);

            #region Asserts
            
            Assert.Equal(assesment.FirstOrDefault().Question.Answers.Where(x => x.Id == AnswerId1).FirstOrDefault().Correct, true);

            #endregion Asserts
        }
        [Fact]
        public void WhenIAskToFindAAssesmentWithASpecificAnswerId_IWantRecieveAAnswerThatIsNotCorrect()
        {
            _assessmentRepo = new AssessmentRepo(Context);
            var assesment = Context.QuestionsAndAnswersOfAssessment.Where(x => x.AssessmentId == AssesmentID2);

            #region Asserts

            Assert.Equal(assesment.FirstOrDefault().Question.Answers.Where(x => x.Id == AnswerId3).FirstOrDefault().Correct, false);

            #endregion Asserts
        }

        #endregion Public Methods


    }
}