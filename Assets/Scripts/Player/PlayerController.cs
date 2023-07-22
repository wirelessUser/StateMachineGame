using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerView playerView;

        private int currentHealth;
        public Vector3 Position => playerView.transform.position;
        private List<EnemyController> enemiesInRange;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            playerView = Object.Instantiate(playerScriptableObject.PlayerPrefab);
            playerView.transform.position = playerScriptableObject.SpawnPosition;
            playerView.transform.rotation = Quaternion.Euler(playerScriptableObject.SpawnRotation);
            playerView.SetController(this);
        }

        private void InitializeVariables()
        {
            currentHealth = playerScriptableObject.MaximumHealth;
            enemiesInRange = new List<EnemyController>();
        }

        public void UpdatePlayer()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                HandleAttack();
        }

        public void FixedUpdatePlayer() => HandleMovement();

        private void HandleMovement()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            RotatePlayer(movementDirection);
            MovePlayer(movementDirection);
        }

        private void RotatePlayer(Vector3 movementDirection)
        {
            if (movementDirection != Vector3.zero)
                playerView.transform.eulerAngles = CalculateTargetRotation(movementDirection);
        }

        private Vector3 CalculateTargetRotation(Vector3 movementDirection)
        {
            float targetRotation = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            return Vector3.up * Mathf.MoveTowardsAngle(playerView.transform.eulerAngles.y, targetRotation, playerScriptableObject.RotationSpeed * Time.deltaTime);
        }

        private void MovePlayer(Vector3 movementDirection)
        {
            Vector3 moveVector = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * movementDirection;
            playerView.Rigidbody.MovePosition(playerView.Rigidbody.position + moveVector * playerScriptableObject.MovementSpeed * Time.deltaTime);
        }

        private void HandleAttack()
        {
            if(enemiesInRange.Count > 0)
            {
                GameService.Instance.SoundService.PlaySoundEffects(SoundType.PLAYER_ATTACK);
                foreach(EnemyController enemy in enemiesInRange)
                {
                    enemy.Die();
                }
                enemiesInRange.Clear();
            }
            else
            {
                GameService.Instance.SoundService.PlaySoundEffects(SoundType.PLAYER_SLASH);
            }
        }

        public void TakeDamage(int damageToInflict)
        {
            currentHealth -= damageToInflict;
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.PLAYER_HIT);
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                PlayerDied();
            }
            // TODO:    Update UI Health
        }

        private void PlayerDied()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.GAME_LOST);
            // Player Death & Game Over Logic
        }

        public void AddEnemy(EnemyController enemy) => enemiesInRange.Add(enemy);
            
        public void RemoveEnemy(EnemyController enemy) => enemiesInRange.Remove(enemy);
    }
}