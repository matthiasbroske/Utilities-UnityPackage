using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Matthias.Utilities
{
    public class TabButton : Selectable, IPointerClickHandler
    {
        [Header("Events")]
        [SerializeField] private UnityEvent _onShow = new UnityEvent();
        [SerializeField] private UnityEvent _onHide = new UnityEvent();
        [Header("Group")]
        [SerializeField] private TabButtonGroup _tabButtonGroup;
        
        public Button.ButtonClickedEvent OnClick => _onClick;

        private Button.ButtonClickedEvent _onClick = new Button.ButtonClickedEvent();
        private UnityAction _onClickHandler;

        protected override void OnEnable()
        {
            base.OnEnable();
            _tabButtonGroup.RegisterTabButton(this);
            _onClickHandler = delegate { _tabButtonGroup.SelectTab(this); };
            _onClick.AddListener(_onClickHandler);
        }
        
        protected override void OnDisable()
        {
            _tabButtonGroup.UnregisterTabButton(this);
            _onClick.RemoveListener(_onClickHandler);
            base.OnDisable();
        }
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _onClick.Invoke();
        }

        public void Show()
        {
            _onShow.Invoke();
        }

        public void Hide()
        {
            _onHide.Invoke();
        }
    }
}
