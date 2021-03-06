import { postPedidos, putPedidos, patchPedidosAtendido } from "./functions/pedidos";
import { pedidoPadrao } from "./mocks/pedidos.mock";
import { postIniciarTestes } from "./functions/init";
import { postContratos, putContratos, getContratos } from "./functions/contratos";
import { contratoAtivo } from "./mocks/contratos.mock";
import { dataAtualAdicionarDias } from "./utils/date";

describe('Editar pedido', () => {
  beforeAll(async () => {
    await postIniciarTestes();
    await postContratos(contratoAtivo);
    const data = await postPedidos(pedidoPadrao.contratoId, pedidoPadrao);
    expect(data.status).toBe(201);
    expect(data.body).toBe(1);
  });

  it('Erro 3001 - O volume do pedido deve ser maior ou igual a 1.', async () => {
    const pedidoEditado = Object.assign({}, pedidoPadrao, { volume: 0.5 });
    const data = await putPedidos(1, 1, pedidoEditado);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3001);
  });

  it('Erro 3002 - A data do pedido deve ser maior ou igual à data atual.', async () => {
    const pedidoEditado = Object.assign({}, pedidoPadrao, { dataPedido: dataAtualAdicionarDias(-60).toJSON() });
    const data = await putPedidos(1, 1, pedidoEditado);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3002);
  });

  it('Erro 3005 - O contrato do pedido deve estar ativo.', async () => {
    const contratoEditadoInativo = Object.assign({}, contratoAtivo, { ativo: false });
    await putContratos(1, contratoEditadoInativo);

    const pedidoEditado = Object.assign({}, pedidoPadrao);
    const data = await putPedidos(1, 1, pedidoEditado);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3005);

    const contratoEditadoAtivo = Object.assign({}, contratoAtivo, { ativo: true });
    await putContratos(1, contratoEditadoAtivo);
  });

  it('Erro 3006 - O volume do pedido deve ser menor ou igual ao volume disponível do contrato.', async () => {
    const pedidoEditado = Object.assign({}, pedidoPadrao, { volume: 150 });
    const data = await putPedidos(1, 1, pedidoEditado);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3006);
  });

  it('Erro 3007 - A data do pedido deve estar entre as datas de vigência do contrato.', async () => {
    const pedidoEditado = Object.assign({}, pedidoPadrao, { dataPedido: dataAtualAdicionarDias(60).toJSON() });
    const data = await putPedidos(1, 1, pedidoEditado);
    expect(data.status).toBe(412);
    expect(data.body.codigoErro).toBe(3007);
  });

  it('Erro 3008 - O pedido deve ter status não atendido para ser editado.', async () => {
    const data1 = await patchPedidosAtendido(1, 1);
    expect(data1.status).toBe(204);

    const pedidoEditado = Object.assign({}, pedidoPadrao);
    const data2 = await putPedidos(1, 1, pedidoEditado);
    expect(data2.status).toBe(412);
    expect(data2.body.codigoErro).toBe(3008);
  });

  it('Sucesso - Pedido editado com sucesso.', async () => {
    const novoPedido = Object.assign({}, pedidoPadrao, { pedidoId: 2 });
    const data1 = await postPedidos(1, novoPedido);
    expect(data1.status).toBe(201);
    expect(data1.body).toBe(2);

    const pedidoEditado = Object.assign({}, novoPedido, { volume: 3 });
    const data2 = await putPedidos(1, 2, pedidoEditado);
    expect(data2.status).toBe(204);

    const contratoAtualizado = Object.assign({}, contratoAtivo, { volumeDisponivel: 97 });
    const data3 = await getContratos(1);
    expect(data3.body).toEqual(contratoAtualizado);
  });

});