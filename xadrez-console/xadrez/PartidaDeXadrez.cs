using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capituradas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capituradas = new HashSet<Peca>();
            terminada = false;
            colocarPeca();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapiturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapiturada != null)
            {
                capituradas.Add(pecaCapiturada);
            }
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capituradas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colococarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPeca()
        {
            colococarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colococarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colococarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colococarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colococarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colococarNovaPeca('e', 2, new Torre(tab, Cor.Branca));

            colococarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colococarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colococarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colococarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            colococarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colococarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
        }
    }
}
