using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public Button[] characterButtons; // Tableau de boutons pour les personnages
    public Image[] selectedCharacters; // Tableau d'images pour les personnages sélectionnés par les joueurs
    public GameObject[] okBanners; // Tableau d'objets pour les bannières "OK"

    private int[] playerSelections = new int[2] {-1, -1}; // Indices des personnages sélectionnés par les joueurs (-1 = pas encore sélectionné)

    private void Start()
    {
        // Active tous les boutons de personnage
        foreach (Button button in characterButtons)
        {
            button.gameObject.SetActive(true);
        }

        // Désactive les bannières "OK" pour les deux joueurs
        foreach (GameObject banner in okBanners)
        {
            banner.SetActive(false);
        }
    }

    // Fonction appelée lorsqu'un joueur sélectionne un personnage
    public void SelectCharacter(int playerIndex, int characterIndex)
    {
        // Vérifie si le personnage a déjà été sélectionné par un autre joueur
        if (playerSelections[1 - playerIndex] == characterIndex)
        {
            // Désélectionne le personnage pour l'autre joueur
            selectedCharacters[1 - playerIndex].gameObject.SetActive(false);
            playerSelections[1 - playerIndex] = -1;
            okBanners[1 - playerIndex].SetActive(false);
        }

        // Sélectionne le personnage pour le joueur actuel
        selectedCharacters[playerIndex].sprite = characterButtons[characterIndex].GetComponent<Image>().sprite;
        selectedCharacters[playerIndex].gameObject.SetActive(true);
        playerSelections[playerIndex] = characterIndex;

        // Vérifie si les deux joueurs ont sélectionné un personnage
        if (playerSelections[0] != -1 && playerSelections[1] != -1)
        {
            // Active les bannières "OK" pour les deux joueurs
            okBanners[0].SetActive(true);
            okBanners[1].SetActive(true);
        }
    }

    // Fonction appelée lorsqu'un joueur annule sa sélection de personnage
    public void DeselectCharacter(int playerIndex)
    {
        // Désélectionne le personnage pour le joueur actuel
        selectedCharacters[playerIndex].gameObject.SetActive(false);
        playerSelections[playerIndex] = -1;

        // Désactive les bannières "OK" pour les deux joueurs
        okBanners[0].SetActive(false);
        okBanners[1].SetActive(false);
    }
}