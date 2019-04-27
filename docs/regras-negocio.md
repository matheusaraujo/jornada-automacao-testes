## Regras da API
- @TO-DO Valores de `id` devem ser os mesmos na query string e nos objetos

## Regras de Negócio

### Inserir Contrato
- @TO-DO Data de início de vigência deve ser menor do que hoje
- @TO-DO Data de fim de vigência deve ser menor do que hoje
- @TO-DO Volume disponível deve ser maior do que hoje

### Atualizar Contrato
- @TO-DO Data de início de vigência deve ser menor do que hoje
- @TO-DO Data de fim de vigência deve ser menor do que hoje
- @TO-DO Volume disponível deve ser maior do que hoje

### Remover Contrato
- @TO-DO Todos os pedidos do contrato devem ser anteriores a hoje

### Inserir Pedido
- @TO-DO O volume do pedido deve ser maior do que 1
- @TO-DO A data-hora do pedido deve ser menor do que a data-hora atual
- @TO-DO O contrato do pedido deve estar ativo
- @TO-DO O volume do pedido deve ser menor ou igual ao volume disponível do contrato
- @TO-DO A data-hora do pedido deve estar entre as datas de vigência do contrato

### Atualizar Pedido
- @TO-DO O volume do pedido deve ser maior do que 1
- @TO-DO A data-hora do pedido deve ser menor do que a data-hora atual
- @TO-DO O contrato do pedido deve estar ativo
- @TO-DO O volume do pedido deve ser menor ou igual ao volume disponível do contrato
- @TO-DO A data-hora do pedido deve estar entre as datas de vigência do contrato