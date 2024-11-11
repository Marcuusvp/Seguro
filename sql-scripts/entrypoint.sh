#!/bin/bash
set -e

# Inicia o SQL Server em segundo plano
/opt/mssql/bin/sqlservr &

# Aguarda o SQL Server estar pronto para conexões
echo "Aguardando o SQL Server iniciar..."
sleep 20s

# Executa os scripts .sql
echo "Executando scripts SQL..."
for script in /var/opt/mssql/scripts/*.sql
do
    echo "Executando $script"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -i "$script"
done

# Mantém o container em execução
wait