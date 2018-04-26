using Rota.ViewModels;
using TK.CustomMap.Overlays;
using Xamarin.Forms;


namespace Rota.Pages
{
    public partial class HtmlInstructionsPage : ContentPage
    {
        public HtmlInstructionsPage(TKRoute route)
        {
            InitializeComponent();

            BindingContext = new HtmlInstructionsViewModel(route);
        }
    }
}
