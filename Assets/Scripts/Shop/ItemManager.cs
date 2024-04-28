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

    // Método para comprar un objeto de tipo 1 (mapa)
    public void BuyMap1()
    {
        // Implementa la lógica de compra para el mapa 1
        // Aquí puedes mostrar la imagen del mapa 1 en la UI
    }

    // Método para comprar un objeto de tipo 2 (mapa)
    public void BuyMap2()
    {
        // Implementa la lógica de compra para el mapa 2
        // Aquí puedes mostrar la imagen del mapa 2 en la UI
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
}
