using System;
using Xamarin.Forms;

namespace TuVotoCuenta.Controls
{
	public class HyperLinkLabel : Label
    {
        public static readonly BindableProperty UriToNavigateProperty =
            BindableProperty.Create(nameof(UriToNavigate), typeof(Uri), typeof(HyperLinkLabel), null);

        public Uri UriToNavigate
        {
            get { return (Uri)base.GetValue(UriToNavigateProperty); }
            set { base.SetValue(UriToNavigateProperty, value); }
        }
    }
}
