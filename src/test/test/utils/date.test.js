import { dataInicioMesAtual, dataFimMes, dataFimMesAtual, dataAtual, adicionarDias, dataAtualAdicionarDias } from './date';

describe('Testes Utilitários Data', () => {

  it('Data atual', () => {
    const data = dataAtual();
    expect(data).toBeDefined();
    expect(data).toBeInstanceOf(Date);
  });

  it('Adicionar dias', () => {

    const teste = (data, ano, mes, dia) => {
      expect(data.getFullYear()).toBe(ano);
      expect(data.getMonth()).toBe(mes);
      expect(data.getDate()).toBe(dia);
    };

    let data = adicionarDias(new Date(2019, 0, 5), 1);
    teste(data, 2019, 0, 6);

    data = adicionarDias(new Date(2019, 0, 5), -1);
    teste(data, 2019, 0, 4);

    data = adicionarDias(new Date(2019, 0, 5), 31);
    teste(data, 2019, 1, 5);

    data = adicionarDias(new Date(2019, 1, 5), -31);
    teste(data, 2019, 0, 5);

    data = adicionarDias(new Date(2019, 0, 5), 365);
    teste(data, 2020, 0, 5);

    data = adicionarDias(new Date(2019, 0, 5), -365);
    teste(data, 2018, 0, 5);
  });

  it('Data atual adicionar dias', () => {
    const data = dataAtualAdicionarDias(1);
    expect(data).toBeDefined();
    expect(data).toBeInstanceOf(Date);
  });

  it('Data início mês atual', () => {
    const data = dataInicioMesAtual();
    expect(data.getDate()).toBe(1);
  });

  it('Data fim mês', () => {
    const teste = (ano, mes, diaAtual, diaEsperado) => {
      expect(dataFimMes(new Date(ano, mes, diaAtual)).getDate()).toBe(diaEsperado);
    };
    teste(2019, 0, 1, 31);
    teste(2019, 1, 1, 28);
    teste(2019, 2, 1, 31);
    teste(2019, 3, 1, 30);
    teste(2019, 4, 1, 31);
    teste(2019, 5, 1, 30);
    teste(2019, 6, 1, 31);
    teste(2019, 7, 1, 31);
    teste(2019, 8, 1, 30);
    teste(2019, 9, 1, 31);
    teste(2019, 10, 1, 30);
    teste(2019, 11, 1, 31);
    teste(2020, 1, 1, 29);
  });

  it('Data início mês atual', () => {
    const data = dataFimMesAtual();
    const diasMes = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    const mes = data.getMonth();
    if (mes != 1) {
      expect(data.getDate()).toBe(diasMes[mes]);
    }
    else {
      const ano = data.getFullYear();
      expect(data.getDate()).toBe(ano % 4 ? 28 : 29);
    }
  });

});