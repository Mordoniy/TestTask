using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public List<Material> characterMaterials;
    public List<Material> doorMaterials;

    public void Show()
    {
        StartCoroutine(ChangeAlpha(0, 1, .3f, characterMaterials));
    }

    public void Hide()
    {
        StartCoroutine(ChangeAlpha(1, 0, .3f, characterMaterials));
    }

    public void ShowDoor()
    {
        StartCoroutine(ChangeAlpha(0, 1, .1f, doorMaterials));
    }

    public void HideDoor()
    {
        StartCoroutine(ChangeAlpha(1, 0, 0, doorMaterials));
    }

    public void PlaySound(string clipName)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("Sound/" + clipName);
        audio.Play();
    }

    IEnumerator ChangeAlpha(float startAlpha, float targetAlpha, float time, List<Material> materials)
    {
        float maxTime = time;
        while (time > 0)
        {
            foreach (Material material in materials)
            {
                material.color = new Color(material.color.r, material.color.g, material.color.b,
                    targetAlpha - (targetAlpha - startAlpha) * (time / maxTime));
            }

            time -= Time.deltaTime;
            yield return null;
        }

        foreach (Material material in materials)
        {
            material.color = new Color(material.color.r, material.color.g, material.color.b,
                targetAlpha);
        }
    }
}