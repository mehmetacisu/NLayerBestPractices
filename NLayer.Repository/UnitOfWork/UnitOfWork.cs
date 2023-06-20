﻿using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public Task CommitAsync()
		{
			return _context.SaveChangesAsync();
		}
	}
}
