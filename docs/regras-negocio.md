[Modelo de negócio](modelo-negocio)

## Regras da API
- Os objetos enviados no corpo da mensagem devem ter o mesmo id dos parâmetros na rota.

## Regras de Negócio

### Inserir Contrato
- `2001` - A data de início de vigência do contrato deve ser menor ou igual à data atual.
- `2002` - A data de fim de vigência do contrato deve ser maior ou igual à data atual.
- `2003` - O colume disponível do contrato deve ser maior ou igual a 1.

### Editar Contrato
- `2001` - A data de início de vigência do contrato deve ser menor ou igual à data atual.
- `2002` - A data de fim de vigência do contrato deve ser maior ou igual à data atual.
- `2003` - O volume disponível do contrato deve ser maior ou igual a 1.
- `2004` - O volume dos pedidos pendentes do contrato deve ser menor do que o novo volume disponível.

### Remover Contrato
- `2005` - O contrato não pode ter pedidos.

### Inserir Pedido
- `3001` O volume do pedido deve ser maior ou igual a 1.
- `3002` A data do pedido deve ser maior ou igual à data atual.
- `3003` O pedido deve ser criado com status não atendido.
- `3004` O pedido deve ser criado para um contrato existente.
- `3005` O contrato do pedido deve estar ativo.
- `3006` O volume do pedido deve ser menor ou igual ao volume disponível do contrato.
- `3007` A data do pedido deve estar entre as datas de vigência do contrato.
- **Ao inserir pedido, o volume disponível do contrato deve ser atualizado.**

### Atualizar Pedido
- `3001` O volume do pedido deve ser maior ou igual a 1.
- `3002` A data do pedido deve ser maior ou igual à data atual.
- `3005` O contrato do pedido deve estar ativo.
- `3006` O volume do pedido deve ser menor ou igual ao volume disponível do contrato.
- `3007` A data do pedido deve estar entre as datas de vigência do contrato.
- `3008` O pedido deve ter status não atendido para ser editado.
- **Ao atualizar o pedido, o volume disponível do contrato deve ser atualizado.**
- **Para calcular o volume disponível, deve-se considerar o volume atual do pedido.**

### Atender Pedido
- `3005` O contrato do pedido deve estar ativo.
- `3008` O pedido deve ter status não atendido para ser editado.

### Remover Pedido
- `3005` O contrato do pedido deve estar ativo.
- `3011` O pedido deve ter status não atendido para ser excluído.
- **Ao remover pedido, o volume disponível do contrato deve ser atualizado.**