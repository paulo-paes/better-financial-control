using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System;

namespace BetterFinancialControl.View
{
    public partial class CadastroMovimentacao : ContentPage
    {
        DateTime SelectedDate = DateTime.Now;
        private bool editionMode = false;
        private Movimentacao editMov;

        public CadastroMovimentacao()
        {
            InitializeComponent();
        }

        public CadastroMovimentacao(Movimentacao mov)
        {
            InitializeComponent();
            editionMode = true;
            editMov = mov;
        }

        public void CarregarCategorias()
        {
            var repository = new CategoriaRepository();
            var menuPage = App.Current!.MainPage as MenuPage;
            var categorias = repository.Buscar(menuPage!.usuarioAtual.Id);
            PickerCategoria.ItemsSource = categorias;
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            CarregarCategorias();

            if (editionMode)
            {
                EntryDescricao.Text = editMov.Description;
                EntryValor.Text = editMov.Valor.ToString();
                SwitchTipo.IsToggled = editMov.Tipo == Tipo.Receita;
                PickerForma.SelectedIndex = PickerForma.ItemsSource.IndexOf(editMov.FormaDePagamento.ToString());
                DataMovimentacao.Date = editMov.Data;

                int i = 0;
                foreach (Categoria item in PickerCategoria.ItemsSource)
                {
                    if (item.Id == editMov.CategoriaId)
                    {
                        PickerCategoria.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
            }
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
                Data = DataMovimentacao.Date,
            };

            var repository = new MovimentacaoRepository();
            var menuPage = App.Current!.MainPage as MenuPage;
            if (editionMode)
            {
                mov.Id = editMov.Id;
                repository.Atualizar(mov);
                await Navigation.PopModalAsync();
                return;
            }
            repository.Criar(mov, menuPage!.usuarioAtual.Id);
            Limpar();
            menuPage.CurrentPage = menuPage.Children[0];
        }

        private void Limpar()
        {
            EntryDescricao.Text = "";
            EntryValor.Text = "";
            DataMovimentacao.Date = DateTime.Now;
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
