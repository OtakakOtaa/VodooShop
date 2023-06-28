using System;
using CodeBase.Customers.Data;
using UnityEngine;

namespace CodeBase.Levels.Data
{
    [Serializable] public sealed class StoryLinePart
    {
        [SerializeField] private byte _levelNumber;
        [SerializeField] private PlotCustomer _storyPlotCustomer;

        public StoryLinePart(int levelNumber, PlotCustomer storyPlotCustomer)
        {
            _levelNumber = (byte)levelNumber;
            _storyPlotCustomer = storyPlotCustomer;
        }

        public byte LevelNumber => _levelNumber;

        public PlotCustomer StoryPlotCustomer => _storyPlotCustomer;
    }
}