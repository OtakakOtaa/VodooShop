using System.Linq;
using CodeBase.Configuration.Data.MainConfig;
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

            InitCustomersPoolList();
            InitStoryLineList();
            InitSimpleCustomersList();
            InitFreeOrdersList();
            
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
            customerStoryLineList.makeItem = () => new TableVisualItemWhichLabel();
            customerStoryLineList.bindItem = (e, i) =>
            {
                if (e is not TableVisualItemWhichLabel item) return;
                
                var storyPartPath = $"_storyLine.Array.data[{i}]";
                var orderPath = $"{storyPartPath}._order";
                var customerPath = $"{orderPath}._holder";
                var customerNamePath = $"{customerPath}._name";
                var customerLevelPath = $"{storyPartPath}._levelNumber";

                item.ItemTagField.text = serializedObject.FindProperty(customerLevelPath).intValue.ToString();
                item.ValueField.label = serializedObject.FindProperty(customerNamePath).stringValue;
                item.ValueField.bindingPath = orderPath; 
                item.Bind(serializedObject);
            };
            customerStoryLineList.selectionType = SelectionType.Multiple;
        }

        private void InitCustomersPoolList()
        {
            var customersPoolElement = _frontRoot.Q<ListView>("customers-pool-list");

            customersPoolElement.itemsSource = _gameConfiguration.CustomersPools.ToArray();
            customersPoolElement.makeItem = () => new PropertyField();
            customersPoolElement.bindItem = (e, i) =>
            {
                if (e is not PropertyField item) return;
                
                var poolPath = $"_customersPools.Array.data[{i}]";
                var poolMinRangePath = $"{poolPath}._levelScopeMinimum";
                var poolMaxRangePath = $"{poolPath}._levelScopeMaximum";
                
                item.label = $"{serializedObject.FindProperty(poolMinRangePath).intValue} - " +
                             serializedObject.FindProperty(poolMaxRangePath).intValue;
                item.bindingPath = poolPath;
                item.Bind(serializedObject);
            };
        }

        private void InitSimpleCustomersList()
        {
            var customersList = _frontRoot.Q<ListView>("simple-customers-list");
            
            customersList.itemsSource = _gameConfiguration.SimpleCustomers.ToArray();
            customersList.makeItem = () => new TableVisualItemWhichLabel();
            customersList.bindItem = (e, i) =>
            {
                if (e is not TableVisualItemWhichLabel item) return;
                
                var simpleCustomersPath = $"_simpleCustomers.Array.data[{i}]";
                var customerNamePath = $"{simpleCustomersPath}._name";
                
                item.ItemTagField.text = (i + 1).ToString();
                item.ValueField.label = serializedObject.FindProperty(customerNamePath).stringValue;
                item.ValueField.bindingPath = simpleCustomersPath;
                item.Bind(serializedObject);
            };
            customersList.selectionType = SelectionType.Multiple;
        }

        private void InitFreeOrdersList()
        {
            var customersList = _frontRoot.Q<ListView>("free-orders-list");
            
            customersList.itemsSource = _gameConfiguration.OrdersWithoutOwners.ToArray();
            customersList.makeItem = () => new TableVisualItemWhichLabel();
            customersList.bindItem = (e, i) =>
            {
                if (e is not TableVisualItemWhichLabel item) return;
                
                var customerPath = $"_ordersWithoutOwners.Array.data[{i}]";
                var orderRewardPath = $"{customerPath}._reward";
                var orderItemPath = $"{customerPath}._requestedItem";
                
                item.ItemTagField.text = (i + 1).ToString();

                var rewardValue = serializedObject.FindProperty(orderRewardPath).floatValue;
                var itemKey = serializedObject.FindProperty(orderItemPath).stringValue;
                
                item.ValueField.label = $"#reward {rewardValue} #item {itemKey}";
                item.ValueField.bindingPath = customerPath;
                item.Bind(serializedObject);
            };
            customersList.selectionType = SelectionType.Multiple;
        }
    }
}