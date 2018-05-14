using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;

namespace TuVotoCuenta.Pages
{
    public partial class Step1Page : StepPage
    {
        public Step1Page()
        {
            InitializeComponent();
			BindingContext = new Step1ViewModel(this.Navigation);
        }

		public override void UnfocusSave()
		{
			base.UnfocusSave();
			((Step1ViewModel)BindingContext).Save();
		}
	}
}