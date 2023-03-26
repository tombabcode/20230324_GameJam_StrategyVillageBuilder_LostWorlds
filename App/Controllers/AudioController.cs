using System.Collections.Generic;
using VXEngine.Audio;
using VXEngine.Controllers;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;

public sealed class AudioController : BasicAudioController {

    private List<MusicInstance> _gameplayMusic;
    private List<int> _gameplayQueuePlayed;

    public void LoadGameplayQueue(ContentController content, BasicConfigController config) {
        // Already loaded
        if (_gameplayMusic != null)
            return;

        // Load music
        _gameplayMusic = new List<MusicInstance> {
            RegisterMusic(config, content.MusicGameplay1),
            RegisterMusic(config, content.MusicGameplay2)
        };

        // Play random music on finish
        foreach (MusicInstance instance in _gameplayMusic)
            instance.OnFinish = _ => PlayRandomGameplaySong((ConfigController)config);
    }

    public void PlayRandomGameplaySong(ConfigController config) {
        // Create queue
        if (_gameplayQueuePlayed == null)
            _gameplayQueuePlayed = new List<int>( );

        // Check if there are available songs that weren't played. If there is none of them - create new queue
        if (_gameplayQueuePlayed.Count == 0)
            for (int i = 0; i < _gameplayMusic.Count; i++)
                _gameplayQueuePlayed.Add(i);

        // Stop music if any is playing
        foreach (MusicInstance music in _gameplayMusic)
            music.Stop( );

        // Play random song from queue
        int queueID = config.Random.Next(0, _gameplayQueuePlayed.Count);
        _gameplayQueuePlayed.RemoveAt(queueID);
        _gameplayMusic[queueID].PlayOnce( );
    }

}
