using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Image fgImg;
    [SerializeField]
    private float updateSpd = .5f;
    private void Awake()
    {
        if (GetComponentInParent<PlayerController>() != null)
        {
            GetComponentInParent<PlayerController>().OnHealthPercentChanged += HealthChangeHdlr;
        }
        else
        {
            GetComponentInParent<RangeEnemyController>().OnREHelathPercentChanged += HealthChangeHdlr;
        }
        
    }
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
    private void HealthChangeHdlr(float percent)
    {
        //fgImg.fillAmount = percent;
        StartCoroutine(ChangeToPercent(percent));
    }
    //Smooth decrease for the health bar.
    private IEnumerator ChangeToPercent(float percent)
    {
        float preChangePercent = fgImg.fillAmount;
        float elapsedTime = 0f;
        while (elapsedTime < updateSpd)
        {
            elapsedTime += Time.deltaTime;
            fgImg.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsedTime / updateSpd);
            yield return null;
        }
        fgImg.fillAmount = percent;
    }
}
