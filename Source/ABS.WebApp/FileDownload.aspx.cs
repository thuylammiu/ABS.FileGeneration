using ABS.WebApp.Extensions;
using ABS.WebApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;


namespace ABS.WebApp
{
    public partial class FileDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void DownloadButton_Click(object sender, EventArgs e)
        {
            string apiUrl = ConfigurationManager.AppSettings["backEndUrl"];
            try
            {
                using (var client = new HttpClient())
                {

                    var body = new FileGenerationDataRequest
                    {
                        FileType = "excel",
                        FileName = "DataGenerator.xls",
                        SheetName = "sheet1"
                    };
                    

                    var response = await client.PostAsJson<FileGenerationDataRequest>(apiUrl + "ExcelGeneration/download", body);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] fileContent = await response.Content.ReadAsByteArrayAsync();

                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={body.FileName}");

                        Response.BinaryWrite(fileContent);
                        Response.End();
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        LogError($"API Error: {response.StatusCode} - {errorContent}");
                        ShowErrorMessage("Unable to generate Excel file. Please try again later.");
                    }

                }
            }
            catch (HttpRequestException ex)
            {
                LogError($"HTTP Request Error: {ex.Message}");
                ShowErrorMessage("Unable to connect to the file generation service. Please check your network connection and try again.");
            }
            catch (Exception ex)
            {
                LogError($"Unexpected Error: {ex.Message}");
                ShowErrorMessage("An unexpected error occurred. Please try again later.");
            }

        }

        private void LogError(string message)
        {
            string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
            try
            {
                if (!File.Exists(logFilePath))
                {
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));                    
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex) {
                
                EventLog.WriteEntry("Application", $"Failed to log error to file: {ex.Message}", EventLogEntryType.Error);
            }
        }


        private void ShowErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }


        private async Task<string> AuthenticateApi()
        {

            string apiUrl = ConfigurationManager.AppSettings["backEndUrl"];
            using (var client = new HttpClient())
            {
                var body = new AutheticationRequest
                {
                    UserName = "admin",
                    PassWord = "password",
                    Role = "admin"
                };

                var response = await client.PostAsJson<AutheticationRequest>(apiUrl + "account/login", body);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Invalid Authentication information");
                }
                var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await response.Content.ReadAsStringAsync());
                return authResponse.Token;
            }
        }


    }
}