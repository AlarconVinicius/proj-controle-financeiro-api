# Documentação da API de Controle Financeiro Pessoal

Bem-vindo à documentação da API de Controle Financeiro Pessoal. Esta API foi desenvolvida utilizando a tecnologia .NET 7 e é projetada para ajudar você a gerenciar suas finanças pessoais. Esta documentação fornecerá informações sobre as tecnologias utilizadas, os pré-requisitos e instruções para executar a API usando Docker e Docker Compose.

<hr>

## Tecnologias Utilizadas


### A API de Controle Financeiro Pessoal utiliza as seguintes tecnologias e ferramentas:

- **.NET 7**: Uma plataforma de desenvolvimento multiplataforma para criar aplicativos modernos e escaláveis.
- **Docker**: Uma plataforma para desenvolvimento, envio e execução de aplicativos em contêineres.
- **Docker Compose**: Uma ferramenta para definir e executar aplicativos Docker multi-container.
- **SQL Server**: Um banco de dados relacional utilizado para armazenar dados da aplicação.
- **Autenticação JWT**: A API inclui um sistema de autenticação baseado em JSON Web Tokens (JWT) para proteger as transações financeiras pessoais. O JWT é utilizado para autenticar os usuários e controlar o acesso aos recursos da API de forma segura.

<hr>

## Pré-requisitos

### Antes de executar a API de Controle Financeiro Pessoal, verifique se você possui os seguintes pré-requisitos instalados em seu sistema:

