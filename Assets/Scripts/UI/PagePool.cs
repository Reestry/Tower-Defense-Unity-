// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

public class PagePool : MonoBehaviour
{
    private static PagePool _instance;

    [SerializeField] private List<Page> _prefabs;

    private readonly List<Page> _pages = new();
    private static Page _openedPage;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        InitPrefabs();
    }

    private void InitPrefabs()
    {
        foreach (var pagePrefab in _prefabs)
        {
            var page = Instantiate(pagePrefab, transform);
            page.gameObject.SetActive(false);
            _pages.Add(page);
        }
    }

    public static T Get<T>() where T : Page
    {
        if (_openedPage != null)
            Release();

        foreach (var page in _instance._pages)
        {
            if (page is T typedObj)
            {
                _openedPage = typedObj;
                return typedObj;
            }
        }

        return null;
    }

    private static void Release()
    {
        var page = _openedPage.gameObject;
        page.SetActive(false);
        page.transform.SetParent(_instance.transform, false);
    }
}