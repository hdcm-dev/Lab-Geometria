# Las claves efímeras, y cómo se probó el arreglo

## Lo que el anfitrión venía diciendo, con el registro apagado
```text
       ---> System.Security.Cryptography.CryptographicException: The key {7feca3da-607f-43d3-a572-44ab1f08ebc7} was not found in the key ring. For more information go to https://aka.ms/aspnet/dataprotectionwarning
       ---> System.Security.Cryptography.CryptographicException: The key {b7742f97-4dbd-42cc-bbeb-0f11a89d7a8b} was not found in the key ring. For more information go to https://aka.ms/aspnet/dataprotectionwarning
       ---> System.Security.Cryptography.CryptographicException: The key {e226af34-e49a-458c-9474-d3667c108325} was not found in the key ring. For more information go to https://aka.ms/aspnet/dataprotectionwarning
      Neither user profile nor HKLM registry available. Using an ephemeral key repository. Protected data will be unavailable when application exits.
warn: Microsoft.AspNetCore.DataProtection.Repositories.EphemeralXmlRepository[50]
```

Cinco procesos en cinco horas y media, **cada uno con su propio juego de claves**.
Tres fallos con tres claves distintas, dos generadas por procesos anteriores.

## La prueba del arreglo: la clave sobrevive al reciclado
```text
PRIMER ARRANQUE    claves efímeras: 0 · en disco: 1 · key-40b763b6-0b5d-4ae1-a4e5-2160afbd4870.xml
SEGUNDO ARRANQUE   claves efímeras: 0 · en disco: 1 · key-40b763b6-0b5d-4ae1-a4e5-2160afbd4870.xml
LA MISMA CLAVE SOBREVIVIO AL RECICLADO
```

## Y las siete corridas del banco que cierran cada variable
```text
PASA  1. El bloque de resolución se dibuja
PASA  2. Apretar «Aprobar» abre el diálogo de confirmación
PASA  3. El diálogo nombra el trabajo, declara la terminalidad y muestra el comentario · nombre=true terminalidad=true comentario=true
PASA  4. «Cancelar» cierra sin aplicar y el bloque sigue disponible
PASA  5. Confirmar aterriza en /entrega-comision · aterrizó en /entrega-comision
CONFORME · los 5 pasos pasaron
PASA  6. El servicio de datos dice que el trabajo quedó en Approved —«Finalizado»—
CONFORME · el botón de aprobar hace lo que dice
CONFORME · los cinco controles pasan
CONFORME · ninguna frase retirada volvió al corpus
```
