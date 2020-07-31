using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsForDinner.Controllers;

namespace WhatsForDinner.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main business logic controller
        /// </summary>
        private readonly Controller controller;

        /// <summary>
        /// Initializes a new <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
           // Creates a new controller and passes a function allowing the controller to
           // insert new views into the maingrid.
            this.controller = new Controller(this.LoadView);
        }

        /// <summary>
        /// Clears currently shown view and injects the <paramref name="view"/> into the <see cref="MainGrid"/>.
        /// </summary>
        /// <param name="view">The view to be displayed.</param>
        private void LoadView( UserControl view)
        {
            this.MainGrid.Children.Clear();
            this.MainGrid.Children.Add(view);
        }
      
    }
}
