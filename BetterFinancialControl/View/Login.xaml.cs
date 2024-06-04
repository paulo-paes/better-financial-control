using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using SQLite;
using BetterFinancialControl.Utils;
using CommunityToolkit.Maui.Alerts;

namespace BetterFinancialControl.View;

public partial class Login : ContentPage
{
    SQLiteConnection connection;
    public Login()
    {
        InitializeComponent();

        connection = new SQLiteConnection(Constantes.PathDB);
        connection.CreateTable<Usuario>();
        connection.CreateTable<Categoria>();
        connection.CreateTable<Movimentacao>();

    }

    public bool ValidarLogin()
    {
        var usuario = new UsuarioRepository();
        if (!(string.IsNullOrEmpty(LoginEntry.Text)|| string.IsNullOrEmpty(SenhaEntry.Text))) 
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
    
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CadastroNovoUsuario());
    }

    private async void EntrarBtn_Clicked(object sender, EventArgs e)
    {
        if (ValidarLogin())
        {
            if (App.Current != null)
            {
                App.Current.MainPage = new MenuPage();
            }
        }
        else
        {
            var toast = Toast.Make("Login ou senha inválidos");
            await toast.Show();
        }
    }
}