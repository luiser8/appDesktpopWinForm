@echo off
for %%f in (SP\*.sql) do (
    echo Ejecutando %%f...
    sqlcmd -S DESKTOP-2ONR9MM -d Inventario -i "%%f" -o "%%f.log"
)

echo Eliminando archivos de log...
del SP\*.log

echo Todos los procedimientos se ejecutaron correctamente
pause