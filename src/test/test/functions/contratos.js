import { URL_API } from '../utils/constants';
import { post, get } from './requests';

export const postContratos = (contrato) => post(`${URL_API}/contratos`, contrato);
export const getContrato = (contratoId) => get(`${URL_API}/contratos/${contratoId}`);