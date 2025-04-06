using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _actionMenu1;
    [SerializeField] private RectTransform _actionMenu1_offPosition;
    [SerializeField] private RectTransform _actionMenu2;
    [SerializeField] private RectTransform _actionMenu2_offPosition;
    [SerializeField] private float _actionMenuAnimationTime = 0.5f;
    [SerializeField] private AnimationCurve _actionMenuAnimationCurve;
    private Vector2 _actionMenu1_onPoint, _actionMenu2_onPoint;
    public bool isActionMenuOpen { get; private set; }
    private float _actionMenuTimer;

    private void Start()
    {
        _actionMenu1_onPoint = _actionMenu1.anchoredPosition;
        _actionMenu2_onPoint = _actionMenu2.anchoredPosition;
        
        _actionMenuTimer = _actionMenuAnimationTime;
        _actionMenu1.anchoredPosition = _actionMenu1_offPosition.anchoredPosition;
        _actionMenu2.anchoredPosition = _actionMenu2_offPosition.anchoredPosition;
    }

    public void ToggleActionMenu()
    {
        if (!Mathf.Approximately(_actionMenuTimer, _actionMenuAnimationTime))
            return;

        isActionMenuOpen = !isActionMenuOpen;
        _actionMenuTimer = 0;
    }

    private void Update()
    {
        if (Mathf.Approximately(_actionMenuTimer, _actionMenuAnimationTime))
            return;

        _actionMenuTimer = Mathf.Clamp(_actionMenuTimer + Time.deltaTime, 0, _actionMenuAnimationTime);

        if (isActionMenuOpen)
        {
            _actionMenu1.anchoredPosition = Vector2.Lerp(_actionMenu1_offPosition.anchoredPosition, _actionMenu1_onPoint, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
            _actionMenu2.anchoredPosition = Vector2.Lerp(_actionMenu2_offPosition.anchoredPosition, _actionMenu2_onPoint, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
        }
        else
        {
            _actionMenu1.anchoredPosition = Vector2.Lerp(_actionMenu1_onPoint, _actionMenu1_offPosition.anchoredPosition, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
            _actionMenu2.anchoredPosition = Vector2.Lerp(_actionMenu2_onPoint, _actionMenu2_offPosition.anchoredPosition, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
        }
    }
}
