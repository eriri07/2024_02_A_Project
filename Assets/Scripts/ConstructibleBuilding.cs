using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("BUilding Setting")]
    public BuildingType BuildingType;           //건물 타입 설정
    public string buildingName;                 //건물 이름
    public int requiredTree = 5;                //건물 건설에 필요한 나무 숫자
    public float constructionTime = 2.0f;       //건물 건설 시간

    public bool canBuild = true;                //건설 가능 변수
    public bool isConstruced = false;           //건설 완료 변수

    private Material buildingMaterial;          //건물의 메테리얼 참조

    // Start is called before the first frame update
    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        //초기 상태 설정(반투명)
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;
    }

    private IEnumerator ConstructionRoutine()
    {
        canBuild = false;
        float timer = 0;

        Color color = buildingMaterial.color;

        while (timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstruced = true;

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"{buildingName} 건설 완료!", transform.position + Vector3.up);
        }
    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstruced) return;

        if (inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);

            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"{buildingName} 건설 시작!", transform.position + Vector3.up);
            }

            StartCoroutine(ConstructionRoutine());
        }
        else
        {
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"나무가 부족합니다! ({inventory.treeCount} / {requiredTree})", transform.position + Vector3.up);
            }
        }
    }
}
