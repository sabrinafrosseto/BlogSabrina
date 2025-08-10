# Blog da Sabrina

Um blog pessoal desenvolvido em ASP.NET Core com foco em três áreas principais:
- **Ciências Sociais**: Explorando comportamentos humanos e dinâmicas sociais
- **Saúde Mental**: Bem-estar psicológico e emocional
- **Carreira**: Desenvolvimento profissional e crescimento

## 🚀 Funcionalidades

- ✅ Design responsivo e moderno
- ✅ Organização por categorias
- ✅ Sistema de tags
- ✅ Compartilhamento de posts
- ✅ Navegação intuitiva
- ✅ SEO-friendly URLs

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 8.0
- Bootstrap 5
- Bootstrap Icons
- HTML5/CSS3
- JavaScript

## 📦 Como Executar Localmente

1. Clone o repositório:
```bash
git clone <seu-repositorio>
cd BlogSabrina
```

2. Execute o projeto:
```bash
cd BlogSabrina
dotnet run
```

3. Acesse: `https://localhost:5001` ou `http://localhost:5000`

## 🌐 Opções de Hospedagem Gratuita

### 1. **Azure App Service (Recomendado)**
- **Vantagens**: Integração nativa com .NET, SSL gratuito, domínio personalizado
- **Limitações**: 60 minutos de CPU por dia no plano gratuito
- **Como fazer**:
  1. Crie uma conta no [Azure](https://azure.microsoft.com/free/)
  2. Use o Azure App Service com plano gratuito F1
  3. Deploy direto do GitHub/Visual Studio

### 2. **Railway**
- **Vantagens**: Deploy simples, suporte nativo ao .NET
- **Limitações**: $5 de crédito gratuito por mês
- **Como fazer**:
  1. Conecte seu GitHub ao [Railway](https://railway.app/)
  2. Selecione o repositório
  3. Deploy automático

### 3. **Render**
- **Vantagens**: SSL gratuito, deploy automático do GitHub
- **Limitações**: Aplicação "dorme" após inatividade
- **Como fazer**:
  1. Conecte seu GitHub ao [Render](https://render.com/)
  2. Crie um novo Web Service
  3. Configure o build command: `dotnet publish -c Release -o out`
  4. Configure o start command: `dotnet out/BlogSabrina.dll`

### 4. **Heroku** (com Docker)
- **Vantagens**: Plataforma madura, muitos add-ons
- **Limitações**: Requer containerização
- **Como fazer**:
  1. Crie um `Dockerfile` na raiz do projeto
  2. Use o Heroku CLI para deploy
  3. Configure as variáveis de ambiente

## 📝 Adicionando Novos Posts

Para adicionar novos posts, edite o arquivo `Controllers/BlogController.cs` e adicione novos objetos `BlogPost` na lista `_posts`.

Exemplo:
```csharp
new BlogPost
{
    Id = 4,
    Title = "Seu Novo Post",
    Summary = "Resumo do post...",
    Content = @"<p>Conteúdo HTML do post...</p>",
    Author = "Sabrina",
    PublishedDate = DateTime.Now,
    Category = "Categoria",
    Slug = "seu-novo-post",
    Tags = new List<string> { "tag1", "tag2" }
}
```

## 🎨 Personalização

### Cores das Categorias
- **Ciências Sociais**: `bg-primary` (azul)
- **Saúde Mental**: `bg-success` (verde)
- **Carreira**: `bg-warning` (amarelo)

### Adicionando Novas Categorias
1. Adicione na lista `_categories` no `BlogController`
2. Escolha um ícone do [Bootstrap Icons](https://icons.getbootstrap.com/)
3. Defina uma cor do Bootstrap

## 📱 Responsividade

O blog é totalmente responsivo e funciona bem em:
- 📱 Dispositivos móveis
- 📱 Tablets
- 💻 Desktops
- 🖥️ Telas grandes

## 🔧 Próximas Melhorias

- [ ] Sistema de comentários
- [ ] Busca por posts
- [ ] RSS Feed
- [ ] Sistema de administração
- [ ] Banco de dados (SQLite/PostgreSQL)
- [ ] Sistema de usuários

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

**Desenvolvido com ❤️ por Sabrina**