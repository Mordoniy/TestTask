using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    public int charactersCountLeft;
    public int charactersCountRight;

    private List<Character> charactersLeft;
    private List<Character> charactersRight;

    [SerializeField] private Transform charactersLeftParent;
    [SerializeField] private Transform charactersRightParent;

    void Start()
    {
        charactersLeft = new List<Character>();
        charactersRight = new List<Character>();
        for (int i = 0; i < charactersCountLeft; i++)
            CreateCharacter(i, CharacterDir.Left);
        for (int i = 0; i < charactersCountRight; i++)
            CreateCharacter(i, CharacterDir.Right);
    }

    private void CreateCharacter(int i, CharacterDir dir)
    {
        Character character =
            Instantiate(Resources.Load<GameObject>("Prefabs/Character")).GetComponent<Character>();
        int type = Random.Range(0, 2);
        CharacterParameters parameters =
            type == 0 ? Settings.Instance.commonCharacter : Settings.Instance.eliteCharacter;
        parameters.dir = dir;
        switch (dir)
        {
            case CharacterDir.Left:
                character.transform.parent = charactersLeftParent;
                charactersLeft.Add(character);
                break;
            case CharacterDir.Right:
                character.transform.parent = charactersRightParent;
                charactersRight.Add(character);
                break;
        }

        character.Init(parameters, i);
    }

    void Update()
    {
    }
}