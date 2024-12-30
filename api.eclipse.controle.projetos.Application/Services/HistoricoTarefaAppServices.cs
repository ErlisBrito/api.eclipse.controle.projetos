using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.ViewModels;
using api.eclipse.controle.projetos.Common;
using api.eclipse.controle.projetos.Domain.Interfaces;
using api.eclipse.controle.projetos.Domain.Models;
using AutoMapper;
using System.Text.Json;

namespace api.eclipse.controle.projetos.Application.Services
{
    public class HistoricoTarefaAppServices : IHistoricoTarefaAppServices
    {
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IMapper _mapper;
        public HistoricoTarefaAppServices(IMapper mapper, IHistoricoTarefaRepository historicoTarefaRepository, ITarefaRepository tarefaRepository)
        {
            _historicoTarefaRepository = historicoTarefaRepository;
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
        }

        public async Task<Resultado<HistoricoTarefaViewModel>> SalvarHistoricoTarefaAsync(Tarefa model, string acao)
        {
            try
            {
                var historicoTarefa = await MontarSalvamentoDoHistoricoTarefa(model, acao);
                await _historicoTarefaRepository.SalvarHistoricoTarefaAsync(historicoTarefa);
                return new Resultado<HistoricoTarefaViewModel>()
                {
                    Mensagem = $"Tarefa cadastrada com sucesso!",
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Resultado<HistoricoTarefaViewModel>()
                {
                    Mensagem = $"Erro ao salvar o Tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }

        }


        public async Task<Resultado<HistoricoTarefaViewModel>> SalvarComentariosTarefaAsync(HistoricoTarefaViewModel model, string acao)
        {
            try
            {
                var historicoTarefa = await MontarEnvioComentarioTarefaAsync(model, acao);
                await _historicoTarefaRepository.SalvarHistoricoTarefaAsync(historicoTarefa);
                return new Resultado<HistoricoTarefaViewModel>()
                {
                    Mensagem = $"Tarefa cadastrada com sucesso!",
                    StatusCode = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new Resultado<HistoricoTarefaViewModel>()
                {
                    Mensagem = $"Erro ao salvar o Tarefa: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }

        }

        public  async Task<HistoricoTarefa> MontarSalvamentoDoHistoricoTarefa(Tarefa model, string acao)
        {

            var historicoTarefa = new HistoricoTarefa()
            {
                DataCadastro = DateTime.Now,
                UsuarioId = model.UsuarioId,
                TarefaId = model.Id,
                ObjetoDeEnvio = JsonSerializer.Serialize(model),
                Acao = acao
            };
            return historicoTarefa;
        }


        public  async Task<HistoricoTarefa> MontarEnvioComentarioTarefaAsync(HistoricoTarefaViewModel model, string acao)
        {

            var historicoTarefa = new HistoricoTarefa()
            {
                DataCadastro = DateTime.Now,
                UsuarioId = model.UsuarioId,
                TarefaId = model.TarefaId,
                Descricao = model.Descricao,               
                Acao = acao
            };
            return historicoTarefa;
        }
    }
}
