API Eclipse Controle de Projetos
Este repositório contém a aplicação api.eclipse.controle.projetos, desenvolvida em .NET 8.0 e preparada para execução em contêineres Docker.

Pré-requisitos
Certifique-se de que os seguintes itens estejam instalados no ambiente:

Docker
Docker Compose (opcional, caso use docker-compose)
SQL Server
Editor de Código ou Visual Studio 2022 (opcional, para editar e rodar localmente)
Configuração do Banco de Dados
Executar o Dump do Banco de Dados

Certifique-se de executar o dump do banco de dados em sua instância do SQL Server antes de iniciar a aplicação. O arquivo dump necessário pode ser encontrado na pasta database/ deste repositório.

Comando genérico para importar o dump no SQL Server (usando sqlcmd):

bash
Copiar código
sqlcmd -S <servidor> -U <usuario> -P <senha> -d <nome_do_banco> -i caminho/para/arquivo_dump.sql
Ajustar a Connection String

Edite o arquivo de configuração appsettings.json do projeto para incluir a connection string correta ao seu ambiente. Exemplo:

json
Copiar código
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=<servidor>;Database=<nome_do_banco>;User Id=<usuario>;Password=<senha>;"
    }
}
Caso esteja utilizando variáveis de ambiente no Docker, configure-as no docker-compose.override.yml ou via terminal.

Execução com Docker
Passo 1: Construir a Imagem Docker
Navegue até o diretório raiz do projeto (onde está localizado o arquivo Dockerfile) e execute:

bash
Copiar código
docker build -t api-eclipse-projetos .
O parâmetro -t api-eclipse-projetos nomeia a imagem como api-eclipse-projetos.
Passo 2: Iniciar o Contêiner
Após a imagem ser construída, execute o contêiner com:

bash
Copiar código
docker run -d -p 8080:8080 --name api-eclipse-projetos-container -e "ConnectionStrings__DefaultConnection=Server=<servidor>;Database=<nome_do_banco>;User Id=<usuario>;Password=<senha>;" api-eclipse-projetos
-d: Executa o contêiner em segundo plano.
-p 8080:8080: Mapeia a porta 8080 do contêiner para a porta 8080 local.
--name api-eclipse-projetos-container: Nomeia o contêiner.
-e: Define a connection string como uma variável de ambiente.
api-eclipse-projetos: Nome da imagem criada no passo anterior.
Passo 3: Verificar a API
A API estará disponível no endereço:

arduino
Copiar código
http://localhost:8080
Comandos Úteis para Gerenciar o Docker
Parar o Contêiner:
bash
Copiar código
docker stop api-eclipse-projetos-container
Remover o Contêiner:
bash
Copiar código
docker rm api-eclipse-projetos-container
Acompanhar os Logs:
bash
Copiar código
docker logs -f api-eclipse-projetos-container
Remover Imagens e Contêineres Não Utilizados:
bash
Copiar código
docker system prune -a
Estrutura do Projeto
plaintext
Copiar código
.
├── api.eclipse.controle.projetos/             # Projeto principal
├── api.eclipse.controle.projetos.Application/ # Camada de aplicação
├── api.eclipse.controle.projetos.Common/      # Módulos comuns
├── api.eclipse.controle.projetos.Domain/      # Regras de domínio
├── api.eclipse.controle.projetos.CrossCutting/# Injeção de dependência
├── api.eclipse.controle.projetos.Data/        # Camada de dados
├── database/                                  # Dump do banco de dados
└── Dockerfile                                 # Configuração do contêiner Docker
