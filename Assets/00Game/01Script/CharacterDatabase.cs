using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public List<Sprite> characterSprite;
    public int characterCount
    {
        get { return characterSprite.Count; }
    }

    public Sprite GetChar(int index)
    {
        return characterSprite[index];
    }
}
