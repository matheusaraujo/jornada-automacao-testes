[Jornada automação de Testes](index)

---

O modelo de negócio utilizado como exemplo para essa jornada é simples, mas não simplista. É um modelo reduzido de uma aplicação que faz uma gestão de pedidos e contratos.

Nessa aplicação, Contratos são entidades que possuem os seguintes atributos:
- Volume disponível
- Período de vigência
- Status: ativo ou não

Os Pedidos são associados aos contratos e têm os atributos:
- Volume
- Data do pedido
- Status: atendido ou não

A entidade Contrato tem as operações básicas de criação, edição, deleção e leitura.

A entidade Pedido, além dessas, tem a operação de _Atender_ o pedido, i.e., alterar seu status de não atendido para atendido.

A criação de um pedido deve ser feita para um contrato ativo, vigente e com volume disponível. Assim como a edição.

Um pedido deve ser criado com o status _não atendido_ e só pode ser editado enquanto estiver nesse estado.

Um contrato só pode ser removido se não tiver pedidos associados.

Todas as [regras de negócio](regras-negocio) estão detalhadas para as operações e com os códigos de exceção [nesse link](regras-negocio).