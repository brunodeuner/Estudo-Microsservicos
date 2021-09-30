# Camadas da arquitetura

## Contexto
�fim de manter as boas pr�ticas alguns conceitos devem ser separados.

## Decis�o
O reposit�rio � dividido em quatro camadas, s�o elas:
* Servi�o: input e output
> Exemplos: backend, console, frontend
* Aplica��o: configura��es necess�rias para a regra de neg�cio operar
> Exemplos: configurar inje��o de depend�ncia, configura��o de estruturas para o banco de dados
* Dom�nio: regras de neg�cio
> Exemplos: entidades, reposit�rios, comandos
* Infraestrutura: funcionalidades necess�rias
> Exemplos: comunica��o com o banco de dados, com um servi�o de fila

## Vantagens
Remo��o de querys em cima de um valor �nico, poupando cria��o de ind�ces com essa �nica inten��o, leitura pelo id
normalmente � mais r�pida e pode ser cacheada mais facilmente.
Remo��o de gera��o de id pelo banco de dados, ganhando assim performance.

## Desvantagens
Maior complexidade no reposit�rios.

## Evolu��o
A decis�o arquitetural hoje est� sendo implementada manualmente, poderia evoluir para uma implementa��o de c�digo 
definindo o padr�o.