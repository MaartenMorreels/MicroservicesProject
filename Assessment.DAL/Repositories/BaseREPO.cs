using System;
using System.Collections.Generic;
using System.Linq;
using Assessment.DAL.Context;
using Assessment.DAL.Entities;
using Assessment.DAL.Helper;
using Assessment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assessment.DAL.Repositories
{
	public abstract class BaseRepo<T> : IBaseRepo<T>
		where T : BaseEnt
	{
		#region Protected Fields

		protected readonly AssessmentContext Context;

		#endregion Protected Fields

		#region Protected Constructors

		protected BaseRepo(AssessmentContext context)
		{
			Context = context;
		}

		#endregion Protected Constructors

		#region Public Methods

		public virtual T Add(T entity)
		{
			var entry = Context.Set<T>().Add(entity);

			entry.State = EntityState.Added;
			Context.SaveChanges();

			return entry.Entity;
		}

		public virtual IQueryable<T> Find(Func<T, bool> predicate)
		{
			return Context.Set<T>().Where(predicate).AsQueryable();
		}

		public virtual IQueryable<T> GetAll()
		{
			return Context.Set<T>();
		}

	    public virtual IQueryable<T> GetAll(string jsonFilter)
	    {
	        return Context.Set<T>().FromSql(JSONFilterConvertor.ReturnSQLFilterString(jsonFilter));
	    }


        public virtual T GetById(int id)
		{
			return Context.Set<T>().FirstOrDefault(x => x.Id == id);
		}

		public virtual T Update(T entity)
		{
			var entry = Context.Set<T>().Update(entity);

			entry.State = EntityState.Modified;
			Context.SaveChanges();

			return entry.Entity;
		}

		#endregion Public Methods
	}
}