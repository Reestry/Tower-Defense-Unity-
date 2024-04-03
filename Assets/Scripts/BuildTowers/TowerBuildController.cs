// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using TestPool;
using UnityEngine;

public class TowerBuildController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _validPositions;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private GameObject _ghostPrefab;

    private readonly List<TowerCollision> _placedTowers = new();
    private readonly Dictionary<GameObject, Collider2D> _validColliders = new();
    private readonly Color _validPlacementColor = Color.green;
    private readonly Color _invalidPlacementColor = Color.red;

    private GameObject _towerPreview;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    public void CreateTowerPreview(Vector3 position)
    {
        _towerPreview = Instantiate(_ghostPrefab, position, Quaternion.identity);
        _spriteRenderer = _towerPreview.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _validPlacementColor;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        CacheColliders(_validPositions, _validColliders);
    }

    private void CacheColliders(List<GameObject> positions, Dictionary<GameObject, Collider2D> colliderDictionary)
    {
        foreach (var position in positions)
        {
            var collider = position.GetComponent<Collider2D>();
            if (collider != null)
                colliderDictionary[position] = collider;
        }
    }

    private void Update()
    {
        HandlePlacement();
    }

    private void HandlePlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_towerPreview == null)
                return;

            TryPlaceTower();
        }
        else
            UpdateTowerPreviewPosition(GetMouseWorldPosition());
    }

    private void TryPlaceTower()
    {
        if (!IsValidPlacementPosition(GetMouseWorldPosition()))
            return;

        var newTower = Pool.Get<TowerCollision>();
        newTower.transform.position = _towerPreview.transform.position;

        _placedTowers.Add(newTower);
        DestroyTowerPreview();
        TowerSelection.FinishPlacingTower();
    }

    private bool IsValidPlacementPosition(Vector3 position)
    {
        foreach (var placedTower in _placedTowers)
        {
            var towerCollider = placedTower.GetComponent<Collider2D>();
            if (towerCollider.bounds.Contains(position))
                return false;
        }

        foreach (var validPosition in _validColliders)
        {
            var validCollider = validPosition.Value;
            if (validCollider.bounds.Contains(position))
                return true;
        }

        return false;
    }

    private void UpdateTowerPreviewPosition(Vector3 position)
    {
        if (_towerPreview == null)
            return;

        var closestValidPosition = FindClosestValidPosition(position);

        _towerPreview.transform.position = closestValidPosition;
        _spriteRenderer.color = IsValidPlacementPosition(closestValidPosition)
            ? _validPlacementColor
            : _invalidPlacementColor;
    }

    private Vector3 FindClosestValidPosition(Vector3 position)
    {
        var closestPosition = position;
        var closestDistance = float.MaxValue;

        foreach (var validPosition in _validColliders)
        {
            var validCollider = validPosition.Value;
            var validPositionCenter = validCollider.bounds.center;

            var distance = Vector3.Distance(position, validPositionCenter);

            if (!(distance < closestDistance))
                continue;

            closestDistance = distance;
            closestPosition = validPositionCenter;
        }

        return closestPosition;
    }

    private void DestroyTowerPreview()
    {
        Destroy(_towerPreview);
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -_mainCamera.transform.position.z;
        return _mainCamera.ScreenToWorldPoint(mousePosition);
    }
}