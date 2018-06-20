using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.Contracts.Models.DTO.Commands;
using CargoTrack.TransactionService.CQRS.Common.Commands;
using CargoTrack.TransactionService.CQRS.Read.Entities;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;

namespace CargoTrack.TransactionService.Configuration
{
    /// <summary>
    /// Automapper configuration
    /// </summary>
    public class AutomapperConfiguration
    {
        /// <summary>
        /// Configuration of automapper maps
        /// </summary>
        public static void Load()
        {
            Mapper.Initialize(config =>
            {
                // Model => DTO
                config.CreateMap<Command, CommandDTO>().ReverseMap();
                config.CreateMap<AddTransactionCommand, AddTransactionCommandDTO>().ReverseMap();

                config.CreateMap<Balance, BalanceDTO>()
                    .ForMember(src => src.CargoBalance, dest => dest.MapFrom(k => k.Amount))
                    .ReverseMap()
                    .ForMember(src => src.Amount, dest => dest.MapFrom(k => k.CargoBalance));
                config.CreateMap<LedgerTransaction, LedgerTransactionDTO>().ReverseMap();
                config.CreateMap<LedgerTransactionDetailed, LedgerTransactionDetailedDTO>().ReverseMap();
            });
        }
    }
}
