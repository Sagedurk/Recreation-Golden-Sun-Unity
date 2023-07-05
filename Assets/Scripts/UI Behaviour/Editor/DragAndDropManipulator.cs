using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropManipulator : PointerManipulator
{
    // Write a constructor to set target and store a reference to the 
    // root of the visual tree.
    public DragAndDropManipulator(VisualElement target)
    {
        this.target = target;
        //root = target.parent;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        // Register the four callbacks on target.
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
        target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        // Un-register the four callbacks from target.
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
        target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    private Vector2 targetStartPosition { get; set; }

    private Vector3 pointerStartPosition { get; set; }

    private bool enabled { get; set; }

    private VisualElement root { get; }

    // This method stores the starting position of target and the pointer, 
    // makes target capture the pointer, and denotes that a drag is now in progress.
    private void PointerDownHandler(PointerDownEvent evt)
    {
        targetStartPosition = target.transform.position;
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        enabled = true;
    }

    // This method checks whether a drag is in progress and whether target has captured the pointer. 
    // If both are true, calculates a new position for target within the bounds of the window.
    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;

            target.transform.position = new Vector2(
                Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, target.parent.resolvedStyle.width - target.resolvedStyle.width),
                Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, target.parent.resolvedStyle.height - target.resolvedStyle.height));
        }
    }

    // This method checks whether a drag is in progress and whether target has captured the pointer. 
    // If both are true, makes target release the pointer.
    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
        }
    }

    // This method checks whether a drag is in progress.
    private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
    {
        if (enabled)
        {
            enabled = false;
        }
    }

}
