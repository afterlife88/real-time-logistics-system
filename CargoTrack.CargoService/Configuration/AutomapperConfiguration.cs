using System.Web.UI.WebControls;
using AutoMapper;
using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.CargoService.Contracts.Models.Service.Cargo;
using CargoTrack.CargoService.Data.Entities;

namespace CargoTrack.CargoService.Configuration
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
                config.CreateMap<CargoType, CargoTypeDTO>()
                    .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Abbreviation, dto => dto.MapFrom(src => src.Abbreviation))
                    .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id));

                config.CreateMap<CargoType, CargoTypeDetailedDTO>()
                   .ForMember(dest => dest.Category, dto => dto.MapFrom(src => src.Category.Name))
                   .ForMember(dest => dest.Ean, dto => dto.MapFrom(src => src.Ean))
                   .ForMember(dest => dest.Leased, dto => dto.MapFrom(src => src.Leased))
                   .ForMember(dest => dest.Description, dto => dto.MapFrom(src => src.Description))
                   .ForMember(dest => dest.Price, dto => dto.MapFrom(src => src.Price))
                   .ForMember(dest => dest.Abbreviation, dto => dto.MapFrom(src => src.Abbreviation))
                   .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id));

                // Request => model
                config.CreateMap<CreateCargoRequest, CargoType>()
                    .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Abbreviation, dto => dto.MapFrom(src => src.Abbreviation))
                    .ForMember(dest => dest.Category, dto => dto.MapFrom(src => new CargoTypeCategory() { Name = src.Category }))
                    .ForMember(dest => dest.Description, dto => dto.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Leased, dto => dto.MapFrom(src => src.Leased))
                    .ForMember(dest => dest.Price, dto => dto.MapFrom(src => src.Price))
                    .ForMember(dest => dest.Ean, dto => dto.MapFrom(src => src.Ean));


                config.CreateMap<UpdateCargoRequest, CargoType>()
                  .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Abbreviation, dto => dto.MapFrom(src => src.Abbreviation))
                  .ForMember(dest => dest.Category, dto => dto.MapFrom(src => new CargoTypeCategory() { Name = src.Category }))
                  .ForMember(dest => dest.Description, dto => dto.MapFrom(src => src.Description))
                  .ForMember(dest => dest.Leased, dto => dto.MapFrom(src => src.Leased))
                  .ForMember(dest => dest.Price, dto => dto.MapFrom(src => src.Price))
                  .ForMember(dest => dest.Ean, dto => dto.MapFrom(src => src.Ean));
              

            });


        }
    }
}
