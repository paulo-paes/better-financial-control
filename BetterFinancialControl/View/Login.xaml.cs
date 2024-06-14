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

    public Usuario ValidarLogin()
    {
        var usuario = new UsuarioRepository();
        if (!(string.IsNullOrEmpty(LoginEntry.Text)|| string.IsNullOrEmpty(SenhaEntry.Text))) 
        {
            try
            {
                return usuario.ObterUsuario(LoginEntry.Text, SenhaEntry.Text);
            }
            catch (Exception e) {
                DisplayAlert("Erro", e.Message,"OK");
            }               
        }
        else
        {
            var toast = Toast.Make("Preencha os campos de login e senha!!");
            toast.Show();
            return null;
        }
        return null;
    }
    
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CadastroNovoUsuario());
    }

    private async void EntrarBtn_Clicked(object sender, EventArgs e)
    {
       Usuario usuarioAtual = ValidarLogin();
        if (usuarioAtual != null)
        {
            if (App.Current != null)
            {
                App.Current.MainPage = new MenuPage(usuarioAtual);
            }
        }
        else
        {
            var toast = Toast.Make("Login ou senha inválidos");
            await toast.Show();
        }
    }
}