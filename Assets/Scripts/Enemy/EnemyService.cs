using StatePattern.Level;
using StatePattern.Main;
using StatePattern.Sound;
using StatePattern.UI;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class EnemyService
    {
        private SoundService SoundService => GameService.Instance.SoundService;
        private UIService UIService => GameService.Instance.UIService;
        private LevelService LevelService => GameService.Instance.LevelService;

        private List<EnemyController> activeEnemies;
        private int totalEnemies;

        public EnemyService()
        {
            InitializeVariables();
            SubscribeToEvents();
        }

        private void InitializeVariables() => activeEnemies = new List<EnemyController>();

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnEnemies);

        public void SpawnEnemies(int levelId)
        {
            List<EnemyScriptableObject> enemyDataForLevel = LevelService.GetEnemyDataForLevel(levelId);
            
            foreach(EnemyScriptableObject enemySO in enemyDataForLevel)
            {
                EnemyController enemy = CreateEnemy(enemySO);
                activeEnemies.Add(enemy);
            }

            SetEnemyCount();
        }

        private void SetEnemyCount()
        {
            totalEnemies = activeEnemies.Count;
            UIService.UpdateEnemyCount(activeEnemies.Count, totalEnemies);
        }

        public EnemyController CreateEnemy(EnemyScriptableObject enemyScriptableObject)
        {
            EnemyController enemy;

            switch (enemyScriptableObject.Type)
            {
                case EnemyType.OnePunchMan:
                    enemy = new OnePunchManController(enemyScriptableObject);
                    break;
                case EnemyType.PatrolMan:
                    enemy = new PatrolManController(enemyScriptableObject);
                    break;
                case EnemyType.Hitman:
                    enemy = new HitmanController(enemyScriptableObject);
                    break;
                case EnemyType.Robot:
                    enemy = new RobotController(enemyScriptableObject);
                    break;
                default:
                    enemy = new EnemyController(enemyScriptableObject);
                    break;
            }

            return enemy;
        }

        public void EnemyDied(EnemyController deadEnemy)
        {
            activeEnemies.Remove(deadEnemy);
            SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_DEATH);
            UIService.UpdateEnemyCount(activeEnemies.Count, totalEnemies);
            if (PlayerWon()) 
            {
                SoundService.PlaySoundEffects(Sound.SoundType.GAME_WON);
                UIService.GameWon();
            }
        }

        public void PlayerDied()
        {
            foreach(EnemyController enemy in activeEnemies)
            {
                enemy.SetState(EnemyState.DEACTIVE);
            }
        }

        private bool PlayerWon() => activeEnemies.Count == 0;
    }
}