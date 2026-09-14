# ADR-00012 — El autorregistro admite los dominios que el despliegue declara

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00012-El-Autorregistro-Admite-Los-Dominios-Que-El-Despliegue-Declara.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-09-14
**Autor:** Arquitecto de Software Senior + API Designer (AG-05), por delegación del Product Owner del 2026-09-14
**Categoría:** Seguridad
**Origen:** `../../../../Audit/Mesa-2026-09-14.md`, ítem `R-07` y escalada `E-1`

---

## 1. Contexto

`A-02` registra una cuenta de alumno **sin acceso firmado**, y es así por diseño: el alumno todavía no tiene cuenta (`PRODUCT-INTAKE` 1.15 §4.1; capacidad `F-02`, `Must Have`). La cuenta nace `Pending` y no entra hasta que el docente la habilita. Hasta esta decisión, **el registro admitía cualquier correo**.

La mesa del 2026-09-14 lo levantó como `R-07`, con veto de la comisión de producto porque la corrección obvia cambia un contrato público, y lo elevó como `E-1`. El Product Owner eligió «Alta sólo por el docente», con esta consecuencia escrita al lado: **cambio de contrato público con versión nueva o apartamiento declarado de [`ADR-00010`](ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md)**.

Al implementarla aparecieron dos costos que la consulta no mostraba:

1. **Retirar `A-02` deja sin sustento a `F-02`**, que es `Must Have` en el intake. El registro por el propio alumno es la forma en que el docente no tiene que tipear la comisión.
2. **Retirar `A-02` de `/v1/` es un cambio mayor.** [`ADR-00010`](ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.1 exige `/v2/` y un cuatrimestre de convivencia con `/v1/`, que seguiría registrando durante ese plazo.

El Product Owner delegó la resolución el mismo día, con autorización plena: «resolvelo vos!, - tenes autorización plena para ejecutar y hacer».

## 2. Decisión

**`A-02` se conserva, y admite sólo los correos de los dominios que el despliegue declara.**

1. **La lista llega por configuración:** `Registration__AdmittedEmailDomains__0`, `__1`… en la composición. Se escribe sin `@`; si lo trae, se ignora.
2. **Sin dominios declarados, no hay restricción.** El producto no conoce el dominio institucional de cada despliegue y no lo inventa. `appsettings.json` trae la lista vacía y la documenta.
3. **La comparación es exacta sobre el dominio entero, sin distinguir mayúsculas.** Un subdominio no entra por el dominio que lo contiene, y un sufijo tampoco: `frre.utn.edu.ar.example.com` no es `frre.utn.edu.ar`.
4. **El dominio se verifica antes que la unicidad del correo.** Un correo de un dominio no admitido recibe la misma respuesta tenga o no cuenta, así que esta regla no abre una forma nueva de averiguar qué correos están registrados (`RN-02`).
5. **El rechazo es el código nuevo `EMAIL_DOMAIN_NOT_ADMITTED`, con `400`.**
   - `400` y no `409`, por el criterio de `CONFIRMATION_MISMATCH`: es un campo de la petición que no cumple lo que el despliegue pide, no un estado del conjunto.
   - El texto no nombra los dominios admitidos.
   - `A-02` ya declaraba `400` en `Definicion-Superficie-HTTP.md` §3, así que **ningún código de respuesta se agrega al punto**.
6. **El código entra dentro de `/v1/`.** Agregar un código al conjunto cerrado es un cambio menor ([`ADR-00010`](ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.1 punto 3). Un cliente que no lo conoce lo recibe como un `400` con código, que es lo que ya sabía manejar.
7. **El front lo dibuja** en `Registro-De-Cuenta`: marca el campo del correo, avisa que no se registró nada y pide el correo institucional.

## 3. Alternativas descartadas

| Alternativa | Por qué no |
| --- | --- |
| Alta sólo por el docente: retirar `A-02` y agregar un alta autenticada | Deja sin sustento a `F-02` (`Must Have`) y exige `/v2/` con un cuatrimestre de convivencia (§1). **Queda abierta:** si el Product Owner la sostiene después de ver este costo, es la vía, y esta ADR se supera |
| Confirmación por correo | El producto no envía correos, y lo declara en la propia pantalla de registro |
| Código de comisión compartido | Requiere un campo nuevo en `AccountRegistrationRequest`, que es un cambio del ensamblado de contratos y de la pantalla, y un secreto que circula por la clase |
| Sólo el límite de tasa por origen | Ya existe ([`ADR-00011`](ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md)) y está dimensionado para una comisión entera detrás de una dirección: no acota quién se registra |

## 4. Consecuencias

- **Mientras la composición no declare un dominio, el registro sigue admitiendo cualquier correo.** La corrección de `R-07` en un despliegue **es declarar la llave**, y eso es del Product Owner, que conoce el dominio de su comisión.
- Un alumno sin correo del dominio no puede registrarse y lo tiene que dar de alta el docente. Hoy no existe ese camino (§3, primera fila): **conviene declarar un dominio sólo si toda la comisión lo tiene**.
- **El conjunto cerrado pasa de diecisiete a dieciocho códigos vivos.** `PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Contracts sigue diciendo «diecisiete vivos». El intake es del Product Owner y no se toca desde esta ADR. **Deuda con evento:** la próxima emisión del intake.

## 5. Verificación

| Qué | Dónde |
| --- | --- |
| Rechazo sin escritura, precedencia sobre la unicidad, comparación sin mayúsculas y dominio entero | `tests/GeometriaFactory.Application.Tests/AccountLifecycleUseCaseTests.cs` |
| `400` con el código y cero cuentas guardadas; el dominio admitido registra | `tests/GeometriaFactory.Integration.Tests/RegistrationDomainTests.cs` |
| La pantalla de registro avisa y no dibuja el éxito | Ídem |
| La traducción emite un código del conjunto cerrado y un código de respuesta admitido | `ContractTranslationTests`, que recorre el catálogo por reflexión |

## 6. Referencias

- `../../../../Audit/Mesa-2026-09-14.md` §8 (`E-1`).
- [`ADR-00010`](ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.1 punto 3.
- [`ADR-00011`](ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md).
- [`ADR-06003`](ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md).
- `../../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3 (`A-02`) y §5.
- `../../../../Producto/Contratos-Inter-Unidad/Contratos-Abstractions.md` §5.1.
- `../../../../Producto/Contratos-Inter-Unidad/CU-08002-Contrato-De-Administracion-De-Cuentas.md` §6.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-14 | Emisión inicial, **Aceptado**. Rama `seguridad/lote1-registro-por-dominio-admitido`, base `cc024c5`. Lote 1 de la mesa del 2026-09-14, `R-07`/`E-1`. |
