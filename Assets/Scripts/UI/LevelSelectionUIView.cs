using UnityEngine;

namespace StatePattern.UI
{
    public class LevelSelectionUIView : MonoBehaviour
    {
        [SerializeField] private Transform levelButtonContainer;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public LevelButtonView AddButton(LevelButtonView levelButtonPrefab) => Instantiate(levelButtonPrefab, levelButtonContainer);
    }
}