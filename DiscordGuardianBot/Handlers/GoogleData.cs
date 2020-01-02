using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DiscordGuardianBot
{
    class GoogleData
    {
        public static string range = "";
        public static string sheetname = "";
        public static string spreadsheetId = "";
        public static string docsId = "";
        static string[] Scopes = { DocsService.Scope.DocumentsReadonly, SheetsService.Scope.Spreadsheets };
        private static void LoadCreds()
        {
            string curDir = Directory.GetCurrentDirectory();
            string credsfile = File.ReadAllText(curDir + "/botsettings.json");
            Config creds = JsonConvert.DeserializeObject<Config>(credsfile);
            range = creds.SheetName + "!A:J";
            sheetname = creds.SheetName;
            spreadsheetId = creds.SpreadSheetID;
            docsId = creds.DocsID;
        }
        private static DocsService LogintoDocs()
        {
            LoadCreds();
            UserCredential credential;
            string ApplicationName = "GuardianBot";
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            DocsService service = new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
        private static SheetsService Logintosheets()
        {
            LoadCreds();
            UserCredential credential;
            string ApplicationName = "GuardianBot";
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }

            // Create Google Sheets API service.
            SheetsService service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
        public static List<string> ReadRules()
        {
            DocsService service = LogintoDocs();
            DocumentsResource.GetRequest request = service.Documents.Get(docsId);
            Document doc = request.Execute();
            List<string> message = new List<string>();
            foreach (var text in doc.Body.Content)
            {
                try
                {
                    foreach (var par in text.Paragraph.Elements)
                    {
                        message.Add(par.TextRun.Content);
                    }
                }
                catch { }
            }

            return message;
        }
        public static List<UserData> ReadDB()
        {
            SheetsService service = Logintosheets();
            List<UserData> newuser = new List<UserData>();
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            ValueRange response = request.Execute();
            List<UserData> users = new List<UserData>();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                int newuserrow = 0;
                bool headers = false;
                int DiscordUsername = 0;
                int team = 0;
                foreach (var row in values)
                {
                    if (headers == true)
                    {

                        if (newuser.ElementAtOrDefault(newuserrow) == null)
                        {
                            UserData tempnewuser = new UserData();
                            newuser.Add(tempnewuser);
                        }
                        try
                        {
                            if (row[DiscordUsername].ToString() != "NULL")
                            {
                                newuser[newuserrow].DiscordUsername = row[DiscordUsername].ToString();
                            }
                        }
                        catch { newuser[newuserrow].DiscordUsername = null; }
                        try
                        {
                            if (row[team].ToString() != "NULL")
                            {
                                newuser[newuserrow].Team = row[team].ToString();
                            }
                        }
                        catch { newuser[newuserrow].Team = null; }
                        newuserrow++;
                    }
                    else
                    {
                        int counter = 0;
                        foreach (var headerrow in row)
                        {
                            if (headerrow.ToString().ToLower().Trim() == "discordusername")
                            {
                                DiscordUsername = counter;
                            }
                            else if (headerrow.ToString().ToLower().Trim() == "team")
                            {
                                team = counter;
                            }
                            counter++;
                        }
                        headers = true;
                    }
                }
            }
            if (newuser.Count > 0)
            {
                foreach (UserData user in newuser)
                {
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
