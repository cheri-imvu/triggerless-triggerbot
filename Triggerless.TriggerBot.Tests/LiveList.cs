using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Specialized;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Tests
{
    [TestFixture]
    public class LiveListTests
    {
        private TestContainer _container;

        private class TestContainer
        {
            public int CollectionChangedCount { get; set; } = 0;
            public int FrontChangedCount { get; set; } = 0;
            public string Current {  get; set; }
            public string Previous { get; set; }
            public NotifyCollectionChangedAction Action { get; set; }

            public LiveList<string> StringList;

            public TestContainer()
            {
                StringList = new LiveList<string>();
                StringList.FrontChanged += (sender, e) =>
                {
                    FrontChangedCount++;
                    Current = e.Current;
                    Previous = e.Previous;
                };
                StringList.CollectionChanged += (sender, e) =>
                {
                    CollectionChangedCount++;
                    Action = e.Action;
                };
                StringList.IsLive = true;
            }
        }

        [SetUp]
        public void Setup()
        {
            _container = new TestContainer();
        }

        [Test]
        public void HelloWorld()
        {
            string expected = "HelloWorld";
            string actual = "HelloWorld";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PushOne()
        {
            string arg = "Apple";
            _container.StringList.Push(arg);
            Assert.That(_container.StringList.Count == 1);
            Assert.That(_container.CollectionChangedCount == 1);
            Assert.That(_container.StringList.Peek().Equals(arg));
            Assert.That(_container.FrontChangedCount == 1);
        }

        [Test]
        public void NewList_IsEmpty()
        {
            Assert.That(_container.StringList.Count, Is.EqualTo(0));
            Assert.That(_container.StringList.IsEmpty, Is.True);
        }

        [Test]
        public void PushOne_HasCorrectFrontEventData()
        {
            _container.StringList.Push("Apple");

            Assert.That(_container.Previous, Is.Null);
            Assert.That(_container.Current, Is.EqualTo("Apple"));
        }

        [Test]
        public void PushToNonEmptyList_DoesNotChangeFront()
        {
            _container.StringList.Push("Apple");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;
            _container.Previous = null;
            _container.Current = null;

            _container.StringList.Push("Banana");

            Assert.That(_container.StringList.Count, Is.EqualTo(2));
            Assert.That(_container.StringList[0], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[1], Is.EqualTo("Banana"));

            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Push_RaisesAddEvent()
        {
            _container.StringList.Push("Apple");

            Assert.That(
                _container.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Add));
        }

        [Test]
        public void Push_RaisesEventForCorrectIndex()
        {
            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Push("Apple");

            Assert.That(args, Is.Not.Null);
            Assert.That(args.NewStartingIndex, Is.EqualTo(0));
            Assert.That(args.NewItems.Count, Is.EqualTo(1));
            Assert.That(args.NewItems[0], Is.EqualTo("Apple"));
        }

        [Test]
        public void Unshift_AddsToFront()
        {
            _container.StringList.Push("Apple");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Unshift("Banana");

            Assert.That(_container.StringList[0], Is.EqualTo("Banana"));
            Assert.That(_container.StringList[1], Is.EqualTo("Apple"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Banana"));
        }

        [Test]
        public void Unshift_EmptyList_ChangesFrontFromDefault()
        {
            _container.StringList.Unshift("Apple");

            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.Null);
            Assert.That(_container.Current, Is.EqualTo("Apple"));
        }

        [Test]
        public void Pop_RemovesLastItem()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            string result = _container.StringList.Pop();

            Assert.That(result, Is.EqualTo("Cherry"));
            Assert.That(_container.StringList.Count, Is.EqualTo(2));
            Assert.That(_container.StringList[0], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[1], Is.EqualTo("Banana"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Pop_LastItem_ChangesFrontToDefault()
        {
            _container.StringList.Push("Apple");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            string result = _container.StringList.Pop();

            Assert.That(result, Is.EqualTo("Apple"));
            Assert.That(_container.StringList.IsEmpty, Is.True);
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.Null);
        }

        [Test]
        public void Shift_RemovesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            string result = _container.StringList.Shift();

            Assert.That(result, Is.EqualTo("Apple"));
            Assert.That(_container.StringList[0], Is.EqualTo("Banana"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Banana"));
        }

        [Test]
        public void Remove_NonFrontItem_DoesNotChangeFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            bool result = _container.StringList.Remove("Banana");

            Assert.That(result, Is.True);
            Assert.That(_container.StringList.Count, Is.EqualTo(2));
            Assert.That(_container.StringList[0], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Remove_FrontItem_ChangesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            bool result = _container.StringList.Remove("Apple");

            Assert.That(result, Is.True);
            Assert.That(_container.StringList[0], Is.EqualTo("Banana"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Banana"));
        }

        [Test]
        public void Remove_MissingItem_DoesNothing()
        {
            _container.StringList.Push("Apple");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            bool result = _container.StringList.Remove("Banana");

            Assert.That(result, Is.False);
            Assert.That(_container.StringList.Count, Is.EqualTo(1));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(0));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Move_NonFrontItems_DoesNotChangeFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");
            _container.StringList.Push("Date");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Move(1, 2);

            Assert.That(_container.StringList[0], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[2], Is.EqualTo("Banana"));
            Assert.That(_container.StringList[3], Is.EqualTo("Date"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Move_FromFront_ChangesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Move(0, 2);

            Assert.That(_container.StringList[0], Is.EqualTo("Banana"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[2], Is.EqualTo("Apple"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Banana"));
        }

        [Test]
        public void Move_ToFront_ChangesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Move(2, 0);

            Assert.That(_container.StringList[0], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[1], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[2], Is.EqualTo("Banana"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Cherry"));
        }

        [Test]
        public void MoveToFront_Works()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.MoveToFront("Cherry");

            Assert.That(_container.StringList[0], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[1], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[2], Is.EqualTo("Banana"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
        }

        [Test]
        public void MoveToBack_DoesNotChangeFront_WhenMovingNonFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.MoveToBack("Banana");

            Assert.That(_container.StringList[0], Is.EqualTo("Apple"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[2], Is.EqualTo("Banana"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void MoveToBack_FromFront_ChangesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.MoveToBack("Apple");

            Assert.That(_container.StringList[0], Is.EqualTo("Banana"));
            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.StringList[2], Is.EqualTo("Apple"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Banana"));
        }

        [Test]
        public void Clear_RemovesEverything()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Clear();

            Assert.That(_container.StringList.Count, Is.EqualTo(0));
            Assert.That(_container.StringList.IsEmpty, Is.True);
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.Null);
        }

        [Test]
        public void Clear_EmptyList_DoesNothing()
        {
            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Clear();

            Assert.That(_container.CollectionChangedCount, Is.EqualTo(0));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Peek_ReturnsFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            Assert.That(_container.StringList.Peek(), Is.EqualTo("Apple"));
        }

        [Test]
        public void PeekBack_ReturnsLast()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            Assert.That(_container.StringList.PeekBack(), Is.EqualTo("Banana"));
        }

        [Test]
        public void IndexOf_ReturnsCorrectIndex()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            Assert.That(_container.StringList.IndexOf("Banana"), Is.EqualTo(1));
        }

        [Test]
        public void Contains_ReturnsCorrectResult()
        {
            _container.StringList.Push("Apple");

            Assert.That(_container.StringList.Contains("Apple"), Is.True);
            Assert.That(_container.StringList.Contains("Banana"), Is.False);
        }

        [Test]
        public void Indexer_ReplaceNonFrontItem()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList[1] = "Cherry";

            Assert.That(_container.StringList[1], Is.EqualTo("Cherry"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Indexer_ReplaceFrontItem_ChangesFront()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList[0] = "Cherry";

            Assert.That(_container.StringList[0], Is.EqualTo("Cherry"));
            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.EqualTo("Apple"));
            Assert.That(_container.Current, Is.EqualTo("Cherry"));
        }

        [Test]
        public void Indexer_SetSameValue_DoesNothing()
        {
            _container.StringList.Push("Apple");

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList[0] = "Apple";

            Assert.That(_container.CollectionChangedCount, Is.EqualTo(0));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void IsLiveFalse_SuppressesFrontChanged()
        {
            _container.StringList.IsLive = false;

            _container.CollectionChangedCount = 0;
            _container.FrontChangedCount = 0;

            _container.StringList.Push("Apple");

            Assert.That(_container.CollectionChangedCount, Is.EqualTo(1));
            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void IsLiveFalse_DoesNotSuppressCollectionChanged()
        {
            _container.StringList.IsLive = false;

            _container.CollectionChangedCount = 0;

            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            Assert.That(_container.CollectionChangedCount, Is.EqualTo(2));
        }

        [Test]
        public void EnablingIsLive_ReportsCurrentFront()
        {
            _container.StringList.IsLive = false;

            _container.StringList.Push("Apple");

            _container.FrontChangedCount = 0;
            _container.Previous = null;
            _container.Current = null;

            _container.StringList.IsLive = true;

            Assert.That(_container.FrontChangedCount, Is.EqualTo(1));
            Assert.That(_container.Previous, Is.Null);
            Assert.That(_container.Current, Is.EqualTo("Apple"));
        }

        [Test]
        public void EnablingIsLiveOnEmptyList_DoesNotRaiseFrontChanged()
        {
            _container.StringList.IsLive = false;

            _container.FrontChangedCount = 0;

            _container.StringList.IsLive = true;

            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void DisablingIsLive_DoesNotRaiseEvent()
        {
            _container.StringList.IsLive = false;

            Assert.That(_container.FrontChangedCount, Is.EqualTo(0));
        }

        [Test]
        public void Move_RaisesCorrectCollectionEventData()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Move(1, 2);

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Move));
            Assert.That(args.OldStartingIndex, Is.EqualTo(1));
            Assert.That(args.NewStartingIndex, Is.EqualTo(2));
            Assert.That(args.NewItems[0], Is.EqualTo("Banana"));
        }

        [Test]
        public void Remove_RaisesCorrectCollectionEventData()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Remove("Banana");

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Remove));
            Assert.That(args.OldStartingIndex, Is.EqualTo(1));
            Assert.That(args.OldItems[0], Is.EqualTo("Banana"));
        }

        [Test]
        public void Clear_RaisesResetEvent()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Clear();

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Reset));
        }

        [Test]
        public void Indexer_Replacement_RaisesReplaceEvent()
        {
            _container.StringList.Push("Apple");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList[0] = "Banana";

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Replace));
            Assert.That(args.OldStartingIndex, Is.EqualTo(0));
            Assert.That(args.OldItems[0], Is.EqualTo("Apple"));
            Assert.That(args.NewItems[0], Is.EqualTo("Banana"));
        }

        [Test]
        public void Pop_EmptyList_Throws()
        {
            Assert.Throws<InvalidOperationException>(
                (Action)(() => _container.StringList.Pop()));
        }

        [Test]
        public void Shift_EmptyList_Throws()
        {
            Assert.Throws<InvalidOperationException>(
                (Action)(() => _container.StringList.Shift()));
        }

        [Test]
        public void Peek_EmptyList_Throws()
        {
            Assert.Throws<InvalidOperationException>(
                (Action)(() => _container.StringList.Peek()));
        }

        [Test]
        public void PeekBack_EmptyList_Throws()
        {
            Assert.Throws<InvalidOperationException>(
                (Action)(() => _container.StringList.PeekBack()));
        }

        [Test]
        public void Move_InvalidOldIndex_Throws()
        {
            _container.StringList.Push("Apple");

            Assert.Throws<ArgumentOutOfRangeException>(
                (Action)(() => _container.StringList.Move(-1, 0)));
        }

        [Test]
        public void Move_InvalidNewIndex_Throws()
        {
            _container.StringList.Push("Apple");

            Assert.Throws<ArgumentOutOfRangeException>(
                (Action)(() => _container.StringList.Move(0, 1)));
        }

        [Test]
        public void Move_MissingItem_Throws()
        {
            _container.StringList.Push("Apple");

            Assert.Throws<ArgumentException>(
                (Action)(() => _container.StringList.Move("Banana", 0)));
        }

        [Test]
        public void MoveToFront_MissingItem_Throws()
        {
            Assert.Throws<ArgumentException>(
                (Action)(() => _container.StringList.MoveToFront("Apple")));
        }

        [Test]
        public void MoveToBack_MissingItem_Throws()
        {
            Assert.Throws<ArgumentException>(
                (Action)(() => _container.StringList.MoveToBack("Apple")));
        }

        [Test]
        public void CollectionChanged_Add()
        {
            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Push("Apple");

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Add));

            Assert.That(args.NewStartingIndex, Is.EqualTo(0));
            Assert.That(args.NewItems.Count, Is.EqualTo(1));
            Assert.That(args.NewItems[0], Is.EqualTo("Apple"));

            Assert.That(args.OldItems, Is.Null);
            Assert.That(args.OldStartingIndex, Is.EqualTo(-1));
        }

        [Test]
        public void CollectionChanged_Remove()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Remove("Banana");

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Remove));

            Assert.That(args.OldStartingIndex, Is.EqualTo(1));
            Assert.That(args.OldItems.Count, Is.EqualTo(1));
            Assert.That(args.OldItems[0], Is.EqualTo("Banana"));

            Assert.That(args.NewItems, Is.Null);
            Assert.That(args.NewStartingIndex, Is.EqualTo(-1));
        }

        [Test]
        public void CollectionChanged_Replace()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList[1] = "Cherry";

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Replace));

            Assert.That(args.NewStartingIndex, Is.EqualTo(1));
            Assert.That(args.NewItems.Count, Is.EqualTo(1));
            Assert.That(args.NewItems[0], Is.EqualTo("Cherry"));

            Assert.That(args.OldStartingIndex, Is.EqualTo(1));
            Assert.That(args.OldItems.Count, Is.EqualTo(1));
            Assert.That(args.OldItems[0], Is.EqualTo("Banana"));
        }

        [Test]
        public void CollectionChanged_Move()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Move(1, 2);

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Move));

            Assert.That(args.OldStartingIndex, Is.EqualTo(1));
            Assert.That(args.NewStartingIndex, Is.EqualTo(2));

            Assert.That(args.OldItems.Count, Is.EqualTo(1));
            Assert.That(args.OldItems[0], Is.EqualTo("Banana"));

            Assert.That(args.NewItems.Count, Is.EqualTo(1));
            Assert.That(args.NewItems[0], Is.EqualTo("Banana"));
        }

        [Test]
        public void CollectionChanged_Reset()
        {
            _container.StringList.Push("Apple");
            _container.StringList.Push("Banana");
            _container.StringList.Push("Cherry");

            NotifyCollectionChangedEventArgs args = null;

            _container.StringList.CollectionChanged += (sender, e) =>
            {
                args = e;
            };

            _container.StringList.Clear();

            Assert.That(args, Is.Not.Null);
            Assert.That(
                args.Action,
                Is.EqualTo(NotifyCollectionChangedAction.Reset));

            Assert.That(args.NewItems, Is.Null);
            Assert.That(args.OldItems, Is.Null);
            Assert.That(args.NewStartingIndex, Is.EqualTo(-1));
            Assert.That(args.OldStartingIndex, Is.EqualTo(-1));
        }
    }
}
