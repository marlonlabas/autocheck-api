# AutoCheck.ConsoleApp — Motor de Vistoria Veicular

## Objetivo do sistema

Trata-se da atualização de uma engine em C# que recebe a inspeção técnica de um veículo, aplica as regras do Checklist de Vistoria, calcula a pontuação final de aproveitamento, classifica a situação do veículo e emite um relatório detalhado com os serviços recomendados para a oficina.

## Como executar

Pré-requisitos: [.NET SDK 8.0](https://dotnet.microsoft.com/download) ou superior instalado.

```bash
# Clone o repositório
git clone https://github.com/marlonlabas/autocheck-api.git

# Entre na pasta do projeto
cd autocheck-dotnet/src/AutoCheck.Domain

# Execute a aplicação
dotnet run
```

Ao rodar, o menu principal será exibido no terminal com as opções de cadastrar uma nova vistoria (1), ver o relatório das vistorias já realizadas (2) ou sair (0).

## Regras de negócio

**Pontuação por item.** Cada item do checklist recebe uma nota conforme o status avaliado pelo técnico:

| Status  | Pontos |
|---------|--------|
| Bom     | 10     |
| Regular | 5      |
| Ruim    | 0      |
| Ausente | 0      |

**Percentual de aprovação.** Calculado como (pontuação obtida ÷ pontuação máxima possível) × 100, onde a pontuação máxima é sempre `quantidade de itens × 10`.


**Classificação final:**

| Percentual | Classificação              |
|------------|----------------------------|
| 90% – 100% | Aprovado com Excelência    |
| 60% – 89%  | Aprovado com Apontamentos  |
| 0% – 59%   | Reprovado na Vistoria      |

## Estrutura do projeto

```
autocheck-dotnet/
├── src/
│   └── AutoCheck.ConsoleApp/
│       ├── Program.cs
│       ├── Models/
│       │   ├── ItemVistoria.cs
│       │   ├── Veiculo.cs
│       │   ├── Carro.cs
│       │   ├── Moto.cs
│       │   └── Caminhao.cs
│       ├── Services/
│       │   └── MotorVistoria.cs
│       └── AutoCheck.ConsoleApp.csproj
├── README.md
```

## Conceitos de POO aplicados

**Encapsulamento**

- Na classe ItemVistoria o campo real (_status) é private, inacessível de fora da classe. A única porta de entrada é a propriedade Status, cujo set valida o valor antes de aceitar (só permite "Bom", "Regular", "Ruim" ou "Ausente"). Isso impede que qualquer parte do programa deixe um ItemVistoria num estado inválido — a classe protege sua própria integridade.
- Na classe Veiculo o método AdicionarItemVistoriado() também é uma forma de evitar que o código externo manipule a lista de itens diretamente. A classe oferece um método específico que encapsula esse processo — cria o ItemVistoria e adiciona à lista internamente. Isso mantém a responsabilidade de adicionar os itens dentro da própria classe Veiculo.

**Herança**

- Nesse sistema as classes "Carro, Moto e Caminhao" herdaram as propriedades (Marca, Modelo, Ano, Quilometragem e VistoriaRealizada) e o método AdicionarItemVistoriado() da classe "Veiculo". Desta forma, aquelas classes puderam reaproveitar parte da estrutura evitando reescrever esses trechos do código da classe "pai" Veiculo.

**Polimorfismo**

- O polimorfismo foi utilizado nos métodos ObterChecklistObrigatorio(), ObterTipoDescricao() e ObterAtributoEspecifico() da classe Veiculo. Nesta classe esses métodos estão marcados como "virtual", já nas classes Carro, Moto e Caminhao eles aparecem marcados como "override". Desta forma, o C# decide, em tempo de execução, qual versão desses métodos rodar de acordo com o "veículo real", não com o tipo declarado da variável. Isso só é possível porque esses métodos foram marcados "virtual" na classe base e "override" nas subclasses. 
- No sistema, o método ObterChecklistObrigatorio() é sobrescrito em cada classe reaproveitando a lista genérica via base.ObterChecklistObrigatorio() e adicionando sua lista específica de itens de checklist.
- Da mesma forma, o método ObterTipoDescricao() retorna o tipo de veículo ("Carro", "Moto", "Caminhão") e o método ObterAtributoEspecifico() retorna o(s) atributo(s) específico(s) ("4 Portas","150 cc", "3 Eixos | Cap. Carga: 30,0 Toneladas").
- Em Program.cs no método RealizarNovaVistoria() também foi usado o polimorfirmo ao declarar "veiculoNovo" como "Veiculo" e logo na sequência fazê-lo receber "Carro, Moto ou Caminhao" de acordo com a escolha do usuário no menu. Sem polimorfismo, toda vez que o sistema precisasse do checklist ou dos dados de exibição, teria que existir um código que verificasse qual tipo de veículo. Isso dificultaria a manutenção do código no caso de adição de novos tipos de veículos (ônibus, motorhome, etc.).

**Construtores**

- Todo os objetos do sistema — item de vistoria ou veículo de qualquer tipo — nascem através de um construtor que recebe os dados obrigatórios e garante que os objetos já comecem num estado consistente, e as subclasses reaproveitam o construtor da classe base em vez de duplicar essa lógica.

**Coleções**

Coleções são estruturas de dados que guardam múltiplos valores agrupados, permitindo percorrer, adicionar e remover itens sem precisar de uma variável separada pra cada elemento. As coleções utilizadas no projeto:
- List < ItemVistoria > - que guarda todos os itens já avaliados naquele veículo específico.
- List < Veiculo > - guarda todas as vistorias já cadastradas no menu, uma por veículo processado.
- List < string > - a lista de nomes dos itens obrigatórios de cada tipo de veículo.

## Arquitetura

Este é um sistema que não precisa de internet: toda a lógica (entrada de dados, cálculo e exibição) roda em um único processo de console, sem separação cliente-servidor. A separação que existe é interna, entre `Models` (dados), `Services` (regras de negócio) e `Program.cs` (interface com o usuário).

## Vídeo de apresentação

https://drive.google.com/file/d/13f2PfKH5zH05jAEVoaqaNJTB0jdpebf6/view?usp=sharing