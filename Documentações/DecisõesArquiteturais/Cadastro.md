# Cadastro

## Contexto
Grande parte das aplica��es necessitam de um cadastro.

## Decis�o
Foi decidido implementar um modelo de CQRS, usando os pacotes de MediatR com um padr�o de criar:
manipuladores de comandos, comandos e validadores. Os validadores devem herdar de 
Estudo.Dom�nio.Validadores.ValidadorBase e usar o pacote FluentValidation, os comandos devem herdar 
Estudo.Dom�nio.Comandos.ComandoSemRetorno e os manupuladores de comando devem implementar a interface IRequestHandler.
Os comandos devem declarar os seus validadores por meio do atributo Estudo.Dom�nio.Validadores.ValidadorAttribute.
A valida��o por meio do FLuentValidation n�o vale apena abstrair.

## Vantagens
Estrutura robusta e simplificada, com a cria��o do padr�o do manipulador de comandos � poss�vel 
processamento de solicita��o sem complica��es.

## Desvantagens
Depend�ncias obrigat�rias com o MediatR e FluentValidation.

## Evolu��o
A depend�ncia com o MediatR com mais esfor�o poderia ser eliminada.