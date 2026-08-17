# ADR-04001 — Casos de uso con inversión de dependencias, con una sola dependencia saliente

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

El producto necesita una capa donde vivan los **once** casos de uso y donde se decida **quién puede hacer qué**, sin que esa decisión dependa de tener una base de datos levantada. La fuente lo exige de manera explícita: la autorización por pertenencia —«un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador en la petición»— es **lo que hay que poder probar**, y probarlo contra una base real la vuelve lenta, frágil y dependiente del orden de las pruebas.

La restricción estructural es que este proyecto de código es **nivel 1** del orden topológico: por debajo sólo tiene `GeometriaFactory-Domain`, que ya declaró cero dependencias salientes. Si esta capa referenciara una biblioteca de persistencia, la propiedad que el nivel 0 compró se perdería una capa más arriba, porque toda prueba de caso de uso arrastraría el motor.

Motivación upstream: NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00006, NB-00007, NB-00009; RN-04002, RN-04003, RN-04005, RN-04008, RN-04009; INV-01, INV-02, INV-03, INV-05; `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Application, §17.1.P.2 · GeometriaFactory-Application, §17.1.P.6 · GeometriaFactory-Application y §17.1.P.12 · GeometriaFactory-Application.

## 2. Decisión

**Los casos de uso de esta capa declaran los puertos que necesitan y otra capa los implementa: la dependencia se invierte.** El proyecto de código referencia `GeometriaFactory-Domain` y **nada más** —ni biblioteca de persistencia, ni marco web, ni cliente de transporte, ni biblioteca de serialización—.

En consecuencia, **un caso de uso completo se ejerce con dobles de sus puertos**, sin base de datos y sin frontera de proceso, y la batería de esta capa es **100 %** unitaria: la integración pertenece a `GeometriaFactory-Api`.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Casos de uso con inversión de dependencias (**adoptada**) | La autorización por pertenencia se prueba sin base; el nivel 1 conserva la propiedad del nivel 0; el validador de figuras —la pieza con más reglas verificadas del producto— queda aislado detrás de un puerto | Obliga a escribir a mano el mapeo entre entidades y tipos de transferencia, y renuncia a consultar la base con proyecciones a medida desde el caso de uso |
| Servicios que consultan directamente el contexto de persistencia | Menos tipos y menos ceremonia; consultas a medida por caso de uso | Haría imposible probar la autorización por pertenencia sin base de datos, que es justo lo que la fuente exige probar. **Descartada por `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Application** |
| Mediador con manejadores y canalización de comportamientos | Los comportamientos transversales —autorización, unidad de trabajo— se resuelven una sola vez en la canalización | Sobre-ingeniería para el alcance que la fuente declara **básica**, y metería en el nivel 1 una infraestructura de la que hoy no depende. **Descartada por `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Application** |
| Un caso de uso por operación elemental, en lugar de los once del recorte de 02 | Contratos más chicos, una postcondición cada uno | Multiplicaría los lugares donde repetir las cuatro comprobaciones y la unidad de trabajo, y cambiaría identificadores `CU-XX` que 03, 06 y 08 ya citan. **Descartada por esta categoría** |

## 5. Consecuencias positivas

1. La verificación de pertenencia, que es la razón por la que `tiene_auth` vale true en este proyecto de código, se prueba con dobles y sin base de datos.
2. El validador de figuras queda detrás de un puerto y se puede ejercer con textos fijos, incluido el escenario semilla `E-1`.
3. El momento entra por puerto, de modo que los sellos son reproducibles en prueba y la batería no depende del reloj de la máquina.
4. Un cambio de motor de persistencia no toca ningún caso de uso: toca al adaptador.
5. La propiedad de «cero dependencias» del nivel 0 se conserva un nivel más arriba con una sola arista, que es verificable por inspección del archivo de proyecto.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta escribir a mano el mapeo entre entidades y tipos de transferencia.** El intake lo declara explícitamente como trade-off aceptado (§17.1.P.12 · GeometriaFactory-Application).
2. **Se renuncia a consultar la base con proyecciones a medida desde el caso de uso**, a cambio de poder probarlo entero con dobles. El costo se paga en la forma del puerto de repositorio, que tiene que ofrecer la proyección que el listado necesita.
3. **Se acepta que el doble de un puerto pueda mentir.** Una prueba con dobles verifica que el caso de uso hace lo que corresponde con lo que el puerto devuelve, no que el adaptador devuelva eso. Esa segunda mitad la cubre la batería de integración de `GeometriaFactory-Api`, y esta ADR la declara para que no se dé por cubierta acá.

## 7. Implementación

- Los **seis** orquestadores de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 materializan los once casos de uso; los otros dos componentes —guarda de autorización y declaración de puertos— son transversales.
- Ningún orquestador depende de otro orquestador: los seis se apoyan en la guarda, en los puertos y en el dominio.
- El archivo de proyecto declara **una** referencia de proyecto de código y ninguna referencia a bibliotecas de persistencia, transporte, serialización o marco web.
- Convención impuesta al consumidor: la composición de raíz de `GeometriaFactory-Api` conecta cada puerto con su adaptador de `GeometriaFactory-Infrastructure`; esta capa no conoce ninguno de los dos.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Referencias salientes del archivo de proyecto | Exactamente **1**, y **0** a persistencia, transporte, serialización o marco web | Inspección del archivo de proyecto, bloqueante en revisión |
| Pruebas de esta capa que tocan la base de datos real | Exactamente **0** | Puerta propia y bloqueante del pipeline (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Application) |
| Proporción de pruebas unitarias en la batería del proyecto de código | **100 %** | Recuento por proyecto de pruebas |
| Tiempo del caso de uso más pesado | Menos de **500 ms** para el texto semilla de **3** piezas de `E-1`, sin acceso a base [ASUNCIÓN del intake] | Medición sobre la batería unitaria con doble del puerto de validación |
| Cobertura de la biblioteca | **85 %** de líneas y **80 %** de ramas [ASUNCIÓN del intake] | Informe de cobertura del pipeline |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §17.1.P.1 · GeometriaFactory-Application, §17.1.P.2 · GeometriaFactory-Application, §17.1.P.6 · GeometriaFactory-Application, §17.1.P.8 · GeometriaFactory-Application, §17.1.P.10 · GeometriaFactory-Application y §17.1.P.12 · GeometriaFactory-Application; §22 asunciones A-3 y A-5.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §1 y §3.
- [`../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), la decisión del nivel 0 que ésta continúa.
- ADR relacionadas: [`ADR-04002`](ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), [`ADR-04005`](ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el estilo de casos de uso con inversión de dependencias con su única dependencia saliente, evalúa cuatro alternativas —dos descartadas por el intake y dos por esta categoría—, declara tres trade-offs incluido el que la prueba con dobles no cubre, y fija cinco métricas de validación. |
