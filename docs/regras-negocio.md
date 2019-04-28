## Regras da API
- `1001`, `1002` - Os objetos enviados no corpo da mensagem devem ter o mesmo id dos parâmetros na rota

## Regras de Negócio

### Inserir Contrato
- `2001` - A data de início de vigência do contrato deve ser menor ou igual à data atual!
- `2002` - A data de fim de vigência do contrato deve ser maior ou igual à data atual!
- `2003` - O colume disponível do contrato deve ser maior ou igual a 1!

### Atualizar Contrato
- `2001` - A data de início de vigência do contrato deve ser menor ou igual à data atual!
- `2002` - A data de fim de vigência do contrato deve ser maior ou igual à data atual!
- `2003` - O volume disponível do contrato deve ser maior ou igual a 1!

### Remover Contrato
- `2004` - O contrato não pode possuir pedidos para ser removido!

### Inserir Pedido
- `3001` O volume do pedido deve ser maior ou igual a 1!
- `3002` A data do pedido deve ser maior ou igual à data atual!
- `3003` O pedido deve ser criado para um contrato existente!
- `3004` O contrato do pedido deve estar ativo!
- `3005` O volume do pedido deve ser menor ou igual ao volume disponível do contrato!
- `3006` A data do pedido deve estar entre as datas de vigência do contrato!
- @TO-DO **Ao inserir pedido, o volume disponível do contrato deve ser atualizado.**

### Atualizar Pedido
- `3001` O volume do pedido deve ser maior ou igual a 1!
- `3002` A data do pedido deve ser maior ou igual à data atual!
- `3004` O contrato do pedido deve estar ativo!
- `3005` O volume do pedido deve ser menor ou igual ao volume disponível do contrato!
- `3006` A data do pedido deve estar entre as datas de vigência do contrato!

### Remover Pedido
- `3007` O pedido deve ter status não atendido para ser excluído!
- @TO-DO **Ao remover pedido, o volume disponível do contrato deve ser atualizado.**