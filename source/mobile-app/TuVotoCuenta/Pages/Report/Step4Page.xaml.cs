using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;

namespace TuVotoCuenta.Pages
{
	public partial class Step4Page : StepPage
    {
        public Step4Page()
        {
            InitializeComponent();
			BindingContext = new Step4ViewModel(this.Navigation);
        }      
    }
}