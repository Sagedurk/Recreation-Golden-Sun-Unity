using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropManipulator : PointerManipulator
{

    private Vector2 TargetStartPosition { get; set; }
    private Vector3 PointerStartPosition { get; set; }
    private bool Enabled { get; set; }
    private VisualElement Root { get; }
    private VisualElement SubRoot { get; }
    private List<DragAndDropManipulator> GroupedDragAndDrops { get; set; } = new List<DragAndDropManipulator>();

    private DragAndDropType DragType { get; }

    public enum DragAndDropType
    {
        NONE,
        INSIDE,
        OUTSIDE,
    }



    // Write a constructor to set target and store a reference to the 
    // root of the visual tree.
    public DragAndDropManipulator(VisualElement target, VisualElement root, VisualElement subRoot = null, DragAndDropType dragAndDropType = DragAndDropType.INSIDE)
    {
        this.target = target;
        Root = root;
        SubRoot = subRoot;
        DragType = dragAndDropType;
    }

    public void AddManipulatorToGroup(DragAndDropManipulator manipulator)
    {
        GroupedDragAndDrops.Add(manipulator);
    }
    public void RemoveManipulatorFromGroup(DragAndDropManipulator manipulator)
    {
        GroupedDragAndDrops.Remove(manipulator);
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


    // This method stores the starting position of target and the pointer, 
    // makes target capture the pointer, and denotes that a drag is now in progress.
    private void PointerDownHandler(PointerDownEvent evt)
    {

        TargetStartPosition = target.transform.position;
        PointerStartPosition = evt.position;
        Enabled = true;

        foreach (DragAndDropManipulator manipulator in GroupedDragAndDrops)
        {
            manipulator.PointerDownHandler(evt);
            manipulator.target.ReleasePointer(evt.pointerId);
        }
        target.CapturePointer(evt.pointerId);
    }

    // This method checks whether a drag is in progress and whether target has captured the pointer. 
    // If both are true, calculates a new position for target within the bounds of the window.
    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (Enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - PointerStartPosition;

            switch (DragType)
            {
                case DragAndDropType.NONE:
                    break;

                case DragAndDropType.INSIDE:
                    target.transform.position = new Vector2(
                        Mathf.Clamp(TargetStartPosition.x + pointerDelta.x, 0, Root.resolvedStyle.width - target.resolvedStyle.width),
                        Mathf.Clamp(TargetStartPosition.y + pointerDelta.y, 0, Root.resolvedStyle.height - target.resolvedStyle.height));
                    break;

                case DragAndDropType.OUTSIDE:
                    float portraitMinWidth = Mathf.Clamp(SubRoot.transform.position.x - target.resolvedStyle.width, 0, SubRoot.transform.position.x - target.resolvedStyle.width);
                    float portraitMaxWidth = Mathf.Clamp(SubRoot.transform.position.x + SubRoot.resolvedStyle.width, SubRoot.transform.position.x + SubRoot.resolvedStyle.width, Root.resolvedStyle.width - target.resolvedStyle.width);

                    float portraitMinHeight = Mathf.Clamp(SubRoot.transform.position.y - target.resolvedStyle.height, 0, SubRoot.transform.position.y - target.resolvedStyle.height);
                    float portraitMaxHeight = Mathf.Clamp(SubRoot.transform.position.y + SubRoot.resolvedStyle.height, SubRoot.transform.position.y + SubRoot.resolvedStyle.height, Root.resolvedStyle.height - target.resolvedStyle.height);
                    target.transform.position = new Vector2(
                    Mathf.Clamp(TargetStartPosition.x + pointerDelta.x, portraitMinWidth, portraitMaxWidth),
                    Mathf.Clamp(TargetStartPosition.y + pointerDelta.y, portraitMinHeight, portraitMaxHeight));
                    break;
                default:
                    break;
            }

            target.ReleasePointer(evt.pointerId);
        }


        foreach (DragAndDropManipulator manipulator in GroupedDragAndDrops)
        {
            manipulator.target.CapturePointer(evt.pointerId);
            manipulator.PointerMoveHandler(evt);
            manipulator.target.ReleasePointer(evt.pointerId);
        }

        target.CapturePointer(evt.pointerId);
    }


    // This method checks whether a drag is in progress and whether target has captured the pointer. 
    // If both are true, makes target release the pointer.
    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (Enabled && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
        }

        foreach (DragAndDropManipulator manipulator in GroupedDragAndDrops)
        {
            manipulator.target.ReleasePointer(evt.pointerId);
        }
    }


    private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
    {
        if (Enabled)
        {
            Enabled = false;
        }

        foreach (DragAndDropManipulator manipulator in GroupedDragAndDrops)
        {
            manipulator.Enabled = false;
        }
    }

}
