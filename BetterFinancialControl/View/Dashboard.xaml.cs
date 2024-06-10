using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;

namespace BetterFinancialControl.View;

public partial class Dashboard : ContentPage
{

	public DashboardModel dash = new DashboardModel();
	public Dashboard()
	{
		InitializeComponent();
	}

	public void BuscarDados(DateTime date)
	{
        var repository = new DashboardRepository();
        var menuPage = App.Current?.MainPage as MenuPage;
        dash = repository.Buscar(date, menuPage!.usuarioAtual.Id);
        lblSaldo.Text = dash.Saldo.ToString();
        lblBalanco.Text = dash.Balanco.ToString();
        dashboardView.ItemsSource = dash.Movimentacoes;
    }

    public void BuscarDados()
    {
        var repository = new DashboardRepository();
        var menuPage = App.Current?.MainPage as MenuPage;
        dash = repository.Buscar(DateTime.Now, menuPage!.usuarioAtual.Id);
        lblSaldo.Text = dash.Saldo.ToString();

        lblBalanco.Text = dash.Balanco.ToString();
        dashboardView.ItemsSource = dash.Movimentacoes;
    }

    private void PickerMonth_DateSelected(object sender, DateChangedEventArgs e)
    {
        BuscarDados(e.NewDate);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

        BuscarDados();
    }

    private async void Tap_Tapped(object sender, TappedEventArgs e)
    {
        var retorno = await DisplayActionSheet(
            "Editar ou Remover?", "Voltar", null, "Editar", "Remover");
        if (retorno != null) {
            if (retorno == "Remover")
            {
                var grid = (Grid)sender;
                if (grid.BindingContext is Movimentacao m)
                {
                    var repository = new MovimentacaoRepository();
                    repository.Excluir(m.Id);
                    BuscarDados();
                }
            } else if(retorno == "Editar")
            {
                var grid = (Grid)sender;
                if (grid.BindingContext is Movimentacao m)
                {
                    await Navigation.PushModalAsync(new CadastroMovimentacao(m));
                }
            }
        } 
    }

    private void ContentPage_Focused(object sender, FocusEventArgs e)
    {
        BuscarDados();
    }
}