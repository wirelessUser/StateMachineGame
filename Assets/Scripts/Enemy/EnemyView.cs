using StatePattern.Main;
using StatePattern.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public EnemyController Controller { get; private set; }
        [SerializeField] public NavMeshAgent Agent;
        private SphereCollider rangeTriggerCollider;
        private PlayerController target;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private ParticleSystem bloodSplatter;

        private void Start()
        {
            rangeTriggerCollider = GetComponent<SphereCollider>();
            Controller?.InitializeAgent();
        }

        public void SetController(EnemyController controllerToSet) => Controller = controllerToSet;

        public void SetTarget(PlayerController targetToSet) => target = targetToSet;

        public void SetTriggerRadius(float radiusToSet)
        {
            if (rangeTriggerCollider != null)
                rangeTriggerCollider.radius = radiusToSet;
        }

        public void PlayShootingEffect() => muzzleFlash.Play();

        private void Update() => Controller?.UpdateEnemy();

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerEnteredRange();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerExitedRange();
        }

        public void Destroy() => StartCoroutine(EnemyDeathSequence());

        private IEnumerator EnemyDeathSequence()
        {
            bloodSplatter.Play();
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }

    }
}