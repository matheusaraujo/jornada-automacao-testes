using GestaoContratos.Dominio.Dto;
using GestaoContratos.Interface.Processo;
using GestaoContratos.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContratos.Teste.Integracao
{
    [TestClass]
    public class TesteObterPedidos
    {
        private ITesteProcesso _testeProcesso;
        private IContratoProcesso _contratoProcesso;
        private IPedidoProcesso _pedidoProcesso;

        [TestInitialize]
        public void IniciarTestes()
        {
            _testeProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<ITesteProcesso>();
            _contratoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IContratoProcesso>();
            _pedidoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IPedidoProcesso>();

            _testeProcesso.IniciarTestes();
        }

        [TestMethod]
        public void Teste_ObterPedidos_Vazio()
        {
            var pedidos = _pedidoProcesso.ObterPedidos(1);
            Assert.IsTrue(pedidos == null || pedidos.Count == 0);
        }

        [TestMethod]
        public void Teste_ObterContratos_UmContrato()
        {
            _contratoProcesso.InserirContrato(new ContratoDto()
            {
                ContratoId = 1,
                VolumeDisponivel = 100,
                DataInicioVigencia = ExtensaoDateTime.DataInicioMesAtual(),
                DataFimVigencia = ExtensaoDateTime.DataFimMesAtual(),
                Ativo = true
            });
            
            _pedidoProcesso.InserirPedido(new PedidoDto()
            {
                PedidoId = 1,
                ContratoId = 1,
                Volume = 5,
                DataPedido = ExtensaoDateTime.DataAtual(),
                Atendido = false
            });

            var pedidos = _pedidoProcesso.ObterPedidos(1);
            Assert.AreEqual(1, pedidos.Count);
            Assert.AreEqual(1, pedidos.First().PedidoId);
        }
    }
}
