using System;
using AdapterPattern.Client;

namespace AdapterPattern
{
    class Program
    {
        static void Main()
        {
            var player = new AudioPlayer();

            Console.WriteLine("=== Adapter Pattern: Media Player Demo ===");
            player.Play("mp3", "rock.mp3");
            player.Play("mp4", "movie.mp4");
            player.Play("vlc", "series.vlc");
            player.Play("avi", "documentary.avi"); // Unsupported

            Console.ReadKey();
        }
    }
}
