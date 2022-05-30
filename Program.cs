using System.Globalization;
using System;


string tipoJogo;


string jogadorUm = "";
string jogadorDois = "";

string tipoNavio = "";
string coordNavio = "";

string[,] tabuleiro = new string[10, 10];
string[,] tabuleiro2 = new string[10, 10];
string[,] tabuleiroResposta = new string[10, 10];
string[,] tabuleiroResposta2 = new string[10, 10];

List<string> ataqueJogadorUm = new List<string>();
List<string> ataqueJogadorDois = new List<string>();

string ataque;

int portaAviao = 1;
int navioTanque = 2;
int destroyer = 3;
int submarino = 4;

int[] posicaoNavio = new int[4];

int[] ataqueConvertido = new int[2];

bool vezJogadorUm = true;
bool vezJogadorDois = true;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Bem-vindo ao jogo Batalha Naval!");
Console.WriteLine("Por favor selecione uma opção:");
Console.WriteLine("1 - Player 1 vs IA");
Console.WriteLine("2 - Player 1 vs Player 2");
Console.ResetColor();


//Lê qual o tipo de jogo escolhido
do
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("=>");
    tipoJogo = Console.ReadLine().Trim();
    if (String.IsNullOrWhiteSpace(tipoJogo) || (tipoJogo != "1") && (tipoJogo != "2"))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Por favor insira um valor válido '1 ou 2'");
    }
}
while (String.IsNullOrWhiteSpace(tipoJogo) || (tipoJogo != "1") && (tipoJogo != "2"));


switch (tipoJogo)
{
    case "1":
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("================================ ALERTA ================================");
        Console.WriteLine("= Função em manutenção, desculpe o transtorno, agradeço a compreensão! =");
        Console.WriteLine("========================================================================");
        Console.ResetColor();
        break;
}

