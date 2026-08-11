# Representación — Sello de versión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Representacion-Sello-De-Version.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `Design-Rules-Identidad-De-Version.md` §1 a §9 completo; `Design-Rules-Blazor-Mudblazor.md` §4.2; `Design-Rules-Web-Generico.md` §2.2, §7, §10; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03); `../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md` §10; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14 (RA-03), §17.6 P.7 y P.8 (la versión se deriva de la construcción), §11 (RN-B2, RN-B4); `../../../00-Contexto/Compatibilidad-Plataformas.md` §5
**Trazabilidad downstream:** la **Fase B2** de validación visual de maqueta, que la materializa como componente reutilizado del inventario identificado, presente en los dos shells; `05-Arquitectura-Tecnica`, que resuelve el contrato en el punto de composición; `09-Devops`, que produce el dato al construir el artefacto; `06-Backlog-Tecnico`, **`US-08`** heredada de `Wireframes-Aprovisionamiento-Inicial.md` y **`US-03`, `US-04`, `US-05`** de `Wireframes-Ingreso.md`, en su parte de identificación de la instancia; `08-Calidad-Y-Pruebas`, con cuatro escenarios propios: **presencia del sello en las dos ubicaciones obligatorias** —superficie de acceso y sistema en funcionamiento—, **copiado del diagnóstico en un solo gesto con su confirmación anunciada**, **el distintivo de artefacto preliminar y el marcador de origen indeterminado exhibidos como texto**, y **el detalle sin ninguna dirección de servicio interno**, que es la verificación de que el diagnóstico expone identidad y no topología

---

## Tabla de contenido

