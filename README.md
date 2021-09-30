# Estudo-Microsservicos [![Status do build](https://github.com/brunodeuner/Estudo-Microsservicos/actions/workflows/build.yml/badge.svg)](https://github.com/brunodeuner/Estudo-Microsservicos/actions/workflows/build.yml)
Repositório de estudos para microsserviços, reune alguns conceitos como:
* Código Limpo
* Arquitetura Limpa
* SOLID
* DRY
* KISS
* DDD
* Object Calisthenics
* CQRS
* GoF
* Unidade de Trabalho
* Multicamada
* MVC
* Eventos
* Microsserviços
* Publish-Subscribe

# Como executar?

    dotnet run --project src/Clientes/Estudo.Clientes.Serviço.Api/Estudo.Clientes.Serviço.Api.csproj --urls=http://localhost:5000
    dotnet run --project src/Cobranças/Estudo.Cobranças.Serviço.Api/Estudo.Cobranças.Serviço.Api.csproj --urls=http://localhost:5001
    dotnet run --project src/CálculoDeConsumo/Estudo.CálculoDeConsumo.Serviço.Api/Estudo.CálculoDeConsumo.Serviço.Api.csproj --urls=http://localhost:5002

# O que foi feito?
Implementado duas api's independentes, sendo elas: **Estudo.Clientes.Serviço.Api** e **Estudo.Cobranças.Serviço.Api**
para cumprir com este objetivo foi usado o padrão de **Publish-Subscribe** e **arquitetura orientada a eventos** 
no projeto **Estudo.Core.Infraestrutura.Bus.Abstrações**. Devido a necessidade de cumprir com um desafio o 
**RepositórioDePessoa** acabou assumindo uma dependência indireta da regra de negócio de clientes, que envolve
saber que o cliente é unico por cpf, após a revisão deste desafio será removido esta dependência.

A api de **Estudo.CálculoDeConsumo.Serviço.Api** não é independente propositalmente para simular um serviço que consuma um
ou mais microsserviços.

# Arquitetura
* [Decisões arquiteturais](Documentações/DecisõesArquiteturais/AbstraçõesDoArmazenamento.md)
* [Validação de dependência](Arquitetura/ValidaçãoDeDependência/DiagramaDeValidaçãoDeDependência.pdf)

# Documentação das api's
Podem ser visualizadas concatenando **swagger/index.html** no final da url.

# Como contribuir
Qualquer contribuição é aceita, basta ir em discussões ou criar um novo pull request.
