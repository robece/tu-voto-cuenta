using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;
using Xamarin.Forms;

namespace TuVotoCuenta.Pages
{
	public partial class Step3Page : StepPage
    {
        public Step3Page()
        {
            InitializeComponent();
			BindingContext = new Step3ViewModel(this.Navigation);
        }

		public override void UnfocusSave()
        {
            base.UnfocusSave();
            ((Step3ViewModel)BindingContext).Save();
        }
    }
}