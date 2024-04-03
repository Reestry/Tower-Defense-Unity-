// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public static bool IsDead;
    private static GameLogicController _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
        
        AudioManager.Instance.PlayMusic("GameMusic", true);
    }
}