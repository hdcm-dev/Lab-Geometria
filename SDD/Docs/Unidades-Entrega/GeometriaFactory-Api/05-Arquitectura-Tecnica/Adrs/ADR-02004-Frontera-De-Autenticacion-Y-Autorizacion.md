# ADR-02004 — Frontera de autenticación: el dominio modela la condición y no el mecanismo

**Proyecto de código:** GeometriaFactory-Domain
**Documento:** ADR-02004-Frontera-De-Autenticacion-Y-Autorizacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

El flag `tiene_auth` de este proyecto de código es **true**, y el `PRODUCT-MANIFEST` §5 declara por qué y con qué efecto: fue corregido de false a true el 2026-08-09 alineando el manifiesto con la sección de trazabilidad del `PRODUCT-INTAKE`, porque el flag no distingue mecanismo de regla —el dominio contiene la regla que condiciona la autenticación, INV-06— y **el efecto declarado de esa corrección es que la categoría 05 de este proyecto de código emita su ADR de autenticación**, que es esta.

Lo que el dominio sí hace: modela el estado de la cuenta, la credencial derivada, la marca de cambio de contraseña pendiente y los invariantes INV-06, INV-08 e INV-09. Lo que no hace: derivar contraseñas, compararlas, emitir accesos ni manejar secretos; la contraseña le llega **ya derivada** (`PRODUCT-INTAKE` §17.1.P.5).

Sobre esa frontera cayeron dos decisiones recientes del producto que este proyecto de código tiene que sostener sin implementarlas: **RN-02014**, que la contraseña provisoria la produce el sistema y no la escribe el administrador, y **RN-02016**, que habilitar una cuenta produce su provisoria, con lo cual el producto pasó a tener un solo mecanismo de credencial inicial.

Motivación upstream: NB-00001, NB-00002; RN-02001, RN-02006, RN-02012, RN-02013, RN-02014, RN-02015, RN-02016; INV-06, INV-08, INV-09.

## 2. Decisión

El dominio **modela las condiciones de la autenticación y de la autorización por papel y por pertenencia, y no implementa ningún mecanismo**. En concreto:

1. La credencial derivada es un valor **opaco** para el dominio: se guarda, se reemplaza y se comprueba su presencia, nunca su contenido. El dominio **no compara credenciales**: la verificación de que la credencial presentada corresponde la hace quien la derivó, y el dominio la recibe como un hecho ya resuelto.
2. La **producción** de la contraseña provisoria de RN-02014 no ocurre acá: el valor le llega ya derivado, exactamente como cualquier otra credencial.
3. Lo que sí es de este proyecto de código es el **efecto** de esas dos operaciones sobre la cuenta: la habilitación (RN-02016) y el reseteo (RN-02014) ponen la **marca de cambio de contraseña pendiente**, y sólo el reemplazo hecho por la propia cuenta la levanta.
4. La autorización por **papel** —quién da desenlace, quién elimina en qué estado— y por **pertenencia** —de quién es el trabajo— son predicados del dominio y viven acá.
5. **Ningún tipo ni mensaje de este proyecto de código expone direcciones de servicios internos**, en cumplimiento de RA-03. Es trivial de sostener porque el dominio no conoce ninguna, y se declara para que no deje de serlo.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Modelar la condición y no el mecanismo (**adoptada**) | Mantiene las cero dependencias del nivel 0; los invariantes de acceso se prueban sin criptografía ni emisión de acceso; el mecanismo se puede cambiar sin tocar el dominio | Obliga a confiar en que el consumidor resolvió la verificación antes de invocar; la frontera hay que declararla explícitamente o se difumina |
| Derivar y comparar la contraseña dentro del dominio | Una sola puerta de credencial, imposible de saltear | Exige una biblioteca de criptografía en el nivel 0, lo que rompe ADR-02001 y la condición del intake §17.1.P.1; y ataría el dominio a un algoritmo concreto |
| Sacar del dominio también el estado de cuenta y la marca, dejándolos en la capa que autentica | El dominio quedaría más chico | INV-06, INV-08 e INV-09 dejarían de ser invariantes verificables sin infraestructura, que es la propiedad que ADR-02001 protege; y la instancia perdería la guarda que impide quedarse sin administrador |
| Producir la contraseña provisoria en el dominio | Un solo lugar donde nace la credencial inicial | El dominio no tiene fuente de aleatoriedad ni derivación, y RN-02014 exige que la provisoria no sea adivinable ni se repita: son propiedades de un mecanismo, no de una regla |

