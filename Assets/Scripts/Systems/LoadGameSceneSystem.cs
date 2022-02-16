using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game {
    sealed class LoadGameSceneSystem : IEcsInitSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly StaticData staticData = null;
        readonly GameSceneDataView sceneData = null;

        private LevelsStaticData levelsStaticData;
        public void Init ()
        {
            levelsStaticData = staticData.LevelsStaticData;

            int loadingLevelIndex = GetLoadingLevelIndex();
            LevelStaticData levelStaticData;
            try
            {
                levelStaticData = levelsStaticData.LevelStaticDatas[loadingLevelIndex];
            }
            catch (System.Exception)
            {
                int newLevelIndex;
                if (levelsStaticData.LevelStaticDatas.Length == 0)
                {
                    Debug.LogError($"No level was assigned");
                    return;

                }
                if (loadingLevelIndex < 0)
                {
                    newLevelIndex = 0;
                }
                else
                {
                    newLevelIndex = levelsStaticData.LevelStaticDatas.Length - 1;
                }
                Debug.LogError($"Was tried to load level {loadingLevelIndex}. System will load {newLevelIndex}.");
                loadingLevelIndex = newLevelIndex;
                levelStaticData = levelsStaticData.LevelStaticDatas[loadingLevelIndex];
            }

            SpawnObjectsOnPoint(levelsStaticData.PostPrefab, sceneData.SpawnPost.position, levelsStaticData.DistanceBetweenPosts, Vector3.right, levelStaticData.PostCount);
            SpawnObjectsOnPoint(levelsStaticData.CharacterPrefab, sceneData.SpawnCharacter.position, levelsStaticData.DistanceBetweenCharacters, Vector3.right, levelStaticData.CharactersCount);
        }

        private static void SpawnObjectsOnPoint(GameObject prefab, Vector3 spawnPos, float spawnDistance, Vector3 spawnDirection, int spawnCount)
        {
            Transform parent = new GameObject(prefab.name).transform;
            for (int i = 0; i < spawnCount; i++)
            {
                float positionInRow = spawnDistance * i - (float)(spawnCount-1)*spawnDistance / 2f;
                var postGO = prefab.Instantiate(parent, spawnPos + spawnDirection * positionInRow);
            }
        }

        private int GetLoadingLevelIndex()
        {
            return levelsStaticData.LoadingLevelIndex;
        }
    }
}