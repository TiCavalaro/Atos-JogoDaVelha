using System;
using System.Runtime.Intrinsics.X86;
using System.Timers;


namespace ConsoleAppAula11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("Digite 1 para jogar com outro Player");
                Console.WriteLine("Insira 2 para jogar com o computador");
                Console.WriteLine("Insira 3 para jogar com o computador Hard:");// o computador ira escolher ou impedir jogadas vitoriosas laterais(apenas nas laterais)
                Console.WriteLine("Insira 4 para jogar com o computador Extreme:");// Dificuldade extremamente elevada
                Console.WriteLine("Insira 5 para finalizar o programa:");

                int opcao;
                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            Player();//faz o contra jogador
                            Contador(); // timer
                            Console.Clear(); //reseta
                            break;
                        case 2:
                            Computador();//contra o pc
                            Contador();//timer
                            Console.Clear();// clear
                            break;
                        case 3:
                            ComputadorHard();//contra o pc
                            Contador();//timer
                            Console.Clear();// clear
                            break;
                        case 4:
                            ComputadorExtreme();//contra o pc
                            Contador();//timer
                            Console.Clear();// clear
                            break;
                        case 5:
                            continuar = false; // finaliza
                            break;
                        default:
                            Console.WriteLine("Opção invalida");// casi numero errado
                            Console.WriteLine("Clique ENTER para sair");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada invalida. Digite um numero inteiro.");//caso algo sem ser numero seja digitado
                    Console.WriteLine("Clique ENTER para sair");
                    Console.ReadLine();
                }
            }
        }



        public static void Player()
        {
            bool jogarNovamente = true;

            while (jogarNovamente)// laco jogar
            {
                char[,] matriz = new char[3, 3];
                int linha, coluna, contador;

                for (int i = 0; i < 3; i++)//faz matriz
                {
                    Console.WriteLine();
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write("\t.");// faz o jogo \t faz a separacao
                        matriz[i, j] = '.';//o que eu decidi
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                // Primeiro jogador sempre sera 'X' e o segundo sempre sera 'O';
                for (contador = 0; contador < 9; contador++)//9 quadrados, 9 contagens
                {
                    if (contador == 8)//empatador
                    {
                        Console.WriteLine("Empate!");
                        jogarNovamente = false;
                        break;
                    }
                    Console.WriteLine($"Jogador {(contador % 2) + 1} escolha a posição (linha, coluna) para jogar:");

                    try//digitacao da posicao (aprendido em Java)
                    {
                        Console.Write("Linha: ");
                        linha = int.Parse(Console.ReadLine()) - 1;

                        Console.Write("Coluna: ");
                        coluna = int.Parse(Console.ReadLine()) - 1;
                    }
                    catch (FormatException)//catch para caso o usuario digitar errado
                    {
                        Console.WriteLine("Entrada invalida para a linha ou coluna, tente novamente\n");
                        contador--;
                        continue;
                    }

                    if (linha < 0 || linha > 2 || coluna < 0 || coluna > 2)//erro de digitacao 2
                    {
                        Console.WriteLine("Linha ou coluna invalida, tente novamente\n");
                        contador--;
                        continue;
                    }

                    Console.WriteLine();
                    if (matriz[linha, coluna] == '.')//deixa  a jogada acontecer
                    {
                        matriz[linha, coluna] = contador % 2 == 1 ? 'O' : 'X';//verifica qual vai ser o jogador
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine();
                            for (int j = 0; j < 3; j++)
                            {
                                Console.Write($"\t{matriz[i, j]}");
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        if((matriz[0, 1] == matriz[1, 1] && matriz[0, 1] == matriz[2, 1] && matriz[0, 1] != '.') ||// verifica vitoria
                            (matriz[1, 0] == matriz[1, 1] && matriz[1, 0] == matriz[1, 2] && matriz[1, 0] != '.') ||
                            (matriz[0, 0] == matriz[1, 0] && matriz[0, 0] == matriz[2, 0] && matriz[0, 0] != '.') ||
                            (matriz[2, 0] == matriz[2, 1] && matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.') ||
                            (matriz[0, 2] == matriz[1, 2] && matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.') ||
                            (matriz[0, 0] == matriz[0, 1] && matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.') ||//corrigir
                            (matriz[0, 2] == matriz[1, 1] && matriz[0, 2] == matriz[2, 0] && matriz[0, 2] != '.') ||
                            (matriz[0, 0] == matriz[1, 1] && matriz[0, 0] == matriz[2, 2] && matriz[0, 0] != '.'))
                        {
                            Console.WriteLine($"Jogador {(contador % 2) + 1} ganhou!");
                            jogarNovamente = false;
                            break;
                        }
                    }
                    else if (matriz[linha, coluna] != '.')//nao deixa a jogada acontecer
                    {
                        Console.WriteLine("O espaco escolhido ja esta ocupado, repita a operacao para um espaço valido");
                        contador--;
                    }
                }

            }
        }


        public static void Computador()// jogar contra o comptuador
        {
            bool jogarNovamente = true;//continuar

            while (jogarNovamente)
            {
                char[,] matriz = new char[3, 3];
                int linha, coluna, contador;

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write("\t.");
                        matriz[i, j] = '.';
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                Random rnd = new Random();

                // O jogador sempre sera 'X' e o computador sempre sera 'O';
                for (contador = 0; contador < 9; contador++)//inicio
                {
                
                    if (contador % 2 == 0)//verifica quem vai jogar (comecando pelo computador)
                    {
                        linha = rnd.Next(0, 3);
                        coluna = rnd.Next(0, 3);

                        while (matriz[linha, coluna] != '.')
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);
                        }

                        Console.WriteLine($"O computador jogou na posicao ({linha + 1}, {coluna + 1})");
                        matriz[linha, coluna] = 'O';
                        Pc();
                    }

                    else//jogador escolhe
                    {
                        Console.WriteLine("Escolha a posicao (linha, coluna) para jogar:");

                        try
                        {
                            Console.Write("Linha: ");
                            linha = int.Parse(Console.ReadLine()) - 1;//posicao da linha escolhida

                            Console.Write("Coluna: ");
                            coluna = int.Parse(Console.ReadLine()) - 1;//posicao da coluna escolhida
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Entrada invalida para a linha ou coluna, tente novamente\n");
                            contador--;
                            continue;
                        }

                        if (linha < 0 || linha > 2 || coluna < 0 || coluna > 2)
                        {
                            Console.WriteLine("Linha ou coluna invalida, tente novamente\n");
                            contador--;
                            continue;
                        }

                        Console.WriteLine();
                        if (matriz[linha, coluna] == '.')
                        {
                            matriz[linha, coluna] = 'X';
                        }
                        else if (matriz[linha, coluna] != '.')
                        {
                            Console.WriteLine("O espaço escolhido ja esta ocupado, repita a operação para um espaço valido");
                            contador--;
                            continue;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine();
                        for (int j = 0; j < 3; j++)
                        {
                            Console.Write($"\t{matriz[i, j]}");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    if ((matriz[0, 1] == matriz[1, 1] && matriz[0, 1] == matriz[2, 1] && matriz[0, 1] != '.') ||//verifica vitoria 2
                        (matriz[1, 0] == matriz[1, 1] && matriz[1, 0] == matriz[1, 2] && matriz[1, 0] != '.') ||
                        (matriz[0, 0] == matriz[1, 0] && matriz[0, 0] == matriz[2, 0] && matriz[0, 0] != '.') ||
                        (matriz[2, 0] == matriz[2, 1] && matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.') ||
                        (matriz[0, 2] == matriz[1, 2] && matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[0, 1] && matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.') ||//corrigir
                        (matriz[0, 2] == matriz[1, 1] && matriz[0, 2] == matriz[2, 0] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[1, 1] && matriz[0, 0] == matriz[2, 2] && matriz[0, 0] != '.'))
                    {
                        if (contador % 2 == 0)
                        {
                            Console.WriteLine("O computador venceu!");//caso vitoria pc
                        }
                        else
                        {
                            Console.WriteLine("Voce venceu!");//caso vitoria sua
                        }
                        break;
                    }
                    if (contador == 8)
                    {
                        Console.WriteLine("Deu velha!");// caso empate
                    }
                }
                break;
            }

        }


        static void Contador()//temporizador
        {
            int count = 0;
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) => {
                count++;
                if (count == 5)
                {
                    Console.WriteLine("Contador de 5 segundos terminou.");
                    Console.WriteLine("Clique ENTER para sair");


                    timer.Stop();
                }
            };
            timer.Start();
            Console.WriteLine("Contando 5 segundos...");
            Console.ReadLine();//sai do timer e da clear
        }


        static void Pc()//timer para melhorar a jogabilidade
        {
           
            Console.WriteLine("Digite ENTER para confimar a jogada do computador...");
            Console.ReadLine();//sai do timer e da clear
        }

        public static void ComputadorHard()// jogar contra o comptuador
        {
            bool jogarNovamente = true;//continuar

            while (jogarNovamente)
            {
                char[,] matriz = new char[3, 3];
                int linha, coluna, contador;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matriz[i, j] = '.';
                    }
                }

                Random rnd = new Random();

                // O jogador sempre sera 'X' e o computador sempre sera 'O';
                for (contador = 0; contador < 9; contador++)//inicio
                {   
                    if(contador == 0)
                    {
                        matriz[2, 0]= 'O';
                        Console.WriteLine($"O computador jogou na posicao 2,0");
                    }

                    else if (contador % 2 == 0 && contador !=0)//verifica quem vai jogar (comecando pelo computador)
                    {
                        if((matriz[2, 0] == matriz[1, 0] && matriz[2, 0] != '.' && matriz[0,0] == '.'))
                        {
                            matriz[0, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,0");
                            Pc();

                        }
                        else if ((matriz[2, 0] == matriz[2, 1] && matriz[2, 0] != '.' && matriz[2, 2] == '.'))
                        {
                            matriz[2, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 1] && matriz[0, 0] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 1] && matriz[0, 0] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[2, 2] == matriz[1, 2] && matriz[2, 2] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[0, 2] == matriz[1, 2] && matriz[0, 2] != '.' && matriz[2, 2] == '.'))
                        {
                            matriz[2, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 0] == matriz[0, 0] && matriz[2, 0] != '.' && matriz[1, 0] == '.'))
                        {
                            matriz[1, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.' && matriz[2, 1] == '.'))
                        {
                            matriz[2, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.' && matriz[1, 2] == '.'))
                        {
                            matriz[1, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.' && matriz[0, 1] == '.'))
                        {
                            matriz[0, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);

                            while (matriz[linha, coluna] != '.')
                            {
                                linha = rnd.Next(0, 3);
                                coluna = rnd.Next(0, 3);
                            }

                            Console.WriteLine($"O computador jogou na posicao ({linha + 1}, {coluna + 1})");
                            matriz[linha, coluna] = 'O';
                            Pc();
                        }

                    }

                    else //jogador escolhe
                    {
                        Console.WriteLine("Escolha a posicao (linha, coluna) para jogar:");

                        try
                        {
                            Console.Write("Linha: ");
                            linha = int.Parse(Console.ReadLine()) - 1;//posicao da linha escolhida

                            Console.Write("Coluna: ");
                            coluna = int.Parse(Console.ReadLine()) - 1;//posicao da coluna escolhida
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Entrada invalida para a linha ou coluna, tente novamente\n");
                            contador--;
                            continue;
                        }

                        if (linha < 0 || linha > 2 || coluna < 0 || coluna > 2)
                        {
                            Console.WriteLine("Linha ou coluna invalida, tente novamente\n");
                            contador--;
                            continue;
                        }

                        Console.WriteLine();
                        if (matriz[linha, coluna] == '.')
                        {
                            matriz[linha, coluna] = 'X';
                        }
                        else if (matriz[linha, coluna] != '.')
                        {
                            Console.WriteLine("O espaço escolhido ja esta ocupado, repita a operação para um espaço valido");
                            contador--;
                            continue;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine();
                        for (int j = 0; j < 3; j++)
                        {
                            Console.Write($"\t{matriz[i, j]}");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    if ((matriz[0, 1] == matriz[1, 1] && matriz[0, 1] == matriz[2, 1] && matriz[0, 1] != '.') ||//verifica vitoria 2
                        (matriz[1, 0] == matriz[1, 1] && matriz[1, 0] == matriz[1, 2] && matriz[1, 0] != '.') ||
                        (matriz[0, 0] == matriz[1, 0] && matriz[0, 0] == matriz[2, 0] && matriz[0, 0] != '.') ||
                        (matriz[2, 0] == matriz[2, 1] && matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.') ||
                        (matriz[0, 2] == matriz[1, 2] && matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[0, 1] && matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.') ||//corrigir
                        (matriz[0, 2] == matriz[1, 1] && matriz[0, 2] == matriz[2, 0] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[1, 1] && matriz[0, 0] == matriz[2, 2] && matriz[0, 0] != '.'))
                    {
                        if (contador % 2 == 0)
                        {
                            Console.WriteLine("O computador venceu!");//caso vitoria pc
                        }
                        else
                        {
                            Console.WriteLine("Voce venceu!");//caso vitoria sua
                        }
                        break;
                    }
                    if (contador == 8)
                    {
                        Console.WriteLine("Deu velha!");// caso empate
                    }
                }
                break;
            }

        }

        public static void ComputadorExtreme()// jogar contra o comptuador
        {
            bool jogarNovamente = true;//continuar

            while (jogarNovamente)
            {
                char[,] matriz = new char[3, 3];
                int linha, coluna, contador;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matriz[i, j] = '.';
                    }
                }

                Random rnd = new Random();

                // O jogador sempre sera 'X' e o computador sempre sera 'O';
                for (contador = 0; contador < 9; contador++)//inicio
                {
                    if (contador == 0)
                    {
                        matriz[2, 0] = 'O';
                        Console.WriteLine($"O computador jogou na posicao 2,0");
                    }

                    else if (contador % 2 == 0 && contador != 0)//verifica quem vai jogar (comecando pelo computador)
                    {
                        if ((matriz[2, 0] == matriz[1, 0] && matriz[2, 0] != '.' && matriz[0, 0] == '.'))
                        {
                            matriz[0, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,0");
                            Pc();

                        }
                        else if ((matriz[2, 0] == matriz[2, 1] && matriz[2, 0] != '.' && matriz[2, 2] == '.'))
                        {
                            matriz[2, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 1] && matriz[0, 0] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 1] && matriz[0, 0] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[2, 2] == matriz[1, 2] && matriz[2, 2] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 0,2");
                            Pc();
                        }
                        else if ((matriz[0, 2] == matriz[1, 2] && matriz[0, 2] != '.' && matriz[2, 2] == '.'))
                        {
                            matriz[2, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 0] == matriz[0, 0] && matriz[2, 0] != '.' && matriz[1, 0] == '.'))
                        {
                            matriz[1, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.' && matriz[2, 1] == '.'))
                        {
                            matriz[2, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.' && matriz[1, 2] == '.'))
                        {
                            matriz[1, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.' && matriz[0, 1] == '.'))
                        {
                            matriz[0, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 1] == matriz[1, 1] && matriz[2, 1] != '.' && matriz[0, 1] == '.'))
                        {
                            matriz[0, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 1] == matriz[1, 1] && matriz[0, 1] != '.' && matriz[2, 1] == '.'))
                        {
                            matriz[2, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 1] == matriz[2, 1] && matriz[0, 1] != '.' && matriz[1, 1] == '.'))
                        {
                            matriz[1, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 0] == matriz[1, 1] && matriz[2,0] != '.' && matriz[0, 2] == '.'))
                        {
                            matriz[0, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 2] == matriz[2, 0] && matriz[0, 2] != '.' && matriz[1, 1] == '.'))
                        {
                            matriz[1, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[1, 0] == matriz[1, 1] && matriz[1, 0] != '.' && matriz[1, 2] == '.'))
                        {
                            matriz[1, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[1, 2] == matriz[1, 1] && matriz[1, 2] != '.' && matriz[1, 0] == '.'))
                        {
                            matriz[1, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[1, 0] == matriz[1, 2] && matriz[1, 2] != '.' && matriz[1, 1] == '.'))
                        {
                            matriz[1, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[1, 1] && matriz[0, 0] != '.' && matriz[2, 2] == '.'))
                        {
                            matriz[2, 2] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[2, 2] == matriz[1, 1] && matriz[2, 2] != '.' && matriz[0 , 0] == '.'))
                        {
                            matriz[0, 0] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }
                        else if ((matriz[0, 0] == matriz[2, 2] && matriz[0, 0] != '.' && matriz[1, 1] == '.'))
                        {
                            matriz[1, 1] = 'O';
                            Console.WriteLine("O computador jogou na posicao 2,2");
                            Pc();
                        }



                        else
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);

                            while (matriz[linha, coluna] != '.')
                            {
                                linha = rnd.Next(0, 3);
                                coluna = rnd.Next(0, 3);
                            }

                            Console.WriteLine($"O computador jogou na posicao ({linha + 1}, {coluna + 1})");
                            matriz[linha, coluna] = 'O';
                            Pc();
                        }

                    }

                    else //jogador escolhe
                    {
                        Console.WriteLine("Escolha a posicao (linha, coluna) para jogar:");

                        try
                        {
                            Console.Write("Linha: ");
                            linha = int.Parse(Console.ReadLine()) - 1;//posicao da linha escolhida

                            Console.Write("Coluna: ");
                            coluna = int.Parse(Console.ReadLine()) - 1;//posicao da coluna escolhida
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Entrada invalida para a linha ou coluna, tente novamente\n");
                            contador--;
                            continue;
                        }

                        if (linha < 0 || linha > 2 || coluna < 0 || coluna > 2)
                        {
                            Console.WriteLine("Linha ou coluna invalida, tente novamente\n");
                            contador--;
                            continue;
                        }

                        Console.WriteLine();
                        if (matriz[linha, coluna] == '.')
                        {
                            matriz[linha, coluna] = 'X';
                        }
                        else if (matriz[linha, coluna] != '.')
                        {
                            Console.WriteLine("O espaço escolhido ja esta ocupado, repita a operação para um espaço valido");
                            contador--;
                            continue;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine();
                        for (int j = 0; j < 3; j++)
                        {
                            Console.Write($"\t{matriz[i, j]}");
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    if ((matriz[0, 1] == matriz[1, 1] && matriz[0, 1] == matriz[2, 1] && matriz[0, 1] != '.') ||//verifica vitoria 2
                        (matriz[1, 0] == matriz[1, 1] && matriz[1, 0] == matriz[1, 2] && matriz[1, 0] != '.') ||
                        (matriz[0, 0] == matriz[1, 0] && matriz[0, 0] == matriz[2, 0] && matriz[0, 0] != '.') ||
                        (matriz[2, 0] == matriz[2, 1] && matriz[2, 0] == matriz[2, 2] && matriz[2, 0] != '.') ||
                        (matriz[0, 2] == matriz[1, 2] && matriz[0, 2] == matriz[2, 2] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[0, 1] && matriz[0, 0] == matriz[0, 2] && matriz[0, 0] != '.') ||//corrigir
                        (matriz[0, 2] == matriz[1, 1] && matriz[0, 2] == matriz[2, 0] && matriz[0, 2] != '.') ||
                        (matriz[0, 0] == matriz[1, 1] && matriz[0, 0] == matriz[2, 2] && matriz[0, 0] != '.'))
                    {
                        if (contador % 2 == 0)
                        {
                            Console.WriteLine("O computador venceu!");//caso vitoria pc
                        }
                        else
                        {
                            Console.WriteLine("Voce venceu!");//caso vitoria sua
                        }
                        break;
                    }
                    if (contador == 8)
                    {
                        Console.WriteLine("Deu velha!");// caso empate
                    }
                }
                break;
            }

        }
    }
}

