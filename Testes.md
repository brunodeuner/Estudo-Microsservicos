# Testes

## Contexto
Afim de garantir qualidade para o sistema é necessário automatizar os testes, um modelo bem conhecido de testes 
é o modelo em pirâmide onde basicamente na base do sistema temos os testes unitários e no topo os testes 
exploratórios ou end to end, um princípio da pirâmide é que quanto mais ao topo estamos, mais custoso é criar e 
manter os testes, junto com o custo de executar os mesmos.

Tomando como premissa que se tivermos um sistema com 100% de cobertura de testes end to end teriamos um software
com mais qualidade que um sistema com 100% de cobertura com uma parte de testes end to end e a maior parte com 
testes de unidade podemos chegar a uma conclusão: testes end to end normalmente garantem mais qualidade de 
software (não de código) em relação a testes de unidade. Normalmente quanto mais alto estamos na pirâmide mais
valioso é o teste, a pirâmide é como é por causa dos custos de cada teste.

Mas e se os testes end to end forem tão faceis quanto os testes de unidade, faria sentido seguir a pirâmide?
Se existisse uma ferramenta capaz de facilitar a criação de testes end to end temos que rever a pirâmide.
Por exemplo, se existir um cadastro de pessoa, onde deve ser validado o cpf e o seu nome, poderia ser criado
alguns testes unitários para validar o cpf, validar se foram preenchidos os campos, haveriam testes para cobrir
as situações de cadastros inválidos também, até ai tudo bem, o domínio está coberto, provavelmente uma meia duzia 
de testes seria o suficiente, mas quem disse que o serviço está funcionando, provavelmente teriamos que criar
alguns testes end to end para verificar se a controller está funcionando, se o serviço está repassando 
corretamente os dados para a camada de domínio, e vice versa, assim como ao dar algum erro de validação 
o serviço está retornando um código 400 por exemplo se for uma API, também faltaria validar o modelo de dados
de entrada e saída da api. Provavelmente uma duzia de testes seria o suficiente, teriamos digamos que duas 
duzias ao menos de testes para garantir completamente o caso de cadastro de usuário, mas na prática em muitos 
projetos estes testes end to end são feitos sob demanda, em endpoint mais criticos devido ao seu custo de 
criação e manutenção.

Levando em consideração os testes end to end criados, se eles abrangerem todas as situações conforme os 
requisitos do sistema, qual seria a vantagem em termos de qualidade de software dos testes de unidade? 
Considerando que o sistema tenha apenas esta regra de negócio, criando apenas o teste end to end já teriamos
uma qualidade provavelmente muito interessante.

## Decisão
Para testar o sistema a piramide será invertida (conhecido como casquinha de sorvete).

## Vantagens
Maior qualidade, quantidade de testes reduzida, maior estabilidade devido a testes end to end em uma api 
normalmente são mais estáveis, sendo assim a manutenção será melhor do que muitos testes de unidade e 
integração.

## Desvantagens
A programação seria diferente em relação a outros projetos que adotam o modelo de pirâmide.

## Evolução
A biblioteca de classe que facilita a criação de testes end to end já está bem validada em outros projetos, 
porém não foi trazida todas as suas funcionalidades para este projeto. Em outros projetos há casos de envio de 
blobs, comparação de relatórios por html e etc.