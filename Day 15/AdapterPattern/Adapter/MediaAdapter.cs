using AdapterPattern.Interfaces;
using AdapterPattern.Legacy;

namespace AdapterPattern.Adapter
{
    public class MediaAdapter : IMediaPlayer
    {
        private readonly AdvancedMediaPlayer _advancedPlayer = new AdvancedMediaPlayer();

        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
                _advancedPlayer.PlayVlc(fileName);
            else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
                _advancedPlayer.PlayMp4(fileName);
        }
    }
}
