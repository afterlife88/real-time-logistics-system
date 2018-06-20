using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.TransactionQueries
{
    public class TransactionByIdQuery : IQuery<TransactionDTO>
    {
        public TransactionByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
    public class TransactionByIdQueryHandler : IQueryHandler<TransactionByIdQuery, TransactionDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TransactionDTO Execute(TransactionByIdQuery context)
        {
            var entity = _unitOfWork.TransactionRepository.GetById(context.Id);
            return Mapper.Map<Transaction, TransactionDTO>(entity);
        }

    }
}
