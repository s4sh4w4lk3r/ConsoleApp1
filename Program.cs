using Newtonsoft.Json;
using Telegram.Bot;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Не введены аргументы.");
                Console.ReadKey();
            }

            if (args[0] == "--split"
                && (string.IsNullOrWhiteSpace(args[1]) is false))
            {
                SplitPlaylist(args[1]);
            }

            if (args[0] == "--telegram"
                && (string.IsNullOrWhiteSpace(args[1]) is false)
                && (string.IsNullOrWhiteSpace(args[2]) is false)
                && (string.IsNullOrWhiteSpace(args[3]) is false))
            {
                bool idOK = long.TryParse(args[3], out long telegramId);
                bool startValueOk = int.TryParse(args[3], out int startValue);

                if (idOK is true)
                {

                    if (startValueOk is true)
                    {
                        SendTelegram(args[1], args[2], telegramId, startValue).Wait();
                    }
                    else
                    {
                        SendTelegram(args[1], args[2], telegramId).Wait();
                    }
                }
                else
                {
                    Console.WriteLine("TelegramId пользователя не распознан.");
                    Console.ReadKey();
                }
            }
        }

        public static void SplitPlaylist(string spotifyPlaylistPath)
        {

            var parentDir = Directory.GetParent(spotifyPlaylistPath)?.ToString() ?? throw new ArgumentNullException("Путь не найден до плейлиста.");

            var outputDir = Path.Combine(parentDir, "output");
            Directory.CreateDirectory(outputDir);

            string inputJson = File.ReadAllText(spotifyPlaylistPath);
            inputJson = inputJson.Replace("trackName", "track");
            inputJson = inputJson.Replace("artistName", "artist");
            inputJson = inputJson.Replace("albumName", "album");
            inputJson = inputJson.Replace("trackUri", "uri");

            var playlists = (JsonConvert.DeserializeObject<RootPlaylist>(inputJson)?.Playlists) ?? throw new ArgumentNullException("Плейлисты не обнаружены.");
            foreach (var playlist in playlists)
            {
                var outputPath = Path.Combine(outputDir, playlist.Name) + ".json";

                var newYourLib = new YourLibrary();
                var tracks = playlists.Where(e => e.Name == playlist.Name).Select(e => e.Items.Select(e => e.Track)).First().ToList();

                newYourLib.Tracks = new List<Track>();
                newYourLib.Tracks.AddRange(tracks);

                File.WriteAllText(outputPath, JsonConvert.SerializeObject(newYourLib, Formatting.Indented));
            }
        }

        public static async Task SendTelegram(string yourlibPath, string botToken, long telegramId, int startValue = 0)
        {
            var yourlib = (JsonConvert.DeserializeObject<YourLibrary>(File.ReadAllText(yourlibPath))) ?? throw new ArgumentNullException("Библиотека не обнаружена.");
            yourlib.Tracks = new List<Track>();
            var tracksStr = yourlib.Tracks.Select(e => $"{e.Artist} - {e.TrackName}");

            var bot = new TelegramBotClient(botToken);

            int limiter = 0;
            int messageNumber = startValue;
            for (int i = limiter; i < tracksStr.Count(); i++)
            {
                await bot.SendTextMessageAsync(telegramId, tracksStr.ElementAt(i));
                i++;
                messageNumber++;
                Console.WriteLine($"messageNumber {messageNumber}");
                await Task.Delay(200);

                if (limiter > 30)
                {
                    Console.WriteLine("Ждем");
                    limiter = 0;
                    await Task.Delay(5000);
                }
            }
        }
    }
}