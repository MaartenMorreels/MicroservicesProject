using Assessment.DAL.Entities;
using Assessment.DAL.Repositories;
using Assessment.DAL.Tests.Core;
using Assessment.DAL.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Assessment.DAL.Tests
{
    public class QuestionApplicationLanguageREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private QuestionApplicationLanguageREPO _questionApplicationLanguageRepo;

        private QuestionApplicationLanguage _questionApplicationLanguage;
        private QuestionApplicationLanguage _questionApplicationLanguageAdded;
        private QuestionApplicationLanguage _questionApplicationLanguageDB;
        private QuestionApplicationLanguage _questionApplicationLanguageUpdate;
        //Lists
        private List<QuestionApplicationLanguage> _questionApplicationLanguages;
        private List<QuestionApplicationLanguage> _questionApplicationLanguagesDB;

        private int _questionApplicationLanguageId1 = 1;
        private int _questionApplicationLanguageId2 = 2;
        private int _newQuestionApplicationLanguageId = 3;

        private int _questioncompositionId1 = 1;
        private int _applicationLanguageId1 = 1;
        private int _questioncompositionId2 = 2;
        private int _applicationLanguageId2 = 2;

        private int _newQuestioncompositionId = 3;
        private int _newApplicationLanguageId = 3;

        #endregion


        public override void Init()
        {
            _questionApplicationLanguage = new QuestionApplicationLanguageFactory()
                .WithQuestionApplicationLanguageId(_newApplicationLanguageId)
                .WithApplicationLanguageId(_newApplicationLanguageId)
                .WithQuestionCompositionId(_newQuestioncompositionId)
                .Build();

            _questionApplicationLanguages = new List<QuestionApplicationLanguage>
            {
                new QuestionApplicationLanguageFactory().WithQuestionApplicationLanguageId(_questionApplicationLanguageId1).WithApplicationLanguageId(_applicationLanguageId1).WithQuestionCompositionId(_questioncompositionId1).Build(),
                new QuestionApplicationLanguageFactory().WithQuestionApplicationLanguageId(_questionApplicationLanguageId2).WithApplicationLanguageId(_applicationLanguageId2).WithQuestionCompositionId(_questioncompositionId2).Build()
            };

            InitializeDbSet(_questionApplicationLanguages);
        }

        [Fact]
        public void ANewQuestionApplicationLanguageIsAddedCorrectlyToTheDB_AndCanBeRetrieved()
        {
            _questionApplicationLanguageRepo = new QuestionApplicationLanguageREPO(Context);
            _questionApplicationLanguageAdded = _questionApplicationLanguageRepo.Add(_questionApplicationLanguage);
            _questionApplicationLanguageDB = Context.QuestionApplicationLanguages.First(x => x.Id == _newQuestionApplicationLanguageId);

            #region Asserts
            Assert.Equal(_newQuestionApplicationLanguageId, _questionApplicationLanguageDB.Id);
            #endregion
        }

        [Fact]
        public void AQuestionApplicationLanguageCanBeRetrievedById()
        {
            _questionApplicationLanguageRepo = new QuestionApplicationLanguageREPO(Context);
            var questionApplicationLanguage = _questionApplicationLanguageRepo.GetById(_questionApplicationLanguageId1);
            _questionApplicationLanguageDB = Context.QuestionApplicationLanguages.First(x => x.Id == _questionApplicationLanguageId1);

            #region Asserts
            Assert.NotNull(_questionApplicationLanguageDB);
            Assert.Equal(questionApplicationLanguage.QuestionCompositionId, _questionApplicationLanguageDB.QuestionCompositionId);
            Assert.Equal(questionApplicationLanguage.ApplicationLanguageId, _questionApplicationLanguageDB.ApplicationLanguageId);
            #endregion

        }

        [Fact]
        public void WhenIUpdateAQuestionApplicationLanguage_TheChangesAreCorrectlyChangedInTheDB()
        {
            _questionApplicationLanguageRepo = new QuestionApplicationLanguageREPO(Context);
            _questionApplicationLanguageUpdate = _questionApplicationLanguageRepo.GetById(_questionApplicationLanguageId1);

            _questionApplicationLanguageUpdate.ApplicationLanguageId = _applicationLanguageId2;
            _questionApplicationLanguageRepo.Update(_questionApplicationLanguageUpdate);
            _questionApplicationLanguageDB = Context.QuestionApplicationLanguages.FirstOrDefault(x => x.Id == _questionApplicationLanguageId1);

            #region Asserts
            Assert.Equal(_questionApplicationLanguageUpdate.ApplicationLanguageId, _questionApplicationLanguageDB.ApplicationLanguageId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAListOfQuestionApplicationLanguages_IReceiveAListOfQuestionApplicationLanguages()
        {
            _questionApplicationLanguageRepo = new QuestionApplicationLanguageREPO(Context);
            _questionApplicationLanguagesDB = _questionApplicationLanguageRepo.GetAll().ToList();

            QuestionApplicationLanguage first = _questionApplicationLanguages.FirstOrDefault();
            QuestionApplicationLanguage last = _questionApplicationLanguages.LastOrDefault();

            QuestionApplicationLanguage firstDB = _questionApplicationLanguagesDB.FirstOrDefault();
            QuestionApplicationLanguage lastDB = _questionApplicationLanguagesDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
        }
    }
}



