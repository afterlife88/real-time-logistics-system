using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;

namespace CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider
{
    public interface ITransactionEngine
    {
        int ProccessTransaction(AddTransactionCommand cmd, CargoTypeDetailedDTO cargoDTO);
    }
}