1. **Docker**: Certifique-se de que o Docker esteja instalado em seu sistema. Você pode baixar e instalar o Docker a partir do [site oficial](https://www.docker.com/get-started).

2. **Docker Compose**: Certifique-se de que o Docker Compose esteja instalado em seu sistema. O Docker Compose é geralmente instalado automaticamente quando você instala o Docker.

<hr>

## Clonando o Repositório

Para obter o código-fonte da API de Controle Financeiro Pessoal e começar a trabalhar com ele, você pode clonar o repositório do GitHub. Siga as etapas abaixo:

1. Abra um terminal ou prompt de comando em seu sistema.

2. Execute o seguinte comando para clonar o repositório:

```bash
git clone https://github.com/AlarconVinicius/proj-controle-financeiro-api.git
```
*Isso irá baixar o código-fonte da API para o seu sistema local.*

3. Navegue até o diretório clonado usando o comando cd:

```bash
cd ProjControleFinanceiro
```

<hr>

## Executando a API

###  Para executar a API de Controle Financeiro Pessoal, siga estas etapas:

1. Abra um terminal na raiz do projeto onde está localizado o arquivo `docker-compose.yml`.

2. Execute o seguinte comando para iniciar a API usando o Docker Compose:

```bash
docker-compose up
```
#### A API será compilada e implantada em um contêiner Docker. Você poderá acessá-la em <strong>http://localhost:8001/swagger/index.html</strong>.

*Nota: Ao iniciar a aplicação pela primeira vez, um perfil de administrador é criado automaticamente com as seguintes informações:*

- **Nome**: Usuário
- **Sobrenome**: Administrador
- **Número de Telefone**: (99) 99999-9999
- **E-mail**: admin@controlefinanceiro.com
- **Senha**: Admin@123
<hr>

## Rotas da API

### A API de Controle Financeiro Pessoal possui as seguintes rotas:

## Rotas da API de Autenticação

* ### Registrar um novo usuário
    * Endpoint: POST /api/auth/registrar

Descrição: Registra um novo usuário na aplicação.

### Parâmetros da Solicitação:

* name (string): Nome do usuário.
* lastName (string): Sobrenome do usuário.
* phoneNumber (string): Número de telefone do usuário.
* email (string): Endereço de e-mail do usuário.
* password (string): Senha do usuário.
* confirmPassword (string): Confirmação de senha do usuário.

```json
{
  "name": "string",
  "lastName": "string",
  "phoneNumber": "string",
  "email": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

### Códigos de Resposta:

#### Código de resposta 200: Sucesso ao registrar um novo usuário.

Corpo da resposta: 

```json
{
    "success": true, 
    "data": null
}
```

* ### Autenticar um usuário
    * Endpoint: POST /api/auth/autenticar

Descrição: Autentica um usuário na aplicação.

### Parâmetros da Solicitação:

* email (string): Endereço de e-mail do usuário.
* password (string): Senha do usuário.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao autenticar o usuário.

Corpo da resposta:

* accessToken (string): Token de acesso gerado após a autenticação.
* expiresIn (int): Tempo de expiração do token em segundos.
* usuarioToken (objeto): Objeto contendo informações do usuário autenticado.
* id (string): ID do usuário.
* email (string): Endereço de e-mail do usuário.
* claims (array de objetos): Lista de reivindicações (claims) associadas ao usuário autenticado.
* value (string): Valor da reivindicação.
* type (string): Tipo da reivindicação.

```json
{
  "success": true,
  "data": {
    "accessToken": "string",
    "expiresIn": 0,
    "usuarioToken": {
      "id": "string",
      "email": "string",
      "claims": [
        {
          "value": "string",
          "type": "string"
        }
      ]
    }
  }
}
```

### Código de resposta 400: Erros de validação ou problemas na requisição.

Corpo da resposta:

```json
{
  "success": false,
  "errors": {
    "additionalProp1": ["string"],
    "additionalProp2": ["string"],
    "additionalProp3": ["string"]
  }
}
```

## Rotas da API de Transações

* ### Adicionar uma nova transação
    * Endpoint: POST /api/transactions

Descrição: Adiciona uma nova transação.

### Parâmetros da Solicitação:

* clienteId (Guid): ID do cliente que está realizando a transação.
* descricao (string): Descrição da transação.
* valor (decimal): Valor da transação.
* data (string): Data da transação no formato "dd/MM/yyyy".
* tipoTransacao (int): Tipo da transação. Opções disponíveis:
    * Receita (1): Transação de receita.
    * Despesa (2): Transação de despesa.
* categoria (int): Categoria da transação. Opções disponíveis:
    * Lazer (1)
    * Restaurantes (2)
    * Viagem (3)
    * Educacao (4)
    * Vestuario (5)
    * Saúde (6)
    * Cartao (7)
    * Salario (8)
    * Casa (9)
    * Mercado (10)
    * Servicos (11)
    * Assinaturas (12)
    * Pets (13)
    * Investimentos (14)
    * Transporte (15)
    * Presentes (16)
    * Outros (17)
* pago (bool): Indica se a transação foi paga (true) ou não (false).
* repete (bool): Indica se a transação é uma transação repetitiva (true) ou não (false).
* qtdRepeticao (int): Quantidade de repetições da transação (aplicável apenas se "repete" for true).

```json
{
  "clienteId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "descricao": "string",
  "valor": 0,
  "data": "dd/MM/yyyy",
  "tipoTransacao": 1,
  "categoria": 1,
  "pago": false,
  "repete": false,
  "qtdRepeticao": 0
}
```
### Códigos de Resposta:

#### Código de resposta 200: Sucesso ao adicionar uma nova transação.

Corpo da resposta:

```json
{
  "success": true,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "descricao": "string",
    "valor": 0,
    "data": "dd/MM/yyyy",
    "tipoTransacao": {
      "id": 0,
      "tipoTransacao": "string"
    },
    "categoria": {
      "id": 0,
      "categoria": "string"
    },
    "pago": true,
    "pagoRelatorio": "string"
  }
}
```


* ### Obter uma transação pelo seu ID
    * Endpoint: GET /api/transactions/{id}

Descrição: Obtém uma transação pelo seu ID.

### Parâmetros da Solicitação:

* id (Guid): ID da transação a ser obtida.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao obter a transação.

Corpo da resposta:

```json
{
  "success": true,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "descricao": "string",
    "valor": 0,
    "data": "dd/MM/yyyy",
    "tipoTransacao": {
      "id": 0,
      "tipoTransacao": "string"
    },
    "categoria": {
      "id": 0,
      "categoria": "string"
    },
    "pago": true,
    "pagoRelatorio": "string"
  }
}
```


* ### Obter todas as transações
    * Endpoint: GET /api/transactions

Descrição: Obtém todas as transações.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao obter a lista de transações.

Corpo da resposta:

```json
{
  "success": true,
  "data": {
    "entrada": 0,
    "saida": 0,
    "saldo": 0,
    "transacoes": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "descricao": "string",
        "valor": 0,
        "data": "dd/MM/yyyy",
        "tipoTransacao": {
          "id": 0,
          "tipoTransacao": "string"
        },
        "categoria": {
          "id": 0,
          "categoria": "string"
        },
        "pago": true,
        "pagoRelatorio": "string"
      }
    ]
  }
}
```


* ### Obter todas as transações de um respectivo mês e ano
    * Endpoint: GET /api/transactions/mes-ano

Descrição: Obtém todas as transações de um respectivo mês e ano.

### Parâmetros da Solicitação:

* mes (int): Mês desejado.
* ano (int): Ano desejado.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao obter a lista de transações.

Corpo da resposta:

```json
{
  "success": true,
  "data": {
    "entrada": 0,
    "saida": 0,
    "saldo": 0,
    "transacoes": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "descricao": "string",
        "valor": 0,
        "data": "dd/MM/yyyy",
        "tipoTransacao": {
          "id": 0,
          "tipoTransacao": "string"
        },
        "categoria": {
          "id": 0,
          "categoria": "string"
        },
        "pago": true,
        "pagoRelatorio": "string"
      }
    ]
  }
}
```

* ### Atualizar uma transação
    * Endpoint: PUT /api/transactions

Descrição: Atualiza uma transação existente.

### Parâmetros da Solicitação:

* id (Guid): ID da transação a ser atualizada.
* descricao (string): Nova descrição da transação.
* valor (decimal): Novo valor da transação.
* data (string): Nova data da transação no formato "dd/MM/yyyy".
* tipoTransacao (int): Novo tipo da transação (1 para débito, 2 para crédito, etc.).
* categoria (int): Nova categoria da transação (1 para alimentação, 2 para moradia, etc.).

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "descricao": "string",
  "valor": 0,
  "data": "dd/MM/yyyy",
  "tipoTransacao": 1,
  "categoria": 1
}
```

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao atualizar a transação.

