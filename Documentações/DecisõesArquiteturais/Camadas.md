# Camadas da arquitetura

## Contexto
Afim de manter as boas pr�ticas alguns conceitos devem ser separados.

## Decis�o
O reposit�rio � dividido em quatro camadas, s�o elas:
* Servi�o: input e output
> Exemplos: backend, console, frontend
* Aplica��o: configura��es necess�rias para a regra de neg�cio operar
> Exemplos: configurar inje��o de depend�ncia, configura��o de estruturas para o banco de dados
* Dom�nio: regras de neg�cio
> Exemplos: entidades, reposit�rios, comandos
* Infraestrutura: funcionalidades necess�rias que servem o dom�nio
> Exemplos: comunica��o com o banco de dados, com um servi�o de fila

Seguindo o padr�o do DDD estas camadas podem ser representadas em um Core, ou seja, � a base do sistema
todo mundo pode usar ela, ou pode estar dentro de um contexto espec�fico.

## Vantagens
Separa��o de respons�bilidades, cada camada executada apenas o que � designada ganhando assim coes�o e uma
melhoria n�tida na manuten��o do sistema.
As camadas s� podem ter depend�ncias com as suas camadas inferiores, n�o somente a mais pr�xima e sim em 
todas as camadas a baixo dela, conforme ordem mencionada anteriormente. 

## Desvantagens
Complexidade, todas as camadas s�o opcionais por�m o conceito n�o � opcional, sendo assim, no momento que 
existir o conceito ele deve ser implementado conforme o padr�o de camadas definidas.

## Evolu��o
Nenhuma evolu��o prevista.