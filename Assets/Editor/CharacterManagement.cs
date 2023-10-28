using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
using UnityEditor;
#endif

public class CharacterManagement : Singleton<CharacterManagement>
{
    [SerializeField] CharacterDatabase characters;
    [SerializeField] Image selectedPlayer;
    [SerializeField] GameObject playerSelectCanvas;
    [SerializeField] GameObject gameManager;
    int selectedIndex = 0;
    private void Start()
    {
        selectedPlayer = this.GetComponent<Image>();
    }
    public void NextOption()
    {
        selectedIndex++;
        if (selectedIndex >= characters.characterCount)
            selectedIndex = 0;
        UpdateCharacter();
    }
    public void PreviousOption()
    {
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = characters.characterCount - 1;
        UpdateCharacter();
    }
    public AnimatorController SelectPlayer()
    {
       // PlayerPrefs.SetInt("PlayerSelected", selectedIndex);
        AnimatorController playerAnimator = characters.GetAnimator(selectedIndex);
        return playerAnimator;
    }
    public void OkClicked()
    {
        gameManager.SetActive(true);
        playerSelectCanvas.SetActive(false);
    }
    public void UpdateCharacter()
    {
        selectedPlayer.sprite = characters.GetChar(selectedIndex);
    }
}
