using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using System.ComponentModel;

namespace BetterFinancialControl.View;

public partial class Categorias : ContentPage
{
	public Categorias()
	{
        InitializeComponent();
      
    }


	

    private async void adicionarCategoriaBtn_Clicked(object sender, EventArgs e)
    {
        CategoriaRepository categoriaRepository = new CategoriaRepository();
        MenuPage menuPage = App.Current?.MainPage as MenuPage;

        string categoriaDescricao;

        categoriaDescricao = await DisplayPromptAsync("Nova categoria", "Preencha a descrição da nova categoia:", initialValue: "", keyboard: Keyboard.Default);
      
        if (categoriaDescricao != null)
        {
            var novaCategoria = new Categoria();
            novaCategoria.UserId = menuPage.usuarioAtual.Id;
            novaCategoria.Nome = categoriaDescricao;

            try
            { 
                categoriaRepository.Criar(novaCategoria, menuPage.usuarioAtual.Id);
            }
            catch(Exception ex)
            {
                DisplayAlert("ERRO", ex.Message, "OK");
            }
        }
        listarCategorias();
    }
    
    public void listarCategorias()
    {
        CategoriaRepository categoriaRepository = new CategoriaRepository();
        MenuPage menuPage = App.Current?.MainPage as MenuPage;
        List<Categoria> categoriasCadastradas = new List<Categoria>();
        foreach(Categoria i in categoriaRepository.Buscar(menuPage.usuarioAtual.Id))
        {
            categoriasCadastradas.Add(i);
;       }
        categoriaListView.ItemsSource = categoriasCadastradas;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        listarCategorias();
    }
}