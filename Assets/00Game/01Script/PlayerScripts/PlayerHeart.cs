using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Image[] hearts;
    public int health;

    private void Start()
    {
        if(hearts == null)
        hearts = this.GetComponentsInChildren<Image>();
    }
    private void Update()
    {
        health = GameManager.Instance.playerController.playerHP;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;
        }
    }
}
