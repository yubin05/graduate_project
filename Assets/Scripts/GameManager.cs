// This is test script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // skill cooltime control by enable/disable skill script
    public IEnumerator OnSkillCoolTime(Object script, float cooltime)
    {
        Debug.Log("��Ÿ�� ����");

        while (cooltime > 1.0f)
        {
            cooltime -= Time.deltaTime;
            // skill_UI.fillAmount = (1.0f / cooltime)
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("��Ÿ�� ����");
    }
}
