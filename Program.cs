using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

class Program
{
    static void Main()
    {
        Random random = new Random();

        // Criação do personagem
        int atributosParaGastar, dados, força, constituiçao, hp, armadura;

        Console.WriteLine("Escreva o nome do seu personagem");
        string nomePersonagem = Console.ReadLine();
        atributosParaGastar = 15;

        Console.WriteLine("Distribua seus atributos entre força, constituição e armadura");

        Console.WriteLine($"Escolha quanto de {atributosParaGastar} você quer distribuir em força");
        dados = int.Parse(Console.ReadLine());

        while (dados > atributosParaGastar)
        {
            Console.WriteLine($"Escolha um valor menor que {atributosParaGastar}");
            dados = int.Parse(Console.ReadLine());
        }
        força = dados;

        atributosParaGastar -= força;

        Console.WriteLine($"Escolha quanto de {atributosParaGastar} você quer distribuir em constituição e o restante irá para a sua armadura");
        dados = int.Parse(Console.ReadLine());

        while (dados > atributosParaGastar)
        {
            Console.WriteLine($"Escolha um valor menor que {atributosParaGastar}");
            dados = int.Parse(Console.ReadLine());
        }
        constituiçao = dados;
        hp = 40 + constituiçao*2;

        atributosParaGastar -= constituiçao;

        armadura = 10 + atributosParaGastar / 2;

        PersonagemDND Protagonista = new PersonagemDND(nomePersonagem, hp, armadura, força, constituiçao);

        // Criação do inimigo
        int atributosParaGastarM = 15, dadosM = 16, forçaM = 16, constituiçaoM = 16, hpM, armaduraM = 16;

        while (dadosM > atributosParaGastarM)
        {
            dadosM = random.Next(0, 15);
        }
        forçaM = dadosM;

        atributosParaGastarM -= forçaM;

        dadosM = 16;

        while (dadosM > atributosParaGastarM)
        {
            dadosM = random.Next(0, 15);
        }
        constituiçaoM = dadosM;
        hpM = 40 + constituiçaoM*2;

        atributosParaGastar -= constituiçaoM;

        armaduraM = 10 + atributosParaGastarM / 2;

        Monstro Inimigo = new Monstro(hpM, armaduraM, forçaM, constituiçaoM);

        // História
        Console.WriteLine($"A alguns anos atrás, Asudi, um pequeno país, começou a prosperar após uma crise que se sucedera devido a má colheita.");
        Console.WriteLine($"{Protagonista.nome}, é um fazendeiro que deixara a sua fazenda para seguir um outro caminho, o caminho do guerreiro!");
        Console.WriteLine($"{Protagonista.nome} treinou muitos anos na arte da espada, acreditando que seu país um dia prosperaria e tentaria conquistar as nações nos seus arredores!");
        Console.WriteLine($"Até que enfim, esse dia chegou, e {Protagonista.nome} foi ao campo de batalha, onde em meio ao caos, se deu de frente com um grande oponente!");
        Console.WriteLine($"- Meu nome é {Inimigo.nome}, e eu espero que você satisfaça a minha vontade de ter um duelo digno de vida ou morte!");
        Console.WriteLine("E assim, o duelo começou!");
        Console.WriteLine("Aperte qualquer tecla para continuar.");
        Console.ReadKey();

        // Duelo
        
        Console.Clear();
        
        while (true)
        {   
            if (Protagonista.vida < 0)
            {
                Console.WriteLine("Você morreu!");
                Console.ReadLine();
                break;
            }
            else if (Inimigo.vida < 0)
            {
                Console.WriteLine($"Você matou {Inimigo.nome}!");
                Console.ReadLine();
                break;
            }

            Console.WriteLine($"Nome: {Protagonista.nome} | Vida: {Protagonista.vida} | Armadura: {Protagonista.ac} | Força: {Protagonista.força} | Constituição:{Protagonista.constituição}");

            Console.WriteLine($"Nome: {Inimigo.nome} | Vida: {Inimigo.vida} | Armadura: {Inimigo.ac} | Força: {Inimigo.força} | Constituição: {Inimigo.constituição}");

            Console.WriteLine("Escolha entre ATAQUE, ATAQUE ESPECIAL, INTERAGIR");

            string inputComando = Console.ReadLine();

            int açãoInimigo = random.Next(1, 20);

            Console.WriteLine("_________");

            if (inputComando == "ATAQUE")
            {
                Protagonista.Ataque(Inimigo);

                Console.WriteLine("_________");

                if (açãoInimigo < 13)
                {
                    Inimigo.Ataque(Protagonista);
                }
                else if (açãoInimigo < 20)
                {
                    Inimigo.Interagir(Protagonista);
                }
                else
                {
                    Inimigo.AtaqueEspecial(Protagonista);
                }
            }
            else if(inputComando == "ATAQUE ESPECIAL")
            {
                Protagonista.AtaqueEspecial(Inimigo);

                Console.WriteLine("__________");

                if (açãoInimigo < 13)
                {
                    Inimigo.Ataque(Protagonista);
                }
                else if (açãoInimigo < 20)
                {
                    Inimigo.Interagir(Protagonista);
                }
                else
                {
                    Inimigo.AtaqueEspecial(Protagonista);
                }
            }
            else if(inputComando == "INTERAGIR")
            {
                Protagonista.Interagir(Inimigo);

                Console.WriteLine("_________");

                if (açãoInimigo < 13)
                {
                    Inimigo.Ataque(Protagonista);
                }
                else if (açãoInimigo < 20)
                {
                    Inimigo.Interagir(Protagonista);
                }
                else
                {
                    Inimigo.AtaqueEspecial(Protagonista);
                }
            }
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

public class PersonagemDND
{
    public string nome;
    public int vida, ac, força, constituição, acerto = 0, escudo = 1, espada = 1, chances = 1;

    public PersonagemDND(string novoNome, int novaVida, int novaAc, int novaForça, int novaConstituição)
    {
        nome = novoNome;
        vida = novaVida;
        ac = novaAc;
        força = novaForça;
        constituição = novaConstituição;
    }

    public PersonagemDND()
    { }

    public void Ataque(Monstro alvo)
    {    
        Random random = new Random();

        int ataqueTentativa = random.Next(1, 20) + acerto;

        Console.WriteLine("Você desferiu um golpe com sua espada!");
        Console.WriteLine("Sua rolagem foi: " + (ataqueTentativa + acerto));

        if(ataqueTentativa > alvo.ac)
        {
            int Ataque = random.Next(1, 8) + força / 2;
            alvo.vida -= Ataque;
            Console.WriteLine($"Você acertou! {alvo.nome} perdeu {Ataque} pontos de vida");
        }
        else
        {
            Console.WriteLine("Você errou!");
        }
    }

    public void AtaqueEspecial(Monstro alvo)
    {
        Random random = new Random();

        if(chances > 0)
        {
            int ataqueTentativa = random.Next(1, 20) + acerto;

            Console.WriteLine("Você tenta desferir um golpe devastador, mirando nos pontos vitais de " + alvo.nome + "!");
            Console.WriteLine("Sua rolagem foi: " + (ataqueTentativa + acerto));

            if (ataqueTentativa > alvo.ac)
            {
                int Ataque = random.Next(1, 8) + força / 2 + 10;
                alvo.vida -= Ataque;
                Console.WriteLine($"Você rasgou o peito de seu inimigo com um corte na diagonal!");
                Console.WriteLine($"{alvo.nome} perdeu {Ataque} pontos de vida");
                Console.WriteLine("Você não pode mais realizar um ATAQUE ESPECIAL!");
            }
            else
            {
                Console.WriteLine("Você errou!");
                Console.WriteLine("Você não pode mais realizar um ATAQUE ESPECIAL!");
            }

            chances = 0;
        }
       else if(chances == 0)
        {
            Console.WriteLine("Você tenta reunir forças para desferir um golpe devastador, mas você não tem energia para fazer isso!");
        }

    }

    public void Interagir(Monstro alvo)
    {
        Random rand = new Random();
        int possibilidades = rand.Next(0, 99);

        if(possibilidades < 30)
        {
            Console.WriteLine($"{nome} encontra uma pedra e joga na direção de {alvo.nome}! {alvo.nome} recebe 5 de dano!");
            alvo.vida -= 5;
        }
        else if(possibilidades < 50)
        {
            Console.WriteLine($"Em meio ao combate, {nome} vê uma flecha indo na direção de {alvo.nome}! A flecha acerta em cheio, causando 10 de dano!");
            alvo.vida -= 10;
        }
        else if(possibilidades < 70)
        {
            Console.WriteLine($"Em meio ao combate, uma flecha vai na direção de {nome}! A flecha acerta em cheio, {nome} recebe 10 de dano!");
            vida -= 10;
        }
        else if(possibilidades < 90)
        {
            Console.WriteLine($"{nome} encontra uma garrafa de álcool jogada no meio do campo de batalha, {nome} a coleta e bebe! {nome} recupera 15 de vida!");
            vida += 15;
        }
        else if(possibilidades < 95)
        {
            if(escudo == 1)
            {
                Console.WriteLine($"{nome} encontra um guerreiro morto ao lado que estava portando um escudo! {nome} pega o escudo e recebe +1 de Armadura!");
                ac += 1;
                escudo -= 1;
            }
            else
            {
                Console.WriteLine($"{nome} esbarra em um corpo morto e cai no chão com as costas para cima! Com isso, {alvo.nome} aproveita a oportunidade para finalizar o combate!");
                vida = 0;
            }
        }
        else
        {
            if(espada == 1)
            {
                Console.WriteLine($"{nome} encontra um guerreiro caído que estava portando uma espada longa que ao longe parece ser mais eficiente que sua espada atual! {nome} pega a espada e recebe +2 de acerto!");
                acerto += 2;
                espada -= 1;
            }
            else
            {
                Console.WriteLine($"Uma flecha voa na direção de {alvo.nome} que não estava tão atento aos seus arredores. A flecha perfura o seu crânio, atravessando e finalizando-o!");
                alvo.vida = 0;
            }
        }
    }
}

public class Monstro
{
    public string nome;
    public int vida, ac, força, constituição, acerto = 0, escudo = 1, espada = 1, chances = 1;

    public Monstro(int novaVida, int novaAc, int novaForça, int novaConstituição)
    {
        Random rand = new Random();
        string[] nomesString = new string[] { "Lucas Falcão Quintana", "Aurélio Figueiredo Araújo", "Fabiano Pedroso Menezes", "Igor Peres Tavares", "Antônio Ferraz Delgado", "Conrado Shinoda Luz", "Arthur Andrade Gomes", "Roberto Guerra Watanabe", "Maurício Carvalho Sanches", "Tomás Prestes Ferraz" };

        nome = nomesString[rand.Next(0, nomesString.Length - 1)];
        vida = novaVida;
        ac = novaAc;
        força = novaForça;
        constituição = novaConstituição;
    }

    public Monstro()
    { }
    

    public void Ataque(PersonagemDND alvo)
    {
        Random random = new Random();

        int ataqueTentativa = random.Next(1, 20) + acerto;

        Console.WriteLine(nome + " desferiu um golpe com sua espada!");
        Console.WriteLine("Sua rolagem foi: " + (ataqueTentativa + acerto));

        if (ataqueTentativa > alvo.ac)
        {
            int Ataque = random.Next(4, 8) + força / 2;
            alvo.vida -= Ataque;
            Console.WriteLine($"{nome} acertou! {alvo.nome} perdeu {Ataque} pontos de vida");
        }
        else
        {
            Console.WriteLine($"{nome} errou!");
        }
    }

    public void AtaqueEspecial(PersonagemDND alvo)
    {
        Random random = new Random();

        if (chances > 0)
        {
            int ataqueTentativa = random.Next(1, 20) + acerto;

            Console.WriteLine(nome + " tenta desferir um golpe devastador, mirando nos seus pontos vitais!");
            Console.WriteLine("Sua rolagem foi: " + (ataqueTentativa + acerto));

            if (ataqueTentativa > alvo.ac)
            {
                int Ataque = random.Next(1, 8) + força / 2 + 10;
                alvo.vida -= Ataque;
                Console.WriteLine($"{nome } desfere uma estocada que perfura seu estomago!");
                Console.WriteLine($"{alvo.nome} perdeu {Ataque} pontos de vida");
            }
            else
            {
                Console.WriteLine(nome + " errou!");
            }

            chances = 0;
        }
        else if (chances == 0)
        {
            Console.WriteLine(nome + " tenta desferir um golpe devastador, mas falha devido a não possuir forças para realizar um segundo golpe que necessita de tamanho esforço!");
        }
    }

    public void Interagir(PersonagemDND alvo)
    {
        Random rand = new Random();
        int possibilidades = rand.Next(0, 99);

        if (possibilidades < 30)
        {
            Console.WriteLine($"{nome} encontra uma pedra e joga na direção de {alvo.nome}! {alvo.nome} recebe 5 de dano!");
            alvo.vida -= 5;
        }
        else if (possibilidades < 50)
        {
            Console.WriteLine($"Em meio ao combate, {nome} vê uma flecha indo na direção de {alvo.nome}! A flecha acerta em cheio, causando 10 de dano!");
            alvo.vida -= 10;
        }
        else if (possibilidades < 70)
        {
            Console.WriteLine($"Em meio ao combate, uma flecha vai na direção de {nome}! A flecha acerta em cheio, {nome} recebe 10 de dano!");
            vida -= 10;
        }
        else if (possibilidades < 90)
        {
            Console.WriteLine($"{nome} encontra uma garrafa de álcool jogada no meio do campo de batalha, {nome} a coleta e bebe! {nome} recupera 15 de vida!");
            vida += 15;
        }
        else if (possibilidades < 95)
        {
            if (escudo == 1)
            {
                Console.WriteLine($"{nome} encontra um guerreiro morto ao lado que estava portando um escudo! {nome} pega o escudo e recebe +1 de Armadura!");
                ac += 1;
                escudo -= 1;
            }
            else
            {
                Console.WriteLine($"{nome} esbarra em um corpo morto e cai no chão com as costas para cima! Com isso, {alvo.nome} aproveita a oportunidade para finalizar o combate!");
                vida = 0;
            }
        }
        else
        {
            if (espada == 1)
            {
                Console.WriteLine($"{nome} encontra um guerreiro caído que estava portando uma espada longa que ao longe parece ser mais eficiente que sua espada atual! {nome} pega a espada e recebe +2 de acerto!");
                acerto += 2;
                espada -= 1;
            }
            else
            {
                Console.WriteLine($"Uma flecha voa na direção de {alvo.nome} que não estava tão atento aos seus arredores. A flecha perfura o seu crânio, atravessando e finalizando-o!");
                alvo.vida = 0;
            }
        }
    }
}