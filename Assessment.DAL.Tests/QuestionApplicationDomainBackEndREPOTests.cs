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
    public class QuestionApplicationDomainBackEndREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private QuestionApplicationDomainBackEndRepo _questionApplicationDomainBackEndRepo;

        private QuestionApplicationDomainBackEnd _questionApplicationDomainBackEnd;
        private QuestionApplicationDomainBackEnd _questionApplicationDomainBackEndAdded;
        private QuestionApplicationDomainBackEnd _questionApplicationDomainBackEndDB;
        private QuestionApplicationDomainBackEnd _questionApplicationDomainBackEndUpdate;
        //Lists
        private List<QuestionApplicationDomainBackEnd> _questionApplicationDomainBackEnds;
        private List<QuestionApplicationDomainBackEnd> _questionApplicationDomainBackEndsDB;

        private int _questionApplicationDomainBackEndId1 = 1;
        private int _questionApplicationDomainBackEndId2 = 2;
        private int _newQuestionApplicationDomainBackEndId2 = 3;

        private int _questioncompositionId1 = 1;
        private int _questioncompositionId2 = 2;
        private int _newQuestioncompositionId2 = 3;

        private int _applicationDomainBackEndId1 = 1;
        private int _applicationDomainBackEndId2 = 2;
        private int _newApplicationDomainBackEndId2 = 3;

        #endregion

        public override void Init()
        {
            _questionApplicationDomainBackEnd = new QuestionApplicationDomainBackEndFactory()
                .WithQuestionApplicationDomainBackEndId(_newQuestionApplicationDomainBackEndId2)
                .WithQuestionCompositionId(_newQuestioncompositionId2)
                .WithApplicationDomainBackEndId(_newApplicationDomainBackEndId2)
                .Build();

            _questionApplicationDomainBackEnds = new List<QuestionApplicationDomainBackEnd>
            {
                new QuestionApplicationDomainBackEndFactory()
                .WithQuestionApplicationDomainBackEndId(_questionApplicationDomainBackEndId1)
                .WithQuestionCompositionId(_questioncompositionId1)
                .WithApplicationDomainBackEndId(_applicationDomainBackEndId1)
                .Build()
                ,
                new QuestionApplicationDomainBackEndFactory()
                .WithQuestionApplicationDomainBackEndId(_questionApplicationDomainBackEndId2)
                .WithQuestionCompositionId(_questioncompositionId2)
                .WithApplicationDomainBackEndId(_applicationDomainBackEndId2)
                .Build()
            };

            InitializeDbSet(_questionApplicationDomainBackEnds);
        }

        [Fact]
        public void ANewQuestionApplicationDomainBackEndIsAddedCorrectlytoTheDB_AndCanBeRetrieved()
        {
            _questionApplicationDomainBackEndRepo = new QuestionApplicationDomainBackEndRepo(Context);
            _questionApplicationDomainBackEndAdded = _questionApplicationDomainBackEndRepo.Add(_questionApplicationDomainBackEnd);
            _questionApplicationDomainBackEndDB = Context.QuestionApplicationDomainBackEnds.First(x => x.Id == _newQuestionApplicationDomainBackEndId2);

            #region Asserts
            Assert.Equal(_newQuestionApplicationDomainBackEndId2, _questionApplicationDomainBackEndDB.Id);
            #endregion
        }

        [Fact]
        public void AQuestionApplicationDomainBackEndCanBeRetrievedById()
        {
            _questionApplicationDomainBackEndRepo = new QuestionApplicationDomainBackEndRepo(Context);
            var questionApplicationDomainBackEnd = _questionApplicationDomainBackEndRepo.GetById(_questionApplicationDomainBackEndId1);
            _questionApplicationDomainBackEndDB = Context.QuestionApplicationDomainBackEnds.First(x => x.Id == _questionApplicationDomainBackEndId1);
            #region Asserts
            Assert.NotNull(_questionApplicationDomainBackEndDB);
            Assert.Equal(questionApplicationDomainBackEnd.QuestionCompositionId, _questionApplicationDomainBackEndDB.QuestionCompositionId);
            #endregion
        }

        [Fact]
        public void WhenIUpdateAQuestionApplicationDomainBackEnd_TheChangesAreCorrectChangedInTheDB()
        {
            _questionApplicationDomainBackEndRepo = new QuestionApplicationDomainBackEndRepo(Context);
            _questionApplicationDomainBackEndUpdate = _questionApplicationDomainBackEndRepo.GetById(_questionApplicationDomainBackEndId1);

            _questionApplicationDomainBackEndUpdate.QuestionCompositionId = _questioncompositionId2;
            _questionApplicationDomainBackEndRepo.Update(_questionApplicationDomainBackEndUpdate);
            _questionApplicationDomainBackEndDB = Context.QuestionApplicationDomainBackEnds.FirstOrDefault(x => x.Id == _questionApplicationDomainBackEndId1);

            #region  Assert
            Assert.Equal(_questionApplicationDomainBackEndUpdate.QuestionCompositionId, _questionApplicationDomainBackEndDB.QuestionCompositionId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAnListOfQuestionApplicationDomainBackEnds_IRecieveAnListOfQuestionApplicationDomainBackEnds()
        {
            _questionApplicationDomainBackEndRepo = new QuestionApplicationDomainBackEndRepo(Context);
            _questionApplicationDomainBackEnds = _questionApplicationDomainBackEndRepo.GetAll().ToList();
            _questionApplicationDomainBackEndsDB = Context.QuestionApplicationDomainBackEnds.ToList();


            QuestionApplicationDomainBackEnd first = _questionApplicationDomainBackEnds.FirstOrDefault();
            QuestionApplicationDomainBackEnd last = _questionApplicationDomainBackEnds.LastOrDefault();
                                    

            QuestionApplicationDomainBackEnd firstDB = _questionApplicationDomainBackEndsDB.FirstOrDefault();
            QuestionApplicationDomainBackEnd lastDB = _questionApplicationDomainBackEndsDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
            Assert.Equal(last.Id, lastDB.Id);
        }
    }
}
