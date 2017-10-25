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

        private void FiltroOperacao(string texto)
        {
            if (string.IsNullOrWhiteSpace(txtValor1.Text))
            {
                txtValor1.Text = txtValor2.Text + " " + texto;
                txtValor2.Text = "0";
            }
            else if (txtValor2.Text != "0")
            {
                txtValor1.Text = VerOpção(txtValor1.Text) + " " + texto;
                txtValor2.Text = "0";
            }

            if (!string.IsNullOrWhiteSpace(txtValor2.Text))
            {
                txtValor1.Text = txtValor1.Text.Remove(txtValor1.Text.Length - 1) + texto;
            }
        }
        #endregion

        #region Botão de Igual
        private void Igual(object sender, RoutedEventArgs e)
        {
            if (txtValor2.Text.Equals("7452"))
            {
                ModoDebug();



                return;
            }


            if (!string.IsNullOrWhiteSpace(txtValor1.Text))
            {
                txtValor2.Text = VerOpção(txtValor1.Text);
                txtValor1.Text = "";
            }
        }

        private void ModoDebug()
        {
            if (PainelTestes.Children.Contains(txtTeste))
                PainelTestes.Children.Remove(txtTeste);
            else
                PainelTestes.Children.Add(txtTeste);

        }


        #endregion

        #region Numeros
        private void Numero(object sender, RoutedEventArgs e)
        {
            if (txtValor2.Text.Length == 25)
                return;
            AdicionarNumero((sender as Button).Content.ToString());
        }

        private void AdicionarNumero(string input)
        {
            if (txtValor2.Text != "0")
                txtValor2.Text += input;
            else
                txtValor2.Text = input;
        }
        #endregion

        #region Função VerOpção
        private string VerOpção(string op)
        {
            string opcao = "0";

            if (op.Length >= 3)
                opcao = op.Substring(op.Length - 1);

            double valor1 = double.Parse(txtValor1.Text.ToString().Substring(0, txtValor1.Text.Length - 1)),
                valor2 = double.Parse(txtValor2.Text);


            if (op.IndexOf('+') > 0)
            {
                return $"{valor1 + valor2}";
            }
            else if (op.IndexOf('-') > 0)
            {
                return $"{valor1 - valor2}";
            }
            else if (op.IndexOf('x') > 0)
            {
                return $"{valor1 * valor2}";
            }
            else if (op.IndexOf('/') > 0)
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
        private void MaisOuMenos(object sender, RoutedEventArgs e)
        {
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
        private void Ponto(object sender, RoutedEventArgs e)
        {
            if (txtValor2.Text.IndexOf('.') == -1)
                txtValor2.Text += ".";
            //ponto
        }
        #endregion

        #region Tecla Prescionada
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            txtTeste.Text = e.Key.ToString();
            char eChar = e.Key.ToString().ToCharArray()[e.Key.ToString().Length - 1];

            if ((char.IsDigit(eChar)) && 
                (e.Key.ToString().Substring(0, e.Key.ToString().Length -1) == "NumPad" ||
                e.Key.ToString().Substring(0, 1) == "D" && e.Key.ToString().Length == 2 )   )
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
                }
            }
        }
        #endregion
    }
}
