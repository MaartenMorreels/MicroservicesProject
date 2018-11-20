using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    public class QuestionApplicationDomainFrontEndETC : IEntityTypeConfiguration<QuestionApplicationDomainFrontEnd>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<QuestionApplicationDomainFrontEnd> builder)
        {
            builder.ToTable("QuestionApplicationDomainFrontEnd", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
