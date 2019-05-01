[Jornada](jornada)

---

## Testes usando ambiente de desenvolvimento

---

```
git checkout v1.8
```

[TO-DO] `v1.8` Refatorar código dividindo em camadas

Alterações:
- Criados quatro novos projetos:
- - `GestaoContratos.Processo`
  - `GestaoContratos.Dominio`
  - `GestaoContratos.Repositorio`
  - `GestaoContratos.Util`
- A classe estática `Repositorio` foi dividida em duas:
  - `ContratoRepositorio`
  - `PedidoRepositorio`
- As regras de negócio foram movidas para as classes de processo, com isso será possível testar diretamente as classes de processo.
- A refatoração feita foi apenas na estrutura das classes, não houve refatoração de funções

![Diagrama de dependências](jornada-2/diagrama-dependencias.png)

---

```
git checkout v1.9
```

[TO-DO] `v1.9` Implementar testes da camada de negócio, não implemtar função de estado inicial

---

```
git checkout v2.0
```

[TO-DO] `v2.0` Resolver problema de estado inicial