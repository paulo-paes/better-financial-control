using BetterFinancialControl.Repository;

namespace BetterFinancialControl.View;

public partial class Dashboard : ContentPage
{
	public Dashboard()
	{
		InitializeComponent();
/*		var repository = new DashboardRepository();
		var dashboard = repository.Buscar(DateTime.Now, 1);
		dashboardView.ItemsSource = dashboard.Movimentacoes;*/
	}
}