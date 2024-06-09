using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;

namespace BetterFinancialControl.View;

public partial class Dashboard : ContentPage
{

	public DashboardModel dash = null;
	public Dashboard()
	{
		InitializeComponent();
		BuscarDados();
	}

	public void BuscarDados()
	{
        var repository = new DashboardRepository();
        dash = repository.Buscar(DateTime.Now, 1);
        lblSaldo.Text = dash.Saldo.ToString();
        dashboardView.ItemsSource = dash.Movimentacoes;
    }
}