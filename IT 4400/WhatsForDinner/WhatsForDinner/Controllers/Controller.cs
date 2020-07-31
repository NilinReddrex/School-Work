using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using WhatsForDinner.Assets;
using WhatsForDinner.Models;
using WhatsForDinner.Views;

namespace WhatsForDinner.Controllers
{
    public class Controller
    {
        public const string CookBookView = "cookbookview";
        public const string Logo = "logo";
        private readonly Action<UserControl> loadMainWindowView;
        private ViewModel viewModel = new ViewModel();
        private bool initialized = false;

        public Controller(Action<UserControl> loadMainWindowView)
        {
            this.loadMainWindowView = loadMainWindowView;
       
            // Show the loading screen while we load the viewmodel.
            // For a nicer experience, lets guarantee a 3 second load on first 
            // load to show our pretty logo.
            Timer timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            LoadView(Logo);

            // Start a background task to attempt to load the program state from disk.
            // When completed, this process will mark the intialized property.
            System.Threading.Tasks.Task.Run(
                () =>
                {
                    System.Threading.Thread.Sleep(5000);
                    this.viewModel = LoadStateFromDisk();
                    this.initialized = true;
                });

            // Subscribe to main window closed event to save state before closing.
            Application.Current.MainWindow.Closed += MainWindow_Closed;
        }

        /// <summary>
        /// Executed when main window closed. Used to save program state before closing.
        /// </summary>
        /// <param name="sender"> Main window</param>
        /// <param name="e"> event arguments</param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Run(
                () =>
                {
                    this.SaveStateToDisk();
                });
        }

        /// <summary>
        /// When intial loading <see cref="Timer"/> elapses, evaluates whether program state has successfuly loaded or not.
        /// If so, loads the <see cref="CookBookView"/> otherwise, continues waiting at shorter intervals.
        /// </summary>
        /// <param name="sender"> <see cref="Timer"/></param>
        /// <param name="e"> event arguments</param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer timer = sender as Timer;
            timer.Stop();
            if (this.initialized)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Dispose();
                Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        // Load our default page on the UI thread.
                        LoadView(CookBookView);
                    });
            }
            else
            {
                // Shorten subsequent loads to not punish the user.
                timer.Interval = 500;
                timer.Start();
            }
        }
        /// <summary>
        /// Determines which <see cref="UserControl"/> view will be displayed based on the <paramref name="viewName"/> that is passed.
        /// </summary>
        /// <param name="viewName"> View to be displayed</param>
        private void LoadView(string viewName)
        {
            UserControl view = null;
            switch (viewName)
            {
                case CookBookView:
                    view = new CookBookView(this.viewModel);
                    break;

                case Logo:
                    view = new Logo();
                    break;
            }

            this.loadMainWindowView(view as UserControl);
        }

        /// <summary>
        /// Deserilizes and loads the <see cref="FileStream"/> that contains the applications saved data.
        /// If there is no saved data to be loaded, then a new blank view model is generated.
        /// </summary>
        /// <returns> The application state to be used by the controller</returns>
        private ViewModel LoadStateFromDisk()
        {
            try
            {
                FileStream fileStream = new FileStream("SaveState", FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                ViewModel vw = (ViewModel)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return vw;
            }
            catch (Exception)
            {
                return new ViewModel();
            }
        }

        /// <summary>
        /// Serilizes and saves the data entered into the application to a file.
        /// </summary>
        private void SaveStateToDisk()
        {
            FileStream filestream = new FileStream("SaveState", FileMode.OpenOrCreate);
            BinaryFormatter  binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(filestream, this.viewModel);
            filestream.Close();
        }
    }
}
