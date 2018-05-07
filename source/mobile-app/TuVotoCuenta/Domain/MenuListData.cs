using System.Collections.Generic;
using TuVotoCuenta.MasterDetail;
using TuVotoCuenta.Pages;

namespace TuVotoCuenta.Domain
{
    public class MenuListData : List<Group>
    {
        public MenuListData()
        {
            Group g1 = new Group("Información de casillas");

            g1.Add(new MasterPageItem
            {
                Title = "Inicio",
                IconSource = "svgdarkhome.svg",
                TargetType = typeof(WelcomePage)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Registro",
                IconSource = "svgdarkadd.svg",
                TargetType = typeof(Step1Page)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Consulta",
                IconSource = "svgdarkview.svg",
                TargetType = typeof(SettingsPage)
            });

            g1.Add(new MasterPageItem
            {
                Title = "Uso y privacidad",
                IconSource = "svgdarkprivacy.svg",
                TargetType = typeof(LegalConcernsPage)
            });

            this.Add(g1);
        }
    }
}