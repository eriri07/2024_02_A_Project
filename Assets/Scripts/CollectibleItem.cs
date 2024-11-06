using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;       //������ ����
    public string itemName;         //������ �̸�
    public float respawnTime = 30.0f;   //������ �ð�
    public bool canCollect = true;      //���� ���� ����

    //�������� �����ϴ� �޼���, PlayerInventory�� ���� �κ��丮�� �߰�
    public void CollectItem(PlayerInventory inventory)
    {
        //���� ���� ���θ� üũ
        if (!canCollect) return;

        inventory.AddItem(itemType);    //�������� �κ��丮�� �߰�
        Debug.Log($"{itemName} ���� �Ϸ�");     //������ ���� �Ϸ� �޼��� ���
        StartCoroutine(RespawnRoutine());       //������ ������ �ڷ�ƾ ����
    }

    //������ �������� ó���ϴ� �ڷ�ƾ
    private IEnumerator RespawnRoutine()
    {
        canCollect = false;                             //���� �Ұ��� ���·� ����
        GetComponent<MeshRenderer>().enabled = false;   //�������� MeshRenderer�� ���� ������ �ʰ� ��
        GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(respawnTime);   //������ ������ �ð� ��ŭ ���

        GetComponent<MeshRenderer>().enabled = true;    //�������� �ٽ� ���̰� ��
        GetComponent<MeshCollider>().enabled = true;
        canCollect = true;                              //���� ���� ���·� ����

    }
}
