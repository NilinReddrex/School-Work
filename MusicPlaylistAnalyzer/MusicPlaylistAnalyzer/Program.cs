using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ValidateInputs(args);

                string inputTxtFilePath = args[0];
                string outputReportPath = args[1];

                IEnumerable<string> readLines = ReadInputFile(inputTxtFilePath);

                Dictionary<string, int> headerIndexes = CreateHeaderLines(readLines);
                List<PlaylistInformation> playlistInformationList = ParsePlaylistInformationList(readLines, headerIndexes);
                List<string> outputContent = BuildOutPutReport(playlistInformationList);

                WriteOutputToFile(outputReportPath, outputContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadLine();
        }

        private static void WriteOutputToFile(string outputReportPath, List<string> outputContent)
        {
            try
            {
                File.WriteAllLines(outputReportPath, outputContent);
                Console.WriteLine("Successfully wrote report into file {0}", outputReportPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to write to output file. {0}", e.Message);
                Environment.Exit(1);
            }
        }
        
        private static List<string> BuildOutPutReport(List<PlaylistInformation> playlistInformationList)
        {
            List<string> outputContent = new List<string>();

            // How many songs received 200 or more plays?
            var plays200OrMore = playlistInformationList.Where(playlistInformation => playlistInformation.Plays >= 200);
            Console.WriteLine("Music Playlist Report \n");
            Console.WriteLine("Songs that received 200 or more plays: ");
            foreach (var play in plays200OrMore)
            {
                Console.WriteLine($"Name: {play.Name}, Artist: {play.Artist}, Album: {play.Album}, Genre: {play.Genre}, Size: {play.Size}, Time: {play.Time}, Year: {play.Year}, Plays: {play.Plays}");
            }
            Console.WriteLine('\n');
            
            // How many songs are in the playlist with the Genre of “Alternative”?
            var numOfSongsAlternative = playlistInformationList.Where(playlistInformation => playlistInformation.Genre == "Alternative").Count();
            Console.WriteLine($"Number of Alternative songs: {numOfSongsAlternative}\n");

            //How many songs are in the playlist with the Genre of “Hip - Hop / Rap”?
            var numOfSongsHipHopRap = playlistInformationList.Where(playlistInformation => playlistInformation.Genre == "Hip-Hop/Rap").Count();
            Console.WriteLine($"Number of Hip-Hop/Rap songs:{numOfSongsHipHopRap}\n");

            //What songs are in the playlist from the album “Welcome to the Fishbowl?”
            var songsFromAlbumWelcomeToThefishbowl = playlistInformationList.Where(playlistInformation => playlistInformation.Album == "Welcome to the Fishbowl");
            Console.WriteLine("Songs from the album Welcome to the Fishbowl:");
            foreach (var songs in songsFromAlbumWelcomeToThefishbowl)
            {
                Console.WriteLine($"Name: {songs.Name}, Artist: {songs.Artist}, Album: {songs.Album}, Genre: {songs.Genre}, Size: {songs.Size}, Time: {songs.Time}, Year: {songs.Year}, Plays: {songs.Plays}");
            }
            Console.WriteLine('\n');

            //What are the songs in the playlist from before 1970 ?
            var songsBefore1970 = playlistInformationList.Where(playlistInformation => playlistInformation.Year < 1970 );
            Console.WriteLine("Songs from before 1970:");
            foreach (var song in songsBefore1970)
            {
                Console.WriteLine($"Name: {song.Name}, Artist: {song.Artist}, Album: {song.Album}, Genre: {song.Genre}, Size: {song.Size}, Time: {song.Time}, Year: {song.Year}, Plays: {song.Plays}");
            }
            Console.WriteLine('\n');

            //What are the song names that are more than 85 characters long?
            var songNameLongerThan85Char = playlistInformationList.Where(playlistInformation => playlistInformation.Name.Length > 85);
            Console.WriteLine("Song names longer than 85 characters:");
            foreach (var song in songNameLongerThan85Char)
            {
                Console.WriteLine($"Name: {song.Name}");
            }
            Console.WriteLine('\n');

            //What is the longest song ? (longest in Time)
            var longestTime = playlistInformationList.Max(playlistInformation => playlistInformation.Time);
            var longestSong = playlistInformationList.Where(playlistInformation => playlistInformation.Time == longestTime);
            foreach (var song in longestSong)
            {
                Console.WriteLine($"Name: {song.Name}, Artist: {song.Artist}, Album: {song.Album}, Genre: {song.Genre}, Size: {song.Size}, Time: {song.Time}, Year: {song.Year}, Plays: {song.Plays}");
            }

            Console.ReadLine();
            return outputContent;
        }
        private static List<PlaylistInformation> ParsePlaylistInformationList(IEnumerable<string> readLines, Dictionary<string, int> headerIndexes)
        {
            List<PlaylistInformation> playlistInformationList = new List<PlaylistInformation>();

            for (int i = 1; i < readLines.Count(); i++)
            {
                string readLine = readLines.ElementAt(i);
                try
                {
                    PlaylistInformation c = PlaylistInformation.BuildFromReadData(headerIndexes, readLine);
                    playlistInformationList.Add(c);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to build record from line {0} from input file.", i);
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }

            return playlistInformationList;
        }

        private static Dictionary<string, int> CreateHeaderLines(IEnumerable<string> readLines)
        {
            string headerLine = readLines.First();

            string[] headers = headerLine.Split(new char[] { '\t' });
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();

            for (int i = 0; i < headers.Length; i++)
            {
                headerIndexes.Add(headers[i], i);
            }

            return headerIndexes;
        }

        private static IEnumerable<string> ReadInputFile(string inputTxtFilePath)
        {
            try
            {
                return File.ReadAllLines(inputTxtFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }

            return null;
        }
        

        private static void ValidateInputs(string[] args)
        {
            if (args.Length < 2 || args.Length > 2)
            {
                Console.WriteLine("You didn't give me 2 paths. Usage:");
                Console.WriteLine("MusicPlaylistAnalyzer <music_playlist_file_path> <report_file_path>");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
