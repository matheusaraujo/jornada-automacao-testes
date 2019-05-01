import { dataAtual } from "../utils/date";

export const pedidoPadrao = {
  "pedidoId": 1,
  "contratoId": 1,
  "volume": 5,
  "dataPedido": dataAtual().toJSON(),
  "atendido": false
};