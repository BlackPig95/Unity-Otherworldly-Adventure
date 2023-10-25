using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManagement : MonoBehaviour
{
    [SerializeField] CharacterDatabase characters;
    [SerializeField] Image selectedPlayer;
    int selectedIndex = 0;
    private void Start()
    {
        selectedPlayer = this.GetComponent<Image>();
    }
    public void NextOption()
    {
        Debug.Log("Next");
        selectedIndex++;
        if (selectedIndex >= characters.characterCount)
            selectedIndex = 0;
        Debug.Log("NExt  " + selectedIndex);
        UpdateCharacter();
    }
    public void PreviousOption()
    {
        Debug.Log("Previous");
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = characters.characterCount - 1;
        Debug.Log("Back  " + selectedIndex);
        UpdateCharacter();
    }
    public void UpdateCharacter()
    {
        selectedPlayer.sprite = characters.GetChar(selectedIndex);
    }
}
