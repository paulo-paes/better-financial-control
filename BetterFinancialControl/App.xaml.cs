using BetterFinancialControl.View;
namespace BetterFinancialControl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Login();
        }
    }
}
