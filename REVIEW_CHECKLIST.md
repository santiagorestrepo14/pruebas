# REVIEW_CHECKLIST

## Correcciones implementadas en esta revisión

- [x] **MEMENTO** en pedidos para agregar/remover ítems con deshacer.
- [x] **OBSERVER** para notificación de stock bajo.
- [x] **STRATEGY de pagos** con métodos conmutables.
- [x] Se conserva **COMMAND + SIMPLE FACTORY + CHAIN OF RESPONSIBILITY**.
- [x] Se mantienen reglas de negocio de stock/cola/facturación.

## Archivos clave y responsabilidad

- `Domain/Entities/Order.cs`
  - Agregado de pedido y estado.
  - **STATE** + **MEMENTO** (`CreateItemsMemento`, `RestoreItemsMemento`).

- `Domain/Memento/OrderItemsMemento.cs`
  - Snapshot de ítems.
  - **MEMENTO**.

- `Application/Services/OrderService.cs`
  - Casos de uso de pedidos.
  - Gestiona historial de mementos y deshacer.

- `Application/Notifications/StockNotificationCenter.cs`
  - Subject de notificaciones de stock.
  - **OBSERVER**.

- `ConsoleApp/Notifications/ConsoleLowStockObserver.cs`
  - Observer concreto de alertas en consola.
  - **OBSERVER**.

- `Application/Services/Payments/*`
  - Estrategias de pago y resultado.
  - **STRATEGY**.

- `Application/Factories/PaymentStrategyFactory.cs`
  - Crea estrategia de pago según método.
  - **SIMPLE FACTORY**.

- `Application/Services/PaymentService.cs`
  - Orquesta pago de factura y evita duplicados.

- `Application/Services/OrderProcessingService.cs`
  - Procesa pedidos por pipeline.
  - **CHAIN OF RESPONSIBILITY** + disparo de observer de stock.

- `ConsoleApp/Commands/*`
  - Menú desacoplado por acción.
  - **COMMAND**.

## Observaciones SOLID

- SRP: responsabilidades separadas en servicios dedicados (Order, Billing, Payment, Notification).
- OCP: nuevos métodos de pago/observadores/filters sin modificar consumidores.
- DIP: servicios dependen de repositorios/abstracciones.
- ISP: interfaces específicas por función.
- LSP: repositorios in-memory sustituyen contratos sin romper comportamiento.

- Regla adicional validada: Cancelación válida solo en estado pendiente.
