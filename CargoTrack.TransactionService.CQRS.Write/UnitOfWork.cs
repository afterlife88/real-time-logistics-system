using System;
using System.Collections.Generic;
using CargoTrack.TransactionService.CQRS.Write.Contracts;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;


namespace CargoTrack.TransactionService.CQRS.Write
{
    /// <summary>
    /// Implementation of the unit of work design pattern based on EF
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructors / Destructors

        /// <summary>
        /// Default injectable constructor
        /// </summary>
        /// <param name="dataDbContext"></param>
        /// <param name="balanceRepository"></param>
        /// <param name="ledgerTransactionRepository"></param>
        /// <param name="transactionRepository"></param>
        public UnitOfWork(DataDbContext dataDbContext, IBalanceRepository balanceRepository,
            ILedgerTransactionRepository ledgerTransactionRepository, ITransactionRepository transactionRepository)
        {
            
            DataDbContext = dataDbContext;
            BalanceRepository = balanceRepository;
            LedgerTransactionRepository = ledgerTransactionRepository;
            TransactionRepository = transactionRepository;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Db context
        /// </summary>
        public DataDbContext DataDbContext { get; set; }

        #endregion

        #region Repositories

        public IBalanceRepository BalanceRepository { get; }
        public ILedgerTransactionRepository LedgerTransactionRepository { get; }
        public ITransactionRepository TransactionRepository { get; }

        #endregion

        #region IUnitOfWork

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            
            DataDbContext.SaveChanges();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataDbContext?.Dispose();
            }
        }
        #endregion
    }
}
