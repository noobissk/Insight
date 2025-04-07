using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable_UIAction : DraggableUI
{
    [HideInInspector] public UIActions uiActions;
    [HideInInspector] public DropZone dropZone;

    public override void OnBeginDrag(PointerEventData i_eventData)
    {
        base.OnBeginDrag(i_eventData);
        dropZone.OnPlayerDragBegin();
    }

    public override void OnEndDrag(PointerEventData i_eventData)
    {
        Instantiate(_audioPrefab).PlayAudio(_audioClipOnEnd);

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        uiActions.RefreshActionObjects();
        dropZone.OnPlayerDragEnd();

        // Check if the pointer is over a valid drop handler
        if (i_eventData.pointerEnter == null || i_eventData.pointerEnter.GetComponent<IDropHandler>() == null)
        {
            // destroy the object
            Destroy(gameObject);
            return;
        }
        transform.SetSiblingIndex(FindInsertIndex(i_eventData, transform.parent));
    }

    private int FindInsertIndex(PointerEventData i_eventData, Transform i_parent)
    {
        for (int i = 0; i < i_parent.childCount; i++)
        {
            Transform child = i_parent.GetChild(i);
            if (transform == child) continue; // skip self

            if (i_eventData.position.y > child.position.y)
            {
                return i;
            }
        }

        return i_parent.childCount; // dropped at the end
    }
}