## 5. Consecuencias positivas

1. Las cero dependencias de [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) se conservan: no entra criptografía en el nivel 0.
2. INV-06, INV-08 e INV-09 se prueban con pruebas unitarias puras.
3. Cambiar el algoritmo de derivación o el formato del acceso no toca este proyecto de código.
4. **RN-02015 sale gratis**: como resetear no es una transición de la máquina de estados de cuenta, el dominio no tiene ninguna precondición de estado que comprobar, y por eso resetear procede sobre cuenta `Pendiente`, `Habilitado` y `Bloqueado`.
5. **INV-08 cierra la familia entera** de la condición sin salida: ninguna de las cuatro operaciones de ciclo de vida procede sobre la cuenta de administrador, de modo que ni nace `Pendiente` ni puede ser bloqueada después.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que el dominio confíe en el consumidor** sobre la verificación de la credencial presentada. La frontera queda declarada acá y en `Definicion-Modelo-De-Dominio.md` §7 para que nadie la busque en el lugar equivocado.
2. **Se acepta que RN-02014 no sea verificable en este proyecto de código.** Sus dos propiedades —no adivinable y no repetida— se verifican donde la provisoria se produce.
3. **Se acepta que INV-01 tampoco lo sea del todo**: la unicidad efectiva del correo la resuelve el consumidor, y el dominio sólo declara la condición.
4. **Se acepta una consecuencia declarada de la puerta única de admisibilidad**: si alguna vez existiera un camino que ejerza una capacidad sin pasar por la admisibilidad, ese camino tendría que volver a comprobar la marca, y esa comprobación no sería del dominio. Está desarrollada en [`ADR-02005`](ADR-02005-Guarda-Unica-De-Admisibilidad.md).

## 7. Implementación

- El componente **Guardas de cuenta** materializa las cuatro operaciones del ciclo de vida acotadas al papel `Alumno`, el reseteo y la fijación y el reemplazo de la credencial derivada.
- El componente **Evaluador de admisibilidad** materializa INV-06 e INV-09.
- La credencial derivada se declara como valor opaco: el contrato no expone ninguna operación de comparación ni de derivación.
- La marca se nombra **siempre con la palabra «marca»**, y `Pendiente` siempre calificado, según la regla de vocabulario de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §7.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Operaciones de derivación o comparación de credencial en la superficie pública | Exactamente **0** | Inspección de la superficie pública en revisión |
| Referencias a bibliotecas de criptografía | Exactamente **0** | Inspección del archivo de proyecto, la misma puerta que ADR-02001 |
| Invariantes de acceso ejercitados | **3 de 3** —INV-06, INV-08, INV-09— con prueba de violación rechazada, sin dobles | Matriz invariante contra prueba en 08 |
| Operaciones de ciclo de vida que proceden sobre la cuenta de administrador | Exactamente **0** de las cuatro | Prueba por operación: habilitar, bloquear, rehabilitar y dar de baja, las cuatro rechazadas |
| Reseteo sobre los tres estados de cuenta | **3 de 3** admitidos, y el estado de cuenta **sin cambiar** después de cada uno | Prueba por estado: `Pendiente`, `Habilitado` y `Bloqueado` |

## 9. Referencias

- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §5, fundamento de `tiene_auth` y de su corrección del 2026-08-09.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.5, §4.1 (RN-02006, RN-02012 a RN-02016), §17.1.P.2 (INV-06, INV-08, INV-09), §14 (RA-03).
- [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1, §5.1, §5.3 y §7.
- [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) y [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md).
- ADR relacionadas: [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-02005`](ADR-02005-Guarda-Unica-De-Admisibilidad.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Es la ADR de autenticación que el `PRODUCT-MANIFEST` §5 declara como efecto de haber corregido `tiene_auth` a true en este proyecto de código. Registra la frontera entre condición y mecanismo en cinco puntos, cuatro alternativas evaluadas, el tratamiento de RN-02014, RN-02015 y RN-02016 y cinco métricas de validación. |
