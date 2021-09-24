# Cadastro

## Contexto
Grande parte das aplicações necessitam de um cadastro.

## Decisão
Foi decidido implementar um modelo de CQRS, usando os pacotes de MediatR com um padrão de criar:
manipuladores de comandos, comandos e validadores. Os validadores devem herdar de 
Estudo.Domínio.Validadores.ValidadorBase e usar o pacote FluentValidation, os comandos devem herdar 
Estudo.Domínio.Comandos.ComandoSemRetorno e os manupuladores de comando devem implementar a interface IRequestHandler.
Os comandos devem declarar os seus validadores por meio do atributo Estudo.Domínio.Validadores.ValidadorAttribute.
A validação por meio do FLuentValidation não vale apena abstrair.

## Vantagens
Estrutura robusta e simplificada, com a criação do padrão do manipulador de comandos é possível 
processamento de solicitação sem complicações.

## Desvantagens
Dependências obrigatórias com o MediatR e FluentValidation.

## Evolução
A dependência com o MediatR com mais esforço poderia ser eliminada.