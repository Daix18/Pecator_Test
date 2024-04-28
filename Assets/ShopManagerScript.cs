using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public int cantidadMonedas;
    public TextMeshProUGUI CoinsTXT;
    void Start()
    {
        CoinsTXT.text =  "Monedas:" + cantidadMonedas.ToString();

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

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (cantidadMonedas >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            cantidadMonedas -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString() ;
        }
    }
    void Update()
    {
        cantidadMonedas  =  GameController.THIS.cantidadMonedas;
        CoinsTXT.text = "Monedas: " + cantidadMonedas;

    }
}
