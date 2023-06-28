using System;
using System.Linq;
using CodeBase.Configuration.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Configuration.ConfigurationDrawer
{
    [CustomEditor(typeof(GameConfiguration))]
    public sealed class GameConfigurationDrawer : Editor
    {
        [SerializeField] private VisualTreeAsset _uxml;
        
        private VisualElement _frontRoot;
        private GameConfiguration _gameConfiguration;
        private bool _isElementsInited;
        
        public override VisualElement CreateInspectorGUI()
        {
            BindProperties();

            if (_isElementsInited) return _frontRoot;
            
            InitStoryLineList();

            _isElementsInited = true;
            return _frontRoot;
        }

        private void BindProperties()
        {
            _frontRoot = new VisualElement();
            _uxml.CloneTree(_frontRoot);
            _gameConfiguration = target as GameConfiguration;
        }
        
        private void InitStoryLineList()
        {
            var customerStoryLineList = _frontRoot.Q<ListView>("story-customers-list");

            customerStoryLineList.itemsSource = _gameConfiguration.StoryLine.ToArray();
            customerStoryLineList.makeItem = () => new CustomerTableVisualItem();
            customerStoryLineList.bindItem = (e, i) =>
            {
                var item = e as CustomerTableVisualItem;
                var storyPartPath = $"_storyLine.Array.data[{i}]"; 
                var customerPath = $"{storyPartPath}._storyPlotCustomer";
                var customerNamePath = $"{customerPath}._name";
                var customerLevelPath = $"{storyPartPath}._levelNumber";
                
                item.ItemTagField.text = serializedObject.FindProperty(customerLevelPath).intValue.ToString();
                item.CustomerValueField.label = serializedObject.FindProperty(customerNamePath).stringValue;
                item.CustomerValueField.bindingPath = customerPath;
                item.Bind(serializedObject);
            };
            customerStoryLineList.selectionType = SelectionType.Multiple;
        }
    }


    public static class CustomEditorExtensions
    {
        public static TValue GetRefValue<TValue>(this SerializedProperty target)
        {
            var rootObject = target.serializedObject.targetObject;
            var property = rootObject.GetType().GetProperty(target.propertyPath) ?? throw new Exception();
            
            return (TValue)property.GetValue(rootObject);
        }
    }
}