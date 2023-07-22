using UnityEngine;
using StatePattern.Utilities;
using StatePattern.Enemy;
using StatePattern.Sound;
using StatePattern.Level;
using StatePattern.Player;
using System.Collections.Generic;
using StatePattern.UI;
using StatePattern.Events;

namespace StatePattern.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public SoundService SoundService { get; private set; }
        public EventService EventService { get; private set; }
        public LevelService LevelService { get; private set; }
        public PlayerService PlayerService { get; private set; }
        public EnemyService EnemyService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        // Scriptable Objects:
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private List<LevelScriptableObject> levelScriptableObjects;
        [SerializeField] private List<EnemyScriptableObject> enemyScriptableObjects;

        // Scene References:
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;

        private void Start()
        {
            uiService.Init(levelScriptableObjects.Count);
            EventService = new EventService();
            SoundService = new SoundService(soundScriptableObject, sfxSource, bgMusicSource);
            LevelService = new LevelService(levelScriptableObjects);
            PlayerService = new PlayerService(playerScriptableObject);
            EnemyService = new EnemyService(enemyScriptableObjects);
        }
    }
}