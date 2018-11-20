using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Tests.Factories
{
    public class AssessmentOfCandidateFactory
    {
        private int _assessmentOfCandidateId = 1;
        private int _assessmentId = 1;
        public int _candidateId = 1;

        public AssessmentOfCandidate Build()
        {
            return new AssessmentOfCandidate()
            {
                Id = _assessmentOfCandidateId,
                AssessmentId = _assessmentId,
                CandidateId = _candidateId
            };
        }

        public AssessmentOfCandidateFactory WithAssessmentOfCandidateId(int assessmentOfCandidateId)
        {
            _assessmentOfCandidateId = assessmentOfCandidateId;
            return this;
        }

        public AssessmentOfCandidateFactory WithAssessmentId(int assessmentId)
        {
            _assessmentId = assessmentId;
            return this;
        }

        public AssessmentOfCandidateFactory WithCandidateId(int candidateId)
        {
            _candidateId = candidateId;
            return this;
        }
    }
}
