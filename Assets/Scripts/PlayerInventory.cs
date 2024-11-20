using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;        //Ŭ���� ����

    //���� ������ ������ �����ϴ� ����
    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.VeagetableStew:
                RemoveItem(ItemType.VeagetableStew, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;

            case ItemType.FruitSalad:
                RemoveItem(ItemType.FruitSalad, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[1].hungerRestoreAmount);
                break;

            case ItemType.RepairKit:
                RemoveItem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.WorkbenchRecipes[0].repairAmount);
                break;
        }
    }

    //���� �������� �Ѳ����� ȹ��
    public void AddItem(ItemType itemType, int amount)
    {
        //amount ��ŭ ������ AddItem ȣ��
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //�������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� �������� ������ ���� ��Ŵ
    public void AddItem(ItemType itemType)
    {
        //������ ������ ���� �ٸ� ���� ����
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� : {crystalCount}");
                break;

            case ItemType.Plant:
                plantCount++;
                Debug.Log($"�Ĺ� ȹ�� ! ���� ���� : {plantCount}");
                break;

            case ItemType.Bush:
                bushCount++;
                Debug.Log($"��Ǯ ȹ�� ! ���� ���� : {bushCount}");
                break;

            case ItemType.Tree:
                treeCount++;
                Debug.Log($"���� ȹ�� ! ���� ���� : {treeCount}");
                break;
        }
    }

    public bool RemoveItem(ItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ��� ! ���� ���� : {crystalCount}");
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"�Ĺ� {amount} ��� ! ���� ���� : {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"��Ǯ {amount} ��� ! ���� ���� : {bushCount}");
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"���� {amount} ��� ! ���� ���� : {treeCount}");
                    return true;
                }
                break;
        }
        return false;
    }

    public int GetItemCount(ItemType itemType)  //Ư�� �������� ���� ������ ��ȯ�ϴ� �Լ�
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            default:
                return 0;
        }
    }

    private void Update()
    {
        //I Ű�� ������ �� �κ��丮 �α� ������ ������
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======�κ��丮=======");
        Debug.Log($"ũ����Ż: {crystalCount}��");
        Debug.Log($"�Ĺ�: {plantCount}��");
        Debug.Log($"��Ǯ: {bushCount}��");
        Debug.Log($"����: {treeCount}��");
        Debug.Log("=====================");

    }
}
