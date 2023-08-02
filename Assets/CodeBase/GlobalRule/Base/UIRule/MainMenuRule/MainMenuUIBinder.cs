using System;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace CodeBase.GlobalRule.Base.UIRule.MainMenuRule
{
    public sealed class MainMenuUIBinder : IDisposable
    {
        private readonly MainMenuUIMediator _menuUIMediator;
        private readonly VisualElement _uiElementsResolver;
        
        public Button GameStartButton { get; private set; }

        public MainMenuUIBinder()
        {
            _uiElementsResolver = new VisualElement();
            Object.FindObjectOfType<UIDocument>().visualTreeAsset.CloneTree(_uiElementsResolver);
        }

        public void BindUI()
        {
            GameStartButton = _uiElementsResolver.Q<Button>("start-button");
            GameStartButton.clicked += _menuUIMediator.EnterToMainGame;
        }

        public void Dispose()
            => UnbindUI();

        private void UnbindUI()
        {
            GameStartButton.clicked -= _menuUIMediator.EnterToMainGame;
        }
    }
}