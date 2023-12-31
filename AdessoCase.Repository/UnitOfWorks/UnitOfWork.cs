﻿using AdessoCase.Core.UnitOfWorks;

namespace AdessoCase.Repository.UnitOfWorks
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

        public async Task CommitAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }
    }
}
