using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Entities;
using Assessment.DAL.Helper;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class QuestionRepoTests : AssessmentCoreTest
    {
        #region Private Fields

        // Repos
        private QuestionRepo _questionRepo;

        // Ents
        private Question _question;
        private Question _questionInDb;
        private Question _questionToUpdate;

        // Lists
        private List<Question> _questions1;
        private List<Question> _questions2;
        private List<Question> _questions = new List<Question>();
        private List<Questionary> _questionaries;

        // Ids
        private const int Question1Id = 1;
        private const int Question2Id = 2;
        private const int Question3Id = 3;
        private const int NewQuestionId = 4;

        #endregion Private Fields

        #region Public Methods

        public override void Init()
        {
            _questionaries = new List<Questionary>();

            var questionary1 = new QuestionaryFactory().WithQuestionaryId(1);
            var questionary2 = new QuestionaryFactory().WithQuestionaryId(2);

            _question = new QuestionFactory().WithQuestionId(NewQuestionId).Build();

            _questions1 = new List<Question>()
            {
                new QuestionFactory().WithQuestionId(Question1Id).withQuestionaryId(1).Build(),
                new QuestionFactory().WithQuestionId(Question2Id).withQuestionaryId(1).Build()
            };

            questionary1.WithQuestions(_questions1).Build();

            _questions2 = new List<Question>()
            {
                new QuestionFactory().WithQuestionId(Question3Id).withQuestionaryId(2).Build()
            };

            questionary2.WithQuestions(_questions2).Build();

            _questionaries.Add(questionary1.Build());
            _questionaries.Add(questionary2.Build());

            _questions.AddRange(_questions1);
            _questions.AddRange(_questions2);

            InitializeDbSet(_questionaries);
        }

        [Fact]
        public void AQuestionCanBeRetrievedById()
        {
            _questionRepo = new QuestionRepo(Context);

            _questionInDb = Context.Questions.FirstOrDefault(x => x.Id == Question1Id);

            Question expectedQuestion = _questions.Find(x => x.Id == Question1Id);

            #region Asserts
            Assert.Equal(expectedQuestion.AllowedTime, _questionInDb.AllowedTime);
            Assert.Equal(expectedQuestion.QuestionDifficultyId, _questionInDb.QuestionDifficultyId);
            Assert.Equal(expectedQuestion.QuestionTypeId, _questionInDb.QuestionTypeId);
            Assert.Equal(expectedQuestion.QuestionaryId, _questionInDb.QuestionaryId);
            Assert.Equal(expectedQuestion.QuestionPhrase, _questionInDb.QuestionPhrase);

            #endregion
        }

        [Fact]
        public void AQuestionIsAddedCorrectlyToDb_AndCanBeRetrieved()
        {
            // Create new question repository with this context
            _questionRepo = new QuestionRepo(Context);

            // Add this question object to the DB
            _questionRepo.Add(_question);
            Context.SaveChanges();

            // Retrieve the full question object in the DB
            _questionInDb = Context.Questions.First(x => x.Id == NewQuestionId);

            #region Asserts

            // Compare some basic properties for this question object
            Assert.Equal(_question.AllowedTime, _questionInDb.AllowedTime);
            Assert.Equal(_question.QuestionDifficultyId, _questionInDb.QuestionDifficultyId);
            Assert.Equal(_question.QuestionTypeId, _questionInDb.QuestionTypeId);
            Assert.Equal(_question.QuestionaryId, _questionInDb.QuestionaryId);
            Assert.Equal(_question.QuestionPhrase, _questionInDb.QuestionPhrase);

            #endregion Asserts
        }

        [Fact]
        public void WhenIUpdateAQuestion_TheChangesAreAddedCorrectlyToTheDb()
        {
            var QuestionPhraseToBeUpdate = "This text has been altered.";
            // Create new question repository with this context
            _questionRepo = new QuestionRepo(Context);

            // Retrieve the full question object in the DB
            _questionToUpdate = Context.Questions.First(x => x.Id == Question1Id);

            // Alter property for this retrieved object
            _questionToUpdate.QuestionPhrase = QuestionPhraseToBeUpdate;

            // Update/save changes in DB for this object
            _questionRepo.Update(_questionToUpdate);
            Context.SaveChanges();

            // Retrieve updated object in DB
            _questionInDb = Context.Questions.First(x => x.Id == Question1Id);

            #region Asserts

            // Compare some basic properties for this question object
            Assert.Equal(_questionToUpdate.AllowedTime, _questionInDb.AllowedTime);
            Assert.Equal(_questionToUpdate.QuestionDifficultyId, _questionInDb.QuestionDifficultyId);
            Assert.Equal(_questionToUpdate.QuestionTypeId, _questionInDb.QuestionTypeId);
            Assert.Equal(_questionToUpdate.QuestionaryId, _questionInDb.QuestionaryId);
            Assert.Equal(QuestionPhraseToBeUpdate, _questionInDb.QuestionPhrase);

            #endregion Asserts
        }

        [Fact]
        public void WhenIAskForAListOfAllQuestions_IReceiveTheListIExpect()
        {
            // Create new question repository with this context
            _questionRepo = new QuestionRepo(Context);

            List<Question> questionsInDbEnts = Context.Questions.ToList();
            // Retrieve all questions in DB
            List<Question> questionsRepo = _questionRepo.GetAll().ToList();

            // Get first en last question in list retrieved from DB
            Question firstOfDb = questionsRepo.FirstOrDefault();
            Question lastOfDb = questionsRepo.LastOrDefault();
            var counterOfDb = questionsRepo.Count();

            // Get first en last question in original list
            Question firstOfOriginal = questionsInDbEnts.FirstOrDefault();
            Question lastOfOriginal = questionsInDbEnts.LastOrDefault();
            var counterOfOriginal = questionsInDbEnts.Count();

            #region Asserts

            // Compare some basic properties for the first and last question in the lists
            // #1
            Assert.Equal(firstOfDb.AllowedTime, firstOfOriginal.AllowedTime);
            Assert.Equal(firstOfDb.QuestionDifficultyId, firstOfOriginal.QuestionDifficultyId);
            Assert.Equal(firstOfDb.QuestionTypeId, firstOfOriginal.QuestionTypeId);
            Assert.Equal(firstOfDb.QuestionaryId, firstOfOriginal.QuestionaryId);
            Assert.Equal(firstOfDb.QuestionPhrase, firstOfOriginal.QuestionPhrase);

            // #2
            Assert.Equal(lastOfDb.AllowedTime, lastOfOriginal.AllowedTime);
            Assert.Equal(lastOfDb.QuestionDifficultyId, lastOfOriginal.QuestionDifficultyId);
            Assert.Equal(lastOfDb.QuestionTypeId, lastOfOriginal.QuestionTypeId);
            Assert.Equal(lastOfDb.QuestionaryId, lastOfOriginal.QuestionaryId);
            Assert.Equal(lastOfDb.QuestionPhrase, lastOfOriginal.QuestionPhrase);
            Assert.Equal(counterOfDb, counterOfOriginal);

            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAllQuestionsWhichMeetTheGivenCriteria_IReceiveTheListIExpect()
        {
            // Create new question repository with this context
            _questionRepo = new QuestionRepo(Context);

            // Retrieve all question in DB which meet the given conditions
            // Condition 1: AllowedTime > 15
            List<Question> questionsInDb_AllowedTimeGreaterThan15_Repo = _questionRepo.Find(a => a.AllowedTime > 14).ToList(); // List of 1
            List<Question> questionsInDb_AllowedTimeGreaterThan15_DB = Context.Questions.Where(a => a.AllowedTime > 14).ToList(); // List of 1
            // Condition 2: QuestionDifficultyId = 1
            List<Question> questionsInDb_WithDifficulty1_Repo = _questionRepo.Find(a => a.QuestionDifficultyId == 31).ToList(); // List of 1
            List<Question> questionsInDb_WithDifficulty1_DB = Context.Questions.Where(a => a.QuestionDifficultyId == 31).ToList(); // List of 1
            // Condition 3: QuestionPhrase contains "second"
            List<Question> questionsInDb_PhraseContainsWord_Second_Repo = _questionRepo.Find(a => a.QuestionPhrase.Contains("second")).ToList(); // List of 1
            List<Question> questionsInDb_PhraseContainsWord_Second_DB = Context.Questions.Where(a => a.QuestionPhrase.Contains("second")).ToList(); // List of 1

            #region Asserts

            // Check condition 1
            Assert.True(questionsInDb_AllowedTimeGreaterThan15_Repo.Count == questionsInDb_AllowedTimeGreaterThan15_DB.Count);
            Assert.True(questionsInDb_AllowedTimeGreaterThan15_Repo.FirstOrDefault().AllowedTime == questionsInDb_AllowedTimeGreaterThan15_DB.FirstOrDefault().AllowedTime);
            Assert.True(questionsInDb_AllowedTimeGreaterThan15_Repo.FirstOrDefault().QuestionDifficultyId == questionsInDb_AllowedTimeGreaterThan15_DB.FirstOrDefault().QuestionDifficultyId);

            // Check condition 2
            Assert.True(questionsInDb_WithDifficulty1_Repo.Count == questionsInDb_WithDifficulty1_DB.Count);
            Assert.True(questionsInDb_WithDifficulty1_Repo.FirstOrDefault().AllowedTime == questionsInDb_WithDifficulty1_DB.FirstOrDefault().AllowedTime);
            Assert.True(questionsInDb_WithDifficulty1_Repo.FirstOrDefault().QuestionDifficultyId == questionsInDb_WithDifficulty1_DB.FirstOrDefault().QuestionDifficultyId);

            // Check condition 3
            Assert.True(questionsInDb_PhraseContainsWord_Second_Repo.Count == questionsInDb_PhraseContainsWord_Second_DB.Count);


            #endregion Asserts
        }

        [Fact]
        public void WhenIAsktoFindAllQuestionFromAnSpecificQuestionaryId_IRecieveAListOfQuestions()
        {
            _questionRepo = new QuestionRepo(Context);

            var questionInDB = Context.Questions.Where(a => a.QuestionaryId == 1).ToList();
            var questionInRepo = _questionRepo.GetAllQuestionsByQuestionaryId(1).ToList();

            #region Asserts
            Assert.True(questionInDB.Count == questionInRepo.Count);
            Assert.Equal(questionInDB.FirstOrDefault().Id , questionInRepo.FirstOrDefault().Id);
            Assert.Equal(questionInDB.LastOrDefault().Id , questionInRepo.LastOrDefault().Id);
            #endregion Asserts
        }

        #endregion Public Methods
    }
}