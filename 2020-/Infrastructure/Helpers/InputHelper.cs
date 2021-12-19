using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AdventOfCode.Infrastructure.Helpers
{
    public static class InputHelper
    {
        private static readonly string Cookie = Config.Get("config.json").Cookie;

        public static string LoadInput(int day, int year)
        {
            var inputFilepath = GetDayPath(day, year) + "/input";
            var inputUrl = GetAocInputUrl(day, year);
            var input = "";

            if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
            {
                input = File.ReadAllText(inputFilepath);
            }
            else
            {
                try
                {
                    var currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                    if (currentEst < new DateTime(year, 12, day)) throw new InvalidOperationException();

                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Cookie", Cookie);
                    
                    using (var response = client.GetAsync(inputUrl).Result)
                    {
                        using (var content = response.Content)
                        {
                            input = content.ReadAsStringAsync().Result.Trim();
                        }
                    }
                    File.WriteAllText(inputFilepath, input);
                }
                catch (WebException e)
                {
                    var statusCode = ((HttpWebResponse)e.Response).StatusCode;
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (statusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine($"Day {day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");

                    }
                    else if (statusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine($"Day {day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
                    }
                    else
                    {
                        Console.ForegroundColor = colour;
                        Console.WriteLine(e.ToString());
                    }
                    Console.ForegroundColor = colour;
                }
                catch (InvalidOperationException)
                {
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Day {day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                    Console.ForegroundColor = colour;
                }
            }
            return input;
        }

        public static string LoadDebugInput(int day, int year)
        {
            var inputFilepath = GetDayPath(day, year) + "/debug";
            return (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
                ? File.ReadAllText(inputFilepath)
                : "";
        }

        private static string GetDayPath(int day, int year)
            => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"../../../Solutions/Year{year}/Day{day:D2}"));

        private static string GetAocInputUrl(int day, int year)
            => $"https://adventofcode.com/{year}/day/{day}/input";
    }
}