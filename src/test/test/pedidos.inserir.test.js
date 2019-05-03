import { postPedidos } from './functions/pedidos';
import { postIniciarTestes } from './functions/init';
import { postContratos, getContratos } from './functions/contratos';
import { contratoAtivo, contratoInativo } from './mocks/contratos.mock';
import { dataAtual, dataAtualAdicionarDias, adicionarDias, dataFimMesAtual } from './utils/date';

describe('Inserir pedido', () => {

  beforeAll(async () => {
    await postIniciarTestes();
    await postContratos(contratoAtivo);
    await postContratos(contratoInativo);
  });

  it('Erro 3001 - O volume do pedido deve ser maior ou igual a 1.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 0.5,
      "dataPedido": dataAtual(),
      "atendido": false
    };

    const data = await postPedidos(1, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3001);
  });

  it('Erro 3002 - A data do pedido deve ser maior ou igual à data atual.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 5,
      "dataPedido": dataAtualAdicionarDias(-1),
      "atendido": false
    };

    const data = await postPedidos(1, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3002);
  });

  it('Erro 3003 - O pedido deve ser criado com status não atendido.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 999,
      "volume": 5,
      "dataPedido": dataAtual(),
      "atendido": true
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3003);
  });

  it('Erro 3004 - O pedido deve ser criado para um contrato existente.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 999,
      "volume": 5,
      "dataPedido": dataAtual(),
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3004);
  });

  it('Erro 3005 - O contrato do pedido deve estar ativo', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 2,
      "volume": 5,
      "dataPedido": dataAtual(),
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3005);
  });

  it('Erro 3006 - O volume do pedido deve ser menor ou igual ao volume disponível do contrato.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 999,
      "dataPedido": dataAtual(),
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3006);
  });

  it('Erro 3007 - A data do pedido deve estar entre as datas de vigência do contrato.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 5,      
      "dataPedido": adicionarDias(dataFimMesAtual(), 1),
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3007);
  });

  it('Sucesso - Pedido criado com sucesso.', async () => {
    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 5,
      "dataPedido": dataAtual(),
      "atendido": false
    };

    const data1 = await getContratos(1);
    expect(data1.body).toEqual(contratoAtivo);

    const data2 = await postPedidos(1, pedido);
    expect(data2.status).toBe(201);
    expect(data2.body).toBe(1);

    const data3 = await getContratos(1);
    const contratoAtualizado = Object.assign({}, contratoAtivo, { volumeDisponivel: 95 });
    expect(data3.body).toEqual(contratoAtualizado);
  });

});