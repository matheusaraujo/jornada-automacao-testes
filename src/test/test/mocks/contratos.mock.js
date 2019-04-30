import { dataInicioMesAtual, dataFimMesAtual } from "../utils/date";

export const contratoAtivo = {
  "contratoId": 1,
  "volumeDisponivel": 100,
  "dataInicioVigencia": dataInicioMesAtual(),
  "dataFimVigencia": dataFimMesAtual(),
  "ativo": true
};

export const contratoInativo = {
  "contratoId": 2,
  "volumeDisponivel": 100,
  "dataInicioVigencia": dataInicioMesAtual(),
  "dataFimVigencia": dataFimMesAtual(),
  "ativo": false
};