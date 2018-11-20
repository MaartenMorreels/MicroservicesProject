using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class AnswerRepoTests : AssessmentCoreTest
    {
        #region Private Fields

        // Repos
        private AnswerRepo _answerRepo;

        // Ents
        private Answer _answer;
        private Answer _answerAdded;
        private Answer _answerInDb;
        private Answer _answerToUpdate;

        // Lists
        private List<Answer> _answers1;
        private List<Answer> _answers2;

        //Ids
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

        private const int NewAnswerId = 50;
        private const bool NewCorrect = true;
        private const string NewAnswerText = "This is a simple answer.";

        //Update
        private const bool UpdateCorrect = true;
        private const string UpdateAnswerText1 = "This text has been altered for this answer.";

        private const int Question1Id = 1;
        private const int Question2Id = 2;

        #endregion Private Fields

        #region Public Constructors

        public override void Init()
        {
            var _questionaries = new List<Questionary>();

            _answers1 = new List<Answer>()
            {
                new AnswerFactory().WithAnswerId(AnswerId1).WithAnswerCorrect(Correct1).WithText(AnswerText1).Build(),
                new AnswerFactory().WithAnswerId(AnswerId2).WithAnswerCorrect(Correct2).WithText(AnswerText2).Build()
            };

            _answers2 = new List<Answer>()
            {
                new AnswerFactory().WithAnswerId(AnswerId3).WithAnswerCorrect(Correct3).WithText(AnswerText3).Build(),
                new AnswerFactory().WithAnswerId(AnswerId4).WithAnswerCorrect(Correct4).WithText(AnswerText4).Build()
            };

            var _questions = new List<Question>()
            {
                new QuestionFactory().WithQuestionId(Question1Id).WithAnswers(_answers2).Build(),
                new QuestionFactory().WithQuestionId(Question2Id).WithAnswers(_answers1).Build()
            };

            _questionaries = new List<Questionary>()
            {
                new QuestionaryFactory().WithQuestionaryId(13).WithQuestions(_questions).Build(),
            };

            _answer = new AnswerFactory().WithAnswerId(NewAnswerId).WithAnswerCorrect(UpdateCorrect).WithText(NewAnswerText).Build();

            InitializeDbSet(_questionaries);
        }

        public AnswerRepoTests()
        {

        }

        #endregion Public Constructors


        #region Public Methods

        [Fact] // Questionary Add
        public void AnAnswerIsAddedCorrectlyToDb()
        {
            _answerRepo = new AnswerRepo(Context);
            _answerRepo.Add(_answer);

            Context.SaveChanges();

            _answerInDb = Context.Answers.First(x => x.Id == NewAnswerId);

            #region Asserts
            Assert.Equal(_answerInDb.Correct, NewCorrect);
            Assert.Equal(_answerInDb.Text, NewAnswerText);
            #endregion

        }

        [Fact] // Questionary GetById
        public void AnAnswerCanBeRetrievedById()
        {
            _answerRepo = new AnswerRepo(Context);
            _answerInDb = _answerRepo.GetById(AnswerId1);

            var answer = _answers1.Find(x => x.Id == AnswerId1);

            #region Asserts

            // Compare some basic properties for this answer object
            Assert.Equal(_answerInDb.Correct, answer.Correct);
            Assert.Equal(_answerInDb.Text, answer.Text);

            #endregion Asserts

        }

        [Fact]
        public void WhenIRetrieveAnAnswerWithChildsInDbThatIsNullById_TheChildsAreNull()
        {
            _answerRepo = new AnswerRepo(Context);
            _answerInDb = _answerRepo.GetById(AnswerId3);


            #region Asserts

            // Check if childs are not null
            Assert.NotNull(_answerInDb);

            #endregion Asserts
        }

        [Fact]
        public void WhenIRetrieveAnAnswerWithChildsInDbThatIsNotNullById_TheChildsCanNotBeNull()
        {
            _answerRepo = new AnswerRepo(Context);
            _answerInDb = _answerRepo.GetById(AnswerId2);

            #region Asserts

            // Check if childs are not null
            Assert.NotNull(_answerInDb);

            #endregion Asserts            
        }

        [Fact] // Questionary Update
        public void WhenIUpdateAnAnswer_TheChangesAreAddedCorrectlyToTheDb()
        {
            _answerRepo = new AnswerRepo(Context);
            _answerToUpdate = Context.Answers.First(x => x.Id == AnswerId1);

            _answerToUpdate.Correct = UpdateCorrect;
            _answerToUpdate.Text = UpdateAnswerText1;

            _answerRepo.Update(_answerToUpdate);
            Context.SaveChanges();

            _answerInDb = Context.Answers.First(x => x.Id == AnswerId1);

            #region Asserts

            Assert.Equal(_answerInDb.Correct, UpdateCorrect);
            Assert.Equal(_answerInDb.Text, UpdateAnswerText1);
            #endregion Asserts
        }

        [Fact] // Questionary GetAll
        public void WhenIAskForAListOfAllAnswers_IReceiveTheListIExpect()
        {
            _answerRepo = new AnswerRepo(Context);
            List<Answer> answersInDb = Context.Answers.ToList();

            // Get first en last answer in list retrieved from DB
            Answer firstOfDb = answersInDb.FirstOrDefault();
            Answer lastOfDb = answersInDb.First(x => x.Id == AnswerId2);

            #region Asserts

            // Compare some basic properties for first and last answer in the lists
            // #1
            Assert.Equal(firstOfDb.Correct, Correct1);
            Assert.Equal(firstOfDb.Text, AnswerText1);
            // #2
            Assert.Equal(lastOfDb.Correct, Correct2);
            Assert.Equal(lastOfDb.Text, AnswerText2);

            #endregion Asserts

        }

        [Fact] // Questionary Find
        public void WhenIAskToFindAllAnswersWhichMeetTheGivenCriteria_IReceiveTheListIExpect()
        {
            _answerRepo = new AnswerRepo(Context);

            List<Answer> answersInDb_WithAnswerValue0 = Context.Answers.Where(a => a.Correct == Correct1).ToList();
            List<Answer> answers_WithAnswerValue0 = _answerRepo.Find(x => x.Correct == Correct1).ToList();
            // List of 1
            // Condition 2: AnswerValue = 2
            List<Answer> answersInDb_WithAnswerValue2 = Context.Answers.Where(a => a.Correct == Correct2).ToList();
            List<Answer> answers_WithAnswerValue2 = _answerRepo.Find(a => a.Correct == Correct2).ToList();
            // List of 2
            // Condition 3: QuestionPhrase contains "second"
            List<Answer> answersInDb_TextContainsWord_Simple = Context.Answers.Where(a => a.Text.Contains("second")).ToList();
            List<Answer> answers_TextContainsWord_Simple = _answerRepo.Find(a => a.Text.Contains("second")).ToList();


            #region Asserts

            // Check condition 1
            Assert.True(answersInDb_WithAnswerValue0.Count() is 2);

            Assert.Contains(answersInDb_WithAnswerValue0.FirstOrDefault().Text, answers_WithAnswerValue0.FirstOrDefault().Text);

            // Check condition 2
            Assert.True(answersInDb_WithAnswerValue2.Count() is 2);

            Assert.True(answersInDb_WithAnswerValue2.FirstOrDefault().Text.Contains("second") || answers_WithAnswerValue2.FirstOrDefault().Text.Contains("second"));

            Assert.True(answersInDb_WithAnswerValue2.LastOrDefault().Text.Contains("thirt") || answers_WithAnswerValue2.LastOrDefault().Text.Contains("thirt"));

            //// Check condition 3
            Assert.True(answersInDb_TextContainsWord_Simple.Count() is 1);

            Assert.Contains(answersInDb_TextContainsWord_Simple.FirstOrDefault().Text, answers_TextContainsWord_Simple.FirstOrDefault().Text);
            #endregion Asserts

        }

        [Fact]
        public void WhenIAskForAQuestion_IRecieveAQuestionWithAListOfAnswers()
        {
            _answerRepo = new AnswerRepo(Context);
            var question = Context.Questions.Where(x => x.Id == Question1Id);

            #region Asserts
            Assert.Equal(2, question.FirstOrDefault().Answers.Count());
            Assert.Equal(AnswerId3, question.FirstOrDefault().Answers.FirstOrDefault().Id);
            #endregion Asserts
        }

        #endregion Public Methods

    }
}