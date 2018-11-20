using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    public class QuestionApplicationDomainBackEndETC : IEntityTypeConfiguration<QuestionApplicationDomainBackEnd>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<QuestionApplicationDomainBackEnd> builder)
        {
            builder.ToTable("QuestionApplicationDomainBackEnd", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
