@echo off
for %%f in (CREATE_TABLES.sql) do (
    echo Ejecutando %%f...
    sqlcmd -S "(localdb)\MSSQLLocalDB" -d Inventario -i "%%f" -o "%%f.log"
)
for %%f in (INSERTS_SEEKS.sql) do (
    echo Ejecutando %%f...
    sqlcmd -S "(localdb)\MSSQLLocalDB" -d Inventario -i "%%f" -o "%%f.log"
)

echo Eliminando archivos de log...
del *.log

echo Todos los procedimientos se ejecutaron correctamente
pause