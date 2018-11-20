using System;
using System.Data.Common;
using Assessment.DAL.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DAL.Tests.TestAssessmentContext
{
	public class TestAssessmentFactory : IDisposable
	{
		#region Private Fields

		private DbConnection _connection;

		#endregion Private Fields

		#region Private Methods

		private DbContextOptions<AssessmentContext> CreateOptions()
		{
			return new DbContextOptionsBuilder<AssessmentContext>()
				.UseLazyLoadingProxies()
				.UseSqlite(_connection, x => x.SuppressForeignKeyEnforcement()).Options;

			//todo waarom de supressforeinkey gebruiken
			//todo md file aanmaken voor documentatie
		}

		#endregion Private Methods

		#region Public Methods

		public AssessmentContext CreateContext()
		{
			if (_connection == null)
			{
				_connection = new SqliteConnection("DataSource=:memory:");
				_connection.Open();

				var options = CreateOptions();
				using (var context = new AssessmentContext(options))
				{
					context.Database.EnsureCreated();
				}
			}

			return new AssessmentContext(CreateOptions());
		}

		public void Dispose()
		{
			if (_connection != null)
			{
				_connection.Dispose();
				_connection = null;
			}
		}

		#endregion Public Methods
	}
}