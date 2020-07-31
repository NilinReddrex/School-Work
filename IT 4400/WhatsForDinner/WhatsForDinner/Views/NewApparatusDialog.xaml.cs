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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsForDinner.Models;

namespace WhatsForDinner.Views
{
    /// <summary>
    /// Interaction logic for NewApparatusDialog.xaml
    /// </summary>
    public partial class NewApparatusDialog : UserControl
    {
        /// <summary>
        /// Dialog closing callback. 
        /// </summary>
        private Action<bool, Apparatus> dialogClosingHandler;


        /// <summary>
        /// Initializes a <see cref="NewApparatusDialog"/>
        /// </summary>
        /// <param name="editting">Determines if the <see cref="ApparatusNameTextBox"/> is enabled or not.</param>
        public NewApparatusDialog( bool editting)
        {
            InitializeComponent();

            if(!editting)
            {
                this.ApparatusNameTextBox.IsEnabled = false;
            }
            else
            {
                this.ApparatusNameTextBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// Intializes a <see cref="NewApparatusDialog"/>.
        /// </summary>
        /// <param name="editting"> Determines if the <see cref="ApparatusNameTextBox"/> is enabled or not.</param>
        /// <param name="dialogClosingHandler"> Dialog Closing callback.</param>
        public NewApparatusDialog(bool editting, Action<bool, Apparatus> dialogClosingHandler) :this(editting)
        {
            this.dialogClosingHandler = dialogClosingHandler;
        }

        /// <summary>
        /// Executes when the <see cref="SubmitButton"/> for the <see cref="NewApparatusDialog"/> is clicked.
        /// Generates apparartus from form and executes <see cref="dialogClosingHandler"/>.
        /// </summary>
        /// <param name="sender"><see cref="SubmitButton"/></param>
        /// <param name="e"> event arguments</param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Apparatus apparatus = new Apparatus(this.ApparatusNameTextBox.Text)
            {
                Name = this.ApparatusNameTextBox.Text
            };

            this.dialogClosingHandler(false, apparatus);
            this.dialogClosingHandler = null;
        }

        /// <summary>
        /// Executes when the <see cref="CancelButton"/> for the <see cref="NewApparatusDialog"/> is clicked.
        /// Executes the <see cref="dialogClosingHandler"/> as cancelled.
        /// </summary>
        /// <param name="sender"><see cref="CancelButton"/></param>
        /// <param name="e"> event arguments</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.dialogClosingHandler(true, null);
            this.dialogClosingHandler = null;
        }
    }
}
