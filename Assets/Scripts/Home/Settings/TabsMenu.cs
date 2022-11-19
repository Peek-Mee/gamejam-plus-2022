using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace GJ2022.Home.Settings
{
    [System.Serializable]
    public struct TabPair
    {
        [SerializeField] private Button _tabButton;
        [SerializeField] private GameObject _tabContent;

        public Button Button => _tabButton;
        public GameObject Content => _tabContent;
    }

    public class TabsMenu : MonoBehaviour
    {
        [Header("Initialization")]
        [SerializeField] private TabPair[] _tabs;

        [Header("Fonts")]
        [SerializeField] private TMP_FontAsset _unselectedFont;
        [SerializeField] private TMP_FontAsset _selectedFont;
        private int _activeIndex = 0;

        private void OnEnable()
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                int x = i;
                _tabs[x].Button.onClick.AddListener(new UnityAction(() =>
                {
                    _tabs[_activeIndex].Content.SetActive(false);
                    _tabs[_activeIndex].Button.GetComponent<TMP_Text>().font = _unselectedFont;
                    _tabs[x].Content.SetActive(true);
                    _tabs[x].Button.GetComponent<TMP_Text>().font = _selectedFont;
                    _activeIndex = x;
                }));
            }
        }

        private void OnDisable()
        {
            foreach (TabPair tab in _tabs)
            {
                tab.Button.onClick.RemoveAllListeners();
            }
        }
        private void Start()
        {
            _tabs[0].Button.onClick.Invoke();
        }
    }

}
