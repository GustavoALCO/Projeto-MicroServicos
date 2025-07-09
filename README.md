# 🧱 Projeto de Microsserviços — Plataforma de Anúncios

Este projeto é uma arquitetura baseada em microsserviços voltada para uma plataforma de anúncios (ex. OLX, Facebook Marktplace), com autenticação, gerenciamento de anúncios, comunicação entre usuários, pagamentos, notificações por e-mail e sistema de avaliações.

---

## 📌 1 — Autenticação & Gerenciamento de Usuários  
**Microserviço:** `Auth-Service`

**Responsável por:**
- Registro de usuários
- Login com JWT
- Recuperação de senha
- Confirmação de e-mail
- Verificação de roles (`admin`, `user`)
- Gerenciamento de perfis
- Bloqueio/desbloqueio de contas

**Tabelas principais:**
- `User`
- `UserRoles`
---

## 📌 2 — Gerenciamento de Anúncios  
**Microserviço:** `Ads-Service`

**Responsável por:**
- CRUD de anúncios (produtos)
- Upload de imagens (via S3, Cloudinary, etc.)
- Validação do proprietário do anúncio
- Busca e filtros por categoria, preço, localização
- Contador de visualizações

**Tabelas principais:**
- `Ad` ou `Product`

---

## 📌 3 — Chat Interno  
**Microserviço:** `Chat-Service`

**Responsável por:**
- Criação de salas de chat entre usuários
- Envio e recebimento de mensagens
- Histórico de mensagens
- Notificações de mensagens não lidas

**Tabelas principais:**
- `ChatRoom` 
- `Message`

---

## 📌 4 — Sistema de E-mails  
**Microserviço:** `Email-Service`

**Responsável por:**
- Envio de confirmação de cadastro
- Notificações sobre status de anúncios
- Alertas de mensagens não lidas
- Recuperação de senha

> 💡 Este serviço é idealmente desacoplado, consumindo mensagens de fila (ex: RabbitMQ, Kafka) originadas pelos serviços de `Auth` e `Ads`.

---

## 📌 5 — Sistema de Pagamentos  
**Microserviço:** `Payment-Service`

**Responsável por:**
- Processamento de checkout seguro
- Integração com gateway de pagamento (ex: Stripe, Mercado Pago)
- Gerenciamento de status do pagamento (pendente, pago, reembolsado)
- Split de pagamentos para comissões da plataforma
- Controle de saldo do vendedor
- Webhooks para atualização de status em tempo real

**Tabelas principais:**
- `Order`
- `Payment`
---

## 📌 6 — Ranqueamento & Avaliações  
**Microserviço:** `Review-Service`

**Responsável por:**
- Mostrar quantidades de acessos 
- Avaliações de vendedores por compradores
- Cálculo de média das avaliações
- Curtidas e favoritos em anúncios
- Contador de visualizações e likes
- Histórico de reputação do usuário

**Tabelas principais:**
- `Review`
- `UserRating`


---

## 🛠️ Tecnologias 
- .NET 8 (Web API)
- PostgreSQL (persistência)
- RabbitMQ (mensageria)
- Redis (cache e filas temporárias)
- Docker & Docker Compose
- JWT (auth)
- Swagger (documentação)

---

## 📁 Organização por pastas
Cada microserviço está isolado em sua própria pasta.

---

