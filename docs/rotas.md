## Rotas da API

### Contratos

| **Verbo**     | **Rota**                        |
|---------------|---------------------------------|
| GET           | `api/v1/contratos`              |
| POST          | `api/v1/contratos`              |
| GET           | `api/v1/contratos/{contratoId}` |
| PUT           | `api/v1/contratos/{contratoId}` |
| DELETE        | `api/v1/contratos/{contratoId}` |

### Pedidos

| **Verbo**     | **Rota**                                                    |
|---------------|-------------------------------------------------------------|
| GET           | `api/v1/contratos/{contratoId}/pedidos`                     |
| POST          | `api/v1/contratos/{contratoId}/pedidos`                     |
| GET           | `api/v1/contratos/{contratoId}/pedidos/{pedidoId}`          |
| PUT           | `api/v1/contratos/{contratoId}/pedidos/{pedidoId}`          |
| DELETE        | `api/v1/contratos/{contratoId}/pedidos/{pedidoId}`          |
| PATCH         | `api/v1/contratos/{contratoId}/pedidos/{pedidoId}/atendido` |