using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    // [SerializeField] private RectTransform _emptyObject;
    private Transform _content;
    private void Start()
    {
        _content = transform.GetChild(0);
    }
    public void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            Debug.Log($"Dropped {dropped.name} on {name}");

            // Optional: snap to drop zone
            dropped.transform.SetParent(_content);
            // _emptyObject.SetSiblingIndex(_content.childCount-1);
        }
    }
    public void OnPlayerDragBegin()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            _content.GetChild(i).GetComponent<Image>().raycastTarget = false;
        }
    }
    public void OnPlayerDragEnd()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            _content.GetChild(i).GetComponent<Image>().raycastTarget = true;
        }
    }
}
