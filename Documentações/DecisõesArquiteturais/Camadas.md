# Camadas da arquitetura

## Contexto
Afim de manter as boas práticas alguns conceitos devem ser separados.

## Decisão
O repositório é dividido em quatro camadas, são elas:
* Serviço: input e output
> Exemplos: backend, console, frontend
* Aplicação: configurações necessárias para a regra de negócio operar
> Exemplos: configurar injeção de dependência, configuração de estruturas para o banco de dados
* Domínio: regras de negócio
> Exemplos: entidades, repositórios, comandos
* Infraestrutura: funcionalidades necessárias que servem o domínio
> Exemplos: comunicação com o banco de dados, com um serviço de fila

Seguindo o padrão do DDD estas camadas podem ser representadas em um Core, ou seja, é a base do sistema
todo mundo pode usar ela, ou pode estar dentro de um contexto específico.

## Vantagens
Separação de responsábilidades, cada camada executada apenas o que é designada ganhando assim coesão e uma
melhoria nítida na manutenção do sistema.
As camadas só podem ter dependências com as suas camadas inferiores, não somente a mais próxima e sim em 
todas as camadas a baixo dela, conforme ordem mencionada anteriormente. 

## Desvantagens
Complexidade, todas as camadas são opcionais porém o conceito não é opcional, sendo assim, no momento que 
existir o conceito ele deve ser implementado conforme o padrão de camadas definidas.

## Evolução
Nenhuma evolução prevista.