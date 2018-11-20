using Assessment.BLL.Services;
using Assessment.BLL.Services.Interfaces;
using Assessment.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment.BLL
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IQuestionaryService, QuestionaryService>();
            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<IAnswerService, AnswerService>();
            services.AddSingleton<IAssessmentService, AssessmentService>();
            services.AddSingleton<IQuestionAndAnswerOfAssessmentService, QuestionAndAnswerOfAssessmentService>();
            services.AddSingleton<IAssessmentOfCandidateService, AssessmentOfCandidateService>();
            services.AddSingleton<IAssessmentOfEmployeeService, AssessmentOfEmployeeService>();
            services.AddSingleton<IQuestionApplicationDomainBackEndService, QuestionApplicationDomainBackEndService>();
            services.AddSingleton<IQuestionApplicationDomainFrontEndService, QuestionApplicationDomainFrontEndService>();
            services.AddSingleton<IQuestionApplicationLanguageService, QuestionApplicationLanguageService>();

            services.AddSingleton<QuestionService>();
            services.AddSingleton<AnswerService>();
            services.AddSingleton<AssessmentService>();
            services.AddSingleton<QuestionAndAnswerOfAssessmentService>();
            services.AddSingleton<AssessmentOfCandidateService>();
            services.AddSingleton<AssessmentOfEmployeeService>();
            services.AddSingleton<QuestionApplicationDomainBackEndService>();
            services.AddSingleton<QuestionApplicationDomainFrontEndService>();
            services.AddSingleton<QuestionApplicationLanguageService>();
            


             services.RegisterRepository(configuration);
        }
    }
}
