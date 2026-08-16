# Propuesta de deduplicación de casos de uso

**Documento:** Migracion-8.1-Deduplicacion-Propuesta.md
**Fecha:** 2026-08-15
**Regla:** `Migracion-Rules.md` §4.3.2, paso 3
**Estado:** **Propuesta. No aplicada.** La decisión es del Product Owner

---

## 1. Por qué existe este documento

Al fundir varios árboles de proyecto de código en una unidad de entrega, los casos de uso de cada capa
se juntan en una sola categoría 02. Muchos describen **la misma capacidad del producto vista desde
otra capa**: el dominio la modela, la aplicación la orquesta, la infraestructura la persiste y la API
la expone.

En el modelo anterior eso era correcto: cada proyecto de código tenía su categoría 02 y su propio
catálogo. En el modelo 8.x la unidad de entrega es una sola y sus capas son internas, de modo que
**cuatro casos de uso de la misma capacidad son cuatro vistas de uno**.

La migración **no los fusiona**: la regla lo prohíbe, porque fundir por coincidencia de título produce
pérdidas invisibles. Los conserva todos, con su origen legible en el identificador, y propone acá la
lista.


## Unidad de entrega `GeometriaFactory-Api`

46 casos de uso tras la fusión. **26** pares candidatos, de capas distintas.

| Semejanza | Caso de uso | Capa de origen | Caso de uso | Capa de origen |
| --- | --- | --- | --- | --- |
| 1.00 | `CU-04010` Configurar La Cuenta De Administrador | Application | `CU-02012` Configurar La Cuenta De Administrador | Domain |
| 0.86 | `CU-02013` Resetear La Contrasena De Una Cuenta De Alumno | Domain | `CU-04011` Resetear La Contrasena De Un Alumno | Application |
| 0.85 | `CU-04001` Registrar El Alta De Una Cuenta | Application | `CU-02001` Registrar El Alta De Un Alumno | Domain |
| 0.84 | `CU-02005` Crear Y Reeditar Un Trabajo | Domain | `CU-04004` Cargar Y Reeditar Un Trabajo Propio | Application |
| 0.80 | `CU-00005` Exponer El Reseteo De La Contrasena De Un Alumno | Api | `CU-04011` Resetear La Contrasena De Un Alumno | Application |
| 0.79 | `CU-04002` Gobernar Las Cuentas De La Comision | Application | `CU-06005` Guardar Y Recuperar Las Cuentas De La Comision | Infrastructure |
| 0.79 | `CU-04002` Gobernar Las Cuentas De La Comision | Application | `CU-00004` Exponer El Gobierno De Las Cuentas De La Comision | Api |
| 0.71 | `CU-02010` Resolver El Desenlace Del Trabajo | Domain | `CU-00008` Exponer El Desenlace De La Revision | Api |
| 0.70 | `CU-00005` Exponer El Reseteo De La Contrasena De Un Alumno | Api | `CU-02013` Resetear La Contrasena De Una Cuenta De Alumno | Domain |
| 0.68 | `CU-00005` Exponer El Reseteo De La Contrasena De Un Alumno | Api | `CU-04003` Resolver El Ingreso Y La Credencial Del Alumno | Application |
| 0.68 | `CU-02010` Resolver El Desenlace Del Trabajo | Domain | `CU-04008` Dar Desenlace A Un Trabajo | Application |
| 0.68 | `CU-02005` Crear Y Reeditar Un Trabajo | Domain | `CU-06003` Guardar Y Recuperar Los Trabajos | Infrastructure |
| 0.67 | `CU-06005` Guardar Y Recuperar Las Cuentas De La Comision | Infrastructure | `CU-00004` Exponer El Gobierno De Las Cuentas De La Comision | Api |
| 0.65 | `CU-02005` Crear Y Reeditar Un Trabajo | Domain | `CU-04009` Eliminar Un Trabajo | Application |
| 0.65 | `CU-02001` Registrar El Alta De Un Alumno | Domain | `CU-04011` Resetear La Contrasena De Un Alumno | Application |
| 0.64 | `CU-06005` Guardar Y Recuperar Las Cuentas De La Comision | Infrastructure | `CU-04007` Revisar Los Trabajos De La Comision | Application |
| 0.64 | `CU-04002` Gobernar Las Cuentas De La Comision | Application | `CU-02012` Configurar La Cuenta De Administrador | Domain |
| 0.64 | `CU-06006` Derivar La Contrasena Y Verificar Una Credencial | Infrastructure | `CU-02013` Resetear La Contrasena De Una Cuenta De Alumno | Domain |
| 0.63 | `CU-04002` Gobernar Las Cuentas De La Comision | Application | `CU-02002` Gobernar El Ciclo De Vida De La Cuenta | Domain |
| 0.63 | `CU-04004` Cargar Y Reeditar Un Trabajo Propio | Application | `CU-06003` Guardar Y Recuperar Los Trabajos | Infrastructure |
| 0.62 | `CU-02008` Gobernar El Estado Del Trabajo | Domain | `CU-00007` Exponer El Listado Y El Detalle De Los Trabajos | Api |
| 0.62 | `CU-04001` Registrar El Alta De Una Cuenta | Application | `CU-02004` Evaluar La Admisibilidad De La Cuenta | Domain |
| 0.61 | `CU-04001` Registrar El Alta De Una Cuenta | Application | `CU-02002` Gobernar El Ciclo De Vida De La Cuenta | Domain |
| 0.61 | `CU-06008` Emitir El Acceso Firmado | Infrastructure | `CU-00001` Canjear Credenciales Por Un Acceso Firmado | Api |
| 0.60 | `CU-02005` Crear Y Reeditar Un Trabajo | Domain | `CU-04008` Dar Desenlace A Un Trabajo | Application |
| 0.60 | `CU-06006` Derivar La Contrasena Y Verificar Una Credencial | Infrastructure | `CU-04011` Resetear La Contrasena De Un Alumno | Application |

## Unidad de entrega `GeometriaFactory-Web`

17 casos de uso tras la fusión. **0** pares candidatos, de capas distintas.

| Semejanza | Caso de uso | Capa de origen | Caso de uso | Capa de origen |
| --- | --- | --- | --- | --- |

## 2. Cómo se lee esta lista

La semejanza es de **título**, no de contenido: es un indicio para ordenar la revisión, no un
veredicto. Un par con semejanza alta puede ser dos casos de uso legítimamente distintos, y uno con
semejanza baja puede ser el mismo flujo con dos nombres.

## 3. Las tres salidas por par

1. **Son el mismo caso de uso.** Se conserva uno —el de la capa que lo expone al usuario— y el resto
   se absorbe como detalle de su flujo o de la arquitectura de la unidad de entrega. El absorbido se
   archiva con la referencia al que lo reemplaza.
2. **Son casos de uso distintos.** Se conservan los dos y se precisa el título para que la distinción
   sea legible sin abrirlos.
3. **Ninguno es un caso de uso de la unidad de entrega.** Describe una operación interna de una capa,
   y su lugar es la arquitectura de la unidad, no su especificación funcional.

## 4. Lo que no hay que hacer

Fusionar por coincidencia de título. Los cuatro casos de uso de una misma capacidad **no dicen lo
mismo**: cada uno declara los actores, las precondiciones y los criterios de aceptación de su capa, y
la unión no es la suma de sus partes. La consolidación exige leer los cuatro y escribir uno.

