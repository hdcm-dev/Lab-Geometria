# ADR-06001 — Un adaptador por puerto, sin repositorio genérico y sin adaptador único

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

`GeometriaFactory-Application` declara **cuatro** puertos y este proyecto de código los implementa. El intake ya descartó dos formas de hacerlo —repositorio genérico sobre el conjunto de entidades y consultas escritas a mano— y dejó dicho el estilo: «adaptadores que implementan los puertos de Application» (`PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Infrastructure). Lo que ninguna fuente resuelve es **cuántas piezas son** y **qué pasa con lo que no es puerto**: los dos mecanismos de seguridad y la preparación del almacén, que la categoría 02 declara explícitamente como cosas que **no** son puertos de la capa de aplicación.

Hay además una tensión propia de esta capa que la partición tiene que resolver. La mitad de lo que vive acá **toca el almacén** —guardar, recuperar, retirar, preparar— y la otra mitad **no lo toca**: interpretar un texto, derivar una contraseña, producir una provisoria, firmar un acceso, decir qué hora es. Si las dos mitades quedan en la misma pieza, la batería obligatoria del validador —que es la mitigación declarada del único riesgo alto de negocio del producto— pasa a necesitar un almacén para correr.

Motivación upstream: NB-00003, NB-00004; RN-06003, RN-06008, RN-06009, RN-06011; INV-02, INV-03; `PRODUCT-INTAKE` §11 (RN-B3), §17.1.P.2 · GeometriaFactory-Infrastructure, §17.1.P.6 · GeometriaFactory-Infrastructure, §17.1.P.12 · GeometriaFactory-Infrastructure.

## 2. Decisión

**Un adaptador por puerto, cuatro en total, y ninguna clase que los reúna.** Los mecanismos que no son puertos se declaran como componentes propios y no se disfrazan de adaptador. La partición queda en **ocho** componentes, con la frontera de prueba pasando por el medio:

1. **Cuatro adaptadores de puerto**: trabajos, cuentas, validación de figuras y reloj.
2. **Un contexto de persistencia y mapeo transversal**, del que dependen sólo los componentes que tocan el almacén.
3. **Dos motores** —interpretación y verificación de valores— detrás del puerto de validación, y **dos mecanismos** —credenciales y acceso firmado— que no son puertos.
4. **La preparación del almacén** vive junto al mecanismo de acceso firmado, por compartir la única condición que detiene el arranque.

**Consecuencia estructural que la decisión compra:** los dos motores, el reloj y el mecanismo de credenciales **no dependen del contexto de persistencia**, de modo que se ejercen enteros sin almacén.

**Este proyecto de código no se autorregistra.** Declara sus adaptadores y no decide sus ciclos de vida ni los conecta: eso es de la composición de raíz de `GeometriaFactory-Api`.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Un adaptador por puerto, con los mecanismos como componentes propios (**adoptada**) | La frontera queda en cuatro lugares contables; la mitad que no toca el almacén se prueba unitariamente; cada adaptador se sustituye por un doble sin arrastrar a los demás | Más tipos que en cualquiera de las alternativas, y la responsabilidad de conectarlos cae afuera |
| Repositorio genérico sobre el conjunto de entidades | Un solo tipo para las cinco entidades | **Descartada por el intake §17.1.P.2 · GeometriaFactory-Infrastructure**: diluye las consultas que sí importan y obliga a armar el recorte del lado del consumidor, que es lo que `CONSULTA_SIN_ALCANCE_DECLARADO` viene a impedir |
| Consultas escritas a mano sobre el almacén | Control total de cada consulta | **Descartada por el intake §17.1.P.2 · GeometriaFactory-Infrastructure**: las transformaciones de esquema aplicadas al arrancar son decisión tomada y el mapeador las provee |
| Un adaptador único que implemente los cuatro puertos | Menos tipos; la unidad de trabajo queda evidente en un solo lugar | **Descartada acá.** El validador arrastraría la dependencia de persistencia y la batería obligatoria dejaría de correr sin almacén, que es exactamente la propiedad que la mitigación del riesgo `RN-B3` necesita |
| Registrar los adaptadores desde este proyecto de código, con un punto de entrada de composición propio | Un solo lugar donde se conectan, escrito por quien los conoce | Haría que la frontera dejara de ser contable desde afuera y que `GeometriaFactory-Api` no pudiera sustituir un adaptador por un doble en su batería de integración sin tocar esta biblioteca |

## 5. Consecuencias positivas

1. La batería obligatoria del validador corre **sin almacén**, que es lo que hace que el riesgo `RN-B3` tenga una mitigación barata de ejecutar en cada fusión.
2. La frontera queda en **cuatro** adaptadores contables, y 08 puede escribir una matriz puerto contra doble y verificar que no hay una quinta vía de salida.
3. Los dos mecanismos de seguridad quedan **separados de los adaptadores**, lo que hace visible que no son puertos y evita que alguien busque una negativa de autorización en esta capa.
4. La proyección de listado y el detalle completo conviven en el mismo adaptador **como dos formas de lectura distintas**, lo que permite exigir por prueba que la primera no cargue componentes.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta un número de tipos mayor que el de cualquier alternativa.** El costo se paga una vez, al escribirlos; la propiedad que compra se cobra en cada corrida de pruebas.
2. **Se acepta que la conexión de puertos con adaptadores viva en otro proyecto de código**, con la consecuencia de que un adaptador nuevo no queda conectado por el solo hecho de existir. Es deliberado: la frontera se audita desde la composición de raíz.
3. **Se acepta que el contexto de persistencia sea un componente transversal**, con cuatro casos de uso apoyados en él. Es el precio de tener un solo mapa del esquema en lugar de uno por adaptador.

## 7. Implementación

- Los ocho componentes son los de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1, y §3.3 declara qué caso de uso cubre cada uno.
- **Convención impuesta:** ningún componente que no toque el almacén puede declarar una dependencia hacia el contexto de persistencia. Una dependencia así se rechaza en revisión, porque rompe la propiedad de §2.
- **Convención impuesta:** los adaptadores no se registran solos. La composición de raíz de `GeometriaFactory-Api` es el único lugar donde se conectan.
- La proyección de listado y el detalle completo son dos operaciones distintas del mismo adaptador, declaradas en [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Adaptadores de puerto | Exactamente **4** | Inspección de la superficie que implementa contratos de `GeometriaFactory-Application` |
| Componentes que no tocan el almacén y dependen del contexto de persistencia | Exactamente **0** | Inspección de dependencias de los dos motores, del reloj y del mecanismo de credenciales |
| Casos de la batería del validador que corren sin almacén | **10 de 10** | Etapa de `test` del pipeline, sin archivo de datos disponible |
| Componentes de pieza cargados en una consulta de listado | Exactamente **0** | Prueba que comprueba que la colección no viene materializada |
| Puntos de registro de adaptadores dentro de este proyecto de código | Exactamente **0** | Inspección en revisión |
| Cobertura del catálogo de condiciones | **100 %** de las **17**, en las dos direcciones | Prueba de inspección contra [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §7.3 |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §11 (RN-B3), §17.1.P.2 · GeometriaFactory-Infrastructure, §17.1.P.6 · GeometriaFactory-Infrastructure y §17.1.P.12 · GeometriaFactory-Infrastructure.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, §4 y §8.
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md`](ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), que declara la frontera que esta ADR materializa.
- ADR relacionadas: [`ADR-06002`](ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md), [`ADR-06006`](ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija un adaptador por puerto y la partición en ocho componentes, con la frontera de prueba pasando entre lo que toca el almacén y lo que no; evalúa cinco alternativas, dos de ellas ya descartadas por el intake; declara tres trade-offs y seis métricas de validación. |
