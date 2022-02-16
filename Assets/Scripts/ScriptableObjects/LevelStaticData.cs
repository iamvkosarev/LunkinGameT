using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelStaticData")]
public class LevelStaticData : ScriptableObject
{
    public int PostCount;
    public int CharactersCount;
    public Color[] Colors;
    public int CharactersColorsCount;
}
