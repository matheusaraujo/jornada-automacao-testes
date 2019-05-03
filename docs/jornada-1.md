[ _DRAFT_ ]

[Jornada](jornada)

---

## Testes externos ao código

---

> Uma definição implícita para a automação de testes é a definição de **fronteira**. Sempre que se vai automatizar algum teste, é necessário definir um limiar entre o que será testado e o que será _mockado_.
>
> _Mockar_ é basicamente criar um estado/objeto/situação falsa. A partir da manipulação dos _mocks_, tem-se um estado controlado; para esse estado, a resposta da aplicação é determinística. 
>
> Após essa primeira etapa de preparação (**Given**), quando a aplicação for executada (**When**), então o resultado obtido é avaliado contra um resultado esperado (**Then**). Se o resultado for o esperado, o teste passou não; senão, não.

O código legado dessa aplicação não está preparado para executar os testes automatizados. O nível de acoplamento entre suas camadas é tão alto que não é possível separarmos o que será testado do que será mockado.

Para esse cenário, uma abordagem é tratar o código como uma caixa-preta e usar uma aplicação externa para testar a aplicação. À medida que os testes forem implementados, é possível refatorar o código de forma a torná-lo mais _testável_.

---

```
git checkout v0.8
```

[TO-DO] Falar sobre o método `IniciarTestes`

---

```
git checkout v0.9
```

[TO-DO] Falar sobre o testes via `Jest`

---  

```
git checkout v1.0
```

[TO-DO] Falar sobre o problema da data