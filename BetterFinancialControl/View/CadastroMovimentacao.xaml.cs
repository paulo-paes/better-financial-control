using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;

namespace BetterFinancialControl.View;

public partial class CadastroMovimentacao : ContentPage
{
	DateTime SelectedDate = DateTime.Now;
	public CadastroMovimentacao()
	{
		InitializeComponent();
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

    private void BtnSalvar_Clicked(object sender, EventArgs e)
    {
		var mov = new Movimentacao()
		{
			CategoriaId = (PickerCategoria.SelectedItem as Categoria).Id,
			FormaDePagamento = ObterFormaPagamento(),
			Tipo = SwitchTipo.IsToggled ? Tipo.Receita : Tipo.Despesa,
			Valor = Convert.ToDecimal(EntryValor.Text),
			Description = EntryDescricao.Text,
			Data = SelectedDate,
        };

		var repository = new MovimentacaoRepository();
		var menuPage = App.Current!.MainPage as MenuPage;
		repository.Criar(mov, menuPage!.usuarioAtual.Id);
		menuPage.CurrentPage = menuPage.Children[0];
    }

	private FormaDePagamento ObterFormaPagamento()
	{
        switch (PickerForma.SelectedItem as string)
		{
			case "Pix":
				return FormaDePagamento.Pix;
			case "Crédito":
				return FormaDePagamento.Credito;
			case "Debito":
				return FormaDePagamento.Debito;
			case "Transferência":
				return FormaDePagamento.Transferencia;
			case "Dinheiro":
				return FormaDePagamento.Dinheiro;
		}

		throw new Exception("Invalid forma de pagamento choice");
	}
}