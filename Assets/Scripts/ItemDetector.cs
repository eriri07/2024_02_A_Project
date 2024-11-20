using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//아이템 종류 정의
public enum ItemType
{
    Crystal,
    Plant,
    Bush,
    Tree,
    VeagetableStew,
    FruitSalad,
    RepairKit
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //아이템 감지 범위
    public Vector3 lastPosition;                //플레이어의 마지막 위치 (플레이어 이동이 감지 될 경우 주변을 찾기 위한 변수)
    public float moveThreshold;                 //이동 감지 임계값
    public CollectibleItem currentNearbyItem;   //가장 가까이 있는 수집 가능한 아이템

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;                  //시작 시 현재 위치를 마지막 위치로 설정
        CheckForItems();                                    //초기 아이템 체크 수행
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 일정 거리 이상 이동했는지 체크
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)     
        {
            CheckForItems();                        //이동 시 아이템 체크
            lastPosition = transform.position;      //현재 위치를 마지막 위치로 어베이트
        }

        //가장 가까운 아이템이 있고 E키를 눌렀을 때 아이템 수집
        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());     //PlayerInventory 참조하여 아이템 수집
        }
    }

    //주변의 수집 가능한 아이템을 감지하는 함수

    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);           //감지 범위 내의 모든 콜라이더를 찾아옴

        float closestDistance = float.MaxValue;                  //가장 가까운 거리의 초기값
        CollectibleItem closestItem = null;                     //가장 가까운 아이템 초기값

        foreach (Collider collider in hitColliders)                                                 //각 콜라이더를 검사하여 수집 가능한 아이템을 찾음
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();           //아이템을 감지
            if (item != null && item.canCollect)        //아이템이 있고 수집 가능한지 확인
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);     //거리 계산
                if (distance < closestDistance)                                                     //더 가까운 아이템을 발견 시 업데이트
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (closestItem != currentNearbyItem)           //가장 가까운 아이템이 변경 되었을 때 메세지 표시
        {
            currentNearbyItem = closestItem;            //가장 가까운 아이템 업데이트
            if (currentNearbyItem != null)
            {
                Debug.Log($" [E] 키를 눌러 {currentNearbyItem.itemName} 수집");       //새로운 아이템 수집 메세지 표시
            }
        }
    }

    private void OnDrawGizmos()                 //유니티 Scene창에 보이는 Debug 그림
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
