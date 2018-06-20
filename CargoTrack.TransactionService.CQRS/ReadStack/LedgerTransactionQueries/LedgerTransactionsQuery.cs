using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.LedgerTransactionQueries
{
    public class LedgerTransactionsQuery : IQuery<IEnumerable<LedgerTransactionDTO>>
    { }

    public class LedgerTransactionsQueryHandler : IQueryHandler<LedgerTransactionsQuery, IEnumerable<LedgerTransactionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LedgerTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<LedgerTransactionDTO> Execute(LedgerTransactionsQuery context)
        {
            var entities = _unitOfWork.LedgerTransactionRepository.GetAll().ToList();
            return Mapper.Map<IEnumerable<LedgerTransaction>, IEnumerable<LedgerTransactionDTO>>(entities);
        }
    }
}
