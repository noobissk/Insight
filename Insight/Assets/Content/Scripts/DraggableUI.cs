using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Canvas _canvas;
    protected RectTransform _rectTransform;
    protected RectTransform _originalTransform;
    protected CanvasGroup _canvasGroup;
    protected Vector2 _originalPosition;
    [SerializeField, FormerlySerializedAs("_audioClip")] protected AudioClip _audioClipOnBegin;
    [SerializeField] protected AudioClip _audioClipOnEnd;
    [SerializeField] protected PlayAudioClip _audioPrefab;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

        _canvas = GetComponentInParent<Canvas>();
        _originalTransform = (RectTransform)_rectTransform.parent;
    }

    public virtual void OnBeginDrag(PointerEventData i_eventData)
    {
        // Make the object not block raycasts so you can drop onto other UI
        Instantiate(_audioPrefab).PlayAudio(_audioClipOnBegin);
        _originalTransform = (RectTransform)_rectTransform.parent;
        _rectTransform.SetParent(_canvas.transform);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f; // Optional visual feedback
    
        _originalPosition = _rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData i_eventData)
    {
        _rectTransform.anchoredPosition += i_eventData.delta / _canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData i_eventData)
    {
        Instantiate(_audioPrefab).PlayAudio(_audioClipOnEnd);

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        // Check if the pointer is over a valid drop handler
        if (i_eventData.pointerEnter == null || i_eventData.pointerEnter.GetComponent<IDropHandler>() == null)
        {
            // snap back to original position
            _rectTransform.SetParent(_originalTransform);
            _rectTransform.anchoredPosition = _originalPosition;
        }
    }
}
