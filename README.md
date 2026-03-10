# TechPartsHub (.NET 8)

Aplicación de consola para Inventario, Pedidos, Facturación y Pagos con arquitectura limpia:

- `TechPartsHub.Domain`
- `TechPartsHub.Application`
- `TechPartsHub.Infrastructure`
- `TechPartsHub.ConsoleApp`

## Patrones aplicados (con comentario en código)

- **PATRÓN STATE**: estados del pedido (`Pending`, `Processed`, `Cancelled`).
- **PATRÓN STRATEGY**: ordenamiento de inventario y métodos de pago.
- **PATRÓN SPECIFICATION**: filtros de búsqueda (incluye compuesto).
- **PATRÓN REPOSITORY**: contratos en Application e implementaciones en Infrastructure.
- **PATRÓN COMMAND**: menú de consola por comando.
- **PATRÓN SIMPLE FACTORY**: creación de comandos, strategies y specifications.
- **PATRÓN CHAIN OF RESPONSIBILITY**: pipeline de procesamiento de pedidos.
- **PATRÓN MEMENTO**: deshacer cambios de ítems del pedido.
- **PATRÓN OBSERVER**: notificación de stock bajo al procesar pedidos.

## Cambios relevantes de esta iteración

- Se incorporó **Memento** para `AddItem/RemoveItem` con deshacer (`UndoLastItemsChangeAsync`).
- Se incorporó **Observer** para alertar stock bajo (`StockNotificationCenter` + `ConsoleLowStockObserver`).
- Se incorporó **Strategy para pagos** (`tarjeta/efectivo/transferencia`) y servicio de pagos.
- Se mantuvieron validaciones de negocio (stock, cola, estados, facturación y pagos duplicados).

## Ejecución

```bash
dotnet restore TechPartsHub.sln
dotnet build TechPartsHub.sln
dotnet run --project TechPartsHub.ConsoleApp/TechPartsHub.ConsoleApp.csproj
```

## Flujo rápido recomendado

1. Crear pedido.
2. Agregar ítems.
3. Probar deshacer ítems.
4. Enviar a cola y procesar (se descuenta stock y puede disparar alerta observer).
5. Generar factura.
6. Pagar factura con método (`tarjeta`, `efectivo` o `transferencia`).
7. Ver facturas y pagos.
