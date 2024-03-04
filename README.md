# ShortLink Service

Este é um projeto de exemplo que demonstra um serviço de encurtamento de URLs usando ASP.NET Core.

## Descrição

O serviço `ShortLink` permite aos usuários encurtar URLs longas em URLs curtas e fáceis de lembrar. Ele fornece duas principais funcionalidades:

1. **ShortenLink:** Este endpoint permite aos usuários fornecer uma URL longa e receber de volta uma URL curta.
2. **RedirectShortLink:** Este endpoint permite aos usuários acessar uma URL curta e serem redirecionados para a URL original.

## Tecnologias Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SQLite
- Microsoft.EntityFrameworkCore.InMemory (Para testes em memória)
- FluentAssertions (Para testes de unidade)

## Como Executar

1. Clone o repositório para sua máquina local.
2. Abra o projeto em sua IDE preferida (Visual Studio, Visual Studio Code, etc.).
3. Execute o projeto.

## Endpoints

### ShortenLink

- **Método:** POST
- **Endpoint:** `/api/links/shortenlink`
- **Payload:** 
    ```json
    {
        "originalUrl": "http://example.com"
    }
    ```
- **Retorno de Sucesso:** 
    - Status Code: 200 OK
    - Body: URL curta gerada.

### RedirectShortLink

- **Método:** GET
- **Endpoint:** `/api/links/{shortUrl}`
- **Retorno de Sucesso:** 
    - Status Code: 302 Found
    - O navegador redirecionará automaticamente para a URL original.

## Testes

Este projeto inclui testes de unidade para as principais funcionalidades do serviço. Os testes são escritos usando xUnit, Moq e FluentAssertions. Eles podem ser encontrados no diretório `ShortLink.ServicesTests` no projeto.

## Autor

Maria Tauany Santos Feitosa

[![linkedin badge](https://img.shields.io/badge/linkedin-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/tauanyfeitosa/)
<a href="mailto:tauanysanttos13@gmail.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>
[<img src="https://img.shields.io/badge/instagram-%23E4405F.svg?&style=for-the-badge&logo=instagram&logoColor=white" />](https://instagram.com/tauanyfeitosa)
