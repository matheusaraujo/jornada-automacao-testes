import { dataInicioMesAtual, dataFimMesAtual } from "../utils/date";

export const contratoAtivo = {
  "contratoId": 1,
  "volumeDisponivel": 100,
  "dataInicioVigencia": dataInicioMesAtual().toJSON(),
  "dataFimVigencia": dataFimMesAtual().toJSON(),
  "ativo": true
};

export const contratoInativo = {
  "contratoId": 2,
  "volumeDisponivel": 100,
  "dataInicioVigencia": dataInicioMesAtual().toJSON(),
  "dataFimVigencia": dataFimMesAtual().toJSON(),
  "ativo": false
};