using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Couleur
{
    Jaune,
    Vert,
    Rouge,
    Violet,
    Bleu,
    Noir
}

[CreateAssetMenu(fileName = "Puya", menuName = "ScriptableObjects/Puya", order = 1)]

public class Puya :ScriptableObject
{
    static int nbPuya=0;

    public int id;
    public Couleur color;
    public List<Puya> listChain;

    void start()
    {
        listChain.Add(this);
        nbPuya++;
        id = nbPuya;
    }
}
