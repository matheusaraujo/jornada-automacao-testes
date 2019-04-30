import { URL_API } from '../utils/constants';
import { post, get } from './requests';

export const postPedidos = (contratoId, pedido) => post(`${URL_API}/contratos/${contratoId}/pedidos`, pedido);