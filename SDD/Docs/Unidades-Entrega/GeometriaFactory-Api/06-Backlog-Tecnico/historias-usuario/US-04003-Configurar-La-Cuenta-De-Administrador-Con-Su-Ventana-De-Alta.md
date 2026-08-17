# US-04003 — Configurar la cuenta de administrador con su ventana de alta

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04003-Configurar-La-Cuenta-De-Administrador-Con-Su-Ventana-De-Alta.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar la configuración de la única cuenta de administrador, habilitada y con credencial, sólo mientras no exista ninguna**, para **que el laboratorio tenga desde el primer arranque quien lo gobierne, sin abrir ninguna otra puerta**.

## 2. Contexto

`NB-00001` pide control de admisión al laboratorio y `F-01` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-00025`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00025-Configurar-La-Cuenta-De-Administrador-En-El-Primer-Arranque.md), que `02` §8 separó de CU-04001 porque **no comparten casi nada**: el estado inicial es opuesto, la credencial se aporta en uno y se prohíbe en el otro, y la ventana de alta existe en uno y no en el otro.

## 3. Criterios de aceptación

- Given que el puerto de repositorio de cuentas responde que **no existe ninguna cuenta con papel `Administrador`**, When se solicita la configuración con su credencial ya derivada, Then la cuenta queda constituida con papel `Administrador`, en estado `Habilitado` y **con credencial**.
- Given esa misma solicitud, When se inspecciona la marca de cambio de contraseña pendiente, Then **no está puesta**: el administrador escribe su propia contraseña y no recibe ninguna provisoria.
- Given una credencial ausente en la solicitud, When se la procesa, Then se rechaza con su motivo: en este camino la credencial **se exige**, al revés que en CU-04001.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-04010 |
| RN e invariantes que ejerce | RN-04001, RN-04002, RN-04006; INV-05, INV-08 |
| Componente de `05` §3.1 | Orquestación del alta de cuentas |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Ninguna: la ventana de alta existe **antes** de que haya identidad que autenticar |
| BT derivadas | BT-04012, BT-04007 |
| Tests previstos en 08 | Prueba unitaria con doble que responde que no existe administrador, sin base de datos |

## 5. Prioridad y estimación

`Must` por derivar de `F-01`, `Must Have`, y porque la etapa `c` no cierra sin ella: el criterio de transición `c` → `d` exige que el administrador se configure en el primer arranque y **sólo** mientras no exista ninguno.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**La existencia previa de una cuenta con papel `Administrador` la resuelve el puerto y no el dominio.** `Domain ADR-04006` declara que el dominio no consulta conjuntos, de modo que esa precondición la aporta esta capa: es la contracara de `INV-05` que `05` §10.3 le asigna.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
