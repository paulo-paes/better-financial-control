using BetterFinancialControl.Repository;

namespace BetterFinancialControl.View;

public partial class Login : ContentPage
{
	public bool ValidarLogin()
    {
        var usuario = new UsuarioRepository();
        if (!(LoginEntry.Text == "" || SenhaEntry.Text == "" || LoginEntry.Text == null || SenhaEntry.Text == null)) 
        {
            try
            {
                return usuario.VerificarUsuario(LoginEntry.Text, SenhaEntry.Text);
            }
            catch (Exception e) {
                DisplayAlert("Erro", e.Message,"OK");
            }               
        }
        else
        {
            DisplayAlert("Campos em branco", "Preencha os campos de login e senha!!", "OK");
            return false;
        }
        return false;
    }
    public Login()
	{
		InitializeComponent();
        
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CadastroNovoUsuario());
    }

    private async void EntrarBtn_Clicked(object sender, EventArgs e)
    {
        if (ValidarLogin())
        {
            await Navigation.PushAsync(new MenuPage());
        }
    }
}