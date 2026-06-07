# OrbitalGuard — Sistema de Monitoramento Climático por Satélite

## Descrição

O OrbitalGuard é uma API REST desenvolvida em ASP.NET Core (.NET 8) que simula um sistema
de monitoramento climático e emissão de alertas de desastres naturais com base em leituras
coletadas por satélites espaciais. O sistema permite cadastrar satélites, regiões monitoradas,
registrar leituras climáticas e gerar alertas automáticos de eventos como enchentes, secas,
tempestades, tsunamis, entre outros.

---

## Integrantes do Grupo

| Nome | RM |
|---|---|
| Artur Alves Tenca | 555171 |
| Igor Brunelli Ralo | 555035 |
| João Pedro Signor Avelar | 558375 |
| Roger Cardoso Ferreira | 557230 |
| Victor Mattenhauer Lopes | 555753 |

**Turma:** 3ESPW \
**Professor:**  Rafael Santos Novo Pereira \
**Data de entrega:** 09/06/2026

---

## Motivação e Conexão com o Tema Espacial

Desastres naturais causam milhares de mortes e prejuízos bilionários anualmente.
Satélites de observação da Terra já são utilizados por agências como NASA e ESA para
monitorar padrões climáticos em tempo real — porém, o acesso a esses dados ainda é limitado para populações vulneráveis.

O OrbitalGuard simula essa infraestrutura espacial, conectando-se ao **ODS 13 (Ação
Climática)** e ao **ODS 11 (Cidades e Comunidades Sustentáveis)** da ONU, ao propor uma solução tecnológica para previsão e resposta rápida a desastres naturais com base
em dados orbitais.

---

## 🛠️ Tecnologias Utilizadas

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 8
- Oracle.EntityFrameworkCore 8.23.50
- Oracle Database
- Swagger / OpenAPI
- C# 12

---

## ▶️ Instruções de Execução

### Pré-requisitos
- Visual Studio 2022 ou superior
- .NET 8 SDK instalado
- Acesso ao banco Oracle

### Passo a passo

**1. Clone o repositório**
```bash
git clone https://github.com/seu-usuario/OrbitalGuard.git
cd OrbitalGuard
```

**2. Configure a connection string**

Abra o arquivo `appsettings.json` e preencha com suas credenciais Oracle:
```json
"ConnectionStrings": {
  "Oracle": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=SUA_CONEXAO"
}
```

**3. Aplique as migrations**

1. 
```bash
Add-Migration [NomeDaMigration]
```
2.
```bash
Update-Database
```

**4. Execute o projeto**

Pressione `F5` no Visual Studio ou rode:
```bash
dotnet run
```

**5. Acesse o Swagger e veja os Endpoints**

https://localhost:{porta}/swagger
