using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;
using System.Reflection;

namespace api.eclipse.controle.projetos.Application.Services
{
    public class ProjetosAppServices : IProjetosAppServices
    {
        private readonly IProjetosRepository _projetosRepository;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;

        public ProjetosAppServices(IProjetosRepository projetosRepository, ITarefaRepository tarefaRepository, IMapper mapper)
        {
            _projetosRepository = projetosRepository;
            _tarefaRepository=tarefaRepository;
            _mapper = mapper;
        }

        public async Task<Resultado<List<ProjetoViewModel>>> ListarProjetosAsync()
        {
            try
            {
                var response = await _projetosRepository.ListarProjetosAsync();
                var lstProjetoViewModel = _mapper.Map<List<ProjetoViewModel>>(response);
                var resultado = new Resultado<List<ProjetoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProjetoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ProjetoViewModel>>()
                {
                    Mensagem = $"Erro ao listar os Projetos: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<List<ProjetoViewModel>>> ListarProjetosPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var response =  _projetosRepository.ListarProjetosPorUsuario(usuarioId);
                var lstProjetoViewModel = _mapper.Map<List<ProjetoViewModel>>(response);
                var resultado = new Resultado<List<ProjetoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProjetoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ProjetoViewModel>>()
                {
                    Mensagem = $"Erro ao listar os Projetos: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }


        public async Task<Resultado<ProjetoViewModel>> ObterProjetoAsync(int id)
        {
            try
            {
                var response = await _projetosRepository.ObterProjetoAsync(id);
                var result = _mapper.Map<ProjetoViewModel>(response);
                var resultado = new Resultado<ProjetoViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = result
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<ProjetoViewModel>()
                {
                    Mensagem = $"Erro ao obter o Projeto: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<ProjetoViewModel>> SalvarProjetoAsync(ProjetoViewModel model)
        {
            try
            {
                var projeto = _mapper.Map<Projetos>(model);
                projeto.DataCadastro = DateTime.Now;
                await _projetosRepository.SalvarProjetoAsync(projeto);
                return Resultado<ProjetoViewModel>.SucessoMensagem("Projeto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Resultado<ProjetoViewModel>()
                {
                    Mensagem = $"Erro ao salvar o Projeto: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<ProjetoViewModel>> AtualizarProjetoAsync(ProjetoViewModel model)
        {
            try
            {
                var projetos = _mapper.Map<Projetos>(model);
                projetos.DataAlteracao = DateTime.Now.Date;
                await _projetosRepository.AtualizarProjetoAsync(projetos);
                return Resultado<ProjetoViewModel>.SucessoMensagem("Projeto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Resultado<ProjetoViewModel>()
                {
                    Mensagem = $"Erro ao Atualizar o Projeto: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<ProjetoViewModel>> DeletarProjetoAsync(int id)
        {
            try
            {
                var tarefasAbertas = await ValidarDelecaoProjetoAsync(id);
                if (!tarefasAbertas)
                    return Resultado<ProjetoViewModel>.ErroMensagem("Existem tarefas pendentes neste projeto. Finalize-as para poder excluir o projeto.");

                var projeto = await _projetosRepository.ObterProjetoAsync(id);  
                await _projetosRepository.DeletarProjetoAsync(projeto);
                return Resultado<ProjetoViewModel>.SucessoMensagem("Projeto Excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return new Resultado<ProjetoViewModel>()
                {
                    Mensagem = $"Erro ao Deletar o Projeto: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }

        }  
        
        private async Task<bool> ValidarDelecaoProjetoAsync(int id)
        {
            var tarefasAbertas =  _tarefaRepository.ObterTarefaPeloProjetoId(id);
            if (tarefasAbertas.Count()> (int)default)
                return false;
            var tarefas =  _tarefaRepository.ListaTarefaPeloProjetoId(id);
             _tarefaRepository.DeletarTarefas(tarefas);
            return true;

        }
    }
}
