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
    public class QuestionApplicationDomainFrontEndREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private QuestionApplicationDomainFrontEndRepo _questionApplicationDomainFrontEndRepo;

        private QuestionApplicationDomainFrontEnd _questionApplicationDomainFrontEnd;
        private QuestionApplicationDomainFrontEnd _questionApplicationDomainFrontEndAdded;
        private QuestionApplicationDomainFrontEnd _questionApplicationDomainFrontEndDB;
        private QuestionApplicationDomainFrontEnd _questionApplicationDomainFrontEndUpdate;
        //Lists
        private List<QuestionApplicationDomainFrontEnd> _questionApplicationDomainFrontEnds;
        private List<QuestionApplicationDomainFrontEnd> _questionApplicationDomainFrontEndsDB;

        private int _questionApplicationDomainFrontEndId1 = 1;
        private int _questionApplicationDomainFrontEndId2 = 2;
        private int _newQuestionApplicationDomainFrontEndId2 = 3;

        private int _questioncompositionId1 = 1;
        private int _questioncompositionId2 = 2;
        private int _newQuestioncompositionId2 = 3;

        private int _applicationDomainFrontEndId1 = 1;
        private int _applicationDomainFrontEndId2 = 2;
        private int _newApplicationDomainFrontEndId2 = 3;

        #endregion

        public override void Init()
        {
            _questionApplicationDomainFrontEnd = new QuestionApplicationDomainFrontEndFactory()
                .WithQuestionApplicationDomainFrontEndId(_newQuestionApplicationDomainFrontEndId2)
                .WithQuestionCompositionId(_newQuestioncompositionId2)
                .WithApplicationDomainFrontEndId(_newApplicationDomainFrontEndId2)
                .Build();

            _questionApplicationDomainFrontEnds = new List<QuestionApplicationDomainFrontEnd>
            {
                new QuestionApplicationDomainFrontEndFactory()
                .WithQuestionApplicationDomainFrontEndId(_questionApplicationDomainFrontEndId1)
                .WithQuestionCompositionId(_questioncompositionId1)
                .WithApplicationDomainFrontEndId(_applicationDomainFrontEndId1)
                .Build()
                ,
                new QuestionApplicationDomainFrontEndFactory()
                .WithQuestionApplicationDomainFrontEndId(_questionApplicationDomainFrontEndId2)
                .WithQuestionCompositionId(_questioncompositionId2)
                .WithApplicationDomainFrontEndId(_applicationDomainFrontEndId2)
                .Build()
            };

            InitializeDbSet(_questionApplicationDomainFrontEnds);
        }

        [Fact]
        public void ANewQuestionApplicationDomainFrontEndIsAddedCorrectlytoTheDB_AndCanBeRetrieved()
        {
            _questionApplicationDomainFrontEndRepo = new QuestionApplicationDomainFrontEndRepo(Context);
            _questionApplicationDomainFrontEndAdded = _questionApplicationDomainFrontEndRepo.Add(_questionApplicationDomainFrontEnd);
            _questionApplicationDomainFrontEndDB = Context.QuestionApplicationDomainFrontEnds.First(x => x.Id == _newQuestionApplicationDomainFrontEndId2);

            #region Asserts
            Assert.Equal(_newQuestionApplicationDomainFrontEndId2, _questionApplicationDomainFrontEndDB.Id);
            #endregion
        }

        [Fact]
        public void AQuestionApplicationDomainFrontEndCanBeRetrievedById()
        {
            _questionApplicationDomainFrontEndRepo = new QuestionApplicationDomainFrontEndRepo(Context);
            var questionApplicationDomainFrontEnd = _questionApplicationDomainFrontEndRepo.GetById(_questionApplicationDomainFrontEndId1);
            _questionApplicationDomainFrontEndDB = Context.QuestionApplicationDomainFrontEnds.First(x => x.Id == _questionApplicationDomainFrontEndId1);
            #region Asserts
            Assert.NotNull(_questionApplicationDomainFrontEndDB);
            Assert.Equal(questionApplicationDomainFrontEnd.QuestionCompositionId, _questionApplicationDomainFrontEndDB.QuestionCompositionId);
            #endregion
        }

        [Fact]
        public void WhenIUpdateAQuestionApplicationDomainFrontEnd_TheChangesAreCorrectChangedInTheDB()
        {
            _questionApplicationDomainFrontEndRepo = new QuestionApplicationDomainFrontEndRepo(Context);
            _questionApplicationDomainFrontEndUpdate = _questionApplicationDomainFrontEndRepo.GetById(_questionApplicationDomainFrontEndId1);

            _questionApplicationDomainFrontEndUpdate.QuestionCompositionId = _questioncompositionId2;
            _questionApplicationDomainFrontEndRepo.Update(_questionApplicationDomainFrontEndUpdate);
            _questionApplicationDomainFrontEndDB = Context.QuestionApplicationDomainFrontEnds.FirstOrDefault(x => x.Id == _questionApplicationDomainFrontEndId1);

            #region  Assert
            Assert.Equal(_questionApplicationDomainFrontEndUpdate.QuestionCompositionId, _questionApplicationDomainFrontEndDB.QuestionCompositionId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAnListOfQuestionApplicationDomainFrontEnds_IRecieveAnListOfQuestionApplicationDomainFrontEnds()
        {
            _questionApplicationDomainFrontEndRepo = new QuestionApplicationDomainFrontEndRepo(Context);
            _questionApplicationDomainFrontEnds = _questionApplicationDomainFrontEndRepo.GetAll().ToList();
            _questionApplicationDomainFrontEndsDB = Context.QuestionApplicationDomainFrontEnds.ToList();


            QuestionApplicationDomainFrontEnd first = _questionApplicationDomainFrontEnds.FirstOrDefault();
            QuestionApplicationDomainFrontEnd last = _questionApplicationDomainFrontEnds.LastOrDefault();


            QuestionApplicationDomainFrontEnd firstDB = _questionApplicationDomainFrontEndsDB.FirstOrDefault();
            QuestionApplicationDomainFrontEnd lastDB = _questionApplicationDomainFrontEndsDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
            Assert.Equal(last.Id, lastDB.Id);
        }
    }
}
