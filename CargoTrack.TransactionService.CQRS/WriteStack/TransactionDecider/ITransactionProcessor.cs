using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;

namespace CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider
{
    public interface ITransactionProcessor
    {
        void DeliveryToStore(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void ReceiveCargoFromSupplier(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void StatusCorrection(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void BuyCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void SellCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void ManualTransferOfCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
        void TrashCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo);
    }
}
