using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI text;
    private PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // Todo esto es super temporal, solo para ver en la pantalla cuantas potas tenemos
        string items = "";
        var dict = playerInventory.GetItemsQuantities();
        foreach (KeyValuePair<string, int> entry  in dict)
        {
            items += entry.Key + ": " + entry.Value + " ";
        }
     
        text.text = "Max inventory size: " + playerInventory.maxSize + " " + items;
    }
}
