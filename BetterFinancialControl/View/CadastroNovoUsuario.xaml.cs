using BetterFinancialControl.Repository;
using System.Xml.Schema;
using BetterFinancialControl.Model;

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

		if (!(string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(SenhaEntry.Text) || string.IsNullOrEmpty(ConfirmarSenhaEntry.Text)))
		{
			if (SenhaEntry.Text == SenhaEntry.Text)
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
		}
		else
		{
            DisplayAlert("Campos em branco", "Preencha os campos de login e senha!!", "OK");
            return false;
        }
		return false;
	}

    private void CadastrarBtn_Clicked(object sender, EventArgs e)
    {
		if (ValidarCampos())
		{
			DisplayAlert("Sucesso", "Novo Login cadastrado com sucesso", "OK");
		}
    }
}