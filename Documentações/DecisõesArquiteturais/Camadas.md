# Camadas da arquitetura

## Contexto
Áfim de manter as boas práticas alguns conceitos devem ser separados.

## Decisão
O repositório é dividido em quatro camadas, são elas:
* Serviço: input e output
> Exemplos: backend, console, frontend
* Aplicação: configurações necessárias para a regra de negócio operar
> Exemplos: configurar injeção de dependência, configuração de estruturas para o banco de dados
* Domínio: regras de negócio
> Exemplos: entidades, repositórios, comandos
* Infraestrutura: funcionalidades necessárias
> Exemplos: comunicação com o banco de dados, com um serviço de fila

## Vantagens
Remoção de querys em cima de um valor único, poupando criação de indíces com essa única intenção, leitura pelo id
normalmente é mais rápida e pode ser cacheada mais facilmente.
Remoção de geração de id pelo banco de dados, ganhando assim performance.

## Desvantagens
Maior complexidade no repositórios.

## Evolução
A decisão arquitetural hoje está sendo implementada manualmente, poderia evoluir para uma implementação de código 
definindo o padrão.