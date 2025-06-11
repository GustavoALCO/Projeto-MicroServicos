Projeto de Microserviços baseado em Clean Architecture
=

Este projeto tem como objetivo criar uma aplicação baseada em Clean Architecture para um sistema de microserviços, separando cada parte do código em serviços independentes. A proposta visa tornar o sistema mais escalável, de fácil manutenção e preparado para evoluções futuras.

Além disso, este projeto serve como base para aplicar essa abordagem em futuros desenvolvimentos, reforçando boas práticas de arquitetura e organização de código.

Soluções
=
O projeto será organizado onde cada solucao tera um arquivo docker e um DBA proprio sendo usado o PostGreSQL

- Produtos
   =
	A solução de produtos será responsável por toda a lógica de criação, organização e filtros dos produtos.

- Utiliza o Azure Blob Storage para o armazenamento de imagens associadas aos produtos.

- Aplica validação de dados com FluentValidation, garantindo a integridade das informações inseridas.
  
- Autenticação
  =
  A solução de autenticação gerencia os logins de usuários e funcionários, promovendo uma organização mais segura e segmentada de acessos no sistema.
  
- **Baseada nas bibliotecas:**

	- Microsoft.AspNetCore.Identity

	- Microsoft.IdentityModel.Tokens

	- System.IdentityModel.Tokens.Jwt

	- Responsável por gerar e validar tokens JWT.

	- Permite controle de acesso por usuário e/ou perfil (role).
 

- Pagamento
  =
	A solução de pagamento será responsável pela comunicação com a API do Mercado Pago, permitindo o processamento de transações financeiras com segurança e confiabilidade.

- Realiza a integração direta com o sistema de pagamentos do Mercado Pago.

 - Armazena os dados da transação (status de pagamento e produtos do carrinho) em um banco de dados isolado, garantindo separação de responsabilidades e segurança.

- Implementada com as bibliotecas oficiais do Mercado Pago

- Emails
  =
  A solução de envio de e-mails é responsável por centralizar e gerenciar o disparo de mensagens eletrônicas em diversos contextos do sistema, como:

	- Confirmação de cadastro

	 - Recuperação de senha

	- Notificações administrativas e operacionais

	- Essa solução foi implementada como um microserviço independente, promovendo reutilização, escalabilidade e separação de responsabilidades.

	- Tecnologias e recursos utilizados:
		- MailKit: biblioteca robusta e moderna para envio de e-mails via protocolo SMTP.
