// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Pause
{
    public class PauseManager : MonoBehaviour, IPauseHandler
    {
        public static PauseManager Instance;

        public static bool IsPaused { get; private set; }

        private static readonly List<IPauseHandler> PauseHandlers = new();

        public static void Register(IPauseHandler pauseHandler)
        {
            PauseHandlers.Add(pauseHandler);
        }

        public static void UnRegister(IPauseHandler pauseHandler)
        {
            PauseHandlers.Remove(pauseHandler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var pauseHandler in PauseHandlers)
                pauseHandler.SetPaused(isPaused);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            IsPaused = false;
        }

        private void Update()
        {
            if (GameLogicController.IsDead)
                return;

            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            if (IsPaused)
                return;

            WindowManager.OpenWindow<PauseWindow>();

            IsPaused = !IsPaused;
            SetPaused(IsPaused);
        }
    }
}