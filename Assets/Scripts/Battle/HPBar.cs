using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject health;


    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }
    public IEnumerator SetHPSmoothly(float finalValue)
    {
        float currentValue = health.transform.localScale.x;
        float changeAmount = currentValue - finalValue;

        while (Mathf.Abs(currentValue - finalValue) > 0.01f)
        {
            currentValue -= changeAmount * Time.deltaTime;
            health.transform.localScale = new Vector3(currentValue, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(finalValue, 1f);

    }

}
