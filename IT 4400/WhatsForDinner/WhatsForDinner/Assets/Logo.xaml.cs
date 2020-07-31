using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsForDinner.Models;

namespace WhatsForDinner.Assets
{
    /// <summary>
    /// Interaction logic for Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        private double initialWidth;
        public Logo()
        {
            InitializeComponent();
            this.Loaded += this.Logo_Loaded;
        }

        private void Logo_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.Logo_Loaded;
            this.initialWidth = this.ImageViewBox.ActualWidth;

            // Make the food dance. :P
            DoubleAnimation widthAnimation =
                new DoubleAnimation(this.initialWidth, this.initialWidth - 100, TimeSpan.FromSeconds(1));
            widthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            widthAnimation.AutoReverse = true;
            this.ImageViewBox.BeginAnimation(Viewbox.WidthProperty, widthAnimation);
        }
    }
}
