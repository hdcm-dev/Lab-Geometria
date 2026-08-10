# ADR-02 — Cuatro puertos, y qué significa que el cuarto no tenga nombre declarado

**Proyecto de código:** GeometriaFactory-Application
**Documento:** ADR-02-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

[`ADR-01`](ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md) decide que la dependencia se invierte. Lo que esa decisión deja abierto es **cuántas fronteras hay y qué pasa por cada una**, y ahí hay un desajuste entre las fuentes que esta categoría tiene que resolver sin inventar nada.

`PRODUCT-INTAKE` §17.2.P.1 y §14 nombran **tres** puertos: `IRepositorioTrabajos`, `IValidadorFiguras` e `IRelojDelSistema`. Pero la Fase C de `GeometriaFactory-Domain` decidió que **el dominio no lee el conjunto de entidades**: la unicidad del correo y la existencia previa de una cuenta con papel `Administrador` se las tiene que aportar el consumidor. Y ese consumidor es esta capa. Una verificación sobre un conjunto no es posible sin una frontera que lo alcance, de modo que el **cuarto** puerto —el repositorio de cuentas— **existe por necesidad estructural**, no por preferencia.

La categoría 02 de esta capa lo declaró, lo nombró en lenguaje de dominio y elevó el punto abierto a esta categoría. Lo que falta no es la decisión de que exista: es su nombre.

Motivación upstream: NB-01, NB-02; RN-01, RN-02, RN-09, RN-14, RN-16; INV-01, INV-05; `PRODUCT-INTAKE` §17.2.P.1, §17.2.P.3, §17.2.P.11.

## 2. Decisión

**Esta capa declara cuatro puertos, y son la única frontera del proyecto de código**: repositorio de trabajos, validación de figuras, reloj del sistema y **repositorio de cuentas**.

Sobre el cuarto, la decisión tiene dos mitades:

1. **Se confirma que el puerto existe**, con el alcance que la categoría 02 le dio: recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado, incluida la marca de cambio de contraseña pendiente.
2. **No se le fija identificador acá.** Su nombre queda declarado punto abierto y atado al punto de control de la etapa `a`, junto con los demás nombres de tipos. Esta ADR nombra al puerto en lenguaje de dominio, como hace toda la cadena de este proyecto de código.

**No se agrega ningún puerto más.** En particular, la producción de la contraseña provisoria **no abre frontera nueva**: el valor llega a esta capa ya producido y ya derivado, del mismo lado desde el que llega la contraseña que el alumno elige.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Cuatro puertos, con el cuarto confirmado y su nombre abierto (**adoptada**) | Refleja lo que la arquitectura de dominio ya decidió; no inventa un identificador que ninguna fuente declara; deja el nombre donde el intake ya puso todos los demás | Arrastra un punto abierto hasta la etapa `a`, y cuatro componentes lo consumen sin nombre estable |
| Tres puertos, con la unicidad resuelta dentro del puerto de trabajos | Respeta la letra del intake sin agregar nada | El puerto de trabajos pasaría a responder preguntas sobre cuentas: una frontera con dos dominios adentro, que además haría que la operación de alta de cuenta dependiera del repositorio de trabajos |
| Cuatro puertos, fijando acá el identificador del cuarto | Cierra el punto abierto de inmediato | **Inventaría un identificador de código que ninguna fuente declara**, en un producto donde el intake ató todos los nombres de tipos al mismo punto de control. Un nombre inventado acá se propagaría a 06, 08 y 09 como si fuera de la fuente |
| Un quinto puerto para producir la contraseña provisoria | Haría explícito que el sistema la produce y no el administrador (RN-14) | El valor llega ya producido y ya derivado: no hay nada que esta capa le pida a nadie. La categoría 02 lo resolvió así en su §8 y esta ADR no lo reabre |

## 5. Consecuencias positivas

1. La verificación de unicidad del correo tiene dónde ejercerse, y `INV-01` queda con su mitad de conjunto cubierta sin que el dominio lea nada.
2. La ventana de alta del administrador tiene cómo resolverse: `INV-05` exige saber si ya existe una cuenta con ese papel, y ése es el puerto que lo responde.
3. El momento entra por puerto y no por reloj de la máquina, que es lo que hace reproducibles los sellos en prueba.
4. La frontera queda en cuatro lugares contables, lo que permite que 08 escriba una matriz puerto contra doble y verifique que no hay una quinta vía de salida.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta arrastrar un punto abierto de nombre hasta la etapa `a`.** El costo es de retrabajo —cuatro componentes lo consumen— y no de corrección. Se acepta porque la alternativa es inventar un identificador de código, que es un costo peor y menos visible.
2. **Se acepta que el intake quede en deuda con su propia §17.2.P.1**, que nombra tres puertos sobre cuatro. Esta ADR **no corrige el intake**: lo declara, porque corregirlo es del Product Owner sobre su propio documento.
3. **Se acepta que el puerto de repositorio de trabajos tenga que ofrecer la proyección de listado sin componentes**, en lugar de que el caso de uso la arme con una consulta a medida. Es la contrapartida de [`ADR-01`](ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md).

## 7. Implementación

- El componente **Declaración de puertos** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único lugar donde los cuatro se declaran; §3.4 declara qué componente consume cada uno.
- Ningún orquestador declara un puerto propio: si un caso de uso necesitara algo que ninguno de los cuatro ofrece, la decisión es **ampliar el puerto que corresponda o abrir uno nuevo con ADR propia**, nunca resolverlo dentro del orquestador.
- Los adaptadores viven en `GeometriaFactory-Infrastructure` y la composición de raíz de `GeometriaFactory-Api` los conecta. Esta capa no nombra a ninguno de los dos proyectos de código.
- Convención impuesta: mientras el cuarto puerto no tenga identificador, se lo nombra en lenguaje de dominio en todos los artefactos, igual que hacen 02 y 03.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Puertos declarados por esta capa | Exactamente **4** | Inspección de la superficie pública |
| Puertos declarados fuera del componente de declaración | Exactamente **0** | Inspección de los seis orquestadores |
| Casos de uso que consumen al menos un puerto | **11 de 11** | Matriz caso de uso contra puerto de [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3 |
| Puertos ejercitados con doble en la batería unitaria | **4 de 4** | Matriz puerto contra doble en 08 |
| Vías de salida del proyecto de código distintas de los cuatro puertos | Exactamente **0** | Inspección de dependencias salientes del archivo de proyecto |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §14, §17.2.P.1, §17.2.P.3 y §17.2.P.11.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, §8 y §11.
- [`../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md`](../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md), que es la decisión del nivel 0 de la que este cuarto puerto se deduce.
- ADR relacionadas: [`ADR-01`](ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-05`](ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma los cuatro puertos como única frontera del proyecto de código, resuelve la mitad decidible del punto abierto que la categoría 02 elevó —el cuarto puerto existe— y declara explícitamente que su identificador no se inventa acá, evalúa cuatro alternativas incluida la de fijar el nombre, declara tres trade-offs y fija cinco métricas de validación. |
