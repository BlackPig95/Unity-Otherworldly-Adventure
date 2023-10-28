using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public List<Sprite> characterSprite;
    public List<AnimatorController> characterAnimator;
    public int characterCount
    {
        get { return characterSprite.Count; }
    }
    public int animatorCount
    {
        get { return characterAnimator.Count; }
    }
    public Sprite GetChar(int index)
    {
        return characterSprite[index];
    }
    public AnimatorController GetAnimator(int index)
    {
        return characterAnimator[index];
    }
}
