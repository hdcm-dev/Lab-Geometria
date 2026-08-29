# Sample `domain/01-basico` — Ciclo de vida de una cuenta, de la configuración del administrador a la admisibilidad

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Básico
**Estado de esta carpeta:** **IMPLEMENTADO y VERIFICADO.** Implementado el 2026-08-27 —con él la precondición dura de la Fase I (`Master-Prompt.md` §7.1) quedó cumplida— y **verificado el 2026-08-29** en la Fase I, incremento 1: su contrato `VER-02001` cumple los cinco criterios
**Documento que la gobierna:** [`ejemplo-01-basico-dominio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-dominio.md) 1.0 — **la ruta se corrigió el 2026-08-27**: apuntaba a `SDD/Docs/Proyectos/GeometriaFactory-Domain/`, que la consolidación de las unidades de entrega retiró, y el documento se renombró además con el sufijo del proyecto de código, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-01` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, todavía en estado `Sin verificar`: actualizarla es de la Fase I, no de esta carpeta

**Comando previsto:**

```bash
dotnet run --project samples/domain/01-basico
```

---

## 1. Objetivo del sample

Demostrar el camino más corto que un consumidor recorre contra esta biblioteca: configurar la única cuenta de administrador, dar de alta un alumno, habilitarlo fijándole la credencial provisoria y preguntar si la cuenta admite acceso. Al terminar, quien lo ejecuta sabe cómo se invoca una operación del dominio, cómo llega el rechazo tipado cuando la operación no procede y por qué la admisibilidad es una puerta única y no una comprobación repartida.

## 2. Prerequisites

- **.NET 10**, la plataforma que el `PRODUCT-INTAKE` declara para los seis proyectos de código de la solución de código.
- **Entorno de desarrollo contenido del repositorio**: el host no tiene el SDK.
- **Etapa `a` del plan de entrega cerrada.**
- **Linux**, la plataforma del entorno contenido.
- **Sin servicios externos**: el proyecto de código declara **0** dependencias salientes.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá, y qué encontró al correr

**El sample está implementado y corre.** El árbol es el que declara la §5 del documento que gobierna
esta carpeta, sin desviarse: `Program.cs` con los cuatro actos, `Recorrido/` con un archivo por acto
y `tests/SalidaEsperada.cs` con el snapshot de §6 y su comparación.

```bash
dotnet run --project samples/domain/01-basico              # ejecuta el recorrido
dotnet run --project samples/domain/01-basico -- --verificar   # y lo compara contra §6
```

**No entra en `GeometriaFactory.sln` a propósito.** Si entrara, su ensamblado contaría en la
cobertura de `QG-03` y movería un número que mide otra cosa.

### Y lo primero que hizo fue encontrar una divergencia — **resuelta el 2026-08-29**

**Su primera corrida, el 2026-08-27, incumplió cuatro de los cinco `stdout_contiene` del contrato.**
El sistema emitía `ADMINISTRATOR_ALREADY_CONFIGURED` y el documento pedía
`ADMINISTRADOR_YA_CONFIGURADO`, y lo mismo con otros tres códigos.

**No era un defecto del sistema.** Era el residuo del renombre **`F-03`** —«los 101 códigos de
condición van a inglés», decisión del Product Owner del **2026-08-12**, en
[`../../../SDD/Docs/Producto/Norma-De-Nomenclatura.md`](../../../SDD/Docs/Producto/Norma-De-Nomenclatura.md)
§5.3—, cuyos tramos documentales **se suspendieron el 2026-08-13** porque renombraban documentos que
describían código que todavía no existía. El código se escribió en inglés desde el primer archivo; la
documentación quedó a mitad de camino.

**El Product Owner lo reconfirmó el 2026-08-29** y el documento que gobierna esta carpeta pasó a la
forma vigente, **con el mapeo leído de `ConditionCode.cs`** y no elegido a mano. Hoy el sample
**cumple su contrato**: `--verificar` devuelve `CONFORME · las 10 líneas coinciden` y sale 0.

**Lo que conviene no perder de este episodio** es que el snapshot **no se ajustó al código durante
esos dos días**, aunque hubiera sido un renglón. Ajustarlo habría decidido en silencio que el código
le gana a veintiún documentos, y lo que había debajo era una decisión del Product Owner a medio
aplicar, que sólo se ve si alguien se detiene.
