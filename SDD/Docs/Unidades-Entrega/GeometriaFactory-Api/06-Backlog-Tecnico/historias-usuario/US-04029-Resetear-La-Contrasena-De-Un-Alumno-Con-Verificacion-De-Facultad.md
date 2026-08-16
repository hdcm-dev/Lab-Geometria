# US-04029 — Resetear la contraseña de un alumno fijando una provisoria, con verificación de facultad

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-04029-Resetear-La-Contrasena-De-Un-Alumno-Con-Verificacion-De-Facultad.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar el reseteo de la contraseña de un alumno, fijando la provisoria que llega ya producida y dejando la marca de cambio pendiente**, para **que un alumno que olvidó su contraseña vuelva a entrar sin perder ninguno de sus trabajos**.

## 2. Contexto

`F-26` del intake §4 es `Must Have` y cierra un agujero que hacía inutilizable el laboratorio al primer olvido: el único camino documentado era dar de baja y volver a dar de alta, y por `RN-04007` eso eliminaba todos los trabajos del alumno. El contrato de uso es [`CU-04011`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04011-Resetear-La-Contrasena-De-Un-Alumno.md). `RN-04015` declara que el reseteo **no exige que la cuenta esté habilitada**.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y una cuenta de alumno, When se pide el reseteo, Then la cuenta queda con la credencial derivada provisoria fijada y con la **marca de cambio de contraseña pendiente** puesta.
- Given una cuenta en estado `Bloqueado` y otra en estado `Pendiente`, When se las resetea, Then el reseteo **procede en las dos** y **ninguna cambia de estado**: esta capa no comprueba el estado y no devuelve ningún motivo por ese concepto.
- Given la cuenta de administrador, When se pide su reseteo, Then se rechaza con el motivo de acotamiento a cuentas de alumno.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-04011 |
| RN e invariantes que ejerce | RN-04001, RN-04012, RN-04013, RN-04015; RN-04014 exigida por escrito y ejercida en `GeometriaFactory-Infrastructure`; INV-08, INV-09 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas, Guarda de autorización |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Facultad**, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04009, BT-04010, BT-04013 |
| Tests previstos en 08 | Pruebas de reseteo sobre `Bloqueado` y sobre `Pendiente` comprobando que el estado no cambia |

## 5. Prioridad y estimación

`Must` por derivar de `F-26`, `Must Have`, y porque la transición `d` → `e` incorpora **cinco** criterios verificables del reseteo.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**`RN-04015` se ejerce acá de forma negativa**: lo que esta capa hace por ella es **no comprobar** el estado de la cuenta y no devolver ningún motivo por ese concepto (`02` §6). Una comprobación agregada por prolijidad rompería la regla, y por eso el criterio segundo la verifica sobre los dos estados.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
