import { URL_API } from '../utils/constants';
import { post, put, patch } from './requests';

export const postPedidos = (contratoId, pedido) => post(`${URL_API}/contratos/${contratoId}/pedidos`, pedido);
export const putPedidos = (contratoId, pedidoId, pedido) => put(`${URL_API}/contratos/${contratoId}/pedidos/${pedidoId}`, pedido);
export const patchPedidosAtendido = (contratoId, pedidoId) => patch(`${URL_API}/contratos/${contratoId}/pedidos/${pedidoId}/atendido`);