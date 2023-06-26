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

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            _uxml.CloneTree(root);

            var storyLineList = root.Q<ListView>("story-line-list");
            if (storyLineList.makeItem is not null) return root;

            var configuration = target as GameConfiguration;
            
            storyLineList.itemsSource = configuration.StoryLine.ToArray();
            storyLineList.makeItem = () => new PropertyField();
            storyLineList.bindItem = (e, i) =>
            {
                var item = e as PropertyField;
                var itemPath = $"_customersStoryLine._items.Array.data[{i}]";
                item.bindingPath = itemPath;
                item.Bind(serializedObject);
            };

            storyLineList.selectionType = SelectionType.Multiple;
            
            storyLineList.fixedItemHeight = 16;
            storyLineList.style.flexGrow = 1.0f;
            storyLineList.style.fontSize = 20;

            Debug.Log("Ok");
            return root;
        }
    }

}