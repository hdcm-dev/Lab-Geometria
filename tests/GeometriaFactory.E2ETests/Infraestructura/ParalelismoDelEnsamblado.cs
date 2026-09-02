using NUnit.Framework;

// UNA CLASE POR RECORRIDO, Y LAS CLASES CORREN EN PARALELO. El número de trabajadores NO se declara
// acá: vive en `pruebas-e2e.runsettings`, para que no puedan divergir.
//
// EL PARALELISMO ES POR CLASE Y NO POR CASO, y hay un motivo: dentro de una clase los casos
// comparten el alumno y los trabajos que esa clase sembró. Paralelizar por caso obligaría a sembrar
// por caso, que contra un despliegue real significa crear y borrar cuentas todo el tiempo.
[assembly: Parallelizable(ParallelScope.Fixtures)]
