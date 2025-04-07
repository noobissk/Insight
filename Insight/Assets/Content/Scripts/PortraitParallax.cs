using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class PortraitParallax : MonoBehaviour
{
    [SerializeField] RectTransform target;
    RectTransform _rect;
    public float moveAmount = 1f; // Maximum movement in world units
    public float smoothSpeed = 5f; // Smooth movement speed
    public float positionScale = 0.2f;

    private Vector3 initialPosition;
    [SerializeField, ReadOnly] Vector3 targetPosition;
    public bool isEnabled {private set; get;}

    void Start()
    {
        _rect = (RectTransform)transform;
    }
    public void Enable(Vector3 target)
    {
        isEnabled = true;
        initialPosition = new Vector3(target.x, target.y); // Store initial anchoredPosition
    }
    public void Disable()
    {
        isEnabled = false;
    }

    void Update()
    {
        if (!isEnabled)
        {
            _rect.localPosition = Vector3.Lerp(_rect.localPosition, Vector3.zero, Time.deltaTime * 15.0f);
            return;
        }
        // Get normalized mouse anchoredPosition (-1 to 1)
        float x = Mathf.Clamp((target.anchoredPosition.x - initialPosition.x) * 0.5f * positionScale, -1, 1);
        float y = Mathf.Clamp((target.anchoredPosition.y - initialPosition.y) * 0.5f * positionScale, -1, 1);

        Debug.DrawLine(initialPosition, initialPosition + Vector3.up);

        // Calculate target anchoredPosition
        targetPosition = initialPosition + new Vector3(x * moveAmount, y * moveAmount, initialPosition.z);

        // Smoothly move the camera
        _rect.anchoredPosition = Vector3.Lerp(_rect.anchoredPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }
}