Corpo da resposta:

```json
{
    "success": true, 
    "data": null
}
```

*  ### Atualizar o status de pagamento de uma transação
    * Endpoint: PATCH /api/transactions/{id}/pago

Descrição: Atualiza o status de pagamento de uma transação.

### Parâmetros da Solicitação:

* id (Guid): ID da transação a ser atualizada.

* pago (bool): Valor booleano indicando se a transação foi paga ou não.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao atualizar o status de pagamento da transação.

Corpo da resposta:

```json
{
    "success": true, 
    "data": null
}
```

* ### Deletar uma transação
    * Endpoint: DELETE /api/transactions/{id}

Descrição: Deleta uma transação.

### Parâmetros da Solicitação:

* id (Guid): ID da transação a ser deletada.

### Códigos de Resposta:

### Código de resposta 200: Sucesso ao deletar a transação.

Corpo da resposta:

```json
{
    "success": true, 
    "data": null
}
```

### Código de resposta 400: Erros de validação ou problemas na requisição.

Corpo da resposta:

```json
{
  "success": false,
  "errors": {
    "additionalProp1": ["string"],
    "additionalProp2": ["string"],
    "additionalProp3": ["string"]
  }
}
```

## Rota da API de Relatório

* ### Gerar PDF com base no filtro de transações
    * Endpoint: POST /api/relatorios

Descrição: Gera um arquivo PDF com base em um filtro de transações especificado.

### Parâmetros da Solicitação:

* De (string, opcional, padrão: "dd/MM/yyyy"): Data de início do período de filtro no formato "dd/MM/yyyy".

* Ate (string, opcional, padrão: "dd/MM/yyyy"): Data de fim do período de filtro no formato "dd/MM/yyyy".

* Mes (int, opcional, padrão: "Entre 1 e 12"): Número do mês para filtro.

* Ano (int, opcional, padrão: "yyyy"): Ano para filtro.

* CicloPdfDto (enum, obrigatório): Ciclo de geração do relatório PDF. Opções disponíveis:

    * Período (1): Gera o relatório para um período específico.
    * Mensal (2): Gera o relatório mensalmente.
    * Anual (3): Gera o relatório anualmente.

### Códigos de Resposta:

### Código de resposta 200: Sucesso na geração do relatório.

Corpo da resposta: 

```json
O arquivo PDF gerado será retornado como resposta.
```

### Código de resposta 400: Erros de validação ou problemas na requisição.

Corpo da resposta:

```json
{
  "success": false,
  "errors": {
    "additionalProp1": ["string"],
    "additionalProp2": ["string"],
    "additionalProp3": ["string"]
  }
}
```

## Rotas da API de Usuário

* ### Obter Usuário por ID
    * Endpoint: GET /api/usuarios/{id}

Descrição: Obtém as informações de um usuário com base no ID fornecido.

### Parâmetros da Solicitação:

* id (Guid): ID do usuário a ser obtido.

### Códigos de Resposta:

### Código de resposta 200: Sucesso na obtenção do usuário.

Corpo da resposta: 

```json
{
  "success": true,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string",
    "lastName": "string",
    "email": "string",
    "phoneNumber": "string",
    "roles": [
      "string"
    ]
  }
}
```

* ### Obter Todos os Usuários (Apenas para Administradores)
    * Endpoint: GET /api/usuarios

Descrição: Obtém a lista de todos os usuários. Apenas os administradores têm acesso a este endpoint.

### Códigos de Resposta:

