import { URL_API } from '../utils/constants';
import { post, put, get } from './requests';

export const postContratos = (contrato) => post(`${URL_API}/contratos`, contrato);
export const putContratos = (contratoId, contrato) => put(`${URL_API}/contratos/${contratoId}`, contrato);
export const getContratos = (contratoId) => get(`${URL_API}/contratos/${contratoId}`);