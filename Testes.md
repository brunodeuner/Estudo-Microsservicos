# Testes

## Contexto
Afim de garantir qualidade para o sistema � necess�rio automatizar os testes, um modelo bem conhecido de testes 
� o modelo em pir�mide onde basicamente na base do sistema temos os testes unit�rios e no topo os testes 
explorat�rios ou end to end, um princ�pio da pir�mide � que quanto mais ao topo estamos, mais custoso � criar e 
manter os testes, junto com o custo de executar os mesmos.

Tomando como premissa que se tivermos um sistema com 100% de cobertura de testes end to end teriamos um software
com mais qualidade que um sistema com 100% de cobertura com uma parte de testes end to end e a maior parte com 
testes de unidade podemos chegar a uma conclus�o: testes end to end normalmente garantem mais qualidade de 
software (n�o de c�digo) em rela��o a testes de unidade. Normalmente quanto mais alto estamos na pir�mide mais
valioso � o teste, a pir�mide � como � por causa dos custos de cada teste.

Mas e se os testes end to end forem t�o faceis quanto os testes de unidade, faria sentido seguir a pir�mide?
Se existisse uma ferramenta capaz de facilitar a cria��o de testes end to end temos que rever a pir�mide.
Por exemplo, se existir um cadastro de pessoa, onde deve ser validado o cpf e o seu nome, poderia ser criado
alguns testes unit�rios para validar o cpf, validar se foram preenchidos os campos, haveriam testes para cobrir
as situa��es de cadastros inv�lidos tamb�m, at� ai tudo bem, o dom�nio est� coberto, provavelmente uma meia duzia 
de testes seria o suficiente, mas quem disse que o servi�o est� funcionando, provavelmente teriamos que criar
alguns testes end to end para verificar se a controller est� funcionando, se o servi�o est� repassando 
corretamente os dados para a camada de dom�nio, e vice versa, assim como ao dar algum erro de valida��o 
o servi�o est� retornando um c�digo 400 por exemplo se for uma API, tamb�m faltaria validar o modelo de dados
de entrada e sa�da da api. Provavelmente uma duzia de testes seria o suficiente, teriamos digamos que duas 
duzias ao menos de testes para garantir completamente o caso de cadastro de usu�rio, mas na pr�tica em muitos 
projetos estes testes end to end s�o feitos sob demanda, em endpoint mais criticos devido ao seu custo de 
cria��o e manuten��o.

Levando em considera��o os testes end to end criados, se eles abrangerem todas as situa��es conforme os 
requisitos do sistema, qual seria a vantagem em termos de qualidade de software dos testes de unidade? 
Considerando que o sistema tenha apenas esta regra de neg�cio, criando apenas o teste end to end j� teriamos
uma qualidade provavelmente muito interessante.

## Decis�o
Para testar o sistema a piramide ser� invertida (conhecido como casquinha de sorvete).

## Vantagens
Maior qualidade, quantidade de testes reduzida, maior estabilidade devido a testes end to end em uma api 
normalmente s�o mais est�veis, sendo assim a manuten��o ser� melhor do que muitos testes de unidade e 
integra��o.

## Desvantagens
A programa��o seria diferente em rela��o a outros projetos que adotam o modelo de pir�mide.

## Evolu��o
A biblioteca de classe que facilita a cria��o de testes end to end j� est� bem validada em outros projetos, 
por�m n�o foi trazida todas as suas funcionalidades para este projeto. Em outros projetos h� casos de envio de 
blobs, compara��o de relat�rios por html e etc.