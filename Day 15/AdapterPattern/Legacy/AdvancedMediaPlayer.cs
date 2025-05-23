using System;

namespace AdapterPattern.Legacy
{
    public class AdvancedMediaPlayer
    {
        public void PlayVlc(string fileName)
        {
            Console.WriteLine("Playing .vlc file: " + fileName);
        }

        public void PlayMp4(string fileName)
        {
            Console.WriteLine("Playing .mp4 file: " + fileName);
        }
    }
}
