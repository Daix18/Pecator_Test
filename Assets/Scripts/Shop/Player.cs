using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    public static Player Instance { get; private set; }

    public event EventHandler OnGoldAmountChanged;
    public event EventHandler OnHealthPotionAmountChanged;

    private int goldAmount;
    private int healthPotionAmount;


    #region Private


    private bool helmet;
    private int chest;
    public int sword;

    #endregion

    private void Awake()
    {
        Instance = this;

        healthPotionAmount = 1;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryConsumeHealthPotion();
        }
    }

    public void EquipWeapon_Punch()
    {
        //espada base (daño base)
        sword = 0;
    }

    public void EquipWeapon_Sword()
    {
        //actualiza el daño
        sword = 1;
    }

    public void EquipWeapon_Sword2()
    {
        //actualiza el daño
        sword = 2;
    }

    public void EquipArmorNone()
    {
        //vida base 
        chest = 0;
    }

    public void EquipArmor_1()
    {
        //actualiza la vida
        chest = 1;
    }

    public void EquipArmor_2()
    {
        //actualiza la vida
        chest = 2;
    }


    public void TryConsumeHealthPotion()
    {
        if (healthPotionAmount > 0)
        {
            healthPotionAmount--;
            OnHealthPotionAmountChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetHealthPotionAmount()
    {
        return healthPotionAmount;
    }

    private void AddHealthPotion()
    {
        healthPotionAmount++;
        OnHealthPotionAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddGoldAmount(int addGoldAmount)
    {
        goldAmount += addGoldAmount;
        OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
        switch (itemType)
        {
            case Item.ItemType.ArmorNone: EquipArmorNone(); break;
            case Item.ItemType.Armor_1: EquipArmor_1(); break;
            case Item.ItemType.Armor_2: EquipArmor_2(); break;
            case Item.ItemType.HealthPotion: AddHealthPotion(); break;
            case Item.ItemType.Sword_2: EquipWeapon_Sword2(); break;
        }
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if (GetGoldAmount() >= spendGoldAmount)
        {
            goldAmount -= spendGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }
}
