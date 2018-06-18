using System.Collections.Generic;
using TuVotoCuenta.MasterDetail;
using TuVotoCuenta.Pages;
using TuVotoCuenta.Pages.FAQ;
using TuVotoCuenta.Pages.Search;
using TuVotoCuenta.Pages.Sponsors;

namespace TuVotoCuenta.Domain
{
    public class MenuListData : List<Group>
    {
        public MenuListData()
        {
            Group g1 = new Group("Menú Principal");

            g1.Add(new MasterPageItem
            {
                Title = "Inicio",
                IconSource = "svgdarkhome.svg",
                TargetType = typeof(WelcomePage)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Registro de casilla",
                IconSource = "svgdarkadd.svg",
                TargetType = typeof(Step1Page)
            });

            g1.Add(new MasterPageItem
            {
				Title = "Consulta de casilla",
                IconSource = "svgdarkview.svg",
                TargetType = typeof(SearchStep1Page)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Uso y privacidad",
                IconSource = "svgdarkprivacy.svg",
                TargetType = typeof(LegalConcernsPage)
            });

			g1.Add(new MasterPageItem
            {
                Title = "Preguntas Frecuentes (FAQ)",
                IconSource = "svgdarkfaq.svg",
                TargetType = typeof(FaqPage)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Patrocinadores",
                IconSource = "svgdarksponsor.svg",
                TargetType = typeof(SponsorsPage)
            });

			g1.Add(new MasterPageItem
            {
                Title = "Mi cuenta",
                IconSource = "svgdarkaccount.svg",
                TargetType = typeof(AccountPage)
            });

			g1.Add(new MasterPageItem
            {
                Title = "Desconectar",
                IconSource = "svgdarkpower.svg",
                TargetType = typeof(SignOutPage)
            });

            this.Add(g1);
        }
    }
}