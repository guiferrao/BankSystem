using BankSystem.Repositories;
using BankSystem.Screens.AccountScreens;

namespace BankSystem.Screens.ClientScreens
{
    public static class MenuClientScreen
    {
        public static void Load()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("------------------");
                Console.WriteLine("|  Banco CSharp  |");
                Console.WriteLine("------------------");
                Console.WriteLine();
                Console.WriteLine("Gestão de Clientes");
                Console.WriteLine();
                Console.WriteLine("1 - Criar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Atualizar Clientes");
                Console.WriteLine("4 - Excluir Clientes");
                Console.WriteLine("5 - Acessar painel do Cliente");
                Console.WriteLine();
                Console.WriteLine("Aperte ESC para voltar");
                Console.WriteLine();

                var option = Console.ReadKey();

                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        CreateClientScreen.Load();
                        break;
                    case ConsoleKey.D2:
                        ListClientScreen.Load();
                        break;
                    case ConsoleKey.D3:
                        UpdateClientScreen.Load();
                        break;
                    case ConsoleKey.D4:
                        DeleteClientScreen.Load();
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine("Digite o documento (CPF/CNPJ) do cliente que deseja acessar: ");
                        var documento = Console.ReadLine();
                        var repoCliente = new ClienteRepository(Database.Connection);
                        var cliente = repoCliente.GetByDocumento(documento);
                        if(cliente != null)
                        {
                            AccountMenuScreens.Load(cliente.Id);
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado com esse documento");
                            Console.WriteLine("Pressione qualquer tecla para voltar");
                            Console.ReadKey();
                            Load();
                        }
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                    default: Load(); break;
                }
            }
        }
    }
}