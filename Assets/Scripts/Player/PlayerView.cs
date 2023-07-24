using StatePattern.Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StatePattern.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController Controller { get; private set; }
        [SerializeField] private ParticleSystem attackVFX;

        public Rigidbody Rigidbody { get; private set; }

        private void Start() => Rigidbody = GetComponent<Rigidbody>();

        public void SetController(PlayerController controllerToSet) => Controller = controllerToSet;

        private void Update() => Controller?.UpdatePlayer();

        private void FixedUpdate() => Controller?.FixedUpdatePlayer();

        public void TakeDamage(int damage) => Controller.TakeDamage(damage);

        public void PlayAttackVFX() => attackVFX.Play();

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                Controller.AddEnemy(other.GetComponent<EnemyView>().Controller);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                Controller.RemoveEnemy(other.GetComponent<EnemyView>().Controller);
            }
        }
    }
}