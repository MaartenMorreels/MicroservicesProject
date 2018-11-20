using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class QuestionCompositionREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private QuestionCompositionREPO _questionCompositionRepo;

        private QuestionComposition _questionComposition;
        private QuestionComposition _questionCompositionAdded;
        private QuestionComposition _questionCompositionDB;
        private QuestionComposition _questionCompositionUpdate;
        //Lists
        private List<QuestionComposition> _questionCompositions;
        private List<QuestionComposition> _questionCompositionsDB;

        private int _questioncompositionId1 = 1;
        private int _questioncompositionId2 = 2;
        private int _newQuestioncompositionId = 3;

        private int _applicationLanguageId1 = 1;
        private int _applicationFrontEndId1 = 1;
        private int _applicationBackEndId1 = 1;
        private int _applicationLanguageId2 = 2;
        private int _applicationFrontEndId2 = 2;
        private int _applicationBackEndId2 = 2;

        private int _newApplicationLanguageId = 3;
        private int _newApplicationFrontEndId = 3;
        private int _newApplicationBackEndId = 3;


        #endregion

        public override void Init()
        {
            _questionComposition = new QuestionCompositionFactory().WithQuestionCompositionId(_newQuestioncompositionId).WithApplicationLanguageId(_newApplicationLanguageId).WithApplicationFrontEndId(_newApplicationFrontEndId).WithApplicationBackEndId(_newApplicationBackEndId).Build();

            _questionCompositions = new List<QuestionComposition>
            {
                new QuestionCompositionFactory().WithQuestionCompositionId(_questioncompositionId1).WithApplicationLanguageId(_applicationLanguageId1).WithApplicationFrontEndId(_applicationFrontEndId1).WithApplicationBackEndId(_applicationBackEndId1).Build(),
                new QuestionCompositionFactory().WithQuestionCompositionId(_questioncompositionId2).WithApplicationLanguageId(_applicationLanguageId2).WithApplicationFrontEndId(_applicationFrontEndId2).WithApplicationBackEndId(_applicationBackEndId2).Build()
            };

            InitializeDbSet(_questionCompositions);
        }

        [Fact]
        public void ANewQuestionCompositionIsAddedCorrectlytoTheDB_AndCanBeRetrieved()
        {
            _questionCompositionRepo = new QuestionCompositionREPO(Context);
            _questionCompositionAdded = _questionCompositionRepo.Add(_questionComposition);
            _questionCompositionDB = Context.QuestionCompositions.First(x => x.Id == _newQuestioncompositionId);

            #region Asserts
            Assert.Equal(_newQuestioncompositionId, _questionCompositionDB.Id);
            #endregion
        }

        [Fact]
        public void AQuestionCompositionCanBeRetrievedById()
        {
            _questionCompositionRepo = new QuestionCompositionREPO(Context);
            var questionComposition = _questionCompositionRepo.GetById(_questioncompositionId1);
            _questionCompositionDB = Context.QuestionCompositions.First(x => x.Id == _questioncompositionId1);
            #region Asserts
            Assert.NotNull(_questionCompositionDB);
            Assert.Equal(questionComposition.ApplicationLanguageId, _questionCompositionDB.ApplicationLanguageId);
            Assert.Equal(questionComposition.ApplicationBackEndId, _questionCompositionDB.ApplicationBackEndId);
            Assert.Equal(questionComposition.ApplicationFrontEndId, _questionCompositionDB.ApplicationFrontEndId);
            #endregion
        }

        [Fact]
        public void WhenIUpdateAQuestionComposition_TheChangesAreCorrectChangedInTheDB()
        {
            _questionCompositionRepo = new QuestionCompositionREPO(Context);
            _questionCompositionUpdate = _questionCompositionRepo.GetById(_questioncompositionId1);

            _questionCompositionUpdate.ApplicationLanguageId = _applicationLanguageId2;
            _questionCompositionRepo.Update(_questionCompositionUpdate);
            _questionCompositionDB = Context.QuestionCompositions.FirstOrDefault(x => x.Id == _questioncompositionId1);

            #region  Assert
            Assert.Equal(_questionCompositionUpdate.ApplicationLanguageId, _questionCompositionDB.ApplicationLanguageId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAnListOfQuestionCompositions_IRecieveAnListOfQuestionCompositions()
        {
            _questionCompositionRepo = new QuestionCompositionREPO(Context);
            _questionCompositionsDB = _questionCompositionRepo.GetAll().ToList();

            QuestionComposition first = _questionCompositions.FirstOrDefault();
            QuestionComposition last = _questionCompositions.LastOrDefault();

            QuestionComposition firstDB = _questionCompositionsDB.FirstOrDefault();
            QuestionComposition lastDB = _questionCompositionsDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
        }
    }
}
