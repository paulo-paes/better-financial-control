using BetterFinancialControl.Model;
using BetterFinancialControl.Repository;
using SQLite;
using BetterFinancialControl.Utils;
namespace BetterFinancialControl
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        SQLiteConnection connection;

        public MainPage()
        {
            InitializeComponent();
            connection = new SQLiteConnection(Constantes.PathDB);
            connection.CreateTable<Usuario>();
            connection.CreateTable<Categoria>();
            connection.CreateTable<Movimentacao>();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CategoriaRepository repository = new CategoriaRepository();

            var categoria = new Categoria();
            categoria.Nome = "Primeira";

            repository.Criar(categoria, 1);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            MovimentacaoRepository repository = new MovimentacaoRepository();
            CategoriaRepository rep = new CategoriaRepository();
            var receita = new Movimentacao();
            receita.Tipo = Tipo.Receita;
            receita.Valor = 100;
            receita.Data = DateTime.Now;
            receita.CategoriaId = (rep.Buscar(1))[0].Id;
            receita.Description = "teste";

            repository.Criar(receita, 1);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            DashboardRepository rep = new DashboardRepository();
            var dash = rep.Buscar(DateTime.Now, 1);
            lblSaldo.Text = dash.Saldo.ToString();
            collectDespesa.ItemsSource = dash.Movimentacoes;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            MovimentacaoRepository repository = new MovimentacaoRepository();
            CategoriaRepository rep = new CategoriaRepository();
            var despesa = new Movimentacao();
            despesa.Tipo = Tipo.Despesa;
            despesa.Valor = 50;
            despesa.Data = DateTime.Now;
            despesa.CategoriaId = (rep.Buscar(1))[0].Id;
            despesa.Description = "teste";
            repository.Criar(despesa, 1);
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            UsuarioRepository repository = new UsuarioRepository();
            Usuario user = new(){ Email = "first@user.com", Senha = "123" };
            repository.CriarUsuario(user);
        }
    }

}
