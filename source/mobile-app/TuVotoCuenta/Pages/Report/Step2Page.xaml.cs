using TuVotoCuenta.Controls;
using TuVotoCuenta.ViewModels;

namespace TuVotoCuenta.Pages
{
	public partial class Step2Page : StepPage
    {      
        public Step2Page()
        {
            InitializeComponent();
			BindingContext = new Step2ViewModel(this.Navigation);
        }

		public override void UnfocusSave()
        {
            base.UnfocusSave();
            ((Step2ViewModel)BindingContext).Save();
        }
    }
}