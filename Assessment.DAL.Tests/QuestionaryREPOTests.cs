using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class QuestionaryRepoTests : AssessmentCoreTest
    {
        #region Private Fields

        // Repos
        private QuestionaryRepo _questionaryRepo;

        // Ents
        private Questionary _questionary;
        private Questionary _questionaryInDb;
        private Questionary _questionaryToUpdate;
        private Questionary _questionaryAdded;

        // Lists
        private List<Questionary> _questionaries;
        private List<Questionary> _questionariesInDb;

        // Ids
        private const int questionary1Id = 1;
        private const int questionary2Id = 2;
        private const int newQuestionaryId = 3;

        private int _questionId1 = 102;
        private int _questionId2 = 2035;

        private int _questionAndAnwserOfAssesmentId1 = 123456789;
        private int _questionAndAnwserOfAssesmentId2 = 1234567890;

        #endregion Private Fields

        #region Public Methods
        public override void Init()
        {
            _questionary = new QuestionaryFactory().WithQuestionaryId(newQuestionaryId).Build();

            _questionaries = new List<Questionary>()
            {
                new QuestionaryFactory().WithQuestionaryId(questionary1Id).WithQuestionId(_questionId1).WithDescription("This is a first questionary.").WithQuestionAndAnwserOfAssesmentId(_questionAndAnwserOfAssesmentId1).Build(),
                new QuestionaryFactory().WithQuestionaryId(questionary2Id).WithQuestionId(_questionId2).WithDescription("This is a second questionary.").WithQuestionAndAnwserOfAssesmentId(_questionAndAnwserOfAssesmentId2).Build()
            };

            InitializeDbSet(_questionaries);
        }

        [Fact]
        public void AQuestionaryIsAddedCorrectlyToTheDb_AndCanBeRetrieved()
        {
            _questionaryRepo = new QuestionaryRepo(Context);

            _questionaryAdded = _questionaryRepo.Add(_questionary);

            _questionaryInDb = Context.Questionaries.First(x => x.Id == newQuestionaryId);

            #region Asserts

            // Compare some basic properties for this test object
            Assert.Equal(_questionaryAdded.Description, _questionaryInDb.Description);
            
            #endregion Asserts
        }
        

        [Fact]
        public void AQuestionaryCanBeRetrievedById()
        {
            _questionaryRepo = new QuestionaryRepo(Context);
            _questionaryInDb = _questionaryRepo.GetById(questionary1Id);

            Questionary expectedQuestionary = _questionaries.Find(x => x.Id == questionary1Id);

            #region Asserts

            // Compare some basic properties for this test object
            Assert.Equal(expectedQuestionary.Description, _questionaryInDb.Description);

            #endregion Asserts
        }

        [Fact]
        public void WhenIUpdateAQuestionary_TheChangesAreAddedCorrectlyToTheDb()
        {
            _questionaryRepo = new QuestionaryRepo(Context);

            _questionaryToUpdate = Context.Questionaries.First(x => x.Id == questionary1Id);

            _questionaryToUpdate.Description = "This description has been altered.";
            _questionaryRepo.Update(_questionaryToUpdate);

            _questionaryInDb = Context.Questionaries.First(x => x.Id == questionary1Id);

            #region Asserts

            // Compare some basic properties for this test object
            Assert.Equal(_questionaryToUpdate.Description, _questionaryInDb.Description);

            #endregion Asserts
        }

        [Fact]
        public void WhenIAskForAListOfAllQuestionaries_IReceiveTheListIExpect()
        {
            _questionaryRepo = new QuestionaryRepo(Context);

            _questionariesInDb = _questionaryRepo.GetAll().ToList();

            Questionary firstOfOriginal = _questionaries.FirstOrDefault();
            Questionary lastOfOriginal = _questionaries.LastOrDefault();
            var counterOfOriginal = _questionaries.Count();

            // Get first en last test in list retrieved from DB
            Questionary firstOfDb = _questionariesInDb.FirstOrDefault();
            Questionary lastOfDb = _questionariesInDb.LastOrDefault();
            var counterOfDb = _questionariesInDb.Count();

            #region Asserts

            // Compare first and last questionary properties
            // #1
            Assert.Equal(firstOfOriginal.Description, firstOfDb.Description);
            // #2
            Assert.Equal(lastOfOriginal.Description, lastOfDb.Description);
            //#3
            Assert.Equal(counterOfDb , counterOfOriginal);
            #endregion Asserts
        }

        [Fact]
        public void WhenIAskToFindAllQuestionariesWhichMeetTheGivenCriteria_IReceiveTheListIExpect()
        {
            var descriptionToFind = "first questionary";
            _questionaryRepo = new QuestionaryRepo(Context);

            // Retrieve all questionaries in DB which meet the given conditions
            // Condition 1: Description contains "first questionary"
            List<Questionary> questionariesInDb_DescriptionContainsWord_FirstQuestionary = _questionaryRepo.Find(a => a.Description.Contains(descriptionToFind)).ToList(); // List of 1
            #region Asserts

            // Check condition 1
            Assert.True(questionariesInDb_DescriptionContainsWord_FirstQuestionary.Count() is 1);
            Assert.Contains(descriptionToFind, questionariesInDb_DescriptionContainsWord_FirstQuestionary.FirstOrDefault().Description);

            #endregion Asserts
        }

        #endregion Public Methods

    }
}