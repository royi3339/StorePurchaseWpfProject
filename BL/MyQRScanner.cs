using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Drawing;
using ZXing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace BL
{
    // akuo3339   fhhnf
    public class MyQRScanner : INotifyPropertyChanged
    {

        // AuthenticateAndListContent();


        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        //public List<Item> currentItems = new List<Item>();
        //public  List<Result> lst = new List<Result>();
        public ObservableCollection<int> lstRes { get; set; } // = new ObservableCollection<Result>();


        public event PropertyChangedEventHandler PropertyChanged;

        private static int _Count = 0;
        public int count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("count"));
                //update("count");
            }
        }
        /* private void update(string propName)
         {
             if (PropertyChanged != null)
                 PropertyChanged(this, new PropertyChangedEventArgs("propName"));
         }*/

        /*      public MyQRScanner()
              {
                  count = 0;
                              lstRes = new ObservableCollection<Result>();

              }   */

        public void AuthenticateAndListContent()
        {
            count = 0;
            lstRes = new ObservableCollection<int>();

            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 200;
            listRequest.Fields = "*";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            //string id = "";
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        if (file.Name == "items")
            //        {
            //            id = file.Id;
            //        }
            //    }
            //}
            Result res;
            int rres;
            bool b;
            foreach (var file in files)
            {
                res = null;
                if (file.Parents != null)
                {
                    //if (file.Parents.Contains(id))
                    //{
                    res = DownloadFile(service, file);
                    if (res != null)
                    {
                        //rres = int.Parse(res.ToString());
                        b = int.TryParse(res.ToString(), out rres);

                        if (b)
                        {
                            lstRes.Add(rres);
                        }
                        else
                        {
                            count++;
                        }
                    }
                    else
                        count++;
                    //DownloadFile(service, file, "items/" + file.Name + file.Id + ".jpg");  
                    //}
                }
            }


        }
        private Result DownloadFile(Google.Apis.Drive.v3.DriveService service, Google.Apis.Drive.v3.Data.File file)
        {
            var request = service.Files.Get(file.Id);
            var stream = new System.IO.MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            Result h = null;
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                if(progress.Status == Google.Apis.Download.DownloadStatus.Completed)
                    h = QRscan(stream, file.Name);
                //switch (progress.Status)
                //{
                //    case Google.Apis.Download.DownloadStatus.Downloading:
                //        {
                //            //Console.WriteLine( (string)progress.BytesDownloaded.ToString() + "Bytes downloaded");
                //            break;
                //        }
                //    case Google.Apis.Download.DownloadStatus.Completed:
                //        {
                //            //Console.WriteLine("Download complete.");
                //            //SaveStream(stream, saveTo);
                //            return QRscan(stream, file.Name);
                //            //break;
                //        }
                //    case Google.Apis.Download.DownloadStatus.Failed:
                //        {
                //            //Console.WriteLine( "Download failed.");
                //            break;
                //        }
                //}                
            };
            request.Download(stream);
            return h;
        }
        //private  void SaveStream(System.IO.MemoryStream stream, string saveTo)
        //{
        //    using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
        //    {
        //        stream.WriteTo(file);
        //    }
        //}

        private Result QRscan(System.IO.MemoryStream stream, string fileName)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.AutoRotate = true;
            reader.Options.TryHarder = true;
            reader.Options.PureBarcode = false;
            reader.Options.PossibleFormats = new List<BarcodeFormat>();
            reader.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
            try
            {
                var res = Image.FromStream(stream);
                //var resultQR = reader.Decode((Bitmap)res);
                return reader.Decode((Bitmap)res);
                //string result = resultQR.ToString() + "\n" + fileName;
            }
            catch (Exception)
            {
                //MessageBox.Show("cloudn't decode file:" + fileName);
                //Console.WriteLine("cloudn't decode file #2:" + fileName);
                return null;
            }
        }
    }
}
