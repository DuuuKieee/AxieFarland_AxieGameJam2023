using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Loot : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    public int dropChance, idItem;

    public Loot(string lootName, int dropChance, int idItem)
    {

        this.lootName = lootName;
        this.dropChance = dropChance;
        this.idItem = idItem;
    }
}