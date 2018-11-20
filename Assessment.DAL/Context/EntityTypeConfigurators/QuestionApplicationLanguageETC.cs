using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.DAL.Context.EntityTypeConfigurators
{
    internal class QuestionApplicationLanguageETC : IEntityTypeConfiguration<QuestionApplicationLanguage>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<QuestionApplicationLanguage> builder)
        {
            builder.ToTable("QuestionApplicationLanguage", schema: "IRM.HRM.Assessment");
        }

        #endregion Public Methods
    }
}
