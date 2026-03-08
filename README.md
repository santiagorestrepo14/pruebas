# TechPartsHub (.NET 8)

Solución completa de consola para gestión de inventario, pedidos y facturación.

## Arquitectura

La solución está separada en cuatro proyectos:

- **TechPartsHub.Domain**: entidades, estados de pedido, reglas de negocio base, especificaciones y estrategias.
- **TechPartsHub.Application**: casos de uso/servicios de negocio, interfaces de repositorio, fábricas y pipeline de procesamiento.
- **TechPartsHub.Infrastructure**: repositorios en memoria y datos semilla.
- **TechPartsHub.ConsoleApp**: interfaz de consola, menú y comandos.

Esta separación aplica DIP y SRP: la app depende de abstracciones (interfaces) y cada capa tiene una responsabilidad clara.

## Patrones implementados

- **PATRÓN STATE**: `Domain/State/*` y uso en `Domain/Entities/Order.cs`.
- **PATRÓN STRATEGY**: `Domain/Strategy/*`, usado desde `Application/Services/InventoryService.cs`.
- **PATRÓN SPECIFICATION**: `Domain/Specifications/*`, usado desde `Application/Services/InventoryService.cs`.
- **PATRÓN REPOSITORY**: interfaces en `Application/Abstractions/Repositories/*` e implementaciones en `Infrastructure/Repositories/*`.
- **PATRÓN COMMAND**: `ConsoleApp/Commands/*`.
- **PATRÓN SIMPLE FACTORY**: `Application/Factories/*` y `ConsoleApp/Factories/CommandFactory.cs`.
- **PATRÓN CHAIN OF RESPONSIBILITY**: `Application/Processing/*`, ejecutado en `Application/Services/OrderProcessingService.cs`.

## Cómo ejecutar

1. Tener instalado **.NET SDK 8.0**.
2. Restaurar y compilar:
   ```bash
   dotnet restore TechPartsHub.sln
   dotnet build TechPartsHub.sln
   ```
3. Ejecutar consola:
   ```bash
   dotnet run --project TechPartsHub.ConsoleApp/TechPartsHub.ConsoleApp.csproj
   ```

## Flujo de prueba recomendado

1. Ver inventario (opción 1) y copiar un `Id` de repuesto.
2. Crear pedido (opción 6) y copiar `Id` pedido.
3. Agregar ítem (opción 7) usando ambos IDs.
4. Ver pedidos (opción 11).
5. Enviar pedido a cola (opción 9).
6. Procesar pedido (opción 10): aquí se descuenta stock.
7. Ver inventario nuevamente (opción 1) para validar descuento.
8. Generar factura (opción 12).
9. Ver facturas (opción 13).

## Reglas de negocio cubiertas

- No permite stock negativo al registrar.
- Al agregar ítems valida stock considerando cantidades ya reservadas en el mismo pedido.
- El stock se descuenta solo al procesar.
- No permite enviar a cola pedidos vacíos ni en estado inválido.
- No permite pedidos duplicados en cola.
- No permite procesar pedidos inexistentes.
- No permite facturar pedidos no procesados.
