using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //아이템 감지 범위
    public Vector3 lastPosition;                //플레이어의 마지막 위치 (플레이어 이동이 감지 될 경우 주변을 찾기 위한 변수)
    public float moveThreshold = 0.1f;                 //이동 감지 임계값
    public ConstructibleBuilding currentNearbyBuilding;   //가장 가까이 있는 수집 가능한 아이템
    public BuildingCrafter currentBuildingCrafter;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;                  //시작 시 현재 위치를 마지막 위치로 설정
        CheckForBuilding();                                    //초기 아이템 체크 수행
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 일정 거리 이상 이동했는지 체크
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                        //이동 시 아이템 체크
            lastPosition = transform.position;      //현재 위치를 마지막 위치로 어베이트
        }

        //가장 가까운 건물이 있고 F키를 눌렀을 때 건물 건설
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstruced)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());     //PlayerInventory 참조하여 아이템 수집
            }
            else if (currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding} 의 제작 메뉴 열기");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);
            }
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);           //감지 범위 내의 모든 콜라이더를 찾아옴

        float closestDistance = float.MaxValue;                  //가장 가까운 거리의 초기값
        ConstructibleBuilding closestBuilding = null;                     //가장 가까운 건물 초기값
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders)                                                 //각 콜라이더를 검사하여 수집 가능한 건물을 찾음
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();           //건물을 감지
            if (building != null)        //건물이 있고 수집 가능한지 확인
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //거리 계산
                if (distance < closestDistance)                                                     //더 가까운 아이템을 발견 시 업데이트
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();
                }
            }
        }

        if (closestBuilding != currentNearbyBuilding)           //가장 가까운 건물이 변경 되었을 때 메세지 표시
        {
            currentNearbyBuilding = closestBuilding;            //가장 가까운 건물 업데이트
            currentBuildingCrafter = closesCrafter;

            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstruced)
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show($" [F] 키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요)", currentNearbyBuilding.transform.position + Vector3.up * 0.5f);

                }
            }
        }
    }
}
