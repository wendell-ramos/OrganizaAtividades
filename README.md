# OrganizaAtividades

API Web desenvolvida em ASP.NET Core para organizar atividades acadêmicas.

## Checkpoint 1

- Cadastro temporário de atividades em memória
- Listagem de todas as atividades
- Consulta de uma atividade por ID
- Respostas HTTP `200 OK` e `404 Not Found`

## Tecnologias

- C#
- ASP.NET Core Web API
- .NET 8
- Swagger
- Insomnia

## Endpoints implementados

```http
GET /api/atividades
GET /api/atividades/{id}
```

Os dados são temporários e voltam ao estado inicial quando a aplicação é reiniciada.
