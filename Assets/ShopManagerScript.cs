using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public int cantidadMonedas;
    public TextMeshProUGUI CoinsTXT;
    public GameObject shopUI;

    // Variable para controlar si los ítems con ID 1 y 2 han sido comprados
    private bool[] itemPurchased = new bool[5];

    void Start()
    {
        CoinsTXT.text = "Monedas:" + cantidadMonedas.ToString();

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Precio
        shopItems[2, 1] = 15;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 25;
        shopItems[2, 4] = 30;

        //Cantidad
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;
        int itemPrice = shopItems[2, itemID];

        // Verificar si hay suficientes monedas y si el ítem puede comprarse
        if (cantidadMonedas >= itemPrice && (itemID == 3 || itemID == 4 || !itemPurchased[itemID]))
        {
            cantidadMonedas -= itemPrice;
            shopItems[3, itemID]++;
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, itemID].ToString();
            UpdateCoinsText(); // Actualizar el texto de las monedas después de comprar

            // Marcar el ítem como comprado si tiene ID 1 o 2
            if (itemID == 1 || itemID == 2)
                itemPurchased[itemID] = true;
        }
    }

    void UpdateCoinsText()
    {
        CoinsTXT.text = "Monedas: " + cantidadMonedas;
    }
    private void Update()
    {
        cantidadMonedas = GameController.THIS.cantidadMonedas;
        CoinsTXT.text = "Monedas: " + cantidadMonedas;
    }
}