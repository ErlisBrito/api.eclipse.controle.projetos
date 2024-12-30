using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;
using System.Reflection;

namespace api.eclipse.controle.projetos.Application.Services
{
    public class TarefasAppServices : ITarefasAppServices
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IHistoricoTarefaAppServices _historicoTarefaAppServices;
        private readonly IMapper _mapper;

        public TarefasAppServices(ITarefaRepository tarefaRepository, IMapper mapper, IHistoricoTarefaAppServices historicoTarefaAppServices)
        {
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
            _historicoTarefaAppServices = historicoTarefaAppServices;
        }

        public async Task<Resultado<TarefaViewModel>> SalvarTarefaAsync(TarefaViewModel model)
        {
            try
            {
                if (_tarefaRepository.ListaTarefaPeloProjetoId(model.ProjetoId).Count > 20)
                    return new Resultado<TarefaViewModel>()
                    {
                        Mensagem = $"Limite máximo de tarefas atingido: Cada projeto pode conter no máximo 20 tarefas. Não é possível adicionar mais tarefas a este projeto.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                    };
                var tarefa = _mapper.Map<Tarefa>(model);
                tarefa.DataCadastro = DateTime.Now;
                await _tarefaRepository.SalvarTarefaAsync(tarefa);
                await _historicoTarefaAppServices.SalvarHistoricoTarefaAsync(tarefa, "Criando");
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Tarefa cadastrada com sucesso!",
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Erro ao salvar o Tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<TarefaViewModel>> AtualizarTarefaAsync(TarefaViewModel model)
        {
            try
            {
                var prioridade = await _tarefaRepository.ObterTarefaAsync(model.Id);

                var tarefa = _mapper.Map<Tarefa>(model);
                if (prioridade.ProjetoId != tarefa.ProjetoId)
                {
                    tarefa.ProjetoId = prioridade.ProjetoId;
                }
                tarefa.DataAlteracao = DateTime.Now.Date;
                await _tarefaRepository.AtualizarTarefaAsync(tarefa);
                await _historicoTarefaAppServices.SalvarHistoricoTarefaAsync(tarefa, "Atualizando");
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Tarefa Atualizada com sucesso!",
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Erro ao Atualizar o Tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<TarefaViewModel>> ObterTarefaAsync(int id)
        {
            try
            {
                var response = await _tarefaRepository.ObterTarefaAsync(id);
                var tarefa = _mapper.Map<TarefaViewModel>(response);
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Tarefa Atualizada com sucesso!",
                    Model = tarefa,
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Erro ao Atualizar o Tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }

        }

        public async Task<Resultado<List<TarefaViewModel>>> ListarTarefasAsync()
        {
            try
            {
                var response = await _tarefaRepository.ListarTarefasAsync();
                var lstProjetoViewModel = _mapper.Map<List<TarefaViewModel>>(response);
                var resultado = new Resultado<List<TarefaViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProjetoViewModel
                };
                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<TarefaViewModel>>()
                {
                    Mensagem = $"Erro ao listar as tarefas: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<List<RelatorioDesempenhoViewModel>>> GerarRelatorioDesempenhoAsync()
        {
            try
            {
                var response = await _tarefaRepository.GerarRelatorioDesempenhoAsync();
                var lstProjetoViewModel = _mapper.Map<List<RelatorioDesempenhoViewModel>>(response);
                var resultado = new Resultado<List<RelatorioDesempenhoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProjetoViewModel
                };
                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<RelatorioDesempenhoViewModel>>()
                {
                    Mensagem = $"Erro ao gerar relatorio de desempenho: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<List<TarefaViewModel>>> ListarTarefaPorProjetoAsync(int projetoId)
        {
            try
            {
                var response = _tarefaRepository.ListaTarefaPeloProjetoId(projetoId);
                var lstProjetoViewModel = _mapper.Map<List<TarefaViewModel>>(response);
                var resultado = new Resultado<List<TarefaViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProjetoViewModel
                };
                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<TarefaViewModel>>()
                {
                    Mensagem = $"Erro ao listar as tarefas: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<Resultado<TarefaViewModel>> DeletarTarefaAsync(int id)
        {
            try
            {
                var tarefa = await _tarefaRepository.ObterTarefaAsync(id);
                _tarefaRepository.DeletarTarefaAsync(tarefa);
                await _historicoTarefaAppServices.SalvarHistoricoTarefaAsync(tarefa, "Deletando");

                var resultado = new Resultado<TarefaViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Mensagem = "Tarefa Excluída com sucesso!"
                };
                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<TarefaViewModel>()
                {
                    Mensagem = $"Erro ao Deletar a tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }

        }
    }
}
