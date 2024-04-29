using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Singleton para acceder desde otros scripts
    public static ItemManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para comprar un objeto de tipo 1 (mapa)
    public void BuyMap1()
    {
        // Implementa la l�gica de compra para el mapa 1
        // Aqu� puedes mostrar la imagen del mapa 1 en la UI
    }

    // M�todo para comprar un objeto de tipo 2 (mapa)
    public void BuyMap2()
    {
        // Implementa la l�gica de compra para el mapa 2
        // Aqu� puedes mostrar la imagen del mapa 2 en la UI
    }

    // M�todo para comprar un objeto de tipo 3 (da�o)
    public void BuyDamageItem()
    {
        // Implementa la l�gica de compra para el objeto de da�o
        // Aqu� puedes incrementar el da�o del personaje principal
        // Calcula el nuevo da�o basado en la cantidad de veces que se ha comprado
        float newDamage = AttackController.THIS.danoGolpe + 5;
        AttackController.THIS.danoGolpe = newDamage;
    }

    // M�todo para comprar un objeto de tipo 4 (vida)
    public void BuyLifeItem()
    {
        // Implementa la l�gica de compra para el objeto de vida
        // Aqu� puedes incrementar la vida del personaje principal
        // Calcula la nueva vida basada en la cantidad de veces que se ha comprado
        float newLife = AttackController.THIS.initialHealth + 10;
        AttackController.THIS.health = newLife;
        AttackController.THIS.initialHealth = newLife;
    }
}
