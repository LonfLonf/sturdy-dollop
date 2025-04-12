
# Sobre o Projeto
A ideia por trás deste projeto surgiu da capacidade natural dos seres humanos de identificar padrões. Trabalhar com frases aleatórias e incentivar a repetição permite que, com o tempo, o usuário perceba padrões tanto nas construções das frases quanto nas palavras usadas, fortalecendo assim sua compreensão do idioma e enriquecendo seu vocabulário.

Olhando para o futuro, há diversas melhorias que podem ser implementadas. Entre elas, aprimorar o sistema de tradução, adicionar mais frases e palavras, melhorar a interface (já que front-end não é meu forte), desenvolver um recurso envolvendo música e, quem sabe, no futuro, se houver recursos, integrar inteligência artificial para conversação em tempo real.

# Escolha das Tecnologias

Desde o início, optei por manter o projeto simples e utilizar ferramentas com as quais já estava familiarizado. Escolhi Blazor e todo o ecossistema .NET, pois oferecem uma estrutura robusta e produtiva para desenvolvimento. Embora pudesse ter explorado outras linguagens, C# sempre foi a opção mais intuitiva para mim.

Inicialmente, decidi trabalhar com SQL Server, mas teve tanto problema que troquei, percebi que Postgres se encaixava melhor nas necessidades do projeto. A migração exigiu ajustes, mas trouxe benefícios que facilitaram a evolução do sistema. 

# Escolha dos Algoritmos

Durante o desenvolvimento, explorei diversos algoritmos para melhorar a experiência do usuário. Inicialmente, implementei Cosine Similarity para medir a semelhança entre textos, mas à medida que o projeto evoluiu, percebi que essa abordagem não se encaixava tão bem na nova direção que tomei.

Acabei optando pelo Flesch Reading Ease, um algoritmo que avalia a legibilidade de um texto com base na estrutura das frases e no vocabulário utilizado. Aprendi sobre ele no curso CS50 e percebi que seria uma escolha mais alinhada com o propósito atual do projeto. Essa mudança me permitiu ajustar melhor a dificuldade dos desafios apresentados aos usuários e aprimorar a dinâmica de aprendizado dentro da aplicação.

# Experiencia

Este projeto tomou rumos que eu nunca imaginei no início. O que começou como algo simples evoluiu para uma jornada de aprendizado intensa, onde explorei conceitos como cache para otimizar recursos, uso de DTOs e records para evitar dados desnecessários, além de diversas melhorias que ainda podem ser feitas no código. Apesar disso, decidi colocá-lo em produção para aprender, na prática, como resolver problemas reais.

Inicialmente, minha ideia era desenvolver um sistema de sincronização de letras de músicas em tempo real. Após pesquisar várias APIs sem encontrar nenhuma que atendesse às minhas necessidades, mudei de abordagem e comecei a trabalhar com um CSV contendo palavras em inglês, organizadas do nível mais fácil ao mais difícil. Com o tempo, decidi expandir o projeto para incluir frases que desafiam o usuário a adivinhar o significado, e esse tem sido o foco desde então.

A jornada foi cheia de desafios, especialmente na configuração do banco de dados e no deploy. Para manter o projeto funcionando sem custos, precisei improvisar algumas soluções. Apesar das dificuldades, essa experiência me ensinou muito sobre desenvolvimento, otimização e resolução de problemas no ambiente de produção.

# Notações Aleatorias

Agora que fui ver que tive meu primeiro Leak de informação, estou me sentindo um completo de um idiota, mas mesmo assim vou deixar ali e ver no que isso pode me causar, se for algo de pessimo eu resolvo.

4:33AM -> Mentira que falei, acabei resolvendo porque deu problema no banco de dados, deu algum problema na credencial e não conectava de jeito nenhum :)

4:55AM -> Tem algumas coisas que não estão me agradando como o delay que está tendo, to pensando em dividir o banco e ir carregando com o tempo e deixar em cache, igual está na parte de palavras! Vou procurar outras soluções para isso
