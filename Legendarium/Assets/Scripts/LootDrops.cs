using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrops : MonoBehaviour
{
    [SerializeField] GameObject[] GuarenteedDrops;
    [SerializeField] LootTable lootTable;
    public void DropItem()
    {
        Debug.Log(this.name + " attempting to drop item ");
        foreach (GameObject item in GuarenteedDrops)
        {
            Drop(item);
        }
        int luck = Random.Range(0, lootTable.totalTickets());
        if (luck <= lootTable.fakeTickets) return;
        luck -= lootTable.fakeTickets;
        foreach(var index in lootTable.table)
        {
            if (luck <= index.Value)
            {
                Drop(index.Key);
                return;
            }
        }
    }
    

    void Drop(GameObject obj)
    {
        Debug.Log(this.name + " dropped " + obj);
        Instantiate(obj, (Vector2)transform.position + randomizedPosition(), Quaternion.identity);
    }
    Vector2 randomizedPosition()
    {
        float xOffset = Random.Range(-.3f, .3f);
        float yOffset = Random.Range(-.3f, .3f);
        return new Vector2(xOffset, yOffset);
    }
}
