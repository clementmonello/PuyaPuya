using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Button[] characterButtons;
    public Image selectedCharacter;
    public GameObject okBanner; 
    public Sprite[] characterImages; 

    public CharacterData selectedCharacterData;
    
    private int playerSelection = -1; 

    private void Start()
    {
        foreach (Button button in characterButtons)
        {
            button.gameObject.SetActive(true);
        }
        
        selectedCharacter.gameObject.SetActive(false);
        okBanner.SetActive(false);
    }

    public void SelectCharacter(int characterIndex)
    {
        if (playerSelection == characterIndex)
        {
            selectedCharacter.gameObject.SetActive(false);
            playerSelection = -1;
            okBanner.SetActive(false);
        }
        else
        {
            selectedCharacter.sprite = characterImages[characterIndex];
            selectedCharacter.gameObject.SetActive(true);
            playerSelection = characterIndex;

            okBanner.SetActive(true);
        }
    }

    public void DeselectCharacter()
    {
        selectedCharacter.gameObject.SetActive(false);
        playerSelection = -1;

        okBanner.SetActive(false);
    }
}