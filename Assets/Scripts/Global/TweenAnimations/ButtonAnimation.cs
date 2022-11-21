using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace GJ2022.Global.TweenAnimation
{
    [RequireComponent(typeof(Button), typeof(TMP_Text))]
    public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Font Assets")]
        [SerializeField] private TMP_FontAsset _fontAssetUnselected;
        [SerializeField] private TMP_FontAsset _fontAssetSelected;

        [Header("SFX")]
        [SerializeField] private AudioSource _sfxForThisButton;

        TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _sfxForThisButton?.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.font = _fontAssetSelected;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.font = _fontAssetUnselected;
        }
    }

}
