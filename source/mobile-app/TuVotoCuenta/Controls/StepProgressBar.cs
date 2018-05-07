using System;

using Xamarin.Forms;

namespace TuVotoCuenta.Controls
{
    public class StepProgressBar1 : StepProgressBarBase
    {
        public StepProgressBar1() : base(4, 1) { }
    }

    public class StepProgressBar2 : StepProgressBarBase
    {
        public StepProgressBar2() : base(4, 2) { }
    }

    public class StepProgressBar3 : StepProgressBarBase
    {
        public StepProgressBar3() : base(4, 3) { }
    }

    public class StepProgressBar4 : StepProgressBarBase
    {
        public StepProgressBar4() : base(4, 4) { }
    }

    public class StepProgressBarBase : ContentView
    {
        int steps = 0;
        int currentStep = 0;

        public StepProgressBarBase(int steps, int currentStep)
        {
            this.steps = steps;
            this.currentStep = currentStep;
            InitiliazeComponent();
            BindingContext = this;
        }

        void InitiliazeComponent()
        {
            StackLayout layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical
            };

            Label message = new Label()
            {
                Style = Application.Current.Resources["StepBarTitle"] as Style,
                Text = "Avance del proceso"
            };

            Grid grid = new Grid()
            {
                Margin = new Thickness(0, 0, 0, 20)
            };

            RowDefinition rd = new RowDefinition()
            {
                Height = 15
            };
            grid.RowDefinitions.Add(rd);

            double total = (double)1 / steps;
            for (int i = 0; i < steps; i++)
            {
                ColumnDefinition cd = new ColumnDefinition()
                {
                    Width = new GridLength(total, GridUnitType.Star)
                };

                grid.ColumnDefinitions.Add(cd);

                Label lbl = new Label();

                if (i <= currentStep - 1)
                {
                    lbl.Style = Application.Current.Resources["ProgressBarStepSelectorDark"] as Style;
                }
                else
                {
                    lbl.Style = Application.Current.Resources["ProgressBarStepSelectorLight"] as Style;
                }

                grid.Children.Add(lbl);

                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, i);
            }

            layout.Children.Add(message);
            layout.Children.Add(grid);
            Content = layout;
        }
    }
}
