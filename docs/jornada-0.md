[Jornada](jornada)

---

```
git checkout v0.0
```

O diagrama de classes abaixo mostra a estrutura inicial do código.

O namespace `GestaoContratos.Repository` tem apenas uma classe estática `Repositorio` que é a única responsável por acessar o banco de dados.

O namespace `GestaoContratos.Controllers` contém um `Controller` pra cada domínio, nessas classes estão as definições de API e também as regras de negócio.

No namespace `GestaoContratos.Models` estão as estruturas de dados.

![Diagrama de classes](jornada-0/diagrama.png)

---

[Etapa 1](jornada-1)