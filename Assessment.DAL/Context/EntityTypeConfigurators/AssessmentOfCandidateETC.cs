using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class AssessmentOfCandidateETC : IEntityTypeConfiguration<AssessmentOfCandidateETC>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<AssessmentOfCandidateETC> builder)
        {
            builder.ToTable("AssessmentOfCandidate", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
