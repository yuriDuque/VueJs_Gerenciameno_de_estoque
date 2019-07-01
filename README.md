# VueJs_Gerenciameno_de_estoque

Aplicação de Gerenciamento de produtos e pedidos.

Gerar banco de dados


# Inicializando projeto

Comandos necessários inicialização do projeto:

Abir o projeto 'WebApi.sln' no VS 2017 e inserir esses comandos no 'Package Manager Console':  
  
  > add-migration GerenciamentoProdutosDb
	
  > update-database –verbose
  
Abir a pasta 'frontend' no VS code e inserir esses comandos no Terminal (CTRL+"):

  > npm install
	
  > npm run serve


# Arquitetura do projeto:

Backend (WebApi):
	
	> Repository: fazem as funções CRUD no banco.
	
	> Service: executam as regras de negocio e fazem a comunicação com o frontend, por JSON.
  
  > Controller: faz a comunicação com o frontend, por JSON.


Frontend (Front):
	
	> Components: contem os components html.
	
	> Pages: contem as paginas do projeto criadas em Vue js e suas funções em javascript.
	
	> Service: contem os service com as funções que utilizam o Axios para se comunicar com o backend.
		
	
# Ferramentas adicionais usadas

eslint:

	> pacote do npm, utilizado para formatar o código no padrão 'AiBNB styleguide'
	> documentação: https://www.npmjs.com/package/eslint
	
vue-bootstrap:

	> pacote do npm, disponibiliza components css para facilitar a criação do layout do site.
	> documentação: https://bootstrap-vue.js.org/
	
axios:

	> pacote do np, utilizado para realizar as requisições em promise para o backend
	> documentação: https://www.npmjs.com/package/axios  
