# Geração de id em relação aos campos da entidade

## Contexto
Algumas entidades possuem o conceito de unicidade de um valor ou mais valores. 

## Decisão
Afim de facilitar a ideia de unicidade e ganhar performance com isso o repositório pode implementar um padrão de 
geração de id, assim antes de salvar deve ser definido o id e opcionalmente pode ser criado um método para ler
a partir do campo único, gerando o id e lendo por ele ao em vez de fazer uma query.

## Vantagens
Remoção de querys em cima de um valor único, poupando criação de indíces com essa única intenção, leitura pelo id
normalmente é mais rápida e pode ser cacheada mais facilmente.
Remoção de geração de id pelo banco de dados, ganhando assim performance.

## Desvantagens
Maior complexidade no repositórios.

## Evolução
A decisão arquitetural hoje está sendo implementada manualmente, poderia evoluir para uma implementação de código 
definindo o padrão.