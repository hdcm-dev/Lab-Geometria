# ADR-08001 — Ensamblado de tipos de transferencia planos, sin comportamiento y sin dependencias

**Producto:** Fábrica de Geometría
**Documento:** ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

El producto tiene **dos unidades desplegables** —la pública, en el hosting, y la de datos, en el servidor propio— y la partición no es una preferencia de estilo sino la respuesta a una restricción de topología: el servidor propio no tiene dirección estable y la red desde donde se usa el laboratorio bloquea las direcciones dinámicas, mientras que el hosting con dominio público resetea el estado persistente (`PRODUCT-INTAKE` §14).

`GeometriaFactory-Contracts` es **el único tipo de dato que cruza esa frontera** (`PRODUCT-INTAKE` §14, tabla de contratos expuestos). Es nivel 0 del orden topológico y no depende de nada; en particular, `PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Contracts declara como quality gate bloqueante que **no referencie `GeometriaFactory-Domain`**, «porque es la vía por la que el acoplamiento vuelve».

Motivación upstream: NB-00001 a NB-00009, las nueve, en el grado que declara [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §4.1; RN-08001 a RN-08016, todas transportadas y ninguna redactada acá; RA-01 y RA-03.

## 2. Decisión

Se adopta un **ensamblado de tipos de transferencia planos, sin comportamiento y sin dependencias**, compartido por las dos unidades desplegables.

«Plano» significa aquí tres cosas concretas y verificables: **sin lógica en los descriptores de acceso**, **sin campos calculados** y **sin ciclos entre tipos**. Y «sin dependencias» significa cero referencias a otros proyectos de código del producto —empezando por `GeometriaFactory-Domain`— y **cero referencias a bibliotecas de serialización**: los tipos tienen que ser serializables, pero la elección del formato y de su configuración es de quien los serializa.

La unidad de organización interna es la **familia de tipos**, que es la unidad con la que un cambio incompatible se propaga y el mismo criterio de recorte que la categoría 02 usó para sus ocho contratos de uso.

## 3. Estado

**Propuesto** desde 2026-08-10. Las dos alternativas principales las descartó el intake en §17.1.P.2 · GeometriaFactory-Contracts; lo que esta ADR agrega es el registro formal, la definición operativa de «plano» y la puerta de verificación.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Ensamblado compartido de tipos planos (**adoptada**) | Un cambio incompatible rompe la compilación de los dos extremos antes que el tiempo de ejecución; la unidad pública nunca conoce el dominio; cero infraestructura agregada | Duplica forma entre entidades y tipos de transferencia; obliga a desplegar las dos unidades juntas ante un cambio de contrato |
| Compartir las entidades de dominio entre las dos unidades | Cero duplicación de forma | Acopla la unidad pública a cambios internos del dominio y **filtra campos que no le corresponden**, empezando por la credencial derivada. Descartada por `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Contracts |
| Generar el cliente desde una descripción formal del servicio | Contrato explícito, verificable por herramienta y con clientes generados | Costo de cadena de herramientas frente a **dos** consumidores de la misma solución compilados juntos. Descartada por `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Contracts |
| Tipos con comportamiento —validación de forma dentro del propio tipo | Un solo lugar donde comprobar la forma de una solicitud | El tipo dejaría de ser transportable sin ejecutar código, y la validación quedaría duplicada con la del dominio. Es la puerta por la que el ensamblado empezaría a tomar decisiones que no le corresponden |

## 5. Consecuencias positivas

1. **Un cambio incompatible es un error de compilación, no un fallo en producción**, que es la propiedad que el intake declara en §17.1.P.3 · GeometriaFactory-Contracts.
2. La unidad pública no puede exponer al navegador un campo del dominio, porque no lo conoce.
3. El ensamblado se carga en los dos procesos sin arrastrar ninguna dependencia a ninguno de los dos.
4. La organización por familias hace que el alcance de un cambio incompatible se lea de un vistazo.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta duplicar forma** entre las entidades del dominio y los tipos de transferencia. Es deliberado y es lo que impide que un cambio de dominio rompa el contrato (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Domain y §17.1.P.12 · GeometriaFactory-Contracts).
2. **Se renuncia a un contrato descrito formalmente y a clientes generados.** Con dos consumidores compilados juntos, el costo no se paga.
3. **Se acepta que la validación de forma de una solicitud viva fuera del ensamblado.** Un campo obligatorio ausente lo detecta el servicio de datos y lo informa con `CONTRATO_CAMPO_REQUERIDO_AUSENTE`, no el tipo.
4. **Se acepta que el ensamblado no imponga formato de intercambio.** La contrapartida es que dos consumidores mal configurados podrían serializar distinto; la mitigación es que los dos se configuran en el mismo producto y que las pruebas de integración golpean el servicio real.

## 7. Implementación

- Un archivo de proyecto **sin ninguna referencia** a otro proyecto de código del producto ni a bibliotecas de serialización.
- Ocho familias de tipos, según [`../Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, con las dependencias apuntando a la familia de error y la única arista adicional de reseteo hacia cuentas.
- Convención impuesta: **nada de comportamiento**. Ni descriptores de acceso con lógica, ni campos calculados, ni métodos de conveniencia que interpreten el texto original.
- Los cuatro estados del trabajo viajan como conjunto cerrado, y **ningún tipo permite salir de `Finalizado` ni de `Rechazado`**, que es la restricción `RT-08` de la categoría 02.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Referencias hacia `GeometriaFactory-Domain` | Exactamente **0** | Inspección del archivo de proyecto, puerta bloqueante de construcción |
| Referencias a bibliotecas de serialización | Exactamente **0** | Inspección del archivo de proyecto |
| Miembros con lógica en la superficie pública | Exactamente **0** | Inspección de los tipos de las ocho familias |
| Ciclos entre tipos | Exactamente **0** | Inspección del grafo de tipos |
| Advertencias de construcción | Exactamente **0** | Etapa de `build` del pipeline, bloqueante para fusionar |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §14, §17.1.P.1 · GeometriaFactory-Contracts, §17.1.P.2 · GeometriaFactory-Contracts, §17.1.P.3 · GeometriaFactory-Contracts, §17.1.P.8 · GeometriaFactory-Contracts, §17.1.P.11 · GeometriaFactory-Contracts y §17.1.P.12 · GeometriaFactory-Contracts.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §2 y §3.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §1, §3.1 y §6 (`RT-03`, `RT-05`, `RT-08`).
- ADR relacionadas: [`ADR-08003`](ADR-08003-Versionado-Por-Compilacion-Compartida.md), [`ADR-08004`](ADR-08004-Regla-De-Exposicion-De-La-Frontera.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el ensamblado de tipos planos sin dependencias, da definición operativa y verificable a «plano» y a «sin dependencias», evalúa cuatro alternativas y fija cinco métricas de validación. |
