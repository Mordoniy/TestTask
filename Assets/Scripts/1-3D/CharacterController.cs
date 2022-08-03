using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public List<Material> characterMaterials;
    public List<Material> doorMaterials;

    public void Show()
    {
        StartCoroutine(Functions.ChangeAlpha(0, 1, .3f, characterMaterials));
    }

    public void Hide()
    {
        StartCoroutine(Functions.ChangeAlpha(1, 0, .3f, characterMaterials));
    }

    public void ShowDoor()
    {
        StartCoroutine(Functions.ChangeAlpha(0, 1, .1f, doorMaterials));
    }

    public void HideDoor()
    {
        StartCoroutine(Functions.ChangeAlpha(1, 0, 0, doorMaterials));
    }

    public void PlaySound(string clipName)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clipName);
        audio.Play();
    }
}