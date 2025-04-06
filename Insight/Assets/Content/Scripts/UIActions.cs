using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    [SerializeField] private List<Draggable_UIAction> _actionPrefabs = new List<Draggable_UIAction>();
    [SerializeField] private RectTransform _actionsParent;
    [SerializeField] private DropZone _dropZone;

    void Start()
    {
        RefreshActionObjects();
    }


    public void RefreshActionObjects()
    {
        foreach (RectTransform item in _actionsParent)
            Destroy(item.gameObject);

        for (int i = 0; i < _actionPrefabs.Count; i++)
        {
            Draggable_UIAction v = Instantiate(_actionPrefabs[i], _actionsParent);
            v.uiActions = this;
            v.dropZone = _dropZone;
        }
    }


}
