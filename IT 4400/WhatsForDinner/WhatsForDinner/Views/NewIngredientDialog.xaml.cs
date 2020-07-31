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
    /// Interaction logic for NewRecipeComponentDialog.xaml
    /// </summary>
    public partial class NewIngredientDialog : UserControl
    {

        /// <summary>
        /// Dialog closing callback. 
        /// </summary>
        private Action<bool, QuantifiedIngredient> dialogClosingHandler;

        /// <summary>
        /// Initializes a <see cref="NewIngredientDialog"/>
        /// </summary>
        /// <param name="editting"> Determines if the <see cref="NameTextBox"/> is enabled or not.</param>
        public NewIngredientDialog(bool editting)
        {
            InitializeComponent();

            if (!editting)
            {
                this.NameTextBox.IsEnabled = false;
            }
            else
            {
                this.NameTextBox.IsEnabled = true;
            }
        }

        /// <summary>
        /// Intializes a <see cref="NewIngredientDialog"/>
        /// </summary>
        /// <param name="editting">Determines if the <see cref="NameTextBox"/> is enabled or not.</param>
        /// <param name="dialogClosingHandler"> Dialog closing callback.</param>
        public NewIngredientDialog(bool editting, Action<bool, QuantifiedIngredient> dialogClosingHandler) : this(editting)
        {
            this.dialogClosingHandler = dialogClosingHandler;
        }

        /// <summary>
        /// handles an already existing <paramref name="ingredient"/> and doesn't allow for editting
        /// of the ingredient name, just the quantity.
        /// </summary>
        /// <param name="ingredient"> Already existing ingredient</param>
        /// <param name="dialogClosingHandler"> dialog closing calllback</param>
        public NewIngredientDialog(Ingredient ingredient, Action<bool, QuantifiedIngredient> dialogClosingHandler) : this(false, dialogClosingHandler)
        {
            this.NameTextBox.Text = ingredient.Name;
        }


        /// <summary>
        /// Executes when the <see cref="SubmitButton"/> for the <see cref="NewIngredientDialog"/> is clicked.
        /// Generates ingredient, with quantity and units, from the form and executes the <see cref="dialogClosingHandler"/>.
        /// </summary>
        /// <param name="sender"> <see cref="SubmitButton"/></param>
        /// <param name="e"> event arguments</param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            QuantifiedIngredient ingredient = new QuantifiedIngredient(this.NameTextBox.Text)
            {
                Quantity = this.QuantityTextBox.Text,
                Units = this.UnitsComboBox.Text
            };

            this.dialogClosingHandler(false, ingredient);
            this.dialogClosingHandler = null;
        }

        /// <summary>
        /// Executes when the <see cref="CancelButton"/> for the <see cref="NewIngredientDialog"/> is clicked.
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
