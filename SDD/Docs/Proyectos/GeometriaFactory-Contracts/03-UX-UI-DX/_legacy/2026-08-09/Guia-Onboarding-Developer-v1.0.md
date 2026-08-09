> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Guia-Onboarding-Developer.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`Guia-Onboarding-Developer.md`](../../Guia-Onboarding-Developer.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Guía de onboarding — GeometriaFactory-Contracts

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §2, §6 (`RT-01` a `RT-07`); `CU-01` §10 y §17, `CU-02` §17, `CU-03` CA-01, CA-02 y §17, `CU-04` CA-01, §6.1 y §17, `CU-05` CA-05, CA-06, §6.1 y §17, `CU-06` §6 y CA-01 y §17; `00-Contexto/Alcance-Producto.md` §2.2 y §8; `01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md` §1; `PRODUCT-INTAKE` §17.4 P.3, P.5, P.6, P.8, P.10 y P.11, §15, §16, §16.1
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de este proyecto de código

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
  - [1.1 Para quién es esta guía](#11-para-quién-es-esta-guía)
  - [1.2 Prerrequisitos](#12-prerrequisitos)
  - [1.3 Lo que hay que leer antes de tocar nada](#13-lo-que-hay-que-leer-antes-de-tocar-nada)
- [2. Instalación o acceso](#2-instalación-o-acceso)
  - [2.1 Abrir el proyecto de código](#21-abrir-el-proyecto-de-código)
  - [2.2 Construir y verificar](#22-construir-y-verificar)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 Recorrer la superficie pública](#31-recorrer-la-superficie-pública)
  - [3.2 Ejercitar los tipos contra el servicio real](#32-ejercitar-los-tipos-contra-el-servicio-real)
  - [3.3 Tres cambios de control](#33-tres-cambios-de-control)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Audiencia y prerrequisitos

### 1.1 Para quién es esta guía

Para quien tiene que trabajar contra el ensamblado de contratos durante la próxima hora. En este producto esa persona es una de tres, declaradas en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1: el mantenedor presente, el mantenedor futuro —la misma persona sin el contexto en la cabeza— y el agente de construcción por etapas. No hay integradores externos: los dos únicos consumidores del contrato son `GeometriaFactory-Api` y `GeometriaFactory-Web`, del mismo producto.

La guía es un tutorial: un solo camino, en orden, sin alternativas. Si lo que necesitás es resolver un síntoma concreto, el camino no es éste sino [`DX-Error-Messages.md`](DX-Error-Messages.md).

### 1.2 Prerrequisitos

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio clonado y abierto en el **contenedor de desarrollo** | Declarado en `.devcontainer/devcontainer.json` (`PRODUCT-INTAKE` §16). **Todo ocurre adentro**: el host de desarrollo no tiene las herramientas de construcción y no va a tenerlas |
| La etapa `a` del plan de entrega cerrada | Es el andamiaje de la solución de código: la estructura de proyectos de código y los comandos de `scripts/` (`PRODUCT-INTAKE` §15). Sin ella no hay nada que construir |
| Ningún prerrequisito de red ni de credenciales | Este proyecto de código es nivel 0 del orden topológico y no depende de nada (`PRODUCT-INTAKE` §13) |

### 1.3 Lo que hay que leer antes de tocar nada

Una sola frase, y está en `PRODUCT-INTAKE` §17.4 P.5: **este ensamblado no implementa autenticación, pero es donde se decide qué se expone**. Ningún tipo de transferencia incluye el hash de contraseña, la clave de firma ni ninguna dirección de servicio interno.

Si en algún momento de esta hora tenés dudas sobre si un campo va o no va, la respuesta por defecto es que no va. Un campo de más en esta superficie compila sin protestar, cruza la frontera de servicio y llega al otro extremo; sacarlo después no deshace lo que ya viajó.

## 2. Instalación o acceso

No hay instalación: no hay paquete que traer. El ensamblado se construye desde el repositorio y se consume por referencia de proyecto de código desde `GeometriaFactory-Api` y `GeometriaFactory-Web`. `redistribuible` es false y no hay feed (`PRODUCT-INTAKE` §13).

### 2.1 Abrir el proyecto de código

El código vive en `src/GeometriaFactory.Contracts/`, con esa identidad exacta (`PRODUCT-INTAKE` §16). Su documentación vive en `SDD/Docs/Proyectos/GeometriaFactory-Contracts/`.

### 2.2 Construir y verificar

Desde la raíz del repositorio, **dentro del contenedor de desarrollo**:

```bash
# Paso 1 — construir. El ensamblado de contratos no depende de nadie,
# así que es lo primero que se construye.
bash scripts/build.sh
```

Resultado esperado: **termina en 0 y sin advertencias**. Ése es el hito del tramo de 5 minutos, y no es una formalidad: «compila sin advertencias» es el quality gate bloqueante del pipeline de este proyecto de código (`PRODUCT-INTAKE` §17.4 P.8).

```bash
# Paso 2 — verificar RT-05 a mano, una vez, para que la restricción deje
# de ser una frase y pase a ser algo que sabés comprobar.
grep -R "GeometriaFactory.Domain" src/GeometriaFactory.Contracts/ || echo "OK: RT-05 se cumple"
```

Resultado esperado: sin coincidencias. El ensamblado **no declara ninguna referencia hacia `GeometriaFactory-Domain`**, y ésa es la ausencia que impide que la pieza pública conozca las entidades del dominio. El intake la declara quality gate bloqueante y aclara por qué: es la vía por la que el acoplamiento vuelve (`PRODUCT-INTAKE` §17.4 P.8).

## 3. Primer ejemplo ejecutable

### 3.1 Recorrer la superficie pública

Con la construcción en verde, el recorrido de la superficie se hace leyendo, y se verifica contra tres preguntas. Las respuestas están en los contratos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`; conviene contestarlas primero de memoria y después confirmarlas.

| Pregunta | Respuesta | Dónde se confirma |
| --- | --- | --- |
| ¿Dónde viaja el texto original completo del trabajo? | Sólo en el detalle del trabajo interpretado. Es el único tipo del ensamblado que lo transporta entero | `CU-05` §10 y `CU-04` CA-01 |
| ¿Por qué el elemento de listado no lo trae? | Porque la proyección de listado existe precisamente para **no** ser el detalle: declara 0 campos de texto original y 0 de componente de pieza, para que el listado del administrador no arrastre el texto completo de cada trabajo | `RT-04` y `CU-04` CA-01 |
| ¿Cuántos campos tiene la respuesta de error, y cuántos pueden transportar una dirección de servicio? | Exactamente cuatro —código, texto, detalles y momento— y **cero** de la segunda clase | `CU-06` CA-01 |
| ¿Qué recibe una persona habilitada que todavía no estableció su contraseña? | Una **respuesta de error** con código propio, `CONTRATO_CONTRASENA_NO_ESTABLECIDA`, y su motivo. **No** una respuesta de sesión con una marca: la respuesta de sesión sigue declarando cuatro campos y ninguno agregado para este caso. Establecer la contraseña es un desenlace distinto del ingreso | `CU-01` FA-02, CA-05 y §10; `CU-06` §6 |

Si las cuatro respuestas salieron, el tramo de 30 minutos está cumplido.

### 3.2 Ejercitar los tipos contra el servicio real

Este proyecto de código **no tiene pruebas propias**: son tipos sin comportamiento. Se ejercita íntegramente desde las pruebas de integración que golpean el servicio real (`RT-07`, `PRODUCT-INTAKE` §17.4 P.6).

```bash
# Paso 3 — ejercitar los tipos de transferencia de punta a punta.
bash scripts/test.sh
```

Es el primer valor real del recorrido: hasta acá el ensamblado compilaba; a partir de acá se sabe que los tipos transportan lo que dicen transportar. Depende de que las etapas que introducen cada familia estén cerradas, así que en las etapas tempranas la cobertura es parcial por construcción y no por defecto.

### 3.3 Tres cambios de control

El último tramo de la hora. Clasificá cada cambio como **compatible** o **incompatible**, y decí cuál es la acción operativa. Las respuestas están abajo; conviene resolverlos antes de leerlas.

1. Agregar un campo opcional a la respuesta de sesión de `CU-01`.
2. Agregar una situación de cuenta al conjunto admitido de `CU-02` —por ejemplo, una cuarta además de pendiente, habilitada y bloqueada—.
3. Agregar el texto original al elemento de listado de `CU-04`, para ahorrarse una solicitud al abrir el detalle.

| Cambio | Clasificación | Por qué | Acción |
| --- | --- | --- | --- |
| 1 | **Compatible** | La pieza pública que no lee el campo nuevo sigue compilando | Se puede introducir sin coordinar despliegues, siempre que el campo no viole `RT-01` |
| 2 | **Incompatible**, aunque compile | La pieza pública deja de cubrir todos los casos: hay una situación que no contempla. Es incompatible de hecho | Despliegue conjunto de las dos piezas desplegables (`RT-06`). No se versionan rutas: no hay consumidores de terceros |
| 3 | **Se rechaza**, aunque compile y aunque nadie se rompa | Viola el requisito estructural `RT-04`, que es el motivo por el que la proyección de listado existe separada del detalle | No se introduce. Si el problema real es el número de solicitudes, se discute en `05-Arquitectura-Tecnica`, no agregando campos acá |

Los tres salieron bien: el tramo de 1 hora está cumplido y ya sabés leer la señal más valiosa de este proyecto de código, que es que la incompatibilidad aparece al compilar y no en producción.

## 4. Diagnóstico de problemas frecuentes en la primera hora

Cinco problemas, con lo que significan y qué hacer. El catálogo completo, con las dos clases de error separadas, está en [`DX-Error-Messages.md`](DX-Error-Messages.md).

| Síntoma en la primera hora | Qué significa | Qué hacer |
| --- | --- | --- |
| La construcción termina en 0 pero **con advertencias** | El hito de 5 minutos **no** está cumplido: el quality gate del pipeline es «sin advertencias», no «sin errores» | Resolver la advertencia antes de seguir. Ver `DXC-09` del catálogo |
| Un comando de `scripts/` no existe | La etapa `a` no está cerrada, o el repositorio no está abierto en el contenedor de desarrollo | Verificar el contenedor. **No** sustituir el comando con herramientas del host: el host no las tiene y no va a tenerlas |
| La verificación de `RT-05` devuelve coincidencias | El ensamblado adquirió una referencia hacia el proyecto de código de dominio. Es el quality gate bloqueante del proyecto de código | Quitar la referencia. Ver `DXC-01`: se rechaza en revisión, no se negocia |
| Las pruebas de integración fallan enteras y no por un caso | Lo que está roto no es el contrato: es el servicio o su base. Este proyecto de código no tiene pruebas propias que puedan fallar solas | Diagnosticar en `GeometriaFactory-Api`. Ver `DXC-08` si la sospecha es desfasaje entre las dos piezas |
| No se encuentra un fragmento de ejemplo que instancie un tipo | No existe y es deliberado: este proyecto de código no produce samples propios, porque no lo consumen integradores externos (`PRODUCT-INTAKE` §16.1) | Usar los contratos de uso de 02 como referencia de forma, y las pruebas de integración como ejemplo vivo |

## 5. Próximos pasos

Los cuatro modos de Diátaxis, con su enlace, según el plan de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4:

- **Tutorial** — este documento. Ya está recorrido.
- **How-to** — [`DX-Error-Messages.md`](DX-Error-Messages.md) para diagnosticar un error de cualquiera de las dos clases; la sección §17 «Compatibilidad de versión pública» de cada contrato de uso, en [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), para decidir si un cambio concreto pasa.
- **Reference** — los seis contratos de uso `CU-01` a `CU-06`, y [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 para las siete restricciones transversales. Es la descripción normativa de la superficie; esta sección no la duplica.
- **Explanation** — [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 y §5.1, y [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §2 y §5, para el porqué de cada decisión. Los ADR pertenecen a `05-Arquitectura-Tecnica`.

Vocabulario: antes de escribir en cualquiera de estos documentos, [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md), en particular §3.1, por los tres referentes de «contrato».

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Recorrido de la primera hora en tres tramos verificables, íntegramente dentro del contenedor de desarrollo: construcción sin advertencias, verificación manual de `RT-05`, tres preguntas de superficie pública, ejercicio de los tipos por prueba de integración y tres cambios de control para clasificar compatibilidad. Suma cinco problemas frecuentes con su diagnóstico y los enlaces a los cuatro modos de Diátaxis. | DX Lead (AG-03) |
| 1.0 | 2026-08-08 | Corrección absorbida de la ronda 1 de auditoría (`Audit/B-02-03-GeometriaFactory-Contracts-r1.md`), sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-01**: una ocurrencia de «solución» a secas designando el agrupador de construcción, corregida a «solución de código» en la fila de la etapa `a` de §1.2, según `Vocabulario-Rules.md` §4 R2 y sin sustitución global (§9.5). **H-09**: las referencias a la sección opcional pasan de §12 a §17 en la cabecera y en §5. **Alineación con el upstream**: la cabecera suma `CU-01` §10 y las dos §6.1 de señales declaradas que no son error de `CU-04` y `CU-05`; §3.1 suma la cuarta pregunta de superficie, por el paso del conjunto cerrado de doce a trece códigos. | DX Lead (AG-03) |
