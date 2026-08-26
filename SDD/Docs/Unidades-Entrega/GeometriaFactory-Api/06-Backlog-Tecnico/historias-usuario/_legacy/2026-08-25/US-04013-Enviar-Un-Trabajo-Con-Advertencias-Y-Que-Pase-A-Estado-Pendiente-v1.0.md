> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md`](../../US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-04013 — Enviar un trabajo con advertencias y que pase a estado `Pendiente`

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04013-Enviar-Un-Trabajo-Con-Advertencias-Y-Que-Pase-A-Estado-Pendiente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04005 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que un envío cuyas observaciones son todas advertencias pase el trabajo a estado `Pendiente`**, para **que el alumno vea la discrepancia entre lo que declaró y lo que la geometría dice, y aun así entregue**.

## 2. Contexto

`NB-00005` pide visibilidad del error de cálculo y `RN-04005` declara que las advertencias **sí** permiten el paso a estado `Pendiente`. El contrato de uso es [`CU-00026`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md). `05` §10.2 declara que **el tramo principal de `RN-04005` está en el dominio**: esta capa entrega el conjunto de observaciones y el dominio resuelve el estado.

## 3. Criterios de aceptación

- Given un envío cuyo texto produce sólo observaciones de especie advertencia, When se resuelve el caso de uso, Then el trabajo queda en estado `Pendiente` y las advertencias quedan incorporadas.
- Given ese mismo envío, When se inspecciona quién decidió el estado, Then lo decidió **el dominio**: esta capa entregó el conjunto de observaciones **completo y con su especie** y no eligió el estado.
- Given un texto con una advertencia de área, When se consulta la observación, Then trae el **valor declarado y el derivado**, sin corregir ninguno de los dos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00004, NB-00005, NB-00003 |
| CU cubiertos | CU-04005 |
| RN e invariantes que ejerce | RN-04005, RN-04008, RN-04009; INV-04 |
| Componente de `05` §3.1 | Orquestación del trabajo |
| Puertos que consume | Validación de figuras, repositorio de trabajos, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04009, BT-04015, BT-04019 |
| Tests previstos en 08 | Escenarios `E-1` y `E-3` del intake §20 como entrada del doble del puerto de validación. **No se inventan textos de prueba** |

## 5. Prioridad y estimación

`Must` por derivar de `F-10` y `F-22`, `Must Have`, y porque el criterio de transición `f` → `g` exige que un cubo del primer ejemplo produzca advertencia de área con los dos valores y **pase a estado `Pendiente` igual**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**El estado no es un fallo del envío.** `GeometriaFactory-Api` declara que un envío cuyo texto no verifica **responde con éxito**, porque el trabajo se guardó y su estado se decidió; lo que no verifica es el texto y no la petición. Esta historia es la mitad feliz de esa distinción, y US-04014 la otra.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
