import { URL_API } from '../utils/constants';
import { post } from './requests';

export const postIniciarTestes = () => post(`${URL_API}/testes`);