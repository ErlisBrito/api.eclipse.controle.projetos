using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;

namespace api.eclipse.controle.projetos.Application.AutoMappers
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Projetos, ProjetoViewModel>();
            CreateMap<Tarefa, TarefaViewModel>();
            //CreateMap<Pedido, PedidoViewModel>();
        }
    }
}
