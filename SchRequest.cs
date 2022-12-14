using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using HtmlAgilityPack;
using IronXL;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NBA_Schedule_Request
{
    class Program
    {
        static HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        };
        
        static readonly HttpClient client = new HttpClient(handler);

        static Depot rData;
        static Schedule fullSchedule;

        static async Task Main()
        {
            //GetTeamProfile();
            await GetSchedule();
            WriteSchedule();
            //await GetStats();
            //WriteStats();
            //Console.Read();
        }

        static async Task GetSchedule()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://cdn.nba.com/static/json/staticData/scheduleLeagueV2_1.json");
                var responseBody = await response.Content.ReadAsStringAsync();
                Schedule schedule = JsonSerializer.Deserialize<Schedule>(responseBody);
                Console.WriteLine(schedule.leagueSchedule.seasonYear);
                //Console.WriteLine(schedule.leagueSchedule.gameDates[0].games[0].homeTeam.teamName);

                fullSchedule = schedule;

                foreach (var gmDate in schedule.leagueSchedule.gameDates)
                {
                    System.Console.WriteLine(gmDate.gameDate);
                    foreach (var match in gmDate.games)
                    {
                        System.Console.WriteLine($"{match.homeTeam.teamName} vs. {match.awayTeam.teamName}");
                        System.Console.WriteLine($"Match Code: {match.gameCode}" );
                    }
                    //System.Console.WriteLine(team.gameDate);
                    //System.Console.WriteLine($"{team.homeTeam.teamName} vs. {team.awayTeam.teamName}");
                }

            }
            catch (HttpRequestException e)
            {
                
                System.Console.WriteLine("\nException Caught!", e.Message );
            }
            
        }

        static void WriteSchedule()
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Excel._Worksheet worksheet = excelApp.ActiveSheet;
            int r = 2;
            int c = 1;

            IList<GameDate> gmDates = fullSchedule.leagueSchedule.gameDates;
            
            //Set Headers
            worksheet.Cells[1, 1] = "Id";
            worksheet.Cells[1, 2] = "Name";
            worksheet.Cells[1, 3] = "Season";
            worksheet.Cells[1, 4] = "HTeam";
            worksheet.Cells[1, 5] = "VTeam";
            worksheet.Cells[1, 6] = "HTScore";
            worksheet.Cells[1, 7] = "VTScore";
            worksheet.Cells[1, 8] = "Victor";
            worksheet.Cells[1, 9] = "Date";
            worksheet.Cells[1, 10] = "SeasonType";

            //Write Schedule
            foreach (GameDate gmDate in gmDates)
            {
                //worksheet.Cells[2, 9] = gmDate.gameDate;
                foreach (var match in gmDate.games)
                {
                    worksheet.Cells[r, c] = match.gameId;
                    worksheet.Cells[r, c+1] = match.gameCode;
                    worksheet.Cells[r, c+2] = "2022-23";
                    worksheet.Cells[r, c+3] = match.homeTeam.teamId;
                    worksheet.Cells[r, c+4] = match.awayTeam.teamId;
                    worksheet.Cells[r, c+5] = match.homeTeam.score;
                    worksheet.Cells[r, c+6] = match.awayTeam.score;
                    worksheet.Cells[r, c+7] = "Victor";
                    worksheet.Cells[r, c+8] = match.gameDateEst;
                    worksheet.Cells[r, c+9] = "Regular";
                    r++;
                }
                
            }

            
        }

        static async Task GetStats()
        { 
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain,");
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Origin", "https://www.nba.com");
            client.DefaultRequestHeaders.Add("Referer", "https://www.nba.com/");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            try
            {
                //HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/leaguegamefinder?Conference=&DateFrom=&DateTo=&Division=&DraftNumber=&DraftRound=&DraftYear=&GB=N&LeagueID=00&Location=&Outcome=&PlayerID=406&PlayerOrTeam=P&Season=&SeasonType=&StatCategory=PTS&TeamID=&VsConference=&VsDivision=&VsTeamID=&gtPTS=40");
                //HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/playerindex?College=&Country=&DraftPick=&DraftRound=&DraftYear=&Height=&Historical=1&LeagueID=00&Season=2021-22&SeasonType=Regular%20Season&TeamID=0&Weight=");
                HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/leaguedashteamstats?Conference=&DateFrom=&DateTo=&Division=&GameScope=&GameSegment=&Height=&LastNGames=0&LeagueID=00&Location=&MeasureType=Base&Month=0&OpponentTeamID=0&Outcome=&PORound=0&PaceAdjust=N&PerMode=PerGame&Period=0&PlayerExperience=&PlayerPosition=&PlusMinus=N&Rank=N&Season=2021-22&SeasonSegment=&SeasonType=Regular%20Season&ShotClockRange=&StarterBench=&TeamID=0&TwoWay=0&VsConference=&VsDivision=");
                var responseBody = await response.Content.ReadAsStringAsync();
                Depot stats = JsonSerializer.Deserialize<Depot>(responseBody);
                rData = stats;

                //Console.WriteLine(stats.resultSets[0].rowSet[1][0]);
                //Console.WriteLine(stats.resultSets[0].headers[2]);
                Console.WriteLine(stats.resultSets[0].rowSet[0][1]);

                /*foreach(var stat in stats.resultSets[0].rowSet)
                {
                    Console.WriteLine($"Name: {stat[1]} - Id {stat[0]}" );
                    //System.Console.WriteLine(stat);
                } 
                */
                //Console.Read();                
                                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);             
                                
            }
            WriteStats();

        }

        static void  GetTeamProfile()
        {
            
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = new HtmlDocument();
            //htmlDoc =  web.Load(url);

            WorkBook firstBook = WorkBook.Load("Teams2.xlsx");
            WorkSheet sheet = firstBook.DefaultWorkSheet;
            IronXL.Range teamIds = sheet.GetRange("C3:C32");

            int col = 4;
            int row = 2;

            foreach (var item in teamIds)
            {
                string url = $"https://www.nba.com/team/{item.StringValue}";
                htmlDoc = web.Load(url);
              
                var headers = htmlDoc.DocumentNode.SelectNodes("//dt");
                var tmData = htmlDoc.DocumentNode.SelectNodes("//dd");

                try
                {
                    string tmCity = tmData[1].InnerHtml;
                    string tmArena = tmData[2].InnerHtml;
                    string tmOwner = tmData[4].InnerHtml;
                    string tmGm = tmData[5].InnerHtml;
                    string tmCoach = tmData[6].InnerHtml;

                    sheet.SetCellValue(row, col, tmCity);
                    sheet.SetCellValue(row, col + 2, tmArena);
                    sheet.SetCellValue(row, col + 3, tmOwner);
                    sheet.SetCellValue(row, col + 4, tmGm);
                    sheet.SetCellValue(row, col + 5, tmCoach);

                    Console.WriteLine(url);
                    
                }
                catch (Exception)
                {

                    continue;
                }
                row++;
            }
            firstBook.Save();
            //var node = htmlDoc.DocumentNode.SelectSingleNode("//dt[1]");

            //sheet.SetCellValue(2,4, tmCity);

            /*int indx = 0;
        
            foreach (var value in tmData)
            {
            
                Console.WriteLine($"{tmData[indx].InnerHtml}");
                indx++;
            
            }
            */

            //Console.WriteLine(url);
            Console.WriteLine("Stats Written");
        }

        static void WriteStats()
        {
            WorkBook firstBook = WorkBook.Load("Teams.xlsx");
            WorkSheet sheet = firstBook.DefaultWorkSheet;

            IList<IList<object>> data = rData.resultSets[0].rowSet;

            int col = 1;
            int row = 2;

            sheet.SetCellValue(1, 1, "Test");

            foreach (var statline in data)
            {
                string tmName = statline[1].ToString();
                string tmId = statline[0].ToString();

                sheet.SetCellValue(row, col, tmName);
                Console.WriteLine(tmName);
                sheet.SetCellValue(row, col+1, tmId);
                Console.WriteLine(statline[0]);
                row++;
            }

            firstBook.SaveAs("Teams2.xlsx");
            Console.WriteLine("Stats Written");
        }

    }

}