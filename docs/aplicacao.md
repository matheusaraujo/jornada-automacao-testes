[Jornada automação de Testes](index)

---

O diagrama de classes simplificado abaixo apresenta o modelo de dados da aplicação.

![Classes](classes.png)

A aplicação é uma API `API RESTful`, escrita em `C#`. Em sua versão inicial, o código está todo em uma única camada e há uma separação básica com apenas três divisões:
- As classes de estruturas de dados
- O acesso ao banco de dados
- A aplicação, contendo as regras de negócio e os controladores.

A [Etapa 1](jornada-1) descreve a estrutura de classes do código.

As [Rotas da API](rotas) estão detalhadas [nesse link](rotas).

A aplicação consome apenas um banco de dados. Para simplificar a execução, foi utilizado um banco de dados `sqlite` em arquivo. 

Os resultados de análise do [SonarQube](sonar) por etapa estão apresentados [nesse link](sonar).