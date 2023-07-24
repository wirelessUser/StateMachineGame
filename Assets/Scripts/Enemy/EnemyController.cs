using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyController
    {
        protected EnemyScriptableObject enemyScriptableObject;
        protected EnemyView enemyView;

        protected int currentHealth;
        protected EnemyState currentState;
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => enemyScriptableObject;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;


        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(enemyScriptableObject.EnemyPrefab);
            enemyView.transform.position = enemyScriptableObject.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(enemyScriptableObject.SpawnRotation);
            enemyView.SetTriggerRadius(enemyScriptableObject.RangeRadius);
        }

        private void InitializeVariables()
        {
            SetState(EnemyState.ACTIVE);
            currentHealth = enemyScriptableObject.MaximumHealth;
            enemyView.SetTarget(GameService.Instance.PlayerService.GetPlayer());
        }

        public void InitializeAgent()
        {
            Agent.enabled = true;
            Agent.SetDestination(enemyScriptableObject.SpawnPosition);
        }

        public virtual void Die() 
        {
            GameService.Instance.EnemyService.EnemyDied(this);
            enemyView.Destroy();
        }

        public void SetRotaion(Vector3 eulerAngles) => enemyView.transform.rotation = Quaternion.Euler(eulerAngles);

        public void SetRotaion(Quaternion desiredRotation) => enemyView.transform.rotation = desiredRotation;

        public void Shoot()
        {
            enemyView.PlayShootingEffect();
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_SHOOT);
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }

        public void SetState(EnemyState stateToSet) => currentState = stateToSet;

        public virtual void PlayerEnteredRange() => GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_ALERT);

        public virtual void PlayerExitedRange() { }

        public virtual void UpdateEnemy() { }
    }

    public enum EnemyState
    {
        ACTIVE,
        DEACTIVE
    }
}