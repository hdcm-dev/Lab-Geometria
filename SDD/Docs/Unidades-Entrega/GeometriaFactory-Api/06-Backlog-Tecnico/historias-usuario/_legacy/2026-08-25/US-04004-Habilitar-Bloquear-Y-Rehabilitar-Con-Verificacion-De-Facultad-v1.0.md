> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md`](../../US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-04004 — Habilitar, bloquear y rehabilitar una cuenta con verificación de facultad

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04004-Habilitar-Bloquear-Y-Rehabilitar-Con-Verificacion-De-Facultad.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar las transiciones de admisión de una cuenta ajena verificando antes que quien las pide tenga el papel `Administrador`**, para **que sólo el docente decida quién entra al laboratorio y quién queda afuera**.

## 2. Contexto

`NB-00001` pide control de admisión y `F-03` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-00023`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00023-Gobernar-Las-Cuentas-De-La-Comision.md). La verificación de **facultad** es una de las cuatro comprobaciones transversales de `02` §4, y admite ser explícita porque **no hay recurso ajeno cuya existencia proteger**.

## 3. Criterios de aceptación

- Given un solicitante con papel `Administrador` y una cuenta de alumno en un estado que admite la transición, When se pide habilitar, bloquear o rehabilitar, Then la transición se aplica y el estado resultante es el que el dominio resuelve.
- Given un solicitante sin el papel `Administrador`, When pide cualquiera de las tres, Then se devuelve el motivo de **facultad requerida** y **el estado de la cuenta no cambia**.
- Given una transición que el dominio no admite desde el estado vigente, When se la pide con la facultad correcta, Then se devuelve el motivo del dominio **sin colapsarlo** con el de facultad.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001 |
| CU cubiertos | CU-04002 |
| RN e invariantes que ejerce | RN-04001, RN-04006, RN-04016; INV-06, INV-08 |
| Componente de `05` §3.1 | Orquestación del gobierno de cuentas, Guarda de autorización |
| Puertos que consume | Repositorio de cuentas |
| Comprobación de `02` §4 que la alcanza | **Facultad**, y **cambio de contraseña pendiente**, que corta antes que ella |
| BT derivadas | BT-04010, BT-04013 |
| Tests previstos en 08 | Prueba de la negativa de facultad sin base de datos, y prueba de que la cuarta comprobación corta antes |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have`, y porque el criterio de transición `d` → `e` exige que el administrador habilite, bloquee y rehabilite.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Habilitar y rehabilitar no terminan acá**: por `RN-04016` producen además la contraseña provisoria y dejan la marca puesta, y eso es US-04008. Esta historia cubre la transición de admisión; la credencial provisoria es la otra mitad del mismo acto y se declara aparte para que cada una tenga su criterio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia prevista en `02` §7.3 con este identificador. |
