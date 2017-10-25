using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Textblock de testes (somente para debug)
        TextBlock txtTeste = new TextBlock()
        {
            TextAlignment = TextAlignment.Center
        };

        #endregion

        #region Construtor


        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region Operação
        /// <summary>
        /// Pega o texto dentro do botão para saber qual operação matematica usar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Operacao(object sender, RoutedEventArgs e)
        {
            FiltroOperacao((sender as Button).Content.ToString());


        }

        /// <summary>
        /// Faz um teste para ver se o resultado pode ser exibido no txtValor1
        /// </summary>
        /// <param name="operador">Operador a ser adicionado</param>
        private void FiltroOperacao(string operador)
        {
            /* caso txtValor1 estiver vazio, o valor troca de lugar,
             * se não estiver vazio e txtValor2 não for zero,
             * txtValor1 mostra o resultado da conta. 
             */
            if (string.IsNullOrWhiteSpace(txtValor1.Text))  // teste para ver se o txtValor1 está vazio.
            {
                txtValor1.Text = txtValor2.Text + " " + operador; // txtValor1 recebe txtValor2 com o sinal operador na frente
                txtValor2.Text = "0"; // txtValor2 é resetado
            }
            else if (!txtValor2.Text.Equals("0")) // teste para ver se txtValor2 é zero
            {
                txtValor1.Text = VerOpção(txtValor1.Text) + " " + operador; // txtValor1 recebe o resultado da conta, mais o operador na frente
                txtValor2.Text = "0"; // txtValor2 é resetado
            }


            /* este teste foi criado para trocar o sinal do operador sem fazer a conta,
             * isso se txtValor2 estiver vazio.
             */
            if (!string.IsNullOrWhiteSpace(txtValor2.Text)) // teste para ver se txtValor2 está vazio
            {
                txtValor1.Text = txtValor1.Text.Remove(txtValor1.Text.Length - 1) + operador;
            }
        }
        #endregion

        #region Botão de Igual
        /// <summary>
        /// Completa a operação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Igual(object sender, RoutedEventArgs e)
        {
            // Teste para ativar o textblock de testes.
            if (txtValor2.Text.Equals("7452"))
            {
                ModoDebug();
                return;
            }

            /* txtValor2 mostra o resultado a operação e 
             * txtValor1 é resetado.
             */
            if (!string.IsNullOrWhiteSpace(txtValor1.Text))
            {
                txtValor2.Text = VerOpção(txtValor1.Text);
                txtValor1.Text = "";
            }
        }

        /// <summary>
        /// Função para ativar/desativar o textblock de testes
        /// </summary>
        private void ModoDebug()
        {
            /* Caso PainelTestes contem o txtTeste nele.
             * ele o remove, caso ele não tenha, ele o 
             * adiciona.
             */
            if (PainelTestes.Children.Contains(txtTeste))
                PainelTestes.Children.Remove(txtTeste);
            else
                PainelTestes.Children.Add(txtTeste);

        }


        #endregion

        #region Numeros
        /// <summary>
        /// Adiciona o numero de acordo com o texto do botão
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Numero(object sender, RoutedEventArgs e)
        {
            // Limita quantos caracteres pode ter
            if (txtValor2.Text.Length == 25)
                return;


            AdicionarNumero((sender as Button).Content.ToString());
        }

        /// <summary>
        /// Adiciona um numero ao txtValor2.
        /// </summary>
        /// <param name="input">Numero a ser adicionado</param>
        private void AdicionarNumero(string input)
        {
            /* Caso txtValor2 não for zero,
             * txtValor2 recebe ele mesmo mais o texto do input,
             * caso txtValor2 for zero, ele recebe o texto do input. 
             */
            if (txtValor2.Text != "0")
                txtValor2.Text += input;
            else
                txtValor2.Text = input;
        }
        #endregion

        #region Função VerOpção
        /// <summary>
        /// Faz a conta matematica de acordo com o sinal operador
        /// </summary>
        /// <param name="op">Sinal operador (+ - x /), ou texto que contenha um sinal operador</param>
        /// <returns>Retorna o resultado da operação em string</returns>
        private string VerOpção(string op)
        {

            string opcao = "0"; // refere-se ao operador matemático


            /* caso o tamanho do op for maior ou igual a 3, opcao recebe o ultimo
             * caractere da string 
             */
            if (op.Length >= 3)
                opcao = op.Substring(op.Length - 1);
            


            if (txtValor1.Text.Length < 3)
                return "0";

            double valor1 = double.Parse(txtValor1.Text.ToString().Substring(0, txtValor1.Text.Length - 1)),
                valor2 = double.Parse(txtValor2.Text);


            if (opcao.Equals("+"))
            {
                return $"{valor1 + valor2}";
            }
            else if (opcao.Equals("-"))
            {
                return $"{valor1 - valor2}";
            }
            else if (opcao.Equals("x"))
            {
                return $"{valor1 * valor2}";
            }
            else if (opcao.Equals("/"))
            {
                if (valor1 != 0 || valor2 != 0)
                {
                    return $"{valor1 / valor2}";
                }
            }

            return "0";

        }
        #endregion

        #region LimparTudo
        /// <summary>
        /// Limpa ambos os campos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimparTudo(object sender, RoutedEventArgs e)
        {
            txtValor1.Text = "";
            txtValor2.Text = "0";
            //limpar tudo
        }
        #endregion

        #region Apagar
        private void Apagar(object sender, RoutedEventArgs e)
        {
            if (txtValor2.Text == "" && txtValor1.Text != "")
            {
                txtValor2.Text = txtValor1.Text.Remove(txtValor1.Text.Length - 2);
                txtValor1.Text = "";
            }
            else if (txtValor2.Text != "")
            {
                txtValor2.Text = txtValor2.Text.Substring(0, txtValor2.Text.Length - 1);
            }

            //Apagar
        }
        #endregion

        #region Trocar Mais ou Menos
        /// <summary>
        /// Adiciona o sinal de negativo no numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaisOuMenos(object sender, RoutedEventArgs e)
        {
            if (!txtValor2.Text.Equals("0"))
                if (txtValor2.Text.IndexOf('-') == -1)
                {
                    txtValor2.Text = "-" + txtValor2.Text;
                }
                else if (txtValor2.Text.IndexOf('-') > -1)
                {
                    txtValor2.Text = txtValor2.Text.Substring(1);
                }
            //mais ou menos
        }
        #endregion

        #region Adicionar ponto

        /// <summary>
        /// Adiciona o ponto 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ponto(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValor2.Text))
                return;

            if (txtValor2.Text.IndexOf('.') == -1)
                txtValor2.Text += ".";
            //ponto
        }
        #endregion

        #region Tecla Prescionada
        /// <summary>
        /// Monitora quais teclas são precionadas e executa ações dependendo de qual seja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            txtTeste.Text = e.Key.ToString();
            char eChar = e.Key.ToString().ToCharArray()[e.Key.ToString().Length - 1];

            if ((char.IsDigit(eChar)) &&
                (e.Key.ToString().Substring(0, e.Key.ToString().Length - 1) == "NumPad" ||
                e.Key.ToString().Substring(0, 1) == "D" && e.Key.ToString().Length == 2))
            {
                if (txtValor2.Text.Length != 25)
                {
                    AdicionarNumero(eChar.ToString());
                }
            }
            else
            {
                txtTeste.Text = e.Key.ToString();
                if (e.Key == Key.Back)
                {
                    Apagar(null, null);
                    return;
                }
                if (e.Key == Key.Multiply)
                {
                    FiltroOperacao("x");
                    return;
                }
                if (e.Key == Key.OemPlus || e.Key == Key.Add)
                {
                    FiltroOperacao("+");
                    return;
                }
                if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
                {
                    FiltroOperacao("-");
                    return;
                }
                if (e.Key == Key.Divide || e.Key == Key.AbntC1)
                {
                    FiltroOperacao("/");
                    return;
                }
                if (e.Key == Key.AbntC2 || e.Key == Key.OemPeriod)
                {
                    Ponto(null, null);
                    return;
                }
                if (e.Key == Key.Enter)
                {
                    Igual(null, null);
                    return;
                }

            }
        }
        #endregion
    }
}
