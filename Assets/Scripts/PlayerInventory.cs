using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;        //클래스 선언

    //각각 아이템 개수를 저장하는 변수
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

    //여러 아이템을 한꺼번에 획득
    public void AddItem(ItemType itemType, int amount)
    {
        //amount 만큼 여러번 AddItem 호출
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //아이템을 추가하는 함수, 아이템 종류에 따라서 해당 아이템의 개수를 증가 시킴
    public void AddItem(ItemType itemType)
    {
        //아이템 종류에 따른 다른 동작 수행
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {crystalCount}");
                break;

            case ItemType.Plant:
                plantCount++;
                Debug.Log($"식물 획득 ! 현재 개수 : {plantCount}");
                break;

            case ItemType.Bush:
                bushCount++;
                Debug.Log($"수풀 획득 ! 현재 개수 : {bushCount}");
                break;

            case ItemType.Tree:
                treeCount++;
                Debug.Log($"나무 획득 ! 현재 개수 : {treeCount}");
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
                    Debug.Log($"크리스탈 {amount} 사용 ! 현재 개수 : {crystalCount}");
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"식물 {amount} 사용 ! 현재 개수 : {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"수풀 {amount} 사용 ! 현재 개수 : {bushCount}");
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"나무 {amount} 사용 ! 현재 개수 : {treeCount}");
                    return true;
                }
                break;
        }
        return false;
    }

    public int GetItemCount(ItemType itemType)  //특정 아이템의 현재 개수를 반환하는 함수
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
        //I 키를 눌렀을 때 인벤토리 로그 내력을 보여줌
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======인벤토리=======");
        Debug.Log($"크리스탈: {crystalCount}개");
        Debug.Log($"식물: {plantCount}개");
        Debug.Log($"수풀: {bushCount}개");
        Debug.Log($"나무: {treeCount}개");
        Debug.Log("=====================");

    }
}
