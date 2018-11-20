using Assessment.DAL.Repositories;
using Assessment.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment.DAL
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQuestionaryRepo, QuestionaryRepo>();
            services.AddTransient<IQuestionRepo, QuestionRepo>();
            services.AddTransient<IAnswerRepo, AnswerRepo>();
            services.AddTransient<IAssessmentRepo, AssessmentRepo>();
            services.AddTransient<IQuestionsAndAnswersOfAssessmentREPO, QuestionsAndAnswersOfAssessmentRepo>();
            services.AddTransient<IAssessmentOfCandidateRepo, AssessmentOfCandidateREPO>();
            services.AddTransient<IAssessmentOfEmployeeREPO, AssessmentOfEmployeeREPO>();
            services.AddTransient<IQuestionApplicationDomainBackEndRepo, QuestionApplicationDomainBackEndRepo>();
            services.AddTransient<IQuestionApplicationDomainFrontEndRepo, QuestionApplicationDomainFrontEndRepo>();
            services.AddTransient<IQuestionApplicationLanguageREPO, QuestionApplicationLanguageREPO>();

        }
    }
}