### Código de resposta 200: Sucesso na obtenção da lista de usuários.

Corpo da resposta: 

```json
{
  "success": true,
  "data": [
    {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "lastName": "string",
        "email": "string",
        "phoneNumber": "string",
        "roles": [
        "string"
        ]
    }
  ]
}
```

* ### Atualizar Usuário
    * Endpoint: PUT /api/usuarios

Descrição: Atualiza as informações de um usuário.

### Parâmetros da Solicitação:

* id (string): Este é o ID único do usuário que você deseja atualizar. O ID identifica o usuário específico que será modificado.
* name (string): Este campo representa o novo nome do usuário. Você pode fornecer uma nova string que substituirá o nome existente do usuário.
* lastName (string): Este campo representa o novo sobrenome do usuário. Da mesma forma, você pode fornecer uma nova string para substituir o sobrenome existente.
* email (string): Este campo é usado para atualizar o endereço de e-mail do usuário. Você pode fornecer uma nova string de e-mail que substituirá o endereço de e-mail atual.
* phoneNumber (string): Este campo é destinado a atualizar o número de telefone do usuário. Assim como nos campos anteriores, você pode fornecer um novo número de telefone para substituir o número atual.

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string"
}
```
### Códigos de Resposta:

### Código de resposta 200: Sucesso na atualização do usuário.

Corpo da resposta: 

```json
{
    "success": true, 
    "data": null
}
```

* ### Alterar Status de Bloqueio de Usuário
    * Endpoint: PATCH /api/usuarios/{userId}/bloqueio

Descrição: Altera o status de bloqueio de um usuário com base no ID do usuário fornecido.

### Parâmetros da Solicitação:

* userId (Guid): ID do usuário a ser atualizado.
* bloquear (bool): Indica se o usuário deve ser bloqueado (true) ou desbloqueado (false).

### Códigos de Resposta:

### Código de resposta 200: Sucesso na alteração do status de bloqueio do usuário.

Corpo da resposta: 

```json
{
    "success": true, 
    "data": null
}
```

* ### Deletar Usuário
    * Endpoint: DELETE /api/usuarios/{userId}

Descrição: Deleta um usuário com base no ID do usuário fornecido.

### Parâmetros da Solicitação:

* userId (Guid): ID do usuário a ser deletado.

### Códigos de Resposta:

### Código de resposta 200: Sucesso na deleção do usuário.

Corpo da resposta: 

```json
{
    "success": true, 
    "data": null
}
```

### Código de resposta 400: Erros de validação ou problemas na requisição.

Corpo da resposta:

```json
{
  "success": false,
  "errors": {
    "additionalProp1": ["string"],
    "additionalProp2": ["string"],
    "additionalProp3": ["string"]
  }
}
```

## Rotas da API de Enum

* ### Obter Categorias Enum
    * Endpoint: GET /api/enum/categorias

Descrição: Este endpoint permite obter as opções disponíveis para o enum de categorias. Retorna as opções disponíveis para o enum de categorias de transações.

### Códigos de Resposta:

### Código de Resposta 200: Sucesso ao obter as opções do enum de categorias.

Corpo da resposta:

```json
{
  "success": true,
  "data": [
    {
      "value": 1,
      "name": "string"
    },
    {
      "value": 2,
      "name": "string"
    }
  ]
}
```
* ### Obter Tipos de Transação Enum
    * Endpoint: GET /api/enum/tipos-transacao


Descrição: Este endpoint permite obter as opções disponíveis para o enum de tipos de transação. Retorna as opções disponíveis para o enum de tipos de transação.

### Códigos de Resposta:

### Código de Resposta 200: Sucesso ao obter as opções do enum de tipos de transação.

Corpo da resposta:

```json
{
  "success": true,
  "data": [
    {
      "value": 1,
      "name": "string"
    },
    {
      "value": 2,
      "name": "string"
    }
  ]
}
```

*Certifique-se de consultar a documentação detalhada da API para obter informações sobre como usar cada rota.*

<hr>

## Documentação Adicional
Para obter informações mais detalhadas sobre como usar a API de Controle Financeiro Pessoal, consulte a documentação fornecida junto com o código-fonte.

Esta documentação serve como um guia básico para configurar e executar a API de Controle Financeiro Pessoal. Certifique-se de adaptar as configurações e instruções de acordo com suas necessidades específicas e ambiente de desenvolvimento.


<hr>

## Informações de Contato

- **Portfólio**: [Link para o seu Portfólio](http://alarconvinicius.com.br/)
- **LinkedIn**: [Link para o seu LinkedIn](https://www.linkedin.com/in/vin%C3%ADcius-alarcon-52a8a820a/)
