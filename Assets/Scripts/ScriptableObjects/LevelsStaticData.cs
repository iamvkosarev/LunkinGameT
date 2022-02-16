using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelsStaticData")]
public class LevelsStaticData : ScriptableObject
{
    public int LoadingLevelIndex;
    public LevelStaticData[] LevelStaticDatas;
    public float DistanceBetweenPosts;
    public float DistanceBetweenCharacters;
    public GameObject PostPrefab;
    public GameObject CharacterPrefab;
}
