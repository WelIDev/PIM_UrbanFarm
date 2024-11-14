using Dominio.Entidades;
using Dominio.Enums;

namespace Infraestrutura.Persistencia.DbInitializer;

public class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        SeedEnderecos(context);

        SeedClientes(context);

        SeedFornecedores(context);

        SeedVendedores(context);

        SeedProdutos(context);

        SeedHistoricoCompra(context);

        SeedMetas(context);
        
        SeedUsuarios(context);

        SeedAbastecimentosEstoque(context);

        SeedVendas(context);

        SeedComissoes(context);
    }

    private static void SeedEnderecos(AppDbContext context)
    {
        if (!context.Enderecos.Any())
        {
            context.Enderecos.AddRange(
                new Endereco
                {
                    Rua = "Rua das Hortas", Bairro = "Centro", Cidade = "São Paulo", Estado = "SP",
                    Cep = "01010-000", Numero = 101
                },
                new Endereco
                {
                    Rua = "Avenida Verde", Bairro = "Jardim Botânico", Cidade = "Rio de Janeiro",
                    Estado = "RJ", Cep = "22222-000", Numero = 202
                },
                new Endereco
                {
                    Rua = "Rua da Colheita", Bairro = "Vila Verde", Cidade = "Curitiba",
                    Estado = "PR", Cep = "80030-000", Numero = 303
                },
                new Endereco
                {
                    Rua = "Avenida Orgânica", Bairro = "Eco Bairro", Cidade = "Campinas",
                    Estado = "SP", Cep = "13040-000", Numero = 404
                },
                new Endereco
                {
                    Rua = "Rua dos Jardins", Bairro = "Tijuca", Cidade = "Rio de Janeiro",
                    Estado = "RJ", Cep = "20520-000", Numero = 505
                },
                new Endereco
                {
                    Rua = "Rua do Sabor", Bairro = "Zona Rural", Cidade = "São Paulo",
                    Estado = "SP", Cep = "01020-000", Numero = 601
                },
                new Endereco
                {
                    Rua = "Rua das Flores", Bairro = "Zona Sul", Cidade = "Rio de Janeiro",
                    Estado = "RJ", Cep = "22230-000", Numero = 702
                },
                new Endereco
                {
                    Rua = "Avenida da Sustentabilidade", Bairro = "Jardim Ecológico",
                    Cidade = "São Paulo", Estado = "SP", Cep = "01302-000", Numero = 803
                },
                new Endereco
                {
                    Rua = "Rua da Natureza", Bairro = "Cidade Jardim", Cidade = "São Paulo",
                    Estado = "SP", Cep = "01303-000", Numero = 904
                },
                new Endereco
                {
                    Rua = "Estrada Verde", Bairro = "Zona Rural", Cidade = "Campinas",
                    Estado = "SP", Cep = "13050-000", Numero = 1005
                },
                new Endereco
                {
                    Rua = "Rua da Lavoura", Bairro = "Centro", Cidade = "São Paulo", Estado = "SP",
                    Cep = "01301-000", Numero = 1101
                },
                new Endereco
                {
                    Rua = "Avenida Agroecológica", Bairro = "Campo Limpo",
                    Cidade = "Rio de Janeiro", Estado = "RJ", Cep = "22015-000", Numero = 1202
                },
                new Endereco
                {
                    Rua = "Rua do Plantio", Bairro = "Bairro Verde", Cidade = "São Paulo",
                    Estado = "SP", Cep = "02340-000", Numero = 1303
                },
                new Endereco
                {
                    Rua = "Rua do Cultivo", Bairro = "São Bernardo", Cidade = "São Paulo",
                    Estado = "SP", Cep = "02350-000", Numero = 1404
                },
                new Endereco
                {
                    Rua = "Avenida da Colheita", Bairro = "Zona Rural", Cidade = "Rio de Janeiro",
                    Estado = "RJ", Cep = "22020-000", Numero = 1505
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedClientes(AppDbContext context)
    {
        if (!context.Clientes.Any())
        {
            context.Clientes.AddRange(
                new Cliente
                {
                    Nome = "João Oliveira", Email = "joao.oliveira@gmail.com",
                    CpfCnpj = "123.456.789-00", Telefone = "(11) 99988-7777",
                    DataNascimento = new DateTime(1985, 3, 15), EnderecoId = 1
                },
                new Cliente
                {
                    Nome = "Maria Santos", Email = "maria.santos@yahoo.com",
                    CpfCnpj = "987.654.321-00", Telefone = "(21) 98877-6666",
                    DataNascimento = new DateTime(1990, 7, 22), EnderecoId = 2
                },
                new Cliente
                {
                    Nome = "Carlos Pereira", Email = "carlos.pereira@hotmail.com",
                    CpfCnpj = "112.233.445-56", Telefone = "(41) 97766-5555",
                    DataNascimento = new DateTime(1982, 12, 5), EnderecoId = 3
                },
                new Cliente
                {
                    Nome = "Ana Costa", Email = "ana.costa@uol.com.br", CpfCnpj = "223.344.556-77",
                    Telefone = "(19) 96655-4444", DataNascimento = new DateTime(1995, 1, 18),
                    EnderecoId = 4
                },
                new Cliente
                {
                    Nome = "Lucas Lima", Email = "lucas.lima@gmail.com", CpfCnpj = "334.455.667-88",
                    Telefone = "(21) 95544-3333", DataNascimento = new DateTime(1998, 8, 25),
                    EnderecoId = 5
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedVendedores(AppDbContext context)
    {
        if (!context.Vendedores.Any())
        {
            context.Vendedores.AddRange(
                new Vendedor
                {
                    Nome = "Carlos Souza", Salario = 3000.00, CpfCnpj = "111.222.333-44",
                    Telefone = "(11) 98765-4321", DataContratacao = new DateTime(2020, 5, 12),
                    EnderecoId = 11
                },
                new Vendedor
                {
                    Nome = "Ana Lima", Salario = 3500.00, CpfCnpj = "555.666.777-88",
                    Telefone = "(21) 88877-6655", DataContratacao = new DateTime(2021, 6, 25),
                    EnderecoId = 12
                },
                new Vendedor
                {
                    Nome = "Felipe Almeida", Salario = 3200.00, CpfCnpj = "999.888.777-66",
                    Telefone = "(41) 98877-6677", DataContratacao = new DateTime(2019, 2, 10),
                    EnderecoId = 13
                },
                new Vendedor
                {
                    Nome = "Juliana Silva", Salario = 2800.00, CpfCnpj = "888.777.666-55",
                    Telefone = "(19) 97766-5555", DataContratacao = new DateTime(2022, 8, 15),
                    EnderecoId = 14
                },
                new Vendedor
                {
                    Nome = "Roberto Costa", Salario = 3100.00, CpfCnpj = "777.666.555-44",
                    Telefone = "(11) 97766-4433", DataContratacao = new DateTime(2023, 1, 5),
                    EnderecoId = 15
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedFornecedores(AppDbContext context)
    {
        if (!context.Fornecedores.Any())
        {
            context.Fornecedores.AddRange(
                new Fornecedor
                {
                    Nome = "Fazenda Verde", Email = "contato@fazendaverde.com",
                    Cnpj = "12.345.678/0001-99", InscricaoEstadual = "123456789",
                    Telefone = "(11) 99123-4567", EnderecoId = 6
                },
                new Fornecedor
                {
                    Nome = "Fazenda Orgânica", Email = "contato@fazendaorganica.com",
                    Cnpj = "98.765.432/0001-88", InscricaoEstadual = "987654321",
                    Telefone = "(21) 99876-5432", EnderecoId = 7
                },
                new Fornecedor
                {
                    Nome = "Horta Urbana", Email = "vendas@hortaurbana.com",
                    Cnpj = "11.223.344/0001-55", InscricaoEstadual = "112233445",
                    Telefone = "(41) 96123-4567", EnderecoId = 8
                },
                new Fornecedor
                {
                    Nome = "Eco Farma", Email = "contato@ecofarma.com", Cnpj = "22.334.455/0001-66",
                    InscricaoEstadual = "223344556", Telefone = "(19) 99123-9876", EnderecoId = 9
                },
                new Fornecedor
                {
                    Nome = "Fazenda Sustentável", Email = "contato@fazendasustentavel.com",
                    Cnpj = "33.445.566/0001-77", InscricaoEstadual = "334455667",
                    Telefone = "(21) 98765-4321", EnderecoId = 10
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedProdutos(AppDbContext context)
    {
        if (!context.Produtos.Any())
        {
            context.Produtos.AddRange(
                new Produto
                {
                    Nome = "Alface Orgânica", Preco = 10.50, Estoque = 200,
                    Descricao = "Alface orgânica cultivada sem agrotóxicos"
                },
                new Produto
                {
                    Nome = "Tomate Cereja", Preco = 8.75, Estoque = 150,
                    Descricao = "Tomate cereja de produção local"
                },
                new Produto
                {
                    Nome = "Rúcula Fresca", Preco = 12.30, Estoque = 180,
                    Descricao = "Rúcula orgânica, fresca e saborosa"
                },
                new Produto
                {
                    Nome = "Cenoura Orgânica", Preco = 6.40, Estoque = 220,
                    Descricao = "Cenouras frescas e orgânicas, direto do campo"
                },
                new Produto
                {
                    Nome = "Espinafre", Preco = 9.00, Estoque = 160,
                    Descricao = "Espinafre orgânico, ideal para saladas"
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedHistoricoCompra(AppDbContext context)
    {
        if (!context.HistoricoCompras.Any())
        {
            context.HistoricoCompras.AddRange(
                new HistoricoCompra { ClienteId = 1 },
                new HistoricoCompra { ClienteId = 2 },
                new HistoricoCompra { ClienteId = 3 },
                new HistoricoCompra { ClienteId = 4 },
                new HistoricoCompra { ClienteId = 5 }
            );
            context.SaveChanges();
        }
    }

    private static void SeedMetas(AppDbContext context)
    {
        if (!context.Metas.Any())
        {
            context.Metas.AddRange(
                new Meta
                {
                    Descricao = "Meta de vendas mensal", Valor = 5000.00, Periodo = 1,
                    VendedorId = 1
                },
                new Meta
                {
                    Descricao = "Meta de vendas trimestral", Valor = 15000.00, Periodo = 3,
                    VendedorId = 2
                },
                new Meta
                {
                    Descricao = "Meta de vendas anual", Valor = 60000.00, Periodo = 12,
                    VendedorId = 3
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedAbastecimentosEstoque(AppDbContext context)
    {
        if (!context.AbastecimentosEstoque.Any())
        {
            context.AbastecimentosEstoque.AddRange(
                new AbastecimentoEstoque
                {
                    ProdutoId = 1, Quantidade = 100, FornecedorId = 1,
                    UsuarioId = 1, Observacoes = "Abastecimento de alface orgânica"
                },
                new AbastecimentoEstoque
                {
                    ProdutoId = 2, Quantidade = 200, FornecedorId = 2,
                    UsuarioId = 1, Observacoes = "Tomates cereja frescos"
                }
            );
            context.SaveChanges();
        }
    }

    private static void SeedVendas(AppDbContext context)
    {
        if (!context.Vendas.Any())
        {
            var random = new Random();

            var clientes = context.Clientes.ToList();
            var vendedores = context.Vendedores.ToList();
            var produtos = context.Produtos.ToList();
            var historicosCompra = context.HistoricoCompras.ToList();

            var vendas = new List<Venda>();
            var vendaProdutos = new List<VendaProduto>();

            for (var i = 0; i < 1000; i++)
            {
                var cliente = clientes[random.Next(clientes.Count)];
                var historicoCompra =
                    historicosCompra.FirstOrDefault(h => h.ClienteId == cliente.Id);

                var vendedor = vendedores[random.Next(vendedores.Count)];

                var produtosVenda = new List<Produto>();
                var numeroProdutos = random.Next(1, 20);
                for (var j = 0; j < numeroProdutos; j++)
                {
                    var produto = produtos[random.Next(produtos.Count)];
                    if (!produtosVenda.Contains(produto))
                    {
                        produtosVenda.Add(produto);
                    }
                }

                var formaDePagamento = (FormaDePagamento)random.Next(0,
                    Enum.GetValues(typeof(FormaDePagamento)).Length);

                var dataInicial = new DateTime(2020, 1, 1);
                var dataFinal = new DateTime(2024, 11, 29);
                var faixa = dataFinal - dataInicial;

                var randomDays = random.Next((int)faixa.TotalDays);
                var randomDate = dataInicial.AddDays(randomDays);

                var venda = new Venda
                {
                    Data = randomDate,
                    FormaDePagamento = formaDePagamento,
                    VendedorId = vendedor.Id,
                    HistoricoCompraId = historicoCompra.Id
                };

                vendas.Add(venda);

                foreach (var produto in produtosVenda)
                {
                    var quantidade = random.Next(1, 20);
                    var valorTotal = produto.Preco * quantidade;
                    var vendaProduto = new VendaProduto
                    {
                        ProdutoId = produto.Id,
                        Quantidade = quantidade,
                        ValorTotal = valorTotal,
                    };

                    if (!context.VendaProduto.Any(vp => vp.VendaId == venda.Id && vp.ProdutoId == produto.Id))
                    {
                        vendaProduto.VendaId = venda.Id;
                        vendaProdutos.Add(vendaProduto);
                    }
                }
            }

            context.Vendas.AddRange(vendas);
            context.SaveChanges();
            
            context.VendaProduto.AddRange(vendaProdutos);
            context.SaveChanges();
        }
    }

    private static void SeedComissoes(AppDbContext context)
    {
        if (!context.Comissoes.Any())
        {
            context.Comissoes.AddRange(
                new Comissao { Valor = 500.00, MetaId = 1 },
                new Comissao { Valor = 1500.00, MetaId = 2 }
            );
            context.SaveChanges();
        }
    }

    private static void SeedUsuarios(AppDbContext context)
    {
        if (!context.Usuarios.Any())
        {
            context.Usuarios.AddRange(
                new Usuario
                {
                    Nome = "Admin", Email = "admin@admin.com", Senha = "admin123",
                    Funcao = Funcao.Admin
                },
                new Usuario
                {
                    Nome = "Usuario1", Email = "usuario1@usuario.com", Senha = "senha123",
                    Funcao = Funcao.Vendedor
                }
            );
            context.SaveChanges();
        }
    }
}
