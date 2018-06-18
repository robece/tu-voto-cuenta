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

            imPt.Source = "pt.jpg";
            await Task.Delay(10);
            imPan.Source = "pan.jpg";
            await Task.Delay(10);
            imPes.Source = "pes.jpg";
            await Task.Delay(10);
            imPmc.Source = "pmc.jpg";
            await Task.Delay(10);
            imPna.Source = "pna.jpg";
            await Task.Delay(10);
            imPrd.Source = "prd.jpg";
            await Task.Delay(10);
            imPri.Source = "pri.jpg";
            await Task.Delay(10);
            imPtes.Source = "ptes";
            await Task.Delay(10);
            imPvem.Source = "pvem.jpg";
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
            imPmorena.Source = "pmorena.jpg";
            await Task.Delay(10);
            imPtesmor.Source = "ptesmor";
            await Task.Delay(10);
            imPrdpanmc.Source = "prdpanmc";
            await Task.Delay(10);
            imPriverna.Source = "priverna";
            await Task.Delay(10);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            imPt.Source = null;
            imPan.Source = null;
            imPes.Source = null;
            imPmc.Source = null;
            imPna.Source = null;
            imPrd.Source = null;
            imPri.Source = null;
            imPtes.Source = null;
            imPvem.Source = null;
            imPanmc.Source = null;
            imPrdmc.Source = null;
            imPrina.Source = null;
            imPanprd.Source = null;
            imPmores.Source = null;
            imPmorpt.Source = null;
            imPriver.Source = null;
            imPverna.Source = null;
            imPmorena.Source = null;
            imPtesmor.Source = null;
            imPrdpanmc.Source = null;
            imPriverna.Source = null;

        }

    }
}