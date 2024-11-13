using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;            //������ ���� ����
    public Vector3 lastPosition;                //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold;                 //�̵� ���� �Ӱ谪
    public ConstructibleBuilding currentNearbyBuilding;   //���� ������ �ִ� ���� ������ ������
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;                  //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForBuilding();                                    //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                        //�̵� �� ������ üũ
            lastPosition = transform.position;      //���� ��ġ�� ������ ��ġ�� ���Ʈ
        }

        //���� ����� �������� �ְ� EŰ�� ������ �� ������ ����
        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());     //PlayerInventory �����Ͽ� ������ ����
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);           //���� ���� ���� ��� �ݶ��̴��� ã�ƿ�

        float closestDistance = float.MaxValue;                  //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                     //���� ����� ������ �ʱⰪ

        foreach (Collider collider in hitColliders)                                                 //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();           //�������� ����
            if (building != null && building.canBuild && !building.isConstruced)        //�������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);     //�Ÿ� ���
                if (distance < closestDistance)                                                     //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestBuilding = building;
                }
            }
        }

        if (closestBuilding != currentNearbyBuilding)           //���� ����� �������� ���� �Ǿ��� �� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;            //���� ����� ������ ������Ʈ
            if (currentNearbyBuilding != null)
            {
                if (FloatingTextManager.instance != null)
                {
                    Vector3 textPosition = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show($" [F] Ű�� {currentNearbyBuilding.buildingName} �Ǽ� (���� {currentNearbyBuilding.requiredTree} �� �ʿ�)", currentNearbyBuilding.transform.position + Vector3.up * 0.5f);

                }
            }
        }
    }
}
