# ADR-08007 — El aviso de selección va en las opciones, no en una séptima función

**Producto:** Fábrica de Geometría
**Documento:** ADR-08007-El-Aviso-De-Seleccion-Va-En-Las-Opciones.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD, con autorización explícita del Product Owner para resolver
**Nivel:** Producto
**Tipo:** **Decisión de arquitectura que amplía un contrato declarado**
**Cierra:** [`../../Audit/Observacion-Sincronizacion-Escena-Arbol.md`](../../Audit/Observacion-Sincronizacion-Escena-Arbol.md) 1.0
**Trazabilidad upstream:** `PRODUCT-INTAKE` §4 (`F-13`, `Must Have`); `Wireframes-Vista-De-Trabajo.md` §4; `Norma-De-Nomenclatura.md` §5.1 (`F-01a`)

---

## 1. El problema, en una línea

`F-13` exige que la escena y el árbol se sincronicen **en las dos direcciones**, y **las seis
funciones de la fachada van todas del anfitrión hacia el visor**: ninguna avisa de vuelta. La mitad
árbol → escena se podía escribir; la mitad escena → árbol **no tenía por dónde**.

## 2. Decisión

**El aviso entra como una opción de `initialize`**, no como una séptima función.

```
initialize(elemento, { onPieceSelected: (posicion) => { … } })
```

**Por qué esta y no la séptima función**, que era la otra salida elevada:

| | El aviso en las opciones | Una séptima función |
| --- | --- | --- |
| Toca la zona de frontera `F-01a` | **No.** Las funciones siguen siendo **seis** | **Sí.** El recuento está citado en cinco documentos y lo fijó el Product Owner |
| Dónde vive | En `ViewerOptions`, que **ya existe** y es donde el anfitrión configura su instancia | En la superficie pública, que es lo que más cuesta cambiar |
| Simetría | Es un **aviso**, y los avisos no son funciones que uno llama: son funciones que a uno le llaman | Sería simétrica con las seis en la forma y asimétrica en el sentido |

**El fundamento decisivo es el último renglón.** Las seis funciones son órdenes que el anfitrión da;
esto es lo contrario, y meterlo entre ellas haría que la superficie tuviera seis cosas que se piden
y una que se recibe, sin nada que las distinga.

## 3. Lo que la decisión conserva, y hay que decirlo

- **`RA-02` intacto.** El visor sigue sin hacer red, sin identidad y sin leer configuración. El
  aviso **se lo da el anfitrión**: es una función que recibe por parámetro, como el color de fondo.
- **La regla de aislamiento del wireframe.** La escena se sigue operando **exclusivamente** por la
  fachada: no se lee su interior desde afuera, no se cuelga un evento en el elemento de dibujo y no
  se sondea su estado. Las tres eran las formas de improvisarlo, y las tres quedaron descartadas.
- **El visor no guarda la selección ni decide qué hacer con ella**: avisa y resalta. Qué se marca en
  el árbol lo decide el anfitrión.

## 4. Lo que la decisión obligó a resolver, y no era obvio

**Un clic no es un arrastre, y confundirlos rompe la selección.** La misma interacción —apretar,
mover, soltar— sirve para encuadrar la escena y para elegir una figura. Sin distinguirlas, **cada vez
que la persona gira la escena para mirar una figura de atrás, al soltar seleccionaría la que quedó
bajo el dedo**, y la selección dejaría de ser una decisión suya.

Se resuelve con un umbral: soltar **sin haber movido** es elegir; soltar después de mover es
encuadrar. El umbral existe para que un temblor de la mano no cuente como arrastre.

**Hay una prueba que lo fija**, y no es un detalle: `scripts/verify-viewer-lifecycle.sh` comprueba
con un clic de verdad en el navegador que un clic avisa **una** vez, que el aviso trae la posición,
y que **arrastrar y soltar no selecciona**.

## 5. Consecuencias

- **`F-13` queda cumplida en sus dos direcciones**, y con eso el quinto criterio de la transición
  `g` → `h` —«el árbol y la escena se sincronizan por índice de pieza»— tiene con qué verificarse.
- **`ViewerOptions` pasa a tener dos miembros** y el contrato de la fachada sube de versión.
- **Ninguna de las seis funciones cambia de firma ni de nombre.**

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Cierra la observación de sincronización escena ⇄ árbol con la salida **más chica de las dos elevadas**: el aviso entra en `ViewerOptions` y **las funciones de la fachada siguen siendo seis**, de modo que la zona de frontera `F-01a` no se toca. El fundamento decisivo es que un aviso **no es una orden**: entre seis funciones que el anfitrión llama, una que le llaman a él quedaría sin nada que la distinga. Declara lo que la decisión conserva —`RA-02`, la regla de aislamiento, y que el visor no guarda la selección— y **lo que obligó a resolver**: distinguir el clic del arrastre, sin lo cual encuadrar la escena seleccionaría la figura que quedó bajo el dedo. Hay prueba con navegador de verdad que lo fija. | Orquestador SDD, con autorización del Product Owner |
