using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Assessment.DAL.Context;
using Assessment.DAL.Tests.TestAssessmentContext;

namespace Assessment.DAL.Tests.Core
{
    public abstract class AssessmentCoreTest : IDisposable
    {
        #region Protected Fields

        protected AssessmentContext Context;

        #endregion


        #region Private Fields

        private TestAssessmentFactory _factory;

        #endregion

        protected AssessmentCoreTest()
        {
            _factory = new TestAssessmentFactory();
            Context = _factory.CreateContext();

            Init();
        }

        protected void InitializeDbSet<TEntity>(List<TEntity> data) where TEntity : class
        {
            Context.Set<TEntity>().AddRange(data);
            Context.SaveChanges();
        }

        public abstract void Init();

        public void Dispose()
        {
            _factory.Dispose();   
        }
    }
}
