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
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => enemyScriptableObject;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;

        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            InitializeView();
            InitializeVariables();
            CreateStateMachine();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(enemyScriptableObject.EnemyPrefab);
            enemyView.transform.position = enemyScriptableObject.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(enemyScriptableObject.SpawnRotation);
            
        }

        private void InitializeVariables()
        {
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
            /*  TODO : 
             *  Play Enemy Death Sound.
             *  Play Particle Effects if any.
             * */
        }

        public void SetRotaion(Vector3 eulerAngles) => enemyView.transform.rotation = Quaternion.Euler(eulerAngles);

        public void SetRotaion(Quaternion desiredRotation) => enemyView.transform.rotation = desiredRotation;

        public virtual void PlayerEnteredRange() { }

        public virtual void PlayerExitedRange() { }

        public virtual void CreateStateMachine() { }

        public virtual void UpdateEnemy() { }

    }
}