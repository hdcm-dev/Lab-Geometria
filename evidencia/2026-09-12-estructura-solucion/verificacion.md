# Corridas del 2026-09-12 (SDK 10.0.400, Node 22.23.2, misma versión que el devcontainer)

## 1. Clon limpio: publish del front en una sola invocación genera y lista el bundle
  Bundle generado y copiado a src/GeometriaFactory.Web/wwwroot/js/
-rw-rw-r-- 1 fernando fernando 501676 sep 12 00:55 geometriafactory-visor.js
-rw-rw-r-- 1 fernando fernando  99830 sep 12 00:55 geometriafactory-visor.js.br
-rw-rw-r-- 1 fernando fernando 123600 sep 12 00:55 geometriafactory-visor.js.gz
js/geometriafactory-visor.cc5tbc39ip.js
js/geometriafactory-visor.cc5tbc39ip.js.br
js/geometriafactory-visor.cc5tbc39ip.js.gz
js/geometriafactory-visor.js
js/geometriafactory-visor.js.br
js/geometriafactory-visor.js.gz
public const string Value = "4ccbf1314740";

## 2. Segunda construcción: el target se salta (sin npm ci)
       Skipping target "BuildVisor" because all output files are up-to-date with respect to the input files.
    0 Warning(s)
    0 Error(s)

## 3. Sin npm en el PATH y sin bandera: falla (como debe)
/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria/src/GeometriaFactory.Web/GeometriaFactory.Web.csproj(80,5): error MSB3073: The command "bash "/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria/src/GeometriaFactory.Web/../../scripts/build-visor.sh"" exited with code 127.
/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria/src/GeometriaFactory.Web/GeometriaFactory.Web.csproj(80,5): error MSB3073: The command "bash "/home/fernando/workspaces/workspace-dev/PROG2/Geometria/Lab-Geometria/src/GeometriaFactory.Web/../../scripts/build-visor.sh"" exited with code 127.

## 4. Sin npm en el PATH y con -p:SkipVisorBuild=true: 0/0
    0 Warning(s)
    0 Error(s)

## 5. La solución entera, sin npm, con bandera (QG-01)
    0 Warning(s)
    0 Error(s)
proyectos en la solución: 20

## 6. build.sh (QG-01) y test.sh (QG-02)
    0 Warning(s)
    0 Error(s)
Passed!  - Failed:     0, Passed:    94, Skipped:     0, Total:    94, Duration: 234 ms - GeometriaFactory.Domain.Tests.dll (net10.0)
Passed!  - Failed:     0, Passed:    56, Skipped:     0, Total:    56, Duration: 232 ms - GeometriaFactory.Application.Tests.dll (net10.0)
Passed!  - Failed:     0, Passed:   372, Skipped:     0, Total:   372, Duration: 14 s - GeometriaFactory.Integration.Tests.dll (net10.0)

## 7. QG-03 antes y después de entrar los nueve samples (coverage.sh)
antes:
QG-03 · cobertura por proyecto de código
  proyecto                                       líneas              ramas   veredicto
  GeometriaFactory.Api                1227/1259    97.5%   192/219     87.7%   PASA
  GeometriaFactory.Application         493/516     95.5%   124/148     83.8%   PASA
  GeometriaFactory.Contracts           142/142    100.0%     0/0        0.0%   sin umbral de líneas en la fuente
  GeometriaFactory.Domain              332/346     96.0%   139/160     86.9%   PASA
  GeometriaFactory.Infrastructure     1658/1717    96.6%   210/258     81.4%   PASA
  GeometriaFactory.Web                1491/1828    81.6%   620/980     63.3%   sin umbral de líneas en la fuente
después:
QG-03 · cobertura por proyecto de código
  proyecto                                       líneas              ramas   veredicto
  GeometriaFactory.Api                1227/1259    97.5%   192/219     87.7%   PASA
  GeometriaFactory.Application         493/516     95.5%   124/148     83.8%   PASA
  GeometriaFactory.Contracts           142/142    100.0%     0/0        0.0%   sin umbral de líneas en la fuente
  GeometriaFactory.Domain              332/346     96.0%   139/160     86.9%   PASA
  GeometriaFactory.Infrastructure     1658/1717    96.6%   210/258     81.4%   PASA
  GeometriaFactory.Web                1491/1828    81.6%   620/980     63.3%   sin umbral de líneas en la fuente
diff: IGUAL

## 8. Los doce samples que corren sin servicio, hoy
domain-01-basico:   CONFORME · las 10 líneas coinciden
domain-02-intermedio:   CONFORME · las 13 líneas coinciden
domain-03-avanzado:   CONFORME · las 13 líneas coinciden
application-01-basico:   CONFORME · las 12 líneas coinciden
application-02-intermedio:   CONFORME · las 14 líneas coinciden
application-03-avanzado:   CONFORME · las 15 líneas coinciden
infrastructure-01-basico:   CONFORME · las 13 líneas coinciden
infrastructure-02-intermedio:   CONFORME · las 14 líneas coinciden con el snapshot de §6
infrastructure-03-avanzado:   CONFORME · las 18 líneas coinciden con el snapshot de §6

## 9. Los tres samples del visor (imagen mcr.microsoft.com/playwright:v1.48.0-jammy)
visor/01-basico:   CONFORME · las 9 líneas coinciden con el snapshot de §6
visor/02-intermedio:   CONFORME · las 16 líneas coinciden con el snapshot de §6
visor/03-avanzado:   CONFORME · las 17 líneas coinciden con el snapshot de §6

## 10. El sello de caché servido antes del guardián (front en 5091, sin administrador)
GET /js/geometriafactory-visor.js?v=<sello> -> 200, 501676 bytes (corrida de las 00:52)
