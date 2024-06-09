using BetterFinancialControl.Model;

namespace BetterFinancialControl.View;

public partial class MenuPage : TabbedPage
{
	public Usuario usuarioAtual;
	public MenuPage(Usuario usuarioAtual)
    {
		this.usuarioAtual = usuarioAtual;
		InitializeComponent();
	}
}