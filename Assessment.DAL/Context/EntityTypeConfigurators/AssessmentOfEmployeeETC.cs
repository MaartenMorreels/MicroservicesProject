using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class AssessmentOfEmployeeETC : IEntityTypeConfiguration<AssessmentOfEmployeeETC>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<AssessmentOfEmployeeETC> builder)
        {
            builder.ToTable("AssessmentOfEmployee", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
