using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Button[] characterButtons;
    public Image selectedCharacterP1;
    public Image selectedCharacterP2;
    public GameObject okBannerP1;
    public GameObject okBannerP2;
    public Sprite[] characterImages;
    public int playerSelectionP1 = -1;
    public int playerSelectionP2 = -1;
    public InputActionAsset iaa;
    private bool isSelectionValidatedP1 = false;
    private bool isSelectionValidatedP2 = false;

    private void Awake()
    {
        okBannerP1.SetActive(false);
        okBannerP2.SetActive(false);
        playerSelectionP1 = 0;
        playerSelectionP2 = 1;

        var mapP1 = iaa.FindActionMap("MenuP1");
        mapP1.Enable();
        mapP1.FindAction("Gauche/Droite").performed += context => SelectCharacterP1(new Vector2(context.ReadValue<float>(), 0));
        mapP1.FindAction("Haut/Bas").performed += context => SelectCharacterP1(new Vector2(0, context.ReadValue<float>()));
        mapP1.FindAction("Valider").performed += context => SubmitCharacterSelectionP1();
        mapP1.FindAction("Retour").performed += context => CancelCharacterSelectionP1();

        var mapP2 = iaa.FindActionMap("MenuP2");
        mapP2.Enable();
        mapP2.FindAction("Gauche/Droite").performed += context => SelectCharacterP2(new Vector2(context.ReadValue<float>(), 0));
        mapP2.FindAction("Haut/Bas").performed += context => SelectCharacterP2(new Vector2(0, context.ReadValue<float>()));
        mapP2.FindAction("Valider").performed += context => SubmitCharacterSelectionP2();
        mapP2.FindAction("Retour").performed += context => CancelCharacterSelectionP2();
    }

    private void SelectCharacterP1(Vector2 direction)
    {
        if (isSelectionValidatedP1) return; // empêche la sélection si un personnage a déjà été validé

        selectedCharacterP1.gameObject.SetActive(true);
        int newIndex = playerSelectionP1;
    
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Déplacement gauche/droite
            int directionX = (int)Mathf.Sign(direction.x);
            int currentColumn = newIndex % 2;
            int newColumn = (currentColumn + directionX + 2) % 2;
            int newRow = Mathf.FloorToInt(newIndex / 2f);
            newIndex = newColumn + newRow * 2;

            if (newIndex < 0 || newIndex >= characterButtons.Length)
            {
                return;
            }
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && Mathf.Abs(direction.y) > 0.5f)
        {
            // Déplacement haut/bas
            newIndex = (newIndex + 2 * (int)Mathf.Sign(direction.y) + characterButtons.Length) % characterButtons.Length;
        }

        selectedCharacterP1.sprite = characterImages[newIndex];
        playerSelectionP1 = newIndex;
        characterButtons[newIndex].Select();

        PlayerPrefs.SetInt("ImageP1", newIndex);
    }

    private void SubmitCharacterSelectionP1()
    {
        if (playerSelectionP1 != -1)
        {
            okBannerP1.SetActive(true);
            isSelectionValidatedP1 = true; // met à jour la validation de sélection

        }
    }

    private void CancelCharacterSelectionP1()
    {
        if (playerSelectionP1 != -1 && okBannerP1.activeSelf)
        {
            okBannerP1.SetActive(false);
            characterButtons[playerSelectionP1].interactable = true;
            playerSelectionP1 = -1;
            isSelectionValidatedP1 = false; // met à jour la validation de sélection

        }
    }

    private void SelectCharacterP2(Vector2 direction)
    {
        if (isSelectionValidatedP2) return; // empêche la sélection si un personnage a déjà été validé
        int newIndex = playerSelectionP2;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Déplacement gauche/droite
            int directionX = (int)Mathf.Sign(direction.x);
            int currentColumn = newIndex % 2;
            int newColumn = (currentColumn + directionX + 2) % 2;
            int newRow = Mathf.FloorToInt(newIndex / 2f);
            newIndex = newColumn + newRow * 2;

            if (newIndex < 0 || newIndex >= characterButtons.Length)
            {
                return;
            }
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && Mathf.Abs(direction.y) > 0.5f)
        {
            // Déplacement haut/bas
            newIndex = (newIndex + 2 * (int)Mathf.Sign(direction.y) + characterButtons.Length) % characterButtons.Length;
        }

        selectedCharacterP2.sprite = characterImages[newIndex];
        playerSelectionP2 = newIndex;
        characterButtons[newIndex].Select();
        PlayerPrefs.SetInt("ImageP2", newIndex);
    }
    private void SubmitCharacterSelectionP2()
    {
        if (playerSelectionP2 != -1)
        {
            okBannerP2.SetActive(true);
            isSelectionValidatedP2 = true; // met à jour la validation de sélection

        }
    }

    private void CancelCharacterSelectionP2()
    {
        if (playerSelectionP2 != -1 && okBannerP2.activeSelf)
        {
            okBannerP2.SetActive(false);
            characterButtons[playerSelectionP2].interactable = true;
            playerSelectionP2 = -1;
            isSelectionValidatedP2 = false; // met à jour la validation de sélection

        }
    }
}
