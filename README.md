# OrganizaAtividades

API REST desenvolvida em ASP.NET Core para organizar atividades acadêmicas. Cada atividade pode ser relacionada a uma disciplina e a uma categoria.

## Tecnologias

- C#
- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- SQL Server LocalDB
- Autenticação JWT
- Swagger
- Insomnia

## Funcionalidades

- CRUD completo de disciplinas
- CRUD completo de categorias
- CRUD completo de atividades
- Relacionamento entre atividades, disciplinas e categorias
- Filtros de atividades por título, prioridade e situação
- Validação dos dados enviados
- Validação da existência de disciplinas e categorias relacionadas
- Tratamento de registros duplicados e não encontrados
- Login e validação de credenciais
- Geração e validação de token JWT
- Autorização pelos perfis `Administrador` e `Usuario`
- Respostas HTTP `200`, `201`, `204`, `400`, `401`, `403` e `404`

## Banco de dados

O projeto utiliza o SQL Server LocalDB com o banco `OrganizaAtividadesDb`.

Antes de executar a API pela primeira vez, abra o Console do Gerenciador de Pacotes no Visual Studio e aplique as migrations:

```powershell
Update-Database
```

## Como executar

1. Abra a solução `OrganizaAtividades.slnx` no Visual Studio.
2. Confirme que o SQL Server LocalDB está instalado e disponível.
3. Execute o comando `Update-Database` no Console do Gerenciador de Pacotes.
4. Inicie o projeto.
5. Faça o login para obter um token JWT.
6. Teste os endpoints pelo Swagger ou pelo Insomnia.

Durante o desenvolvimento, a API foi testada no endereço:

```text
https://localhost:7226
```

## Endpoints

### Autenticação

```http
POST /api/auth/login
GET  /api/auth/me
```

O endpoint de login retorna um token JWT. As consultas são públicas, as operações de criação e alteração exigem autenticação e as exclusões são permitidas somente para o perfil `Administrador`.

Usuários criados automaticamente para os testes:

```text
Administrador: admin@organiza.com / Admin@123
Usuário:       usuario@organiza.com / Usuario@123
```

### Disciplinas

```http
GET    /api/disciplinas
GET    /api/disciplinas/{id}
POST   /api/disciplinas
PUT    /api/disciplinas/{id}
DELETE /api/disciplinas/{id}
```

### Categorias

```http
GET    /api/categorias
GET    /api/categorias/{id}
POST   /api/categorias
PUT    /api/categorias/{id}
DELETE /api/categorias/{id}
```

### Atividades

```http
GET    /api/atividades
GET    /api/atividades/{id}
POST   /api/atividades
PUT    /api/atividades/{id}
DELETE /api/atividades/{id}
```

## Filtros de atividades

Os filtros podem ser utilizados separadamente ou combinados:

```http
GET /api/atividades?titulo=Projeto
GET /api/atividades?prioridade=Alta
GET /api/atividades?situacao=Pendente
```
