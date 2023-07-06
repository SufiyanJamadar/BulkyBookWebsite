﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{

		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbset;


        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this.dbset = _db.Set<T>();
			/// _db.Categories == dbset
			_db.Products.Include(u=>u.Category).Include(u => u.CategoryId);
			
        }
        public void Add(T entity)
		{
			dbset.Add(entity);
		}

		public void Delete(T entity)
		{
			//throw new NotImplementedException();
			dbset.Remove(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query=dbset;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
				   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			query = query.Where(filter);

			return query.FirstOrDefault();
		}

		/// Category,CoverType
		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbset;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				 foreach (var includeProp  in includeProperties
					.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				 {
					query = query.Include(includeProp);
				 }
			}
			return query.ToList();
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbset.RemoveRange(entity);
		}
	}
}
