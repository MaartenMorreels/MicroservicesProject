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
    public class AssessmentOfCandidateREPOTests : AssessmentCoreTest
    {
        #region Private Fields

        private AssessmentOfCandidateREPO _assessmentOfCandidateREPO;

        private AssessmentOfCandidate _assessmentOfCandidate;
        private AssessmentOfCandidate _assessmentOfCandidateAdded;
        private AssessmentOfCandidate _assessmentOfCandidateDB;
        private AssessmentOfCandidate _assessmentOfCandidateUpdate;
        //Lists
        private List<AssessmentOfCandidate> _assessmentOfCandidates;
        private List<AssessmentOfCandidate> _assessmentOfCandidatesDB;

        private int _assessmentOfCandidateId1 = 1;
        private int _assessmentOfCandidateId2 = 2;
        private int _newAssessmentOfCandidateId = 3;

        private int _assessmentId1 = 1;
        private int _candidateId1 = 1;
        private int _assessmentId2 = 2;
        private int _candidateId2 = 2;

        private int _newAssessmentId = 3;
        private int _newCandidateId = 3;

        #endregion

        public override void Init()
        {
            _assessmentOfCandidate = new AssessmentOfCandidateFactory()
                .WithAssessmentOfCandidateId(_newAssessmentOfCandidateId)
                .WithAssessmentId(_newAssessmentId)
                .WithCandidateId(_newCandidateId)
                .Build();

            _assessmentOfCandidates = new List<AssessmentOfCandidate>
            {
                new AssessmentOfCandidateFactory().WithAssessmentOfCandidateId(_assessmentOfCandidateId1).WithAssessmentId(_assessmentId1).WithAssessmentId(_candidateId1).Build(),
                new AssessmentOfCandidateFactory().WithAssessmentOfCandidateId(_assessmentOfCandidateId2).WithAssessmentId(_assessmentId2).WithAssessmentId(_candidateId2).Build()
            };

            InitializeDbSet(_assessmentOfCandidates);
        }

        [Fact]
        public void ANewAssessmentOfCandidateIsAddedCorrectlyToTheDB_AndCanBeRetrieved()
        {
            _assessmentOfCandidateREPO = new AssessmentOfCandidateREPO(Context);
            _assessmentOfCandidateAdded = _assessmentOfCandidateREPO.Add(_assessmentOfCandidate);
            _assessmentOfCandidateDB = Context.AssessmentOfCandidates.First(x => x.Id == _newAssessmentOfCandidateId);

            #region Asserts
            Assert.Equal(_newAssessmentOfCandidateId, _assessmentOfCandidateDB.Id);
            #endregion
        }

        [Fact]
        public void AnAssessmentOfCandidateCanBeRetrievedById()
        {
            _assessmentOfCandidateREPO = new AssessmentOfCandidateREPO(Context);
            var assessmentOfCandidate = _assessmentOfCandidateREPO.GetById(_assessmentOfCandidateId1);
            _assessmentOfCandidateDB = Context.AssessmentOfCandidates.First(x => x.Id == _assessmentOfCandidateId1);

            #region Asserts
            Assert.NotNull(_assessmentOfCandidateDB);
            Assert.Equal(assessmentOfCandidate.AssessmentId, _assessmentOfCandidateDB.AssessmentId);
            Assert.Equal(assessmentOfCandidate.CandidateId, _assessmentOfCandidateDB.CandidateId);
            #endregion

        }

        [Fact]
        public void WhenIUpdateAnAssessmentOfCandidate_TheChangesAreCorrectlyChangedInTheDB()
        {
            _assessmentOfCandidateREPO = new AssessmentOfCandidateREPO(Context);
            _assessmentOfCandidateUpdate = _assessmentOfCandidateREPO.GetById(_assessmentOfCandidateId1);

            _assessmentOfCandidateUpdate.AssessmentId = _assessmentId2;
            _assessmentOfCandidateREPO.Update(_assessmentOfCandidateUpdate);
            _assessmentOfCandidateDB = Context.AssessmentOfCandidates.FirstOrDefault(x => x.Id == _assessmentOfCandidateId1);

            #region Asserts
            Assert.Equal(_assessmentOfCandidateUpdate.AssessmentId, _assessmentOfCandidateDB.AssessmentId);
            #endregion
        }

        [Fact]
        public void WhenIAskForAListOfAssessmentOfCandidates_IReceiveAListOfAssessmentOfCandidates()
        {
            _assessmentOfCandidateREPO = new AssessmentOfCandidateREPO(Context);
            _assessmentOfCandidatesDB = _assessmentOfCandidateREPO.GetAll().ToList();

            AssessmentOfCandidate first = _assessmentOfCandidates.FirstOrDefault();
            AssessmentOfCandidate last = _assessmentOfCandidates.LastOrDefault();

            AssessmentOfCandidate firstDB = _assessmentOfCandidatesDB.FirstOrDefault();
            AssessmentOfCandidate lastDB = _assessmentOfCandidatesDB.LastOrDefault();

            Assert.Equal(first.Id, firstDB.Id);
        }

    }
}
