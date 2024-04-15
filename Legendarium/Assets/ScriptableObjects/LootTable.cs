using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable", order = 1)]
public class LootTable : ScriptableObject
{
    [SerializedDictionary("Item", "NumTickets")]
    public SerializedDictionary<GameObject,int> table;
    public int fakeTickets = 50;
    public int totalTickets()
    {
        int tickets = fakeTickets;
        foreach(var item in table)
        {
            tickets += item.Value;
        }
        return tickets;
    }
}
