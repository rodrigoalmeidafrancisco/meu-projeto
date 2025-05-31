# Visual Studio Code

### 1. Abra a solução
Abra o arquivo de solução Tudao.BackEnd.sln no Visual Studio ou use o terminal:

```bash
dotnet sln 02_BackEnd/Tudao.BackEnd.sln list
```

OBS: Isso listará os projetos contidos na solução.

### 2. Configure o ambiente

Certifique-se de que o arquivo appsettings.Development.json está configurado corretamente, especialmente as strings de conexão e variáveis de ambiente.

### 3. Restaure os pacotes

No terminal, execute:

```bash
dotnet restore 02_BackEnd/Tudao.BackEnd.sln
```

### 4. Compile a solução

Execute:

```bash
dotnet build 02_BackEnd/Tudao.BackEnd.sln
```

### 5. Execute a API

Navegue até a pasta de apresentação e rode o projeto WebApi:

```bash
cd 02_BackEnd/1_Presentation
```
```bash
dotnet run
```

Isso irá iniciar a API, normalmente disponível em <https://localhost:7202> ou <http://localhost:5158> (conforme launchSettings.json).

### 6. Acesse o Swagger

Abra o navegador e acesse:
<https://localhost:7202/swagger> ou <http://localhost:5158/swagger>
Assim, você poderá testar os endpoints da API diretamente pelo Swagger UI.
