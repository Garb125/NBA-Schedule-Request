using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using HtmlAgilityPack;

namespace NBA_Schedule_Request
{
    class Program
    {
        static HttpClientHandler handler = new HttpClientHandler()
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        };
        
        static readonly HttpClient client = new HttpClient(handler);

        static async Task Main()
        {
            GetTeamProfile();

            Console.Read();
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

                foreach (var gmDate in schedule.leagueSchedule.gameDates)
                {
                    System.Console.WriteLine(gmDate.gameDate);
                    foreach (var match in gmDate.games)
                    {
                        System.Console.WriteLine($"{match.homeTeam.teamName} vs. {match.awayTeam.teamName}");
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
                HttpResponseMessage response = await client.GetAsync("https://stats.nba.com/stats/playerindex?College=&Country=&DraftPick=&DraftRound=&DraftYear=&Height=&Historical=1&LeagueID=00&Season=2021-22&SeasonType=Regular%20Season&TeamID=0&Weight=");
                var responseBody = await response.Content.ReadAsStringAsync();
                Depot stats = JsonSerializer.Deserialize<Depot>(responseBody);
                //rData = stats;

                Console.WriteLine(stats.resultSets[0].rowSet[1][0]);
                Console.WriteLine(stats.resultSets[0].headers[2]);
                //Console.Read();                
                                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);             
                                
            }

        }

        static void  GetTeamProfile()
        {
            string url = "https://www.nba.com/team/1610612752";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = new HtmlDocument();
            htmlDoc =  web.Load(url);

            //var node = htmlDoc.DocumentNode.SelectSingleNode("//dt[1]");

            var headers = htmlDoc.DocumentNode.SelectNodes("//dt");
            var tmData = htmlDoc.DocumentNode.SelectNodes("//dd");

            int indx = 0;
        
            foreach (var header in headers)
            {
            
                Console.WriteLine($"{header.InnerHtml} - {tmData[indx].InnerHtml}");
                indx++;
            
            }
        }

    }

}