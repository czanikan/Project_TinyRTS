using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RadialMenuEntryDelegate(RadialMenuEntry pEntry);

    [SerializeField]
    TextMeshProUGUI label;

    [SerializeField]
    RawImage icon;

    AudioSource sfx;

    RectTransform rect;
    RadialMenuEntryDelegate Callback;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        sfx = GetComponent<AudioSource>();
    }

    public void SetLabel(string pText)
    {
        label.text = pText;
    }

    public void SetIcon(Texture pIcon)
    {
        icon.texture = pIcon;
    }

    public Texture GetIcon()
    {
        return (icon.texture);
    }

    public void SetCallback(RadialMenuEntryDelegate pCallback)
    {
        Callback = pCallback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Callback?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
        sfx.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
    }

    
}
