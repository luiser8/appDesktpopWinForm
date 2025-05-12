@echo off
for %%f in (*.sql) do (
    echo Ejecutando %%f...
    sqlcmd -S DESKTOP-S8CAPQE -d Inventario -i "%%f" -o "%%f.log"
)

echo Eliminando archivos de log...
del *.log

echo Todos los procedimientos se ejecutaron correctamente
pause