- [1. Concepto representado y propósito](#1-concepto-representado-y-propósito)
- [2. Apariencia esquemática](#2-apariencia-esquemática)
- [3. Variantes](#3-variantes)
- [4. Datos que consume](#4-datos-que-consume)
- [5. Restricciones de accesibilidad](#5-restricciones-de-accesibilidad)
- [6. Reutilización](#6-reutilización)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Concepto representado y propósito

El **sello de versión** declara qué versión de sí misma está corriendo la instancia que la persona tiene delante, y abre el **detalle de diagnóstico** que hace reportable un problema.

No es un adorno del pie de página. Este producto se despliega en un lugar público gratuito cuya estabilidad es una incógnita medida por puertas técnicas, se publica por transferencia de archivos —que **no es transaccional**— y lo opera una sola persona que es a la vez quien lo construye. Sin versión visible, toda conversación sobre una instancia empieza por averiguar qué instancia es, y con un canal de publicación no transaccional esa pregunta es frecuente y cara.

Se centraliza acá porque aparece en **las dos ubicaciones obligatorias** y en los dos shells, y porque su regla principal es fácil de romper por descuido: **la versión se deriva del proceso que construyó el artefacto, y no la escribe nadie en la superficie que lo aloja.** Una versión escrita a mano miente en cuanto alguien olvida actualizarla, y miente en silencio.

**Nota de vocabulario.** El catálogo enuncia esta regla como «la vista no la compone ni la transcribe». Acá se escribe **«la superficie»**, porque «vista» tiene tres referentes en este producto y su forma está resuelta aguas arriba: queda reservada, siempre calificada, para la **vista de trabajo**. La sustitución es de forma y no de contenido, y se deja constancia para quien compare este documento con su fuente normativa.

## 2. Apariencia esquemática

Sello en su forma normal, al pie de la superficie que lo aloja:

```text
                    Versión 1.4.2
                    ^ type.meta, color.text.tertiary
                      sin borde, sin fondo, sin competir con nada
```

Sello con distintivo de artefacto preliminar:

```text
                    Versión 1.5.0-rc.2   [preliminar]
                                          ^ chip de atención, con su texto
```

Sello con marcador de origen indeterminado:

```text
                    Versión no identificada
                    ^ se muestra tal cual, sin disimulo
```

Detalle de diagnóstico, desplegado desde el sello:

```text
   +--------------------------------------------------+
   |  Versión              1.4.2                      |
   |  Construcción         a3f81c6                    |
   |  Origen               publicado                  |
   |                                                  |
   |                        [ Copiar para reportar ]  |
   +--------------------------------------------------+
```

Ubicación en los dos shells:

```text
Shell de acceso                     Shell de trabajo
+---------------------------+       +----------+--------------------+
|      +-------------+      |       | Laborat. |                    |
|      |  tarjeta    |      |       | ·destino |    contenido       |
|      +-------------+      |       | ·destino |                    |
|                           |       | -------- |                    |
|      Versión 1.4.2        |       | persona  |                    |
|      ^ al pie del lienzo  |       | [Cerrar] |                    |
+---------------------------+       | v1.4.2   |                    |
                                    | ^ al pie |                    |
                                    +----------+--------------------+
```

## 3. Variantes

| Variante | Condición de uso | Diferencias esperadas |
| --- | --- | --- |
| **Versión publicada** | La versión legible está presente y el artefacto no es preliminar | El sello con la versión, sin adornos |
| **Versión preliminar** | El artefacto no proviene de una línea de publicación estable | Sello más distintivo textual contiguo, en estado de atención. **Cambia por completo la lectura de cualquier comportamiento anómalo**, y por eso se declara |
| **Origen indeterminado** | La identidad no pudo derivarse de la construcción | El sello reemplaza la versión por un marcador explícito. **Se muestra tal cual, sin disimulo**: es esperable en ejecución local y alarmante en una instancia publicada, y distinguirlo es de quien lee |
| **Detalle colapsado** | Estado normal del sello | Sólo la línea. Es información, no acción, y no compite visualmente con nada |
| **Detalle expandido** | La persona lo abrió | Filas clave/valor con el contrato completo y la acción de copiado |
| **Detalle copiado** | Se ejecutó el copiado | Confirmación efímera. **La cadena copiada es la misma que se muestra**, en texto plano listo para pegar |
| **En el shell de acceso** | Superficies sin sesión | Al pie del lienzo, debajo de la tarjeta |
| **En el shell de trabajo** | Superficies con sesión | Al pie de la barra lateral, debajo del cierre de sesión |

**Las dos ubicaciones son obligatorias y ninguna sustituye a la otra.** Mostrarlo sólo antes de entrar deja sin dato a quien ya está operando; mostrarlo sólo adentro deja sin dato a quien **no puede entrar**, que es justamente el caso en el que más se lo necesita —y en este producto, con una cuenta que puede estar a la espera de habilitación o bloqueada, es un caso frecuente y no hipotético—.

## 4. Datos que consume

El contrato de identidad de versión, resuelto **una sola vez en el punto de composición del sistema** y leído por todos los consumidores desde ahí.

| Campo | Qué hace en la superficie | Obligatorio |
| --- | --- | --- |
| Versión legible | Es el dato principal: la cadena que se muestra | Sí |
| Identificador de construcción | Amplía el diagnóstico cuando dos instancias comparten versión legible. **Sólo en el detalle** | No |
| Indicador de artefacto preliminar | Habilita el distintivo | No |
| Indicador de origen indeterminado | Habilita el marcador | No |

Reglas de uso, que son de diseño y no de implementación:

- **La superficie recibe la versión ya formada.** No la ensambla, no la reformatea y no le agrega sufijos propios.
- **La superficie no compone la versión a partir de partes** ni la transcribe de una constante propia.
- **Si el dato falta, se muestra el marcador de origen indeterminado**, nunca una versión inventada ni un espacio en blanco.
- **El contrato es de solo lectura.** No existe ninguna superficie de este producto que permita fijar la versión a mano.
- **La superficie no distingue entornos por su cuenta.** Si un entorno debe verse distinto, esa distinción llega como campo del contrato y no como condicional en la superficie.
- **La cadena que se muestra y la que se copia son la misma.** Dos representaciones del mismo artefacto obligarían a traducir en el peor momento.

**Y una prohibición que este producto tiene además de las del catálogo:** el detalle de diagnóstico expone la **identidad del artefacto**, nunca la **topología**. No incluye la dirección de la pieza de datos, ni el nombre del hospedaje, ni ningún parámetro de despliegue. La regla de arquitectura que prohíbe exponer direcciones de servicios internos vale también acá, y acá es donde resulta tentador romperla «para diagnosticar mejor».

## 5. Restricciones de accesibilidad

- **El sello cumple el contraste mínimo de texto pese a su jerarquía baja.** Información secundaria no significa información ilegible, y un sello ilegible es un sello que no existe cuando hace falta.
- **El distintivo de preliminar y el marcador de indeterminado son textuales.** El color es refuerzo y nunca el único canal.
- El detalle de diagnóstico es un despliegue **operable por teclado**, que declara su estado de expansión.
- Cuando el sello abre el detalle, expone **foco visible y rol acorde**. Cuando no lo abre, no es un elemento interactivo y no entra en el recorrido por teclado.
- **La confirmación de copiado se anuncia como región activa.** Un cambio visual efímero no alcanza: quien no lo ve no se entera de si el copiado funcionó y lo repite a ciegas.
- El bloque copiado es **texto plano**, para que se pegue en cualquier canal sin perder nada.

## 6. Reutilización

| Artefacto que la invoca | Ubicación |
| --- | --- |
| [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) | Al pie del lienzo. Primera ubicación obligatoria |
| [`Wireframes-Ingreso.md`](Wireframes-Ingreso.md) | Al pie del lienzo. Es la superficie de acceso por excelencia y donde el sello más se necesita |
| [`Wireframes-Registro-De-Cuenta.md`](Wireframes-Registro-De-Cuenta.md) | Al pie del lienzo |
| [`Wireframes-Credencial-Propia.md`](Wireframes-Credencial-Propia.md) | Al pie del lienzo en el curso de establecimiento; en la barra lateral en el de cambio |
| Las seis superficies del shell de trabajo | Al pie de la barra lateral. Segunda ubicación obligatoria |
| [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §2.3, §3.2, §8.3 | El catálogo de identidad de versión aplicado, y el sello como única vía de diagnóstico ante la ausencia de canal de soporte |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Centraliza el sello de versión y su detalle de diagnóstico, presentes en los dos shells y en las once superficies, con las dos ubicaciones obligatorias y el motivo de que ninguna sustituya a la otra, las ocho variantes incluidos el distintivo de preliminar y el marcador de origen indeterminado, las seis reglas de uso del contrato derivado, la prohibición propia de este producto de exponer topología en el diagnóstico, y las restricciones de accesibilidad del sello y del despliegue. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-02**: se sustituyen las tres ocurrencias de la forma desnuda de «vista» en el referente que `Glosario-Funcional.md` §3.1 prohíbe y que `Glosario-UX.md` §4 registra —§1 y dos en §4—, por «la superficie que lo aloja», «una constante propia» y «la superficie». §1 suma una **nota de vocabulario** que deja constancia de que la formulación se arrastraba literalmente del catálogo, `Rules-UX-UI-DX.md` §1.4, que no conoce la desambiguación de este producto. Sustitución verificada por ocurrencia y no por reemplazo global. **H-03**: la cabecera completa su `Trazabilidad downstream` con `06-Backlog-Tecnico`, que omitía, sus `US-XX` heredadas y cuatro escenarios de prueba propios, incluido el que verifica que el detalle de diagnóstico expone identidad y no topología. |
