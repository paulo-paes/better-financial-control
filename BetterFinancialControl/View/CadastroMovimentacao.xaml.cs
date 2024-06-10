using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using Microsoft.Maui.Controls;
using System;

namespace BetterFinancialControl.View
{
    public partial class CadastroMovimentacao : ContentPage
    {
        DateTime SelectedDate = DateTime.Now;

        public CadastroMovimentacao()
        {
            InitializeComponent();
        }

        public CadastroMovimentacao(Movimentacao mov)
        {
            InitializeComponent();

            EntryDescricao.Text = mov.Description;
            EntryValor.Text = mov.Valor.ToString();
            SwitchTipo.IsToggled = mov.Tipo == Tipo.Receita ? true : false;
            PickerForma.ItemsSource.IndexOf(mov.FormaDePagamento);

            int i = 0;
            foreach (Categoria item in PickerCategoria.ItemsSource)
            {
                if (item.Id == mov.CategoriaId)
                {
                    PickerCategoria.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        public void CarregarCategorias()
        {
            var repository = new CategoriaRepository();
            var menuPage = App.Current!.MainPage as MenuPage;
            var categorias = repository.Buscar(menuPage!.usuarioAtual.Id);
            PickerCategoria.ItemsSource = categorias;
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            CarregarCategorias();
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            if (PickerCategoria.SelectedItem == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione uma categoria.", "OK");
                return;
            }


            if (PickerForma.SelectedItem == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione uma forma de pagamento.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EntryValor.Text) || !decimal.TryParse(EntryValor.Text, out decimal valor))
            {
                await DisplayAlert("Erro", "Por favor, insira um valor válido.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(EntryDescricao.Text))
            {
                await DisplayAlert("Erro", "Por favor, insira uma descrição.", "OK");
                return;
            }

            var mov = new Movimentacao()
            {
                CategoriaId = (PickerCategoria.SelectedItem as Categoria).Id,
                FormaDePagamento = ObterFormaPagamento(),
                Tipo = SwitchTipo.IsToggled ? Tipo.Receita : Tipo.Despesa,
                Valor = valor,
                Description = EntryDescricao.Text,
                Data = SelectedDate,
            };

            var repository = new MovimentacaoRepository();
            var menuPage = App.Current!.MainPage as MenuPage;
            repository.Criar(mov, menuPage!.usuarioAtual.Id);
            Limpar();
            menuPage.CurrentPage = menuPage.Children[0];
        }

        private void Limpar()
        {
            EntryDescricao.Text = "";
            EntryValor.Text = "";
            SelectedDate = DateTime.Now;
            PickerForma.SelectedItem = null;
            PickerCategoria.SelectedItem = null;
        }

        private FormaDePagamento ObterFormaPagamento()
        {
            switch (PickerForma.SelectedItem as string)
            {
                case "Pix":
                    return FormaDePagamento.Pix;
                case "Crédito":
                    return FormaDePagamento.Credito;
                case "Débito":
                    return FormaDePagamento.Debito;
                case "Transferência":
                    return FormaDePagamento.Transferencia;
                case "Dinheiro":
                    return FormaDePagamento.Dinheiro;
                default:
                    throw new Exception("Escolha de forma de pagamento inválida");
            }
        }
    }
}
