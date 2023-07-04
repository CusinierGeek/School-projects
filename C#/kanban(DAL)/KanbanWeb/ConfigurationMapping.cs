using AutoMapper;
using KanbanDonnees.Entities;
using KanbanWeb.DTO;

namespace KanbanWeb;

public class ConfigurationMapping : Profile
{
    public ConfigurationMapping()
    {
        CreateMap<Liste, PatchListeDto>();
        CreateMap<PatchListeDto, Liste>();
        CreateMap<PutCarteDto, Carte>();
        CreateMap<Carte, PutCarteDto>();
        CreateMap<PatchCarteDto, Carte>();
        CreateMap<Carte, PatchCarteDto>();
        
    }
}