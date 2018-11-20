using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.DAL.Repositories.Interfaces
{
	public interface IBaseRepo<T>
	{
		#region Public Methods

		T Add(T entity);

		IQueryable<T> Find(Func<T, bool> predicate);

	    IQueryable<T> GetAll();

	    IQueryable<T> GetAll(string jsonFilter);


        T GetById(int id);

		T Update(T entity);

		#endregion Public Methods
	}
}