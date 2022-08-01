using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaPrefab : DefaultObserverEventHandler
{
    public ImageTargetBehaviour target;
    public GameObject prefab;
 
    protected override void OnTrackingFound()
    {
        InstantiateContent();
    }
 
    void InstantiateContent()
    {
        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, target.transform, true);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = new Vector3(-90, 180, 0);
            obj.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            obj.transform.gameObject.SetActive(true);
        }
    }
}
