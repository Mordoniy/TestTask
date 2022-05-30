using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject buttonsParent;

    public void EnableButtons(bool enable)
    {
        buttonsParent.SetActive(enable);
    }

    public void Attack()
    {
        sceneController.Attack();
    }

    public void Skip()
    {
        sceneController.Skip();
    }
}
