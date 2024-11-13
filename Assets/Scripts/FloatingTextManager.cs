using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;     //�̱���
    public GameObject textPrefabs;                  //UI �ؽ�Ʈ ������

    private void Awake()
    {
        instance = this;                            //�̱��� ���
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);       //���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ

        GameObject textOjb = Instantiate(textPrefabs, transform);           //UI �ؽ�Ʈ ����
        textOjb.transform.position = screenPos;

        TextMeshProUGUI temp = textOjb.GetComponent<TextMeshProUGUI>();

        if (temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textOjb));                           //���� �ִϸ��̼� ȿ�� ���
        }
    }

    private IEnumerator AnimateText(GameObject textObj)
    {
        float duration = 1f;        //���� �ð�
        float timer = 0;            //����� Ÿ�̸�

        Vector3 startPost = textObj.transform.position;
        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();     //�޾ƿ� ������Ʈ���� TMP ��Ʈ ����

        while (timer < duration)                                            //Ÿ�̸� 1�� ������
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textObj.transform.position = startPost + Vector3.up * (progress * 50f);     //��Ʈ�� ���� �ö󰡴� ȿ���� �ش�

            if (temp != null )
            {
                temp.alpha = 1 - progress;  
            }
            yield return null;
        }

        Destroy(textObj);
    }
}
