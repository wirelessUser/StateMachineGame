using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Level Selection UI")]
        private LevelSelectionUIController levelSelectionController;
        [SerializeField] private LevelSelectionUIView levelSelectionView;
        [SerializeField] private LevelButtonView levelButtonPrefab;

        [Header("Level ENd UI")]
        private LevelEndUIController levelEndController;
        [SerializeField] private LevelEndUIView levelEndView;

        private void Start()
        {
            levelSelectionController = new LevelSelectionUIController(levelSelectionView, levelButtonPrefab);
            levelEndController = new LevelEndUIController(levelEndView);
        }

        public void Init(int levelCount) => ShowLevelSelectionUI(levelCount);

        private void ShowLevelSelectionUI(int levelCount) => levelSelectionController.Show(levelCount);

        public void GameWon() => levelEndController.PlayerWon();

        public void GameLost() => levelEndController.PlayerLost();

    }
}