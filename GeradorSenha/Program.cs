using System;
using System.IO;
using System.Text;

class SecurePasswordGenerator
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Gerador de Senhas Seguras ===");
        Console.ResetColor();
        
        Console.Write("Informe o tamanho da senha desejada: ");
        int tamanho;
        while (!int.TryParse(Console.ReadLine(), out tamanho) || tamanho <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Por favor, insira um número válido maior que zero: ");
            Console.ResetColor();
        }

        Console.Write("Deseja incluir letras e caracteres especiais? (S/N): ");
        string opcao = Console.ReadLine().Trim().ToUpper();

        bool incluirEspeciais = opcao == "S";

        Console.ForegroundColor = ConsoleColor.Green;
        string senhaGerada = GerarSenha(tamanho, incluirEspeciais);
        Console.WriteLine($"Senha gerada: {senhaGerada}");
        Console.ResetColor();

        SalvarSenha(senhaGerada);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Senha salva no arquivo 'bkp.TXT'.");
        Console.ResetColor();

        Console.WriteLine("Deseja visualizar todas as senhas salvas? (S/N): ");

        if (Console.ReadLine().Trim().ToUpper() == "S")
        {
            ExibirSenhasSalvas();
        }
    }

    static string GerarSenha(int tamanho, bool incluirEspeciais)
    {
        const string numeros = "0123456789";
        const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string especiais = "@!#-_*";

        string caracteres = numeros;
        if (incluirEspeciais)
        {
            caracteres += letras + especiais;
        }

        StringBuilder senha = new StringBuilder();
        Random rnd = new Random();

        for (int i = 0; i < tamanho; i++)
        {
            senha.Append(caracteres[rnd.Next(caracteres.Length)]);
        }

        return senha.ToString();
    }

    static void SalvarSenha(string senha)
    {
        string caminhoArquivo = "bkp.TXT";

        using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
        {
            sw.WriteLine(senha);
        }
    }

    static void ExibirSenhasSalvas()
    {
        string caminhoArquivo = "bkp.TXT";

        if (File.Exists(caminhoArquivo))
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== Senhas Salvas ===");
            Console.ResetColor();
            string[] senhas = File.ReadAllLines(caminhoArquivo);

            foreach (string senha in senhas)
            {
                Console.WriteLine(senha);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nenhuma senha foi salva até agora.");
            Console.ResetColor();
        }
    }
}