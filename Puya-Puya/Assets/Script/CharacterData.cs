using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public int maxHealth;
    public GameObject powerUpPrefab;
    // Ajoutez ici toutes les données spécifiques au personnage
}