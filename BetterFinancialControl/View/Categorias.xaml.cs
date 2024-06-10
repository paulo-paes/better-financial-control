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
            i.Nome = i.Nome.ToUpper();
            categoriasCadastradas.Add(i);
;       }
        categoriaListView.ItemsSource = categoriasCadastradas;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        listarCategorias();
    }

    private async void Tap_Tapped(object sender, TappedEventArgs e)
    {
        var retorno = await DisplayActionSheet(
            "Editar ou Remover?", "Voltar", null, "Editar", "Remover");
        if (retorno != null)
        {
            if (retorno == "Remover")
            {
                var grid = (Grid)sender;
                if (grid.BindingContext is Categoria c)
                {
                    var repository = new CategoriaRepository();
                    repository.Excluir(c.Id);
                    
                }
            }
            else if (retorno == "Editar")
            {
                var grid = (Grid)sender;
                if (grid.BindingContext is Categoria c)
                {
                    var categoriaDescricao = await DisplayPromptAsync("Nova categoria", "Preencha a descrição da nova categoia:", initialValue: "", keyboard: Keyboard.Default); ;
                    if(categoriaDescricao != null){
                        var repository = new CategoriaRepository();
                        c.Nome = categoriaDescricao;
                        repository.Atualizar(c);
                    }
                }
            }
        }

        listarCategorias();
    }
}