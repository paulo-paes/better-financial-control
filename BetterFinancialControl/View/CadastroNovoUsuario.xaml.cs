using BetterFinancialControl.Repository;
using System.Xml.Schema;
using BetterFinancialControl.Model;
using CommunityToolkit.Maui.Alerts;

namespace BetterFinancialControl.View;

public partial class CadastroNovoUsuario : ContentPage
{
	public CadastroNovoUsuario()
	{
		InitializeComponent();
	}

	public bool ValidarCampos()
	{
		var repoUsuario = new UsuarioRepository();
		var usuario = new Usuario();
		usuario.Email = EmailEntry.Text;
		usuario.Senha = SenhaEntry.Text;

		if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(SenhaEntry.Text) || string.IsNullOrEmpty(ConfirmarSenhaEntry.Text))
		{
            DisplayAlert("Campos em branco", "Preencha os campos de login e senha!!", "OK");
            return false;
        }
		else
		{
            if (SenhaEntry.Text.Equals(ConfirmarSenhaEntry.Text))
            {
                if (repoUsuario.ObterUsuario(usuario.Email, usuario.Senha) == null)
                {
                    usuario.Email = EmailEntry.Text;
                    usuario.Senha = SenhaEntry.Text;
                    try
                    {
                        repoUsuario.CriarUsuario(usuario);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Erro", ex.Message, "OK");
                    }
                }
                else
                {
                    var toast = Toast.Make("Usuário já cadastrado");
                    toast.Show();
                }
                
            }
            else
            {
                var toast = Toast.Make("As senhas não coincidem, preencha os campos novamente");
                toast.Show();
            }
            
        }
		return false;
	}

    private async void CadastrarBtn_Clicked(object sender, EventArgs e)
    {
		if (ValidarCampos())
		{
            await Navigation.PopAsync();
		}
    }
}