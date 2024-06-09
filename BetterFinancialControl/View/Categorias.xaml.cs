using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using System.ComponentModel;

namespace BetterFinancialControl.View;

public partial class Categorias : ContentPage
{
	public CategoriaRepository categoriaRep;
	public Categorias()
	{
		InitializeComponent();
	}
	public void listarCategorias()
	{
		List<Categoria> listaCategorias = new List<Categoria>();
		
	
	}
}