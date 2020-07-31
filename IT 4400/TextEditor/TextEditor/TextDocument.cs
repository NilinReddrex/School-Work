using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;

namespace TextEditor
{
    public class TextDocument
    {
        private string content = string.Empty;

        public event Action OnContentChanged;
        public string Content 
        {
            get
            {
                return this.content;
            }

            set
            {
                this.content = value;
                this.IsModified = true;
                this.OnContentChanged?.Invoke();
            } 
        }
        public string FileAccessToken { get; set; }
        public BasicProperties FileProperties { get; set; }
        public bool IsModified { get; set; }

        public TextDocument()
        {

        }

        public TextDocument(string content, BasicProperties properties, string fileAccessToken)
        {
            this.content = content;
            this.FileProperties = properties;
            this.FileAccessToken = fileAccessToken;
        }

        public async Task<string> Save(StorageFile file)
        {
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                await FileIO.WriteTextAsync(file, this.Content);
                this.IsModified = false;
                this.OnContentChanged();

                //Store the properties
                this.FileProperties = await file.GetBasicPropertiesAsync();

                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    return "File '" + file.Name + "' was saved.";
                }
                else
                {
                    return "File '" + file.Name + "' couldn't be saved.";
                }
            }
            else
            {
                return "Operation cancelled.";
            }

        }

        public static async Task<TextDocument> ReadTextDocumentFromFileAsync(StorageFile file)
        {
            TextDocument document = null;
            // Handles the exception if a txt file is not selected.
            if (file != null)
            {
                //https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files
                 document = new TextDocument(
                    await FileIO.ReadTextAsync(file),
                    await file.GetBasicPropertiesAsync(),
                    StorageApplicationPermissions.FutureAccessList.Add(file));
            }

            return document;
        }

        public bool TryGetFile(out StorageFile file)
        {
            if (!string.IsNullOrWhiteSpace(this.FileAccessToken))
            {
                try
                {
                    file = StorageApplicationPermissions.FutureAccessList.GetFileAsync(this.FileAccessToken).GetAwaiter().GetResult();
                    return true;
                }
                catch (Exception)
                {

                    this.FileAccessToken = null;
                   
                }
            }
            
            file = null;
            return false;
        }
    }
}
