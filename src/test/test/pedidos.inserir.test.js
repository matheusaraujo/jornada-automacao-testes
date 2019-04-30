import { postPedidos } from './functions/pedidos';
import { postIniciarTestes } from './functions/init';
import { postContratos, getContrato } from './functions/contratos';
import { contratoAtivo, contratoInativo } from './mocks/contratos.mock';

describe('Operação inserir pedido', () => {

  beforeAll(async () => {
    await postIniciarTestes();
    await postContratos(contratoAtivo);
    await postContratos(contratoInativo);
  });

  it('Erro 3001', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 0.5,
      "dataPedido": "2019-01-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(1, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3001);
  });

  it('Erro 3002', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 5,
      "dataPedido": "2019-01-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(1, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3002);
  });

  it('Erro 3003', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 999,
      "volume": 5,
      "dataPedido": "2019-05-01T00:00:00.000",
      "atendido": true
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3003);
  });

  it('Erro 3004', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 999,
      "volume": 5,
      "dataPedido": "2019-06-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3004);
  });

  it('Erro 3005', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 2,
      "volume": 5,
      "dataPedido": "2019-06-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3005);
  });

  it('Erro 3006', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 999,
      "dataPedido": "2019-06-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3006);
  });

  it('Erro 3007', async () => {

    const pedido = {
      "pedidoId": 1,
      "contratoId": 1,
      "volume": 5,
      "dataPedido": "2020-06-01T00:00:00.000",
      "atendido": false
    };

    const data = await postPedidos(pedido.contratoId, pedido);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3007);
  });

  it('Sucesso', async () => {

    const contratoId = 1;
    const pedido = {
      "pedidoId": 1,
      "contratoId": contratoId,
      "volume": 5,
      "dataPedido": "2019-06-01T00:00:00.000",
      "atendido": false
    };

    const data1 = await getContrato(contratoId);
    expect(data1.body).toEqual(contratoAtivo);

    const data2 = await postPedidos(pedido.contratoId, pedido);
    expect(data2.status).toBe(201);
    expect(data2.body).toBe(1);

    const contratoAtualizado = Object.assign({}, contratoAtivo, { volumeDisponivel: 95 });

    const data3 = await getContrato(contratoId);
    expect(data3.body).toEqual(contratoAtualizado);

  });

});