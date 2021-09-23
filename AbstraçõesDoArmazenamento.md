# Abstrações do pacote de armazenamento

## Contexto
Grande parte das aplicações precisam de armazenamento de dados.

## Decisão
Foram criado duas interfaces principais, IDao e IRepositório onde o IDao tem o conceito de acessar os dados 
(Data Access Object) tem como propósito abstrair a comunicação com o banco de dados, a interface IRepositório
usa o conceito de repositório do DDD, ou seja, é um padrão para o acesso aos dados solicitadas pelo domínio. 
Foi criado IToAsyncEnumerable para fazer leituras recebendo um IQueryable, atualmente leitura assincronas com o
banco de dados devem ser implementadas manualmente. Para está interface funcionar corretamente foram criados 
IQueryableDao e IQueryProviderDao que servem apenas para armazenar o IDao que criou IQueryable, 
sendo assim o IDao implementado pode obter seus dados para executar a leitura.

## Vantagens
Funciona com entityframework, mongodb, ravendb, armazenamento de blobs da azure e aws, redis e muitos outros.
Outra vantagem clara do IDao é que da maneira que ele foi feito, caso necessite de uma personalização, 
como por exemplo: entidade X precisa armazenar no banco Y e entidade Z precisa armazenar no banco W basta 
criar um gerenciador, ou seja, criar uma classe concreta que implementa IDao que recebe outros classes concretas
que também implementam IDao. Caso precise de algum cache bastaria colocar no gerenciador um lógica com um implementação
de dao com cache.
Todas essas vantagens já são utilizadas em outros projetos.

## Desvantagens
Necessitasse de um construtor vazio nos objetos.

## Evolução
Conforme a necessidade podem ser criado novas intefaces para a leitura e retornar uma lista por exemplo.
