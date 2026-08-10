# RN-14 — La contraseña provisoria la produce el sistema, no la escribe el administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-14-Provisoria-Producida-Por-El-Sistema.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10** §4.1 (enunciado de **RN-14**), §4 (**F-26**, «el sistema produce una contraseña provisoria» y «el panel **no lleva campo de contraseña**»), §17.1.P.2 (las reglas sin invariante asociado), §17.1.P.5 (el dominio no maneja secretos: la contraseña llega ya derivada), §7 (**CL-7**); [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1 y §5; [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Enunciado de la regla](#1-enunciado-de-la-regla)
- [2. Justificación](#2-justificación)
- [3. Ámbito de aplicación](#3-ámbito-de-aplicación)
- [4. Consecuencia si se viola](#4-consecuencia-si-se-viola)
- [5. CU afectados](#5-cu-afectados)
- [6. Pruebas que la verifican](#6-pruebas-que-la-verifican)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Enunciado de la regla

**La contraseña provisoria la produce el sistema, no la escribe el administrador**, y la superficie se la muestra para que se la comunique al alumno. La provisoria **no es adivinable** y **no se repite** entre cuentas ni entre dos reseteos de la misma cuenta.

## 2. Justificación

Es una regla de uso antes que de seguridad, y el Product Owner la fundó así: si la escribe el docente, en la práctica termina siendo **la misma clave para toda la comisión**, y el panel de cuentas cargaría además con un campo donde se escribe la contraseña de otra persona. Producirla el sistema resuelve las dos cosas de una vez: el panel **no lleva campo de contraseña** y cada reseteo entrega un valor distinto.

Lo que la regla protege es la promesa de RN-13 y de INV-09: la provisoria vale para un solo uso, el de llegar al cambio obligatorio. Una provisoria adivinable —derivada del correo, del nombre o de la fecha— o repetida entre cuentas dejaría que cualquiera entrase como cualquier alumno recién reseteado, y la ventana que RN-13 acota a un solo ingreso quedaría abierta desde afuera.

## 3. Ámbito de aplicación

- Se evalúa sobre **el valor provisorio que cada reseteo produce**, en el acto de producirlo, y no sobre la cuenta ni sobre sus trabajos.
- **No se ejerce en este proyecto de código, y conviene decirlo con precisión.** El dominio **no produce la provisoria y no la conoce**: llega ya derivada (`PRODUCT-INTAKE` §17.1.P.5), de modo que acá no hay valor en claro contra el cual verificar ninguna de las dos propiedades. La regla se enuncia acá porque acá viven las reglas del producto, y **se ejerce donde el valor nace**: `GeometriaFactory-Application` `CU-11` §10 las exige por escrito sin declarar mecanismo, `GeometriaFactory-Contracts` `CU-08` §10 las exige del valor devuelto y las verifica en su `CA-10`, y la generación es de `GeometriaFactory-Infrastructure`.
- **Alcanza a los dos reseteos sucesivos de la misma cuenta**, que es el caso de FA-01 de [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md): el segundo reseteo entrega una provisoria nueva y distinta de la primera.
- **No alcanza a la contraseña que el alumno elige** al levantar la marca: ésa la escribe él, el administrador no la conoce (RN-13) y su forma es asunto de otra decisión.
- **Con qué mecanismo se produce un valor que cumpla las dos propiedades no es de esta categoría** ni de ninguna de las dos que la ejercen: es de `05-Arquitectura-Tecnica` y de la infraestructura. La regla exige propiedades, no algoritmo.
- **No tiene invariante asociado**, y el intake lo declara así en la prosa de §17.1.P.2: describe cómo se produce un valor, no una condición permanente sobre los datos del dominio.

## 4. Consecuencia si se viola

**No hay código de rechazo del dominio**, y el motivo es el mismo por el que la regla no se ejerce acá: el dominio recibe un valor ya derivado y no puede distinguir uno producido por el sistema de uno escrito por el administrador. La única condición que el dominio sí comprueba sobre ese valor es que no esté vacío, y es `VALOR_DERIVADO_VACIO` de [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) §6, que no es esta regla.

Violarla no produce un rechazo sino un daño silencioso: una provisoria escrita a mano y repetida, o derivable del correo del alumno, convierte el reseteo en una puerta abierta a la identidad de cualquier cuenta reseteada. Es un defecto que **sólo una prueba detecta**, y por eso §6 declara dónde vive esa prueba.

## 5. CU afectados

- [CU-13](../Casos-De-Uso/CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md) — Resetear la contraseña de una cuenta de alumno: es el acto en cuya invocación el valor producido entra al dominio, ya derivado. Su §2 declara que lo genera la infraestructura y su §10 remite las dos propiedades a las capas que las ejercen.
- [CU-03](../Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) — **por contraste**: es el camino por el que entra la contraseña que **la propia persona elige**, y sobre la cual esta regla no dice nada.

## 6. Pruebas que la verifican

**Ninguna prueba unitaria de este proyecto de código la verifica**, y declararlo es parte de la regla: sin valor en claro no hay nada que comprobar. Las pruebas viven donde el valor existe, y ya están declaradas allá: `GeometriaFactory-Contracts` `CU-08` **CA-10** —tres provisorias sucesivas distintas entre sí— y su §9, que la lleva a `08-Calidad-Y-Pruebas` como prueba de integración del circuito de reseteo. El dato de prueba que el intake declara para esta regla es el de dos reseteos consecutivos sobre la misma cuenta, que producen provisorias distintas, y que ninguna sea derivable del nombre, del correo ni de la fecha (§4.1, columna de verificación).

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la regla **RN-14** que `PRODUCT-INTAKE` **1.10** §4.1 incorpora al transcribir la decisión del Product Owner del 2026-08-09 sobre quién produce la contraseña provisoria de la capacidad **F-26**. Declara el enunciado con sus dos propiedades —no adivinable y no repetida, ni entre cuentas ni entre reseteos—, la justificación de uso que el Product Owner dio, el ámbito **con la declaración explícita de que este proyecto de código no la ejerce** porque el valor le llega ya derivado y de dónde sí se ejerce, la consecuencia sin código de rechazo propio y las pruebas, que viven en `GeometriaFactory-Contracts` `CU-08` CA-10. El contenido no se origina acá: estaba ya modelado en `CU-13` §2 y §10 de esta categoría, en `GeometriaFactory-Application` `CU-11` §10 y en `GeometriaFactory-Contracts` `CU-08` §10, y esta emisión lo recoge bajo el identificador que la fuente le dio. |
