# ADR-03 — Visualizador puro: cero red, cero persistencia, cero configuración y cero identidad

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ADR-03-Visualizador-Puro-Sin-Red-Ni-Identidad.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

La composición del producto responde a una restricción externa: el servidor propio no tiene dirección estable y la red desde donde se usa el laboratorio bloquea las direcciones dinámicas, mientras que el hosting con dominio público resetea el estado persistente. De ahí que el front viva donde no lo bloquean y los datos donde persisten (`PRODUCT-INTAKE` §14).

Esa partición se sostiene sobre tres reglas de arquitectura de nivel producto, y **dos de ellas caen sobre este proyecto de código**:

- **RA-01**, ningún JavaScript del navegador invoca la API. Es lo que sostiene las tres propiedades de la topología: sin contenido mixto, sin restricción de origen cruzado y sin exposición de la dirección del servidor propio. **Romperla en un solo proyecto de código las reabre las tres.**
- **RA-02**, el bundle del visor es un visualizador puro: sin configuración, sin red, sin conocimiento del sistema. Es lo que hace **imposible** violar RA-01 desde el navegador.

Este proyecto de código **es** el JavaScript del navegador del producto. Su contribución a la seguridad es **negativa por diseño**: no hacer red es lo que impide que aparezca una petición desde el navegador hacia el servicio de datos y que vuelvan el contenido mixto, la restricción de origen cruzado y la exposición de la dirección (`PRODUCT-INTAKE` §17.7.P.5 y §17.7.P.12).

Motivación upstream: NB-06, NB-08 en su arista de estado degradado; capacidad F-25; RA-01, RA-02 y RA-03.

## 2. Decisión

**El bundle es un visualizador puro**, y las cuatro ausencias son parte de su contrato y verificables:

| Ausencia | Garantía | Alcance |
| --- | --- | --- |
| **Cero red** | G-1 | Ninguna función, y **ningún movimiento automático mientras corre**, origina una petición. Ni obtención de recursos, ni petición asincrónica, ni conexión persistente |
| **Cero persistencia** | G-2 | Ninguna función guarda estado entre páginas ni escribe en el almacenamiento del navegador |
| **Cero configuración propia** | G-3 | Ninguna función lee configuración propia. Todo lo que la instancia necesita llega por parámetro |
| **Cero identidad** | — | El bundle no sabe quién mira ni qué papel cumple, y no participa de ninguna decisión de autorización |

**La sexta función de la fachada confirma la decisión en lugar de aflojarla**: el anfitrión pasa **dos valores de verdad**, y el bundle **no consulta la preferencia de movimiento reducido del sistema** —consultarla violaría G-3— **ni conserva la elección** —conservarla violaría G-2—.

**La medición de la garantía de cero red se hace con los dos movimientos prendidos**, que es su peor caso.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Visualizador puro, con las cuatro ausencias verificables (**adoptada**) | Hace **imposible** violar RA-01 desde el navegador; el bundle se prueba sin backend; es reemplazable; y la verificación es por inspección, no por confianza | El anfitrión carga con todo lo que la fachada no hace: obtener el texto, dibujar los controles, consultar la preferencia y conservarla |
| Que el visor pida los datos por su cuenta | Más simple de implementar: el anfitrión sólo le pasaría un identificador | **Reabre contenido mixto, restricción de origen cruzado y exposición de la dirección del servidor propio.** Es el riesgo que el intake registra y la razón por la que la topología existe. Descartada por `PRODUCT-INTAKE` §17.7.P.12 |
| Que el bundle consulte la preferencia de movimiento reducido del sistema | El comportamiento accesible saldría por defecto, sin trabajo del anfitrión | Es **leer configuración propia** y viola G-3. Y tiene un efecto de segundo orden peor: las pruebas automatizadas suelen declarar esa preferencia, con lo que los movimientos arrancarían apagados y **la medición de cero red quedaría en verde sin ejercitar nunca el bucle** |
| Que el bundle conserve la elección de quien mira en el almacenamiento del navegador | La preferencia sobreviviría a la recarga sin trabajo del anfitrión | Es **escribir en el almacenamiento** y viola G-2. La preferencia es de quien mira y su lugar es el anfitrión |

