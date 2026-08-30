# Sample `api/01-basico` — El canje, la guardia y el envío que no verifica: por qué esa respuesta es exitosa

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Básico
**Estado de esta carpeta:** **Implementado.** Corre en 0; **11 de 13 líneas coinciden con §6** y las otras 2 son divergencias declaradas (abajo).
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-api.md) 1.0, del que este README es la copia corta de §1, §3 y §4
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

## 4. Qué hay acá

Un punto de entrada único —`run.sh`—, los seis archivos de petición de §5 con sus marcas de sustitución, los dos cuerpos y el snapshot esperado. **Contra el servicio corriendo, sin dobles de ningún tipo.**

**Ni la dirección, ni la clave de firma, ni ninguna contraseña real están escritas en el sample.** La dirección llega en `GF_API_BASE` y la clave en `AccessToken__SigningKey`; las contraseñas de las cuentas de utilería se producen al correr con `/dev/urandom`, y la provisoria la devuelve el propio servicio. Es la condición de §3, y también la que hace que el recuento de la última línea de `[traduccion]` pueda dar cero.

**Sin `jq` ni `python3` en el entorno contenido**, así que el texto del alumno se convierte en literal JSON con `cuerpos/escapar.awk`, que escapa lo que JSON exige y **nada más**: no reordena, no compacta y no reindenta. Es `US-00019`, y el mismo motivo por el que los cuerpos llevan `.txt`.

**El almacén es propio del sample y el servicio se levanta aparte.** El recorrido da de baja y resetea cuentas; correrlo contra el almacén de trabajo se llevaría puesto lo que haya. `ConnectionStrings__Store` apunta a un archivo del sample.

## 5. Las dos divergencias contra §6

| # | §6 espera | El árbol |
| --- | --- | --- |
| `D-1` | `codigo del contrato reconocido: 6 de 6` | **4 de 4** |
| `D-2` | `Peticiones ejecutadas: 14 \| Respuestas comparadas: 14` | **17 y 13** |

**`D-1` es el hallazgo, y no es un problema de conteo.** De las cuatro respuestas de error con cuerpo, **las cuatro** traen un código del contrato reconocido: en eso §6 acierta del todo. Lo que falta son las otras dos, y faltan porque **los tres `401` de autenticación vuelven con `Content-Length: 0` y ningún código**. Los emite la tubería de autenticación de ASP.NET, antes de que corra una línea de código del producto, así que la traducción de errores nunca los ve.

No es una fuga —no hay nada adentro que pueda filtrarse, y por eso la línea siguiente da cero igual— pero sí rompe la uniformidad que §6 daba por sentada: un cliente que espere un código en toda respuesta de error se encuentra con tres que no lo traen. **Y son justamente las tres que un cliente ve más seguido**, porque son las de la sesión vencida.

**`D-2` es aritmética del recorrido.** Con las tres sondas del guardia, el reseteo y la cuenta pendiente, hacen falta diecisiete peticiones. Bajarlo a catorce exigiría no medir algo que §6 pide en otra línea.

## 6. Dos cosas que el sample resolvió corriéndolo

- **El orden en que se puede medir no es el orden en que §6 se lee.** El reseteo deja marcada a la alumna y una cuenta marcada no escribe nada, así que la línea del guardia tiene que medirse **después** de los dos envíos, aunque §6 la agrupe con los otros guardias. El sample mide cuando el producto obliga y **imprime donde corresponde leer**; lo que no hace es reordenar una medición.
- **El guardia de cambio pendiente sólo es alcanzable por un camino, y es el que su propio comentario describe**: un acceso emitido **antes** del reseteo, usado **después**. Pedir uno nuevo no llega hasta ahí, porque el canje ya lo rechaza con el mismo código. La marca se lee del almacén y no del acceso presentado, y eso es lo que hace que un acceso vigente deje de servir sin haber vencido.
- **El acceso vencido se fabrica firmándolo con la clave de verdad y un `exp` del pasado**, con `openssl`. Es la única forma honesta: un acceso mal firmado ya está cubierto por la tercera comprobación de esa misma línea, y esperar a que uno venza de veras haría durar el sample lo que dure la vida útil configurada.
