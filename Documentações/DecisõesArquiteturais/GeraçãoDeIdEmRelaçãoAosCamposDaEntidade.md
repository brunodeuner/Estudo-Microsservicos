# Gera��o de id em rela��o aos campos da entidade

## Contexto
Algumas entidades possuem o conceito de unicidade de um valor ou mais valores. 

## Decis�o
Afim de facilitar a ideia de unicidade e ganhar performance com isso o reposit�rio pode implementar um padr�o de 
gera��o de id, assim antes de salvar deve ser definido o id e opcionalmente pode ser criado um m�todo para ler
a partir do campo �nico, gerando o id e lendo por ele ao em vez de fazer uma query.

## Vantagens
Remo��o de querys em cima de um valor �nico, poupando cria��o de ind�ces com essa �nica inten��o, leitura pelo id
normalmente � mais r�pida e pode ser cacheada mais facilmente.
Remo��o de gera��o de id pelo banco de dados, ganhando assim performance.

## Desvantagens
Maior complexidade no reposit�rios.

## Evolu��o
A decis�o arquitetural hoje est� sendo implementada manualmente, poderia evoluir para uma implementa��o de c�digo 
definindo o padr�o.