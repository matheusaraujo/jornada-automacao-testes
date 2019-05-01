export const dataAtual = () => {
  const h = new Date();
  return new Date(h.getFullYear(), h.getMonth(), h.getDate(), 0, 0, 0, 0);
};

export const adicionarDias = (data, dias) => {
  if (Object.prototype.toString.call(data) !== "[object Date]")
    data = new Date(data);
  const novaData = new Date(data);
  novaData.setDate(data.getDate() + dias);
  return novaData;
};

export const dataAtualAdicionarDias = (dias) => {
  return adicionarDias(dataAtual(), dias);
};

export const dataInicioMesAtual = () => {
  const h = dataAtual();
  return new Date(h.getFullYear(), h.getMonth(), 1, 0, 0, 0, 0);
};

export const dataFimMes = (h) => {
  return new Date(h.getFullYear(), h.getMonth() + 1, 0, 0, 0, 0, 0);
};

export const dataFimMesAtual = () => {
  return dataFimMes(dataAtual());
};