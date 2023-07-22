using System.Collections.Generic;
using UnityEngine;
using StatePattern.Main;
using StatePattern.Events;

namespace StatePattern.Level
{
    public class LevelService
    {
        private List<LevelScriptableObject> levelScriptableObjects;

        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(LoadLevel);

        public void LoadLevel(int levelID)
        {
            var levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == levelID);
            Object.Instantiate(levelData.LevelPrefab);
        }
    }
}