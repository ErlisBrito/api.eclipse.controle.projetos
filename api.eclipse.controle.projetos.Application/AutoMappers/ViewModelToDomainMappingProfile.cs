using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;

namespace api.eclipse.controle.projetos.Application.AutoMappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProjetoViewModel, Projetos>();
            CreateMap<TarefaViewModel, Tarefa>();
            CreateMap<HistoricoTarefaViewModel, HistoricoTarefa>();
        }
    }
}
