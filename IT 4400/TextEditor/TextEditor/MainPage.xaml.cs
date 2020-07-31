using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Popups;
using Windows.Storage.FileProperties;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TextEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TextDocument document = new TextDocument();

        public TextDocument Document
        {
            get
            {
                return this.document;
            }

            set
            {
                this.document.OnContentChanged -= this.UpdateSaveButton;
                this.document = value;
                this.UpdateSaveButton();
                this.document.OnContentChanged += this.UpdateSaveButton;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.New_Document_Click(null, null);
        }

        // Method that only allows txt files to open.
        private async void Open_File_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.FileTypeFilter.Add(".txt");

                StorageFile file = await openPicker.PickSingleFileAsync();

                if (file != null)
                {
                    this.Document = await TextDocument.ReadTextDocumentFromFileAsync(file);
                    this.Display.Text = this.document.Content;
                }
            }
            catch (Exception ex)
            {

                this.ShowMessage($"ERROR OCCURRED: {ex.Message}");
            }
        }

        private async void Save_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Document.TryGetFile(out StorageFile file);

                bool? overWrite = true;

                if (file != null)
                {
                    BasicProperties properties = await file.GetBasicPropertiesAsync();
                    if (properties.DateModified != this.Document.FileProperties.DateModified)
                    {
                        overWrite = await this.ShowOverWriteDialog(file.Name);
                    }
                }

                if (overWrite.HasValue == false)
                {
                    // User hit cancel.
                    return;
                }
                else if (overWrite.Value && file != null)
                {
                    this.ShowMessage(await this.Document.Save(file));
                }
                else
                {
                    this.SaveAs_ClickAsync(sender, e);
                }
            }
            catch (Exception ex)
            {

                this.ShowMessage($"ERROR OCCURRED: {ex.Message}");
            }
        }

        private async void SaveAs_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                FileSavePicker fileSavePicker = new FileSavePicker();

                // Suggest the location where the file should be saved
                //fileSavePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

                // The type of file to save
                fileSavePicker.FileTypeChoices.Add("Plain text", new List<string>() { ".txt" });

                if (this.Document.TryGetFile(out StorageFile file))
                {
                    fileSavePicker.SuggestedFileName = file.Name;
                }
                else
                {
                    // Default file name if one not given.
                    fileSavePicker.SuggestedFileName = "New Document";
                }

                file = await fileSavePicker.PickSaveFileAsync();
                if (file != null)
                {
                    this.Document.FileAccessToken = StorageApplicationPermissions.FutureAccessList.Add(file);

                    this.ShowMessage(await this.Document.Save(file));
                }
            }
            catch (Exception ex)
            {

                this.ShowMessage($"ERROR OCCURRED: {ex.Message}");
            }
        }

        private void New_Document_Click(object sender, RoutedEventArgs e)
        {
            this.Document = new TextDocument();
            this.Display.Text = this.Document.Content;
        }

        private async void ShowMessage(string message)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog(message);

            messageDialog.Commands.Add(new UICommand("Close"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private async Task<bool?> ShowOverWriteDialog(string fileName)
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog($"File '{fileName}' has been modified; Do you want to overwrite it?");
            bool? overWrite = null;

            // Adding commands to the message dialog which it will use to make buttons. 
            messageDialog.Commands.Add(new UICommand("Save", new UICommandInvokedHandler(
                (command) =>
                {
                    overWrite = true;
                })));

            messageDialog.Commands.Add(new UICommand("Save As New", new UICommandInvokedHandler(
                (command) =>
                {
                    overWrite = false;
                })));

            messageDialog.Commands.Add(new UICommand("Cancel"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 2;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 2;

            // Show the message dialog
            await messageDialog.ShowAsync();

            return overWrite;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            string aboutInfo = string.Empty;

            aboutInfo += "Nilin's Text Editor™\r\n";
            aboutInfo += "Author:       Nilin Reddrex\r\n";
            aboutInfo += "Course:       IT 4400\r\n";
            aboutInfo += "University:   Mizzou-Columbia\r\n";
            aboutInfo += "Company:    Reddrex Technologies\r\n";
            this.ShowMessage(aboutInfo);
        }

        private void Display_TextChanged(object sender, RoutedEventArgs e)
        {
            this.Document.Content = (sender as TextBox).Text;
        }

        private void UpdateSaveButton()
        {
            this.SaveOption.IsEnabled = this.Document.IsModified;
        }

        private void Display_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Tab)
            {
                int cursorLoc = this.Display.SelectionStart;
                this.Display.Text = this.Display.Text.Insert(cursorLoc, "    ");
                this.Display.SelectionStart = cursorLoc + 4;
                e.Handled = true;

            }
            
        }
    }
}
