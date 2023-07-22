using StatePattern.Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StatePattern.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController controller;
        [SerializeField] private ParticleSystem attackVFX;

        public Rigidbody Rigidbody { get; private set; }

        private void Start() => Rigidbody = GetComponent<Rigidbody>();

        public void SetController(PlayerController controllerToSet) => controller = controllerToSet;

        private void Update() => controller?.UpdatePlayer();

        private void FixedUpdate() => controller?.FixedUpdatePlayer();

        public void TakeDamage(int damage) => controller.TakeDamage(damage);

        public void PlayAttackVFX() => attackVFX.Play();

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                controller.AddEnemy(other.GetComponent<EnemyView>().Controller);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                controller.RemoveEnemy(other.GetComponent<EnemyView>().Controller);
            }
        }
    }
}