//Coleta o nome dos jogadores
if (tipoJogo == "2")
{
    while (string.IsNullOrEmpty(jogadorUm))
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Escreva seu nome Jogador 1");
        Console.Write("=>");
        jogadorUm = Console.ReadLine().Trim();
        Console.Clear();
        if (string.IsNullOrWhiteSpace(jogadorUm))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Seu nome não pode ser vazio! Aperte Enter e tente novamente!");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            continue;
        }
    }
    while (string.IsNullOrEmpty(jogadorDois))
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Escreva seu nome Jogador 2");
        Console.Write("=>");
        jogadorDois = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(jogadorDois))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Seu nome não pode ser vazio! Aperte Enter e tente novamente!");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            continue;
        }
    }
    jogadorUm = PrimeiraLetraMaiuscula(jogadorUm);
    jogadorDois = PrimeiraLetraMaiuscula(jogadorDois);
}
//Jogador1 posiciona seus navios
if (tipoJogo == "2")
{
    while (portaAviao > 0 || navioTanque > 0 || destroyer > 0 || submarino > 0)
    {


        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hora de posicionar os navios");
        Console.WriteLine($"{jogadorUm} por favor selecione o código do navio a ser posicionado");
        Console.WriteLine($"0{portaAviao}x | PS - Porta-Aviões | (5 quadrantes)");
        Console.WriteLine($"0{navioTanque}x | NT - Navio-Tanque |(4 quadrantes)");
        Console.WriteLine($"0{destroyer}x | DS - Destroyers   | (3 quadrantes)");
        Console.WriteLine($"0{submarino}x | SB - Submarinos   | (2 quadrantes)");

        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("=>");
            tipoNavio = Console.ReadLine().Trim().ToUpper();
            if (!(tipoNavio.Contains("PS") || tipoNavio.Contains("NT") || tipoNavio.Contains("DS") || tipoNavio.Contains("SB")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tipo inválido, por favor verifique e tente novamente!");
                continue;
            }
        } while (!(tipoNavio.Contains("PS") || tipoNavio.Contains("NT") || tipoNavio.Contains("DS") || tipoNavio.Contains("SB")));
        if (portaAviao == 0 && tipoNavio.Contains("PS"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Porta-Avião já posicionadas, tente novamente!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (navioTanque == 0 && tipoNavio.Contains("NT"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Navio-Tanque já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (destroyer == 0 && tipoNavio.Contains("DS"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Destroyer já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (submarino == 0 && tipoNavio.Contains("SB"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Submarino já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine($"O tipo do navio é {tipoNavio}");
        Console.WriteLine("Pressione Enter para continuar..");
        Console.ReadKey();
        do
        {
            Console.Clear();
            Console.WriteLine("Agora informe as coordenadas do navio:");
            Console.Write("=>");
            coordNavio = Console.ReadLine().Trim().ToUpper();
        } while (String.IsNullOrWhiteSpace(coordNavio));

        posicaoNavio = ConverteCoord(coordNavio);
        if (ValidarCoord(posicaoNavio, TamanhoNavio(tipoNavio)) == 0)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Posição Inválida, aperte Enter tente novamente");
            Console.ResetColor();
            Console.ReadKey();
            continue;
        }
        else
        {
            if (ValidaPosicao(posicaoNavio, tabuleiro) == 0)
            {
                Console.WriteLine("Já existe um navio posicionado nessa posição!");
                Console.WriteLine("Aperte Enter e tente novamente!");
                Console.ReadKey();
                continue;
            }

        }
        if (tipoNavio == "PS")
        {
            portaAviao--;
        }
        if (tipoNavio == "NT")
        {
            navioTanque--;
        }
        if (tipoNavio == "DS")
        {
            destroyer--;
        }
        if (tipoNavio == "SB")
        {
            submarino--;
        }
    }
    Console.ResetColor();
}
//Jogador2 posiciona seus navios
if (tipoJogo == "2")
{
    portaAviao = 1;
    navioTanque = 2;
    destroyer = 3;
    submarino = 4;
    while (portaAviao > 0 || navioTanque > 0 || destroyer > 0 || submarino > 0)
    {


        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hora de posicionar os navios");
        Console.WriteLine($"{jogadorDois} por favor selecione o código do navio a ser posicionado");
        Console.WriteLine($"0{portaAviao}x | PS - Porta-Aviões | (5 quadrantes)");
        Console.WriteLine($"0{navioTanque}x | NT - Navio-Tanque |(4 quadrantes)");
        Console.WriteLine($"0{destroyer}x | DS - Destroyers   | (3 quadrantes)");
        Console.WriteLine($"0{submarino}x | SB - Submarinos   | (2 quadrantes)");
        do
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("=>");
            tipoNavio = Console.ReadLine().Trim().ToUpper();
            if (!(tipoNavio.Contains("PS") || tipoNavio.Contains("NT") || tipoNavio.Contains("DS") || tipoNavio.Contains("SB")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tipo inválido, por favor verifique e tente novamente!");
                continue;
            }
        } while (!(tipoNavio.Contains("PS") || tipoNavio.Contains("NT") || tipoNavio.Contains("DS") || tipoNavio.Contains("SB")));
        if (portaAviao == 0 && tipoNavio.Contains("PS"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Porta-Avião já posicionadas, tente novamente!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (navioTanque == 0 && tipoNavio.Contains("NT"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Navio-Tanque já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (destroyer == 0 && tipoNavio.Contains("DS"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Destroyer já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }
        if (submarino == 0 && tipoNavio.Contains("SB"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Todas as unidades de Submarino já posicionadas!");
            Console.WriteLine("Pressione Enter e tente novamente..");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine($"O tipo do navio é {tipoNavio}");
        Console.WriteLine("Pressione Enter para continuar..");
        Console.ReadKey();
        do
        {
            Console.Clear();
            Console.WriteLine("Agora informe as coordenadas do navio:");
            Console.Write("=>");
            coordNavio = Console.ReadLine().Trim().ToUpper();
        } while (String.IsNullOrWhiteSpace(coordNavio));

        posicaoNavio = ConverteCoord(coordNavio);
        if (ValidarCoord(posicaoNavio, TamanhoNavio(tipoNavio)) == 0)
        {   
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Posição Inválida, aperte Enter e tente novamente");
            Console.ResetColor();
            Console.ReadKey();
            continue;
        }
        else
        {
            if (ValidaPosicao(posicaoNavio, tabuleiro2) == 0)
            {
                Console.WriteLine("Já existe um navio posicionado nessa posição!");
                Console.WriteLine("Aperte Enter e tentar novamente..");
                Console.ReadKey();
                continue;
            }

        }
        if (tipoNavio == "PS")
        {
            portaAviao--;
        }
        if (tipoNavio == "NT")
        {
            navioTanque--;
        }
        if (tipoNavio == "DS")
        {
            destroyer--;
        }
        if (tipoNavio == "SB")
        {
            submarino--;
        }
    }
    Console.ResetColor();
    Console.BackgroundColor = ConsoleColor.Green;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("==================  *** HORA DA BATALHA ***  ==================");
    Console.ResetColor();
}

//Inicia os tabuleiros do jogo
IniciaTabuleiro(tabuleiroResposta);
IniciaTabuleiro(tabuleiroResposta2);


//Inicia o jogo, onde acontecem os ataques e os tabuleiros são mostrados.
while (ChecaNavio(tabuleiro) == 1 && ChecaNavio(tabuleiro2) == 1)
{
    while (vezJogadorUm)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Capitão {jogadorUm}, por favor faça sua jogada (A - J) e (1 - 10)\n");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine($"============== Navios de {jogadorDois} ================");
        Console.ResetColor();

        ImprimeTabuleiro(tabuleiroResposta2);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine("==================================================");
        Console.ResetColor();
                Console.Write("==>");
        

        ataque = Console.ReadLine().Trim().ToUpper();
        if (string.IsNullOrEmpty(ataque))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sua jogada não é válida, tente novamente ");
            Console.ResetColor();
            continue;
        }
        ataqueConvertido = ConverteCoord(ataque);

        if (ChecarAtaque(ataqueConvertido, tabuleiroResposta2) == 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sua jogada já foi efetuada, tente novamente");
            Console.WriteLine("Pressione Enter para tentar novamente..");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
            continue;
        }
        else
        {

            TemNavio(ataqueConvertido, tabuleiro2, tabuleiroResposta2);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"============== Navios de {jogadorDois} ================");
            Console.ResetColor();
            ImprimeTabuleiro(tabuleiroResposta2);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"===================================================");
            Console.ResetColor();
            AtualizaTabuleiro(ataqueConvertido, tabuleiro2);
            if (ChecaNavio(tabuleiro2) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===============================================================================");
                Console.WriteLine($"=Parabens {jogadorUm}, você destruiu toda a frota de {jogadorDois} e foi o grande vencedor!=");
                Console.WriteLine("===============================================================================");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            else
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Boa {jogadorUm}! A jogada foi válida, agora aperte Enter para que {jogadorDois} possa fazer sua próxima jogada");
            Console.ResetColor();

            Console.ReadKey();
            Console.Clear();
            vezJogadorUm = false;
            vezJogadorDois = true;
        }
    }


    while (vezJogadorDois)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Capitão {jogadorDois}, por favor faça sua jogada (A - J) e (1 - 10)");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine($"=============== Navios de {jogadorUm} ================");
        Console.ResetColor();

        ImprimeTabuleiro(tabuleiroResposta);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine("==================================================");
        Console.ResetColor();
        Console.Write("==>");

        ataque = Console.ReadLine().Trim().ToUpper();
        if (string.IsNullOrEmpty(ataque))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sua jogada não é válida, tente novamente ");
            Console.ResetColor();
            continue;
        }
        ataqueConvertido = ConverteCoord(ataque);
        if (ChecarAtaque(ataqueConvertido, tabuleiroResposta) == 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sua jogada já foi efetuada, tente novamente");
            Console.WriteLine("Pressione Enter para prosseguir..");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            continue;
        }
        else
        {

            TemNavio(ataqueConvertido, tabuleiro, tabuleiroResposta);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"=============== Navios de {jogadorUm} ================");
            Console.ResetColor();
            ImprimeTabuleiro(tabuleiroResposta);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"===================================================");
            Console.ResetColor();
            AtualizaTabuleiro(ataqueConvertido, tabuleiro);
            if (ChecaNavio(tabuleiro2) == 0)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===============================================================================");
                Console.WriteLine($"=Parabens {jogadorDois}, você destruiu toda a frota de {jogadorUm} e foi o grande vencedor!=");
                Console.WriteLine("===============================================================================");
                Console.ResetColor();
                Console.ReadKey();
                break;
            }
            else
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Boa {jogadorDois}! A jogada foi válida, agora aperte Enter para que {jogadorUm} possa fazer sua próxima jogada");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            vezJogadorUm = true;
            vezJogadorDois = false;
        }
    }
}






//Converte coordenada para uma válida que a matriz aceite
static int[] ConverteCoord(string coordNavio)

{
    string repAux = "";
    repAux = coordNavio.Replace("10", ":");
    int[] saida = new int[repAux.Length];

    for (int i = 0; i < repAux.Length; i++)
    {
        switch (repAux[i])
        {
            case 'A':
                saida[i] = 0;
                break;

            case 'B':
                saida[i] = 1;
                break;

            case 'C':
                saida[i] = 2;
                break;

            case 'D':
                saida[i] = 3;
                break;

            case 'E':
                saida[i] = 4;
                break;

            case 'F':
                saida[i] = 5;
                break;

            case 'G':
                saida[i] = 6;
                break;

            case 'H':
                saida[i] = 7;
                break;

            case 'I':
                saida[i] = 8;
                break;

            case 'J':
                saida[i] = 9;
                break;

            default:
                saida[i] = Convert.ToInt32(repAux[i] - 49);
                break;
        }
    }
    return saida;

}

//Tamanho dos navios pré-configurados
static int TamanhoNavio(string tipoNavio)
{
    if (tipoNavio == "PS")
    {
        return 5;
    }
    if (tipoNavio == "NT")
    {
        return 4;
    }
    if (tipoNavio == "DS")
    {
        return 3;
    }
    if (tipoNavio == "SB")
    {
        return 2;
    }
    return 0;
}

//Vê se a coordenada é válida, se corresponde com o tamanho do navio
static int ValidarCoord(int[] posicaoNavio, int tamanhoNavio)
{
    int x1 = posicaoNavio[0];
    int x2 = posicaoNavio[2];
    int y1 = posicaoNavio[1];
    int y2 = posicaoNavio[3];

    if (x1 != x2 && y1 != y2)
    {
        return 0;
    }

    if (tamanhoNavio == Math.Abs(x1 - x2) + 1)
    {
        return 1;
    }

    if (tamanhoNavio == Math.Abs(y1 - y2) + 1)
    {
        return 1;
    }
    return 0;


}


//Guarda o posicionamento dos navios no tabuleiro implícito
static int ValidaPosicao(int[] coord, string[,] tabuleiro)
{
    for (int i = coord[0]; i <= coord[2]; i++)
    {
        for (int j = coord[1]; j <= coord[3]; j++)
        {
            if (tabuleiro[i, j] != null)
                return 0;
        }
    }

    for (int i = coord[0]; i <= coord[2]; i++)
    {
        for (int j = coord[1]; j <= coord[3]; j++)
        {
            tabuleiro[i, j] = "1";
        }
    }
    return 1;
}


//Checa se ainda existem navios no tabuleiro implícito
static int ChecaNavio(string[,] tabuleiro)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (tabuleiro[i, j] == "1")
                return 1;
        }

    }
    return 0;
}

//Checa se existem navios na coordenada de ataque
static void TemNavio(int[] ataqueConv, string[,] tab2Navios, string[,] tabResposta)
{


    if (tab2Navios[ataqueConv[0], ataqueConv[1]] != null)
    {

        tabResposta[ataqueConv[0], ataqueConv[1]] = "[ X ]";

    }

    else
        tabResposta[ataqueConv[0], ataqueConv[1]] = "[ A ]";

}

//Checa dentro do tabuleiro implícito se a coordenada de ataque é válida ou se já foi utilizada
static int ChecarAtaque(int[] ataqueConv, string[,] tab2Navios)
{


    if (tab2Navios[ataqueConv[0], ataqueConv[1]] == "[ X ]")
    {
        return 1;
    }
    if (tab2Navios[ataqueConv[0], ataqueConv[1]] == "[ A ]")
    {
        return 1;
    }
    return 0;

}

//Imprime o tabuleiro explícito
static void ImprimeTabuleiro(string[,] tabuleiro)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if (tabuleiro[i, j] == "[ X ]")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (tabuleiro[i, j] == "[ A ]")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            Console.Write(tabuleiro[i, j]);
            Console.ResetColor();
        }
        Console.WriteLine();
    }

}

//Inicia os tabuleiros
static void IniciaTabuleiro(string[,] tabuleiro)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            tabuleiro[i, j] = "[   ]";

        }

    }

}

//Atualiza o tabuleiro implícito, "retirando" as posições de navio já atacadas
static void AtualizaTabuleiro(int[] ataqueConv, string[,] tab2Navios)
{


    if (tab2Navios[ataqueConv[0], ataqueConv[1]] == "1")
    {
        tab2Navios[ataqueConv[0], ataqueConv[1]] = "0";

    }

}

//Converte a primeira letra do nome dos jogadores para maiúscula
static string PrimeiraLetraMaiuscula(string input)
{
    if (String.IsNullOrEmpty(input))
        throw new ArgumentException("Insira uma palavra diferente de nula ou vazia");
    return input.Length > 1 ? char.ToUpper(input[0]) + input.Substring(1) : input.ToUpper();
}

