using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum GameState
{
    WaitAction,
    SelectTarget,
    WaitAttack,
}

public class SceneController : MonoBehaviour
{
    public int charactersCountLeft;
    public int charactersCountRight;

    private List<Character> charactersLeft;
    private List<Character> charactersRight;
    private List<Character> orderMoves;
    private Character currentCharacter;
    private Character targetCharacter;

    [SerializeField] private UIController uiController;
    [SerializeField] private Transform charactersLeftParent;
    [SerializeField] private Transform charactersRightParent;

    private GameState state;

    GameState State
    {
        get => state;
        set
        {
            switch (value)
            {
                case GameState.WaitAction:
                    uiController.EnableButtons(true);
                    targetCharacter = null;
                    break;
                case GameState.SelectTarget:
                    break;
                case GameState.WaitAttack:
                    uiController.EnableButtons(false);
                    break;
            }

            state = value;
        }
    }

    void Start()
    {
        charactersLeft = new List<Character>();
        charactersRight = new List<Character>();
        orderMoves = new List<Character>();
        for (int i = 0; i < charactersCountLeft; i++)
            CreateCharacter(i, CharacterDir.Left);
        for (int i = 0; i < charactersCountRight; i++)
            CreateCharacter(i, CharacterDir.Right);
        StartRound();
        Character.CharacterDeath += CharacterDeath;
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

    void StartRound()
    {
        List<Character> allCharacters = new List<Character>();
        if (charactersLeft.Count == 0 || charactersRight.Count == 0)
        {
            SceneManager.LoadScene("Main");
            return;
        }

        for (int i = 0; i < charactersLeft.Count; i++)
            allCharacters.Add(charactersLeft[i]);
        for (int i = 0; i < charactersRight.Count; i++)
            allCharacters.Add(charactersRight[i]);
        orderMoves.Clear();
        while (allCharacters.Count > 0)
        {
            int rand = Random.Range(0, allCharacters.Count);
            orderMoves.Add(allCharacters[rand]);
            allCharacters.RemoveAt(rand);
        }

        SetNextCharacter();
    }

    void SetNextCharacter()
    {
        if (currentCharacter)
        {
            orderMoves.Remove(currentCharacter);
            currentCharacter.SetSelect(false);
        }

        if (orderMoves.Count == 0)
        {
            StartRound();
            return;
        }

        currentCharacter = orderMoves[0];
        currentCharacter.SetSelect(true);
    }

    void Update()
    {
        switch (State)
        {
            case GameState.WaitAction:
                break;
            case GameState.SelectTarget:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider)
                    {
                        Character select = hit.collider.GetComponent<Character>();
                        if (select && select.parameters.dir != currentCharacter.parameters.dir)
                        {
                            if (targetCharacter != null && targetCharacter != select)
                                targetCharacter.SetSelect(false);
                            targetCharacter = select;
                            targetCharacter.SetSelect(true);
                        }

                        if (Input.GetMouseButton(0))
                        {
                            currentCharacter.Attack(targetCharacter);
                            State = GameState.WaitAttack;
                        }
                    }
                }
                else if (targetCharacter)
                    targetCharacter.SetSelect(false);

                break;
            case GameState.WaitAttack:
                if (currentCharacter.animator.IsIdle && (targetCharacter.IsDeath || targetCharacter.animator.IsIdle))
                {
                    AttackCompleted();
                }

                break;
        }
    }

    void AttackCompleted()
    {
        if (!targetCharacter.IsDeath)
            targetCharacter.SetSelect(false);
        State = GameState.WaitAction;
        SetNextCharacter();
    }

    void CharacterDeath(Character character)
    {
        orderMoves.Remove(character);
        switch (character.parameters.dir)
        {
            case CharacterDir.Left:
                charactersLeft.Remove(character);
                break;
            case CharacterDir.Right:
                charactersRight.Remove(character);
                break;
        }
    }

    public void Attack()
    {
        State = GameState.SelectTarget;
    }

    public void Skip()
    {
        State = GameState.WaitAction;
        SetNextCharacter();
    }
}