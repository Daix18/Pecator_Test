using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public TextMeshProUGUI CoinsTXT;

    // Variable para controlar si los ítems con ID 1 y 2 han sido comprados
    private bool[] itemPurchased = new bool[5];

    // GameObjects para los mapas
    public GameObject mapaSimple;
    public GameObject mapaComplejo;

    void Start()
    {
        CoinsTXT.text = "Monedas:" + GameController.THIS.cantidadMonedas.ToString();

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

        // Desactivar los mapas al inicio
        mapaSimple.SetActive(false);
        mapaComplejo.SetActive(false);
    }

    private void Update()
    {
        CoinsTXT.text = "Monedas: " + GameController.THIS.cantidadMonedas;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;
        int itemPrice = shopItems[2, itemID];

        // Verificar si hay suficientes monedas y si el ítem puede comprarse
        if (GameController.THIS.cantidadMonedas >= itemPrice && (itemID == 3 || itemID == 4 || !itemPurchased[itemID]))
        {
            GameController.THIS.cantidadMonedas -= itemPrice;
            shopItems[3, itemID]++;
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, itemID].ToString();
            UpdateCoinsText(); // Actualizar el texto de las monedas después de comprar

            // Limitar que solo puedes comprar un mapa.
            // Marcar el ítem como comprado si tiene ID 1 o 2
            if (itemID == 1 || itemID == 2)
                itemPurchased[itemID] = true;

            switch (itemID)
            {
                case 1:
                    BuyMap1();
                    break;
                case 2:
                    BuyMap2();
                    break;
                case 3:
                    BuyDamageItem();
                    break;
                case 4:
                    BuyLifeItem();
                    break;
                default:
                    // Manejar el caso por defecto si el itemID no coincide con ninguno de los casos anteriores
                    break;
            }
        }
    }

    public void BuyMap1()
    {
        // Implementa la lógica de compra para el mapa 1
        // Aquí puedes mostrar la imagen del mapa 1 en la UI
        mapaSimple.SetActive(true);
    }

    // Método para comprar un objeto de tipo 2 (mapa)
    public void BuyMap2()
    {
        // Implementa la lógica de compra para el mapa 2
        // Aquí puedes mostrar la imagen del mapa 2 en la UI
        mapaComplejo.SetActive(true);
    }

    // Método para comprar un objeto de tipo 3 (daño)
    public void BuyDamageItem()
    {
        // Implementa la lógica de compra para el objeto de daño
        // Aquí puedes incrementar el daño del personaje principal
        // Calcula el nuevo daño basado en la cantidad de veces que se ha comprado
        float newDamage = AttackController.THIS.danoGolpe + 5;
        AttackController.THIS.danoGolpe = newDamage;
    }

    // Método para comprar un objeto de tipo 4 (vida)
    public void BuyLifeItem()
    {
        // Implementa la lógica de compra para el objeto de vida
        // Aquí puedes incrementar la vida del personaje principal
        // Calcula la nueva vida basada en la cantidad de veces que se ha comprado
        float newLife = AttackController.THIS.initialHealth + 10;
        AttackController.THIS.health = newLife;
        AttackController.THIS.initialHealth = newLife;
    }

    void UpdateCoinsText()
    {
        CoinsTXT.text = "Monedas: " + GameController.THIS.cantidadMonedas;
    }
}