## 5. Consecuencias positivas

1. **RA-01 queda sostenida por imposibilidad y no por disciplina**: no hay código de red que revisar.
2. **RA-03 se cumple por ignorancia**: el bundle no conoce ninguna dirección de servicio, así que ninguna de sus siete condiciones puede exponerla.
3. El bundle **se ejercita sin backend**, que es la propiedad que el visualizador previo ya tenía y que el intake exige conservar; es además el sample S-1 del producto.
4. La verificación es barata y mecánica: cero ocurrencias de las tres formas de petición en el código fuente y en el bundle generado, más el conteo en la pestaña de red.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que el anfitrión cargue con más trabajo**: obtener el texto, dibujar los controles de movimiento, consultar la preferencia de movimiento reducido y conservar la elección.
2. **Se acepta que el comportamiento accesible dependa del anfitrión.** Si el anfitrión no consulta la preferencia de movimiento reducido, los movimientos no la respetan. La mitigación es que el arranque por defecto es **apagado**: ante opciones ausentes o parciales, la instancia nace quieta, que es lo que no sorprende.
3. **Se acepta medir la garantía de cero red en su peor caso**, que es más caro de montar que medirla con los movimientos apagados.
4. **Se acepta que este proyecto de código no pueda verificar RA-01 por sí solo.** Lo que puede es no ofrecer ningún camino que la facilite.

## 7. Implementación

- **Puerta bloqueante verificable por inspección**: cero ocurrencias de las tres formas de petición de red —obtención de recursos, petición asincrónica y conexión persistente— **en el código fuente y en el bundle generado**. El intake la declara como gate en lugar de una cobertura de líneas, y rotula `[ASUNCIÓN]` sólo el hecho de expresarla como gate automatizable: la regla es de RA-02 y ya es criterio de aceptación de la etapa `g`.
- **Verificación complementaria en el navegador**: durante la interacción tridimensional, ni una sola petición hacia el servicio de datos.
- Las dos opciones de movimiento entran por parámetro en `inicializar` y se cambian con `establecerMovimiento`; **ausentes o parciales, los dos movimientos arrancan apagados**.
- Ninguna función recibe ni devuelve identidad, papel ni credencial.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Peticiones de red originadas por el bundle | Exactamente **0** | Conteo en la pestaña de red **con los dos movimientos prendidos y sostenidos**, y durante los gestos de rotar y acercar |
| Ocurrencias de las tres formas de petición en fuente y bundle | Exactamente **0** | Inspección automatizable, puerta bloqueante |
| Claves escritas en el almacenamiento del navegador | Exactamente **0** | Inspección del almacenamiento; se comprueba además que recargar no repone la preferencia |
| Lecturas de configuración propia | Exactamente **0** | Inspección del código fuente |
| Consultas a la preferencia de movimiento reducido del sistema | Exactamente **0** | Inspección del código fuente |
| Parámetros de identidad, papel o credencial en las seis funciones | Exactamente **0** | Inspección de la superficie pública |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §14 (RA-01, RA-02, RA-03), §17.7.P.3, §17.7.P.4, §17.7.P.5, §17.7.P.6, §17.7.P.10, §17.7.P.11 punto 1 y §17.7.P.12.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §5, que declara que la sexta función **no afloja RA-02**.
- [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2 (G-1, G-2, G-3), §3.3 y §5.5.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6, lugar único de las condiciones de medición.
- ADR relacionadas: [`ADR-02`](ADR-02-Superficie-De-Seis-Funciones-Planas.md), [`ADR-04`](ADR-04-Motor-De-Dibujo-Empaquetado-Y-Aislado.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra las cuatro ausencias del visualizador puro con la garantía que las expresa, declara que la sexta función confirma RA-02 en lugar de aflojarla, evalúa cuatro alternativas —incluidas las dos formas en que el comportamiento accesible tentaría a romper G-2 y G-3— y fija seis métricas de validación con sus condiciones de medición. |
