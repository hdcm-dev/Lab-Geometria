# ADR-02005 — Puerta única de admisibilidad para las guardas de acceso de la cuenta

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-02005-Guarda-Unica-De-Admisibilidad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

**INV-09** enuncia que una cuenta con la marca de cambio de contraseña pendiente no ejerce **ninguna** capacidad del sistema salvo cambiar su propia contraseña. El alcance es total, y el dominio **no tiene una puerta única por la que pasen todas las capacidades**: tiene trece contratos de uso repartidos en cinco componentes.

La categoría 02 ya tomó una decisión derivada sobre esto y la declaró como tal: concentrar la guarda en **CU-02004**, la evaluación de admisibilidad, con el mismo fundamento por el que INV-06 vive ahí y no repetido en cada caso de uso ([`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4.1, tercera precisión de ubicación). Lo que esta ADR hace es registrar la decisión en el plano arquitectónico, declarar su consecuencia para la capa que expone y fijar cómo se verifica.

Hay un antecedente que pesa. La familia de defectos que se cierra con una condición permanente en lugar de con guardas repartidas **ya se abrió dos veces por puertas distintas** en este proyecto de código: la cuenta de administrador naciendo `Pendiente` (P0) y la misma cuenta pudiendo ser bloqueada después (P1 de la ronda r3), las dos terminando con la instancia sin nadie capaz de habilitar, desbloquear ni revisar (`B-02-03-GeometriaFactory-Domain-r3.md`). La guarda repartida es exactamente la forma en que esos defectos entran.

Motivación upstream: NB-00001, NB-00002; RN-02006, RN-02012, RN-02013, RN-02016; INV-06, INV-09.

## 2. Decisión

**El evaluador de admisibilidad es la puerta única de las guardas de acceso de la cuenta.** INV-06 e INV-09 se ejercen ahí y en ningún otro componente: una cuenta `Pendiente`, una cuenta `Bloqueado` y una cuenta con la marca puesta son **no admisibles**, cada una con su motivo propio.

En consecuencia, **ninguna otra operación del dominio vuelve a comprobar el estado de cuenta ni la marca**: ninguna se ejerce sin admisión resuelta. La única capacidad que la cuenta con la marca puesta alcanza es el reemplazo de su propia credencial derivada, que por eso **no exige admisibilidad** sino la credencial vigente verificada.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Puerta única en el evaluador de admisibilidad (**adoptada**) | Un solo lugar que auditar; el invariante se verifica con una prueba por motivo y no con una por operación; es la misma forma con la que INV-06 ya vivía | Depende de que ningún camino ejerza una capacidad sin pasar por la admisibilidad, y eso no lo puede garantizar el dominio solo |
| Comprobar la marca en cada operación del dominio | No depende de que el consumidor pase por la admisibilidad | Trece lugares que mantener sincronizados; es exactamente la forma en que se abrieron el P0 y el P1, con la diferencia de que ahí eran cuatro operaciones y acá serían trece |
| Un tipo «cuenta admitida» que las demás operaciones exijan como parámetro | Hace estructuralmente imposible operar sin admisión resuelta | Obliga a que toda operación del dominio reciba ese tipo, incluidas las que operan sobre una cuenta ajena —habilitar, resetear—, donde la cuenta admitida es la del administrador y no la operada: el tipo tendría dos sentidos según la operación |
| Dejar la guarda enteramente en la capa que expone | El dominio queda más chico | INV-09 dejaría de ser un invariante del dominio y pasaría a ser una convención de la capa de aplicación, verificable sólo con infraestructura |

**La tercera alternativa se descartó por poco**, y conviene dejarlo escrito: es la única que hace estructuralmente imposible el defecto, y se rechaza por el doble sentido que el parámetro tomaría en las operaciones sobre cuenta ajena. Si esa ambigüedad se resolviera —por ejemplo con dos tipos distintos, el solicitante y el operado—, la decisión merecería una ADR nueva que superara a ésta.

## 5. Consecuencias positivas

1. INV-06 e INV-09 se verifican en un solo componente, con una prueba por motivo.
2. El catálogo de condiciones de 03 lo refleja: el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` vive en CU-02004, que es donde el dominio ejerce la guarda.
3. Aguas abajo, la capa que expone traduce **un solo código** para todas las operaciones bloqueadas por la marca, en lugar de uno por operación. Es exactamente lo que el contrato del producto ya decidió con `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` ([`../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md`](../../../../Producto/Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) §10).
4. Agregar una capacidad nueva al producto no obliga a agregarle una guarda de marca: le basta con pasar por la admisibilidad.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta una dependencia de disciplina en la capa que expone.** El dominio no puede impedir que exista un camino que ejerza una capacidad sin resolver la admisión. La consecuencia está declarada: si ese camino existiera, tendría que volver a comprobar la marca, **y esa comprobación no sería del dominio**.
2. **Se acepta que la admisibilidad devuelva varios motivos y no uno solo.** Una cuenta puede ser no admisible por más de una causa a la vez, y colapsarlas perdería información que la capa que expone usa.
3. **Se acepta que el reemplazo de credencial sea la excepción declarada** a la puerta única: es la única operación que una cuenta con la marca puesta alcanza, y por eso no exige admisibilidad.

## 7. Implementación

- El componente **Evaluador de admisibilidad** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que lee el estado de cuenta y la marca a efectos de acceso.
- **Los otros cuatro componentes no leen la marca.** Las guardas de cuenta la **escriben** —la habilitación y el reseteo la ponen, el reemplazo la levanta—, que es una operación distinta de leerla para decidir acceso.
- Convención impuesta al consumidor: resolver la admisibilidad **antes** de invocar cualquier otra operación en nombre de esa cuenta.
- Verificación estructural sugerida a 08: recorrer los cinco componentes y comprobar que ninguno salvo el evaluador consulta la marca para decidir acceso.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Componentes que leen la marca para decidir acceso | Exactamente **1** | Inspección de los cinco componentes |
| Motivos de no admisibilidad ejercitados | **100 %** de los declarados en CU-02004, cada uno con su prueba | Matriz motivo contra prueba en 08 |
| Operaciones bloqueadas por la marca con código propio | Exactamente **1** código para todas ellas | Prueba que pide tres operaciones distintas con la marca puesta y comprueba el mismo motivo |
| Orígenes de la marca que producen el mismo motivo | **2 de 2** —habilitación y reseteo— | Prueba con una cuenta recién habilitada y una reseteada |
| Reemplazo de credencial con la marca puesta | Procede en **100 %** de los casos, y levanta la marca | Prueba de la única capacidad admitida |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §4.1 (tercera precisión de ubicación) y §5.3.
- [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) y [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).
- `SDD/Docs/Audit/B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo H-01, por el antecedente de la familia de defectos.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.2 · GeometriaFactory-Domain (INV-06, INV-09) y §4.1 (RN-02013, RN-02016).
- ADR relacionadas: [`ADR-02002`](ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), [`ADR-02004`](ADR-02004-Frontera-De-Autenticacion-Y-Autorizacion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra en el plano arquitectónico la puerta única de admisibilidad que la categoría 02 había declarado como decisión derivada, evalúa cuatro alternativas —con la tercera descartada por poco y con la condición bajo la cual merecería una ADR que supere a ésta—, declara la consecuencia para la capa que expone y fija cinco métricas de validación. |
