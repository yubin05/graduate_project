using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUIController : MonoBehaviour
{
    Image cooltimeImage;

    private void Start()
    {
        cooltimeImage = GetComponent<Image>();
    }

    // this method triggered when skill start
    // this method controled by other script
    public IEnumerator OnCoolTime(float end_time)
    {
        cooltimeImage.fillAmount = 0f;  // cooltime start

        float running_time = 0f;
        while (running_time < end_time)
        {
            running_time += Time.deltaTime;
            cooltimeImage.fillAmount = running_time/end_time;
            yield return new WaitForFixedUpdate();
        }
    }
}
