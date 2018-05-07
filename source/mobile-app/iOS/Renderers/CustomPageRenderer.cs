using TuVotoCuenta.iOS.Renderers;
using TuVotoCuenta.Pages;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TuVotoCuenta.Controls;

[assembly: ExportRenderer(typeof(StepPage), typeof(CustomPageRenderer))]

namespace TuVotoCuenta.iOS.Renderers
{
    public class CustomPageRenderer : PageRenderer
    {
        public new StepPage Element
        {
			get { return (StepPage)base.Element; }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var rightNavList = new List<UIBarButtonItem>();

            var navigationItem = this.NavigationController.TopViewController.NavigationItem;

            for (var i = 0; i < Element.ToolbarItems.Count; i++)
            {
                var reorder = (Element.ToolbarItems.Count - 1);
                var ItemPriority = Element.ToolbarItems[reorder - i].Priority;

                if (ItemPriority == 1)
                {
                    UIBarButtonItem RightNavItems = navigationItem.RightBarButtonItems[i];
                    rightNavList.Add(RightNavItems);
                }
            }

            navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);
        }
    }
}