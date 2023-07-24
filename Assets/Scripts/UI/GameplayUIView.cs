using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StatePattern.UI
{
    public class GameplayUIView : MonoBehaviour
    {
        private GameplayUIController controller;
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        [SerializeField] private Image playerHealth;

        public void SetController(GameplayUIController controllerToSet) => controller = controllerToSet;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void UpdateEnemyCounterText(string enemyCounter) => enemyCounterText.SetText(enemyCounter);

        public void UpdatePlayerHealthUI(float helathRatio) => playerHealth.transform.localScale = new Vector3(helathRatio, 1, 1);
    }
}