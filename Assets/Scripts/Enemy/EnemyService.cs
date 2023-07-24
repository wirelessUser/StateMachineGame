using StatePattern.Main;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class EnemyService
    {
        private List<EnemyScriptableObject> enemyScriptableObjects;

        private List<EnemyController> activeEnemies;

        public EnemyService(List<EnemyScriptableObject> enemyScriptableObjects)
        {
            this.enemyScriptableObjects = enemyScriptableObjects;
            activeEnemies = new List<EnemyController>();
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnEnemies);

        public void SpawnEnemies(int levelId)
        {
            List<EnemyScriptableObject> enemyData = GetEnemyDataForLevel(levelId);

            foreach(EnemyScriptableObject enemySO in enemyData)
            {
                CreateEnemy(enemySO);
            }
        }

        public EnemyController CreateEnemy(EnemyScriptableObject enemyScriptableObject)
        {
            EnemyController enemy;

            switch (enemyScriptableObject.Type)
            {
                case EnemyType.OnePunchMan:
                    enemy = new OnePunchManController(enemyScriptableObject);
                    break;
                case EnemyType.DashMan:
                    enemy = new DashManController(enemyScriptableObject);
                    break;
                case EnemyType.Hitman:
                    enemy = new HitManController(enemyScriptableObject);
                    break;
                case EnemyType.Robot:
                    enemy = new RobotController(enemyScriptableObject);
                    break;
                default:
                    enemy = new EnemyController(enemyScriptableObject);
                    break;
            }

            activeEnemies.Add(enemy);
            return enemy;
        }

        private List<EnemyScriptableObject> GetEnemyDataForLevel(int levelId) => enemyScriptableObjects.FindAll(enemySO => enemySO.LevelID == levelId);

        public void EnemyDied(EnemyController deadEnemy)
        {
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_DEATH);
            activeEnemies.Remove(deadEnemy);
            // Update Enemy Count in UI.
            if (DidPlayerWin()) 
            {
                GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.GAME_WON);
                Debug.Log("Player Won the Game.");
                GameService.Instance.UIService.GameWon();
            }
        }

        private bool DidPlayerWin() => activeEnemies.Count == 0;
    }
}