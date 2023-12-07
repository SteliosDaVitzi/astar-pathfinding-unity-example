using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStarExample
{
    public class CameraResizer : MonoBehaviour
{
    public float buffer = 0;

    public RectTransform boardPlaceholder;
    public RectTransform originPoint;

    [SerializeField] private Camera mainCamera;

    private List<Renderer> _spriteRenderersToBeCalculated;

    private bool _canResize = false;
    private void Update()
    {
        if (!_canResize) return;
        
        var boardInScreenSpace = RectTransformToScreenSpace(boardPlaceholder).size;

        if (boardInScreenSpace.x <= 0.0f) return;
        if (boardInScreenSpace.y <= 0.0f) return;
        
        ResizeCamera();
    }

    public void SetupCamera(List<Renderer> spriteRenderersToBeCalculated)
    {
        _spriteRenderersToBeCalculated = spriteRenderersToBeCalculated;
        _canResize = true;
    }
    
    private void ResizeCamera()
    {
        var bounds = new Bounds();
        
        foreach (var spriteRenderer in _spriteRenderersToBeCalculated)
            bounds.Encapsulate(spriteRenderer.bounds);
    
        bounds.Expand(buffer);
        var vertical = bounds.size.y* mainCamera.pixelWidth/ mainCamera.pixelHeight;
        var horizontal = bounds.size.x * mainCamera.pixelHeight/ mainCamera.pixelWidth;
    
        mainCamera.orthographicSize = Mathf.Max(horizontal, vertical) * 0.5f;
        PositionCamera(bounds.center.x);
    }

    private void PositionCamera(float posX)
    {
        var cameraPos = mainCamera.transform.position;
        var originYScreenSpace = RectTransformToScreenSpace(originPoint).y;
        var normalizedYPos = originYScreenSpace / Screen.height;

        var normalizedDistanceFromCenter = 0.5f - normalizedYPos;
        var centerY = normalizedDistanceFromCenter * mainCamera.orthographicSize * 2;

        mainCamera.transform.position = new Vector3(cameraPos.x, centerY, cameraPos.z);
    }

    private static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        var size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
}
