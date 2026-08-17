# ADR-08008 — La superficie HTTP se describe, y el explorador no se publica solo

**Producto:** Fábrica de Geometría
**Documento:** ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD, por decisión explícita del Product Owner
**Nivel:** Producto
**Tipo:** **Decisión de arquitectura que revierte una renuncia declarada aguas arriba**
**Trazabilidad upstream:** `PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Api (sin versionado de rutas); `PRODUCT-INTAKE` §17.5 (la renuncia); `Definicion-Superficie-HTTP.md` 1.8; `RA-03`

---

## 1. El problema, en una línea

El intake declara que **«se renuncia a un contrato descrito en OpenAPI»**, y `09-Devops/README.md`
marca su guía de publicación como **omitida** porque «no tiene sujeto». Con el servicio de datos
puesto en Internet para el despliegue de la etapa `g`, **sí tiene sujeto**: el Product Owner lo pidió
para poder ver y ejercitar los puntos sin leer el código.

## 2. Decisión

**Dos cosas, y son distintas.**

1. **El documento OpenAPI existe y se GENERA.** Se sirve en `/openapi/v1.json`, producido desde los
   puntos ya declarados por `Microsoft.AspNetCore.OpenApi`.
2. **El explorador navegable existe** —Scalar, en `/documentacion`— **y no se publica solo.** En
   desarrollo está siempre; fuera de desarrollo hace falta decir `Documentacion__Publicada=true`.

## 3. Por qué la renuncia se puede revertir sin contradecirse

Lo que el intake quiso evitar **no era tener documentación**: era **el costo y la mentira de un
segundo contrato escrito a mano**, que se desactualiza y termina diciendo algo distinto del código.

| Lo que la renuncia evitaba | ¿Sigue evitado? |
| --- | --- |
| Un documento escrito a mano que se desincroniza | **Sí.** Se genera desde los puntos: no hay segunda fuente |
| Una cadena de herramientas para generar clientes | **Sí.** No se genera ningún cliente: el contrato entre las dos piezas **sigue siendo el ensamblado** `GeometriaFactory.Contracts` |
| Versionado de rutas para terceros | **Sí.** No se versiona ninguna ruta y no se declara ninguna nueva del producto |

**No cambia ningún punto.** `Definicion-Superficie-HTTP.md` sigue siendo la definición: `A-01` a
`A-18` son los mismos, con las mismas formas y los mismos verbos.

## 4. Por qué el explorador se cierra por omisión fuera de desarrollo

Es la parte con consecuencia, y por eso está separada de la anterior.

Un explorador de la API **enumera todos los puntos, sus formas y sus verbos ante cualquiera que abra
la dirección**. Para este producto eso puede estar perfectamente bien —es una API de laboratorio, y
que el alumno la lea tiene valor didáctico—, pero **es una decisión de quien despliega**, y no puede
ser el efecto secundario de haber agregado un paquete. La llave existe para que esa decisión se tome
diciéndola.

**Publicarlo no revela ningún dato.** Describir la forma de la superficie no expone ninguna cuenta ni
ningún trabajo, y por eso el documento **no exige acceso**: exigirlo lo haría inútil para la
herramienta que lo consume sin volver más secreto nada que no fuera ya deducible pidiendo las rutas.

## 5. Lo que se ejerce

`ApiDocumentationSurfaceTests` cubre las dos mitades, y **la que importa es la segunda**: que en
desarrollo se vea lo nota cualquiera la primera vez que lo abre; que **deje de verse al desplegar** no
lo nota nadie hasta que ya está publicado.

| Prueba | Qué fija |
| --- | --- |
| `InDevelopmentTheDocumentAndTheExplorerAreBothServed` | Los dos responden, y el documento describe **esta** superficie |
| `OutsideDevelopmentNeitherIsServedUnlessItIsSaidExplicitly` | Los dos dan 404 sin la llave |
| `OutsideDevelopmentTheSettingIsWhatPublishesThem` | La llave es lo único que hace falta |
| `TheDocumentIsServedWithoutAnyAccessToken` | No pide acceso, y queda dicho por qué |

## 6. Consecuencias

- `09-Devops/README.md` de `GeometriaFactory-Api` deja de marcar la guía de OpenAPI como omitida
  **sin sujeto**: pasa a tener sujeto y este ADR es su fundamento.
- El despliegue suma **una variable de entorno opcional**, `Documentacion__Publicada`. Sin ella el
  comportamiento desplegado es el de antes de esta decisión.
- Los activos del explorador **se sirven desde el propio servicio**, no desde una red de terceros:
  el servicio no adquiere ninguna dependencia de tiempo de ejecución hacia afuera.
