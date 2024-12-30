using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Tests.Contexts
{
    [TestFixture]
    public class EclipseContext_Test
    {
        private EclipseContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EclipseContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new EclipseContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
        [Test]
        public void DbSets_DeveEstarMapeadasCorretamente()
        {
            // Act
            var projetosDbSet = _context.Set<Projetos>();
            var tarefaDbSet = _context.Set<Tarefa>();
            var historicoTarefaDbSet = _context.Set<HistoricoTarefa>();

            // Assert
            Assert.IsNotNull(projetosDbSet);
            Assert.IsNotNull(tarefaDbSet);
            Assert.IsNotNull(historicoTarefaDbSet);
        }

        [Test]
        public void Contexto_DeveDesabilitarQueryTracking()
        {
            // Act
            var changeTrackerBehavior = _context.ChangeTracker.QueryTrackingBehavior;

            // Assert
            Assert.AreEqual(QueryTrackingBehavior.NoTracking, changeTrackerBehavior);
        }

        [Test]
        public void Contexto_DeveDesabilitarAutoDetectChanges()
        {
            // Act
            var autoDetectChanges = _context.ChangeTracker.AutoDetectChangesEnabled;

            // Assert
            Assert.IsFalse(autoDetectChanges);  // Deve estar desabilitado
        }

       
    }
}
