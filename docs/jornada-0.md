[ _DRAFT_ ]

[Jornada](jornada)

---

## Versão inicial sem testes

---

```
git checkout v0.0
```

O diagrama de classes abaixo mostra a estrutura inicial do código.

O namespace `GestaoContratos.Repository` tem apenas uma classe estática `Repositorio` que é a única responsável por acessar o banco de dados.

O namespace `GestaoContratos.Controllers` contém um `Controller` pra cada entidade, nessas classes estão as definições de API e também as regras de negócio.

No namespace `GestaoContratos.Models` estão as estruturas de dados.

![Diagrama de classes](jornada-0/diagrama-classes.png)