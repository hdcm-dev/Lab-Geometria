# US-02004 — Habilitar, bloquear y rehabilitar una cuenta

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02004-Habilitar-Bloquear-Y-Rehabilitar-Una-Cuenta.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **las tres transiciones no destructivas del ciclo de vida de una cuenta de alumno, con su guarda**, para **que el administrador gobierne quién entra al laboratorio sin tener que dar de baja a nadie**.

## 2. Contexto

La capacidad `F-03` del intake §4 declara las cuatro operaciones del administrador sobre una cuenta; ésta cubre las tres no destructivas y la baja es US-02005. El contrato de uso es [`CU-00023`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md). `INV-08` acota el alcance: la cuenta de administrador no admite ninguna de las tres.

## 3. Criterios de aceptación

- Given una cuenta de alumno en estado `Pendiente`, When el administrador la habilita, Then queda `Habilitado` y con la marca de cambio de contraseña pendiente puesta, por `RN-02016` (la credencial provisoria es US-02006).
- Given una cuenta de alumno `Habilitado`, When el administrador la bloquea y después la rehabilita, Then recorre `Bloqueado` y vuelve a `Habilitado` sin perder identidad ni trabajos.
- Given la cuenta con papel `Administrador`, When se intenta cualquiera de las tres transiciones sobre ella, Then se rechaza, porque `INV-08` declara que esa cuenta está **siempre** `Habilitado`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-02002 |
| RN e invariantes que ejerce | RN-02001, RN-02006, RN-02016; INV-05, INV-06, INV-08 |
| BT derivadas | BT-02010 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por transición admitida y por transición rechazada, sobre la máquina de estados de cuenta. |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have` en `PRODUCT-INTAKE` §4, y porque sin habilitación ningún alumno obtiene acceso, por `RN-02006`.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

**Las guardas de cuenta no invocan al evaluador de admisibilidad** (`05` §3.2): habilitar, bloquear y rehabilitar son actos del administrador sobre una cuenta ajena y no exigen que la cuenta operada sea admisible. Quien exige admisibilidad es el consumidor, sobre la cuenta que solicita.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
