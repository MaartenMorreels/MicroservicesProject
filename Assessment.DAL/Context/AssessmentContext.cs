using System;
using System.Fabric;
using Assessment.DAL.Context.EntityTypeConfigurators;
using Assessment.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DAL.Context
{
    public class AssessmentContext : DbContext
    {
        #region Public Constructors

        public AssessmentContext()
        {
        }

        public AssessmentContext(DbContextOptions<AssessmentContext> options) : base(options)
        {
        }

        #endregion Public Constructors



        #region Public Properties

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Entities.Assessment> Assessments { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionAndAnswerOfAssessment> QuestionsAndAnswersOfAssessment { get; set; }

        public DbSet<Questionary> Questionaries { get; set; }

        public DbSet<QuestionComposition> QuestionCompositions { get; set; }

        public DbSet<QuestionApplicationLanguage> QuestionApplicationLanguages { get; set; }

        public DbSet<QuestionApplicationDomainFrontEnd> QuestionApplicationDomainFrontEnds { get; set; }

        public DbSet<QuestionApplicationDomainBackEnd> QuestionApplicationDomainBackEnds { get; set; }

        public DbSet<AssessmentOfCandidate> AssessmentOfCandidates { get; set; }

        public DbSet<AssessmentOfEmployee> AssessmentOfEmployees { get; set; }

        #endregion Public Properties


        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var configurationPackage = FabricRuntime.GetActivationContext().GetConfigurationPackageObject("Config");
            var param = configurationPackage.Settings.Sections["DatabaseConfig"].Parameters["DBConnection"];
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(param.Value, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Questionary>()
                .ToTable("Questionary", schema: "IRM.HRM.Assessment");
            builder.Entity<Question>()
                .ToTable("Question", schema: "IRM.HRM.Assessment");
            builder.Entity<Answer>()
                .ToTable("Answer", schema: "IRM.HRM.Assessment");
            builder.Entity<Entities.Assessment>()
                .ToTable("Assessment", schema: "IRM.HRM.Assessment");
            builder.Entity<QuestionAndAnswerOfAssessment>()
                .ToTable("QuestionAndAnswerOfAssessment", schema: "IRM.HRM.Assessment");
           

            builder
                .ApplyConfiguration(new AnswerEtc())
                .ApplyConfiguration(new QuestionEtc())
                .ApplyConfiguration(new QuestionaryEtc())
                .ApplyConfiguration(new QuestionAndAnswerOfAssessmentEtc())
                .ApplyConfiguration(new AssessmentEtc())
                .ApplyConfiguration(new QuestionApplicationLanguageETC())
                .ApplyConfiguration(new QuestionApplicationDomainFrontEndETC())
                .ApplyConfiguration(new QuestionApplicationDomainFrontEndETC());

           
            base.OnModelCreating(builder);
        }

        #endregion Protected Methods
    }
}