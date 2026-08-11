# Sample `api/01-basico` — El canje, la guardia y el envío que no verifica: por qué esa respuesta es exitosa

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-01`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/api/01-basico/run.sh
```

---

## 1. Objetivo del sample

Demostrar la frontera de esta capa en el recorrido más corto que la deja a la vista: canjear credenciales por un acceso firmado, comprobar que la guardia rechaza lo que tiene que rechazar, y enviar el escenario `E-5` para ver **una respuesta exitosa que trae un trabajo en `Borrador`**.

## 2. Prerequisites

- **Entorno de desarrollo contenido del repositorio.**
- **El almacén llevado a su estado de primer arranque** y **el servicio levantado**, con los guiones del repositorio.
- **La dirección del servicio tomada del entorno.** El sample no contiene ninguna dirección concreta, ninguna clave de firma y ninguna contraseña real.
- **Un cliente de peticiones capaz de leer un archivo de petición y comparar la respuesta**, nombrado por su función y no por su producto.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio: `bash scripts/run-api.sh`.
4. Ejecutar el sample: `bash samples/api/01-basico/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-01` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
