# Abstra��es do pacote de armazenamento

## Contexto
Grande parte das aplica��es precisam de armazenamento de dados.

## Decis�o
Foram criado duas interfaces principais, IDao e IReposit�rio onde o IDao tem o conceito de acessar os dados 
(Data Access Object) tem como prop�sito abstrair a comunica��o com o banco de dados, a interface IReposit�rio
usa o conceito de reposit�rio do DDD, ou seja, � um padr�o para o acesso aos dados solicitadas pelo dom�nio. 
Foi criado IToAsyncEnumerable para fazer leituras recebendo um IQueryable, atualmente leitura assincronas com o
banco de dados devem ser implementadas manualmente. Para est� interface funcionar corretamente foram criados 
IQueryableDao e IQueryProviderDao que servem apenas para armazenar o IDao que criou IQueryable, 
sendo assim o IDao implementado pode obter seus dados para executar a leitura.

## Vantagens
Funciona com entityframework, mongodb, ravendb, armazenamento de blobs da azure e aws, redis e muitos outros.
Outra vantagem clara do IDao � que da maneira que ele foi feito, caso necessite de uma personaliza��o, 
como por exemplo: entidade X precisa armazenar no banco Y e entidade Z precisa armazenar no banco W basta 
criar um gerenciador, ou seja, criar uma classe concreta que implementa IDao que recebe outros classes concretas
que tamb�m implementam IDao. Caso precise de algum cache bastaria colocar no gerenciador um l�gica com um implementa��o
de dao com cache.
Todas essas vantagens j� s�o utilizadas em outros projetos.

## Desvantagens
Necessitasse de um construtor vazio nos objetos.

## Evolu��o
Conforme a necessidade podem ser criado novas intefaces para a leitura e retornar uma lista por exemplo.
