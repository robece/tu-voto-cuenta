using System.Threading.Tasks;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            imPt.Source = "pt";
            await Task.Delay(10);
            imPan.Source = "pan";
            await Task.Delay(10);
            imPes.Source = "pes";
            await Task.Delay(10);
            imPmc.Source = "pmc";
            await Task.Delay(10);
            imPna.Source = "pna";
            await Task.Delay(10);
            imPrd.Source = "prd";
            await Task.Delay(10);
            imPri.Source = "pri";
            await Task.Delay(10);
            imPtes.Source = "ptes";
            await Task.Delay(10);
            imPvem.Source = "pvem";
            await Task.Delay(10);
            imPanmc.Source = "panmc";
            await Task.Delay(10);
            imPrdmc.Source = "prdmc";
            await Task.Delay(10);
            imPrina.Source = "prina";
            await Task.Delay(10);
            imPanprd.Source = "panprd";
            await Task.Delay(10);
            imPmores.Source = "pmores";
            await Task.Delay(10);
            imPmorpt.Source = "pmorpt";
            await Task.Delay(10);
            imPriver.Source = "priver";
            await Task.Delay(10);
            imPverna.Source = "pverna";
            await Task.Delay(10);
            imPmorena.Source = "pmorena";
            await Task.Delay(10);
            imPtesmor.Source = "ptesmor";
            await Task.Delay(10);
            imPrdpanmc.Source = "prdpanmc";
            await Task.Delay(10);
            imPriverna.Source = "priverna";
            await Task.Delay(10);

        }

    }
}