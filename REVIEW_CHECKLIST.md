# REVIEW_CHECKLIST

## Dominio (`TechPartsHub.Domain`)

- `Exceptions/DomainException.cs`
  - Responsabilidad: excepción de dominio con mensajes claros.
  - SOLID: SRP.
  - Patrón: N/A.

- `Enums/OrderStatus.cs`
  - Responsabilidad: estados válidos del pedido.
  - SOLID: SRP.
  - Patrón: soporte para STATE.

- `State/IOrderState.cs`, `State/PendingOrderState.cs`, `State/ProcessedOrderState.cs`, `State/CancelledOrderState.cs`
  - Responsabilidad: reglas por estado de pedido (cola/proceso/facturación).
  - SOLID: OCP/LSP (nuevos estados sin romper consumidores).
  - Patrón: **STATE**.

- `Entities/SparePart.cs`
  - Responsabilidad: entidad de inventario y validación de stock.
  - SOLID: SRP.
  - Patrón: N/A.

- `Entities/OrderItem.cs`
  - Responsabilidad: detalle de pedido.
  - SOLID: SRP.
  - Patrón: N/A.

- `Entities/Order.cs`
  - Responsabilidad: agregado de pedido (ítems + estado), sin lógica de facturación.
  - SOLID: SRP, encapsulación.
  - Patrón: **STATE** (uso concreto).

- `Entities/Invoice.cs`
  - Responsabilidad: entidad de facturación separada de pedido.
  - SOLID: SRP.
  - Patrón: N/A.

- `Specifications/*`
  - Responsabilidad: criterios de búsqueda composables.
  - SOLID: OCP (nuevos filtros por extensión).
  - Patrón: **SPECIFICATION**.

- `Strategy/*`
  - Responsabilidad: ordenamiento por criterio.
  - SOLID: OCP/ISP.
  - Patrón: **STRATEGY**.

## Aplicación (`TechPartsHub.Application`)

- `Abstractions/Repositories/*`
  - Responsabilidad: contratos de persistencia.
  - SOLID: DIP/ISP.
  - Patrón: **REPOSITORY**.

- `Abstractions/Processing/IOrderProcessingHandler.cs`
  - Responsabilidad: contrato del pipeline.
  - SOLID: ISP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/OrderProcessingContext.cs`
  - Responsabilidad: contexto compartido para procesamiento.
  - SOLID: SRP.
  - Patrón: soporte Chain.

- `Processing/OrderProcessingHandlerBase.cs`
  - Responsabilidad: encadenamiento de handlers.
  - SOLID: OCP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/ValidateOrderStateHandler.cs`
  - Responsabilidad: validar estado procesable.
  - SOLID: SRP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/ValidateOrderItemsHandler.cs`
  - Responsabilidad: validar que haya ítems.
  - SOLID: SRP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/ValidateStockHandler.cs`
  - Responsabilidad: validar stock antes de descontar.
  - SOLID: SRP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/DeductStockHandler.cs`
  - Responsabilidad: descontar stock al procesar.
  - SOLID: SRP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Processing/MarkOrderProcessedHandler.cs`
  - Responsabilidad: cerrar flujo marcando procesado.
  - SOLID: SRP.
  - Patrón: **CHAIN OF RESPONSIBILITY**.

- `Factories/SortStrategyFactory.cs`
  - Responsabilidad: crear estrategia de orden.
  - SOLID: SRP/OCP.
  - Patrón: **SIMPLE FACTORY**.

- `Factories/SparePartSpecificationFactory.cs`
  - Responsabilidad: crear especificación de búsqueda.
  - SOLID: SRP/OCP.
  - Patrón: **SIMPLE FACTORY**.

- `Services/InventoryService.cs`
  - Responsabilidad: casos de uso de inventario.
  - SOLID: SRP/DIP.
  - Patrón: usa **STRATEGY**, **SPECIFICATION**, **REPOSITORY**.

- `Services/OrderService.cs`
  - Responsabilidad: creación/edición/cola de pedidos y validaciones de stock en agregado.
  - SOLID: SRP/DIP.
  - Patrón: usa **REPOSITORY**.

- `Services/OrderProcessingService.cs`
  - Responsabilidad: procesar cola y orquestar pipeline seguro.
  - SOLID: SRP/DIP.
  - Patrón: usa **CHAIN OF RESPONSIBILITY**.

- `Services/BillingService.cs`
  - Responsabilidad: facturar pedidos procesados.
  - SOLID: SRP/DIP.
  - Patrón: usa **REPOSITORY**.

## Infraestructura (`TechPartsHub.Infrastructure`)

- `Repositories/InMemorySparePartRepository.cs`
- `Repositories/InMemoryOrderRepository.cs`
- `Repositories/InMemoryInvoiceRepository.cs`
- `Repositories/InMemoryOrderQueueRepository.cs`
  - Responsabilidad: persistencia en memoria de contratos de Application.
  - SOLID: LSP (sustituyen contratos), DIP.
  - Patrón: **REPOSITORY**.

- `Seed/SeedData.cs`
  - Responsabilidad: carga inicial de repuestos para pruebas inmediatas.
  - SOLID: SRP.
  - Patrón: N/A.

## Consola (`TechPartsHub.ConsoleApp`)

- `UI/ApplicationContext.cs`
  - Responsabilidad: composición de dependencias para comandos.
  - SOLID: SRP.

- `Commands/IMenuCommand.cs`
  - Responsabilidad: contrato de comando.
  - SOLID: ISP.
  - Patrón: **COMMAND**.

- `Commands/*.cs` (ViewInventory, RegisterSparePart, SearchSpareParts, SortSpareParts, ViewLowStock, CreateOrder, AddOrderItem, RemoveOrderItem, EnqueueOrder, ProcessNextOrder, ViewOrders, GenerateInvoice, ViewInvoices, Exit)
  - Responsabilidad: una acción de menú por clase.
  - SOLID: SRP/OCP.
  - Patrón: **COMMAND**.

- `Factories/CommandFactory.cs`
  - Responsabilidad: registro centralizado de comandos.
  - SOLID: SRP.
  - Patrón: **SIMPLE FACTORY**.

- `Program.cs`
  - Responsabilidad: bootstrap y loop de UI con manejo de errores.
  - SOLID: composición raíz.
  - Patrón: consumidor de COMMAND.

## Observaciones finales

- Separación clara Inventario/Pedidos/Facturación.
- Pedido no contiene lógica financiera de facturación.
- Reglas críticas de negocio implementadas con validaciones explícitas y excepciones descriptivas.
