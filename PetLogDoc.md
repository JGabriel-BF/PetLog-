# **PetLog**
• Desenvolvimento de projeto acadêmico, com boas práticas de programação, utilizando arquitetura CleanCode

### **Introdução**
•  PetLog tem como objetivo ser um sistema que visa ajudar a questão Logística de estoque para pequenos empresários do ramo de rações e produtos para pets em geral. Minha motivação por trás desse projeto é utilizar como molde para poder desenvolver aplicações futuras de pequena e média escala.

### **Objetivos**: 
• Conseguir gerenciar o estoque de produtos de forma mais simples e intuitiva possível para o usuário final do sistema.  
• Ter Controle total sobre o estoque, clientes e pedidos.         
• Ter uma visão clara e simplista sobre o estoque.

### **Escopo**: 
• PetLog foi desenvolvida pensando em abranger micro e pequenas empresas.

### **Visão Geral do Sistema**
• O projeto segue os princípios da **Clean Architecture**, separando as responsabilidades em camadas, para melhor funcionamento, ser mais testável e ser fácil de manter.
• Funcionalidades: Descreva as funcionalidades que serão desenvolvidas no projeto.
• Possibilidade de Criar Clientes para denominar para quem vai a compra.
• Possibilidade de Criar Produtos, dentro do escopo permitido.
• Cadastrar Vendedores para catalogar cada venda feita.
• Incluí uma I.A Groq que possibilita a geração de um relatório, que ao selecionar o ID do cliente, faz uma breve previsão de compra de acordo com o histórico de compra do cliente.


Configuração do Ambiente
• Requisitos de Software e Hardware: Lista de requisitos necessários para rodar o projeto.
• Sistema operacional: Windows 10 + 
• Placa mãe: Asus B550M +
• Memória Ram: 2GB ddr3 +

•Software
• VisualStudioCode
• C#
• .net8
• EFCore 
• Dapper
• SqlServer
• React 
• Git

• Instruções de Instalação: Passos detalhados para instalar o software necessário.
• Baixe o Visual Studio Code;
• Baixe a biblioteca C#
• Baixe o .net 8 
• Baixe o a biblioteca REACT

• Configuração do Ambiente de Desenvolvimento: Instruções para configurar o ambiente de desenvolvimento.
• Insira sua connection string no appsettings.development.json, na pasta API
• Dê CD para ir a pasta PetLog-API
• Dê o comando dotnet run --project .\src\PetLog.Api para iniciar e conseguir seu localhost
• Abra um Novo Terminal
• Dê CD para ir a pasta PetLog-APP
• Rode npm start

• Dependências: Lista de todas as dependências do projeto.
• Ter Baixado os Softwares apresentados acima.
• Ter as bibliotecas: EFCore, .NET 8, Axios, Dapper, React

Desenvolvimento
• Estrutura do Projeto: Descrição da estrutura do projeto e suas pastas principais.
• A estrutura utilizada foi feita utilizando a arquitetura CleanCode, onde suas principais pastas foram divididas dentro de uma solution, garantindo a independencia de cada bloco na hora da manutenção.

• Descrição das Camadas: Detalhes das diferentes camadas do projeto (apresentação, domínio, repositório,
serviços).
• API - Camada onde acontece a comunicação entre BackEnd e FrontEnd, onde é encaminhado para o Front os métodos HTTP.
• APLICACAO - Camada onde a lógica por trás dos métodos HTTP e as funções são aplicadas.
• SERVICES - Camada onde é feito o uso e chamada de uma API esterna, tal qual a utilizada nesse projeto, Groq.
• REPOSITÓRIO - Camada que é feita a representação das entidades criadas na camada de Domínio, no banco de dados, como SQLServer, utilizado nesse projeto
• DOMÍNIO - Camada onde é feito o mapeamento das entidades e seus devidos atributos.

API
• Documentação da API: Descrição detalhada dos endpoints da API, métodos HTTP, parâmetros e exemplos
de requisição e resposta. (Swagger)

Interface do Usuário
• Descrição das Funcionalidades da Interface: Explicação das principais funcionalidades da interface do
usuário.

Banco de Dados
• Diagrama de Entidade-Relacionamento (ERD): Diagrama ERD do banco de dados.

Considerações Finais
• Lições Aprendidas: Reflexões sobre o desenvolvimento do projeto.
• Melhores Práticas: Melhores práticas adotadas e recomendadas.
• Próximos Passos: Sugestões para futuras melhorias e desenvolvimentos.
Anexos
• Referências e Recursos Adicionais: Links e materiais adicionais relevantes.
• Links Úteis: Links úteis para documentação, ferramentas etc.
• Créditos e Agradecimentos: Reconhecimento das contribuições de membros da equipe e terceiros.
