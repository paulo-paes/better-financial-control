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
        dash = repository.Buscar(date, 1);
        lblSaldo.Text = dash.Saldo.ToString();
        lblBalanco.Text = dash.Balanco.ToString();
        dashboardView.ItemsSource = dash.Movimentacoes;
    }

    public void BuscarDados()
    {
        var repository = new DashboardRepository();
        dash = repository.Buscar(DateTime.Now, 1);
        lblSaldo.Text = dash.Saldo.ToString();

        lblBalanco.Text = dash.Balanco.ToString();
        dashboardView.ItemsSource = dash.Movimentacoes;
    }

    private void PickerMonth_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        BuscarDados(selectedDate);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

        BuscarDados();
    }
}