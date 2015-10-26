﻿using JuliusSweetland.OptiKey.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows;

namespace JuliusSweetland.OptiKey.UnitTests.UI.ViewModels.MainViewModelSpecifications
{
    public abstract class WhenPointToKeyValueMap : MainViewModelTestBase
    {
        protected Dictionary<Rect, KeyValue> OriginalValue { get; set; }
        protected Dictionary<Rect, KeyValue> NewValue { get; set; }

        protected override void Arrange()
        {
            base.Arrange();

            OriginalValue = new Dictionary<Rect, KeyValue> { { new Rect(1, 2, 3, 4), new KeyValue() } };

            MainViewModel.PointToKeyValueMap = OriginalValue;
            InputService.ResetCalls();

            MainViewModel.SelectionResultPoints = new List<Point> { new Point { X = 27, Y = 10 } };
        }

        protected override void Act()
        {
            MainViewModel.PointToKeyValueMap = NewValue;
        }
    }

    [TestFixture]
    public class WhenPointToKeyValueMapGivenValueHasNotChanged : WhenPointToKeyValueMap
    {
        protected override void Arrange()
        {
            base.Arrange();
            NewValue = OriginalValue;
        }

        [Test]
        public void ThenPointToKeyValueMapOnInputServiceShouldNotChange()
        {
            InputService.VerifySet(i => i.PointToKeyValueMap = It.IsAny<Dictionary<Rect, KeyValue>>(), Times.Never());
        }

        [Test]
        public void ThenSelectionResultPointsShouldNotBeReset()
        {
            Assert.IsNotNull(MainViewModel.SelectionResultPoints);
        }
    }

    [TestFixture]
    public class WhenPointToKeyValueMapGivenValueHasChanged : WhenPointToKeyValueMap
    {
        protected override void Arrange()
        {
            base.Arrange();
            NewValue = new Dictionary<Rect, KeyValue> { { new Rect(5, 6, 7, 8), new KeyValue() } };
        }

        [Test]
        public void ThenPointToKeyValueMapOnInputServiceShouldBeUpdated()
        {
            InputService.VerifySet(i => i.PointToKeyValueMap = It.IsAny<Dictionary<Rect, KeyValue>>(), Times.Once());
        }

        [Test]
        public void ThenSelectionResultPointsShouldBeReset()
        {
            Assert.IsNull(MainViewModel.SelectionResultPoints);
        }
    }
}
