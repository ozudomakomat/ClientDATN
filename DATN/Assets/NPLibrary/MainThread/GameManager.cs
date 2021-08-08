﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Editor
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager gameManager;

    private ReorderableList managers;

    public void OnEnable()
    {

        gameManager = (GameManager)target;

        CustomList(out managers, "managers");
    }

    private void CustomList(out ReorderableList list, string fieldName)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(fieldName), true, true, true, true);

        list.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, fieldName, EditorStyles.boldLabel);
        };

        ReorderableList a = list;

        list.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = a.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        managers.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion

public enum GameState { None, Playing, Win, Over }

public class GameManager : Manager
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                instance.Init();
            }
            return instance;
        }
    }

    private static GameState gameState = GameState.None;

    [HideInInspector]
    [SerializeField] private List<Manager> managers = new List<Manager>();

    public List<Manager> Managers
    {
        get
        {
            return managers;
        }
    }

    public Manager GetManager<T>()
    {
        return managers.Find((Manager obj) => obj is T);
    }

    protected delegate void ActionManager(IManager manager);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            RunAllAction((IManager manager) => manager.Init());
            SendEvent(EventName.MANAGER_INIT, null);
        }
    }
    protected virtual void RunAllAction(ActionManager actionManager)
    {
        foreach (var action in managers)
        {
            if (action == null)
            {
                Debug.LogError("Manager not found");
                continue;
            }
            if (actionManager != null) actionManager.Invoke(action);
        }
    }
    public void SendEvent(EventName eventName, object data)
    {
        EventDispatcher.Instance.Dispatch(eventName, data);
    }
    public override void GameOver()
    {
        RunAllAction((IManager manager) => manager.GameOver());

        SendEvent(EventName.MANAGER_LOSE, null);
        SetState(GameState.Over);
    }

    public override void RePlay()
    {
        RunAllAction((IManager manager) => manager.RePlay());

        SendEvent(EventName.MANAGER_REPLAY, null);
        SetState(GameState.Playing);
    }

    public override void Next()
    {
        RunAllAction((IManager manager) => manager.Next());

        SendEvent(EventName.MANAGER_NEXT, null);
        SetState(GameState.Playing);
    }

    public override void StartGame()
    {
        RunAllAction((IManager manager) => manager.StartGame());

        SendEvent(EventName.MANAGER_STARTGAME, null);
        SetState(GameState.Playing);
    }

    public override void GameWin()
    {
        RunAllAction((IManager manager) => manager.GameWin());

        SendEvent(EventName.MANAGER_WIN, null);
        SetState(GameState.Win);
    }

    public void Reload()
    {
        SetState(GameState.None);
    }

    public static void SetState(GameState _gameState)
    {
        gameState = _gameState;
    }


    public static bool IsState(GameState _gameState)
    {
        return gameState == _gameState;
    }

    public virtual void AddThread(Manager manager)
    {
        managers.Add(manager);
        manager.Init();
    }

    public virtual void RemoveThread(Manager manager)
    {
        managers.Remove(manager);
    }
}