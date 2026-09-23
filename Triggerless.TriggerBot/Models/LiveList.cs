using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Triggerless.TriggerBot.Models
{
    public class FrontChangedEventArgs<T> : EventArgs
    {
        public T Previous { get; private set; }

        public T Current { get; private set; }

        public FrontChangedEventArgs(T previous, T current)
        {
            Previous = previous;
            Current = current;
        }
    }

    public delegate void FrontChangeEventHandler<T>(
        object sender,
        FrontChangedEventArgs<T> e);

    /// <summary>
    /// A thread-safe list supporting JavaScript-style array operations,
    /// stack/queue operations, and element reordering.
    ///
    /// CollectionChanged is raised for every collection mutation.
    ///
    /// When IsLive is true, FrontChanged is raised whenever
    /// the element at index zero changes.
    ///
    /// Collection mutations are synchronized internally. Event handlers
    /// are invoked outside the collection lock.
    /// </summary>
    public class LiveList<T> :
        IReadOnlyList<T>,
        INotifyCollectionChanged
    {
        private readonly List<T> _items;
        private readonly object _syncRoot = new object();

        private bool _isLive;

        public LiveList()
        {
            _items = new List<T>();
        }

        public LiveList(int capacity)
        {
            _items = new List<T>(capacity);
        }

        public LiveList(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            _items = new List<T>(items);
        }

        public bool IsLive
        {
            get
            {
                lock (_syncRoot)
                {
                    return _isLive;
                }
            }
            set
            {
                T current = default(T);
                bool raiseFrontChanged = false;

                lock (_syncRoot)
                {
                    bool wasLive = _isLive;

                    _isLive = value;

                    if (!wasLive && _isLive)
                    {
                        current = GetFrontOrDefaultUnsafe();

                        if (!EqualityComparer<T>.Default.Equals(
                            default(T),
                            current))
                        {
                            raiseFrontChanged = true;
                        }
                    }
                }

                if (raiseFrontChanged)
                    RaiseFrontChanged(default(T), current);
            }
        }

        public int Count
        {
            get
            {
                lock (_syncRoot)
                {
                    return _items.Count;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                lock (_syncRoot)
                {
                    return _items.Count == 0;
                }
            }
        }

        public T this[int index]
        {
            get
            {
                lock (_syncRoot)
                {
                    return _items[index];
                }
            }
            set
            {
                T previous;

                lock (_syncRoot)
                {
                    previous = _items[index];

                    if (EqualityComparer<T>.Default.Equals(previous, value))
                        return;

                    _items[index] = value;
                }

                OnCollectionChanged(
                    new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        value,
                        previous,
                        index));

                if (index == 0)
                    RaiseFrontChanged(previous, value);
            }
        }

        public event FrontChangeEventHandler<T> FrontChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Push(T item)
        {
            int index;
            bool wasEmpty;

            lock (_syncRoot)
            {
                wasEmpty = _items.Count == 0;
                index = _items.Count;

                _items.Add(item);
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    item,
                    index));

            if (wasEmpty)
                RaiseFrontChanged(default(T), item);
        }

        public T Pop()
        {
            T item;
            int index;

            lock (_syncRoot)
            {
                if (_items.Count == 0)
                    throw new InvalidOperationException(
                        "Cannot Pop from an empty LiveList.");

                index = _items.Count - 1;
                item = _items[index];

                _items.RemoveAt(index);
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    item,
                    index));

            if (index == 0)
                RaiseFrontChanged(item, default(T));

            return item;
        }

        public void Unshift(T item)
        {
            T previous;

            lock (_syncRoot)
            {
                previous = GetFrontOrDefaultUnsafe();

                _items.Insert(0, item);
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    item,
                    0));

            RaiseFrontChanged(previous, item);
        }

        public T Shift()
        {
            T previous;
            T current;

            lock (_syncRoot)
            {
                if (_items.Count == 0)
                    throw new InvalidOperationException(
                        "Cannot Shift from an empty LiveList.");

                previous = _items[0];

                _items.RemoveAt(0);

                current = GetFrontOrDefaultUnsafe();
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    previous,
                    0));

            RaiseFrontChanged(previous, current);

            return previous;
        }

        public bool Remove(T item)
        {
            int index;
            T current = default(T);

            lock (_syncRoot)
            {
                index = _items.IndexOf(item);

                if (index < 0)
                    return false;

                _items.RemoveAt(index);

                if (index == 0)
                    current = GetFrontOrDefaultUnsafe();
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove,
                    item,
                    index));

            if (index == 0)
                RaiseFrontChanged(item, current);

            return true;
        }

        public void Move(T item, int newIndex)
        {
            int oldIndex;

            lock (_syncRoot)
            {
                oldIndex = _items.IndexOf(item);

                if (oldIndex < 0)
                    throw new ArgumentException(
                        "The item does not exist in the LiveList.",
                        "item");
            }

            Move(oldIndex, newIndex);
        }

        public void Move(int oldIndex, int newIndex)
        {
            T previous;
            T current;
            T item;

            lock (_syncRoot)
            {
                if (oldIndex < 0 || oldIndex >= _items.Count)
                    throw new ArgumentOutOfRangeException("oldIndex");

                if (newIndex < 0 || newIndex >= _items.Count)
                    throw new ArgumentOutOfRangeException("newIndex");

                if (oldIndex == newIndex)
                    return;

                previous = _items[0];
                item = _items[oldIndex];

                _items.RemoveAt(oldIndex);
                _items.Insert(newIndex, item);

                current = _items[0];
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Move,
                    item,
                    newIndex,
                    oldIndex));

            if (oldIndex == 0 || newIndex == 0)
                RaiseFrontChanged(previous, current);
        }

        public void MoveToFront(T item)
        {
            int index;
            T previous;

            lock (_syncRoot)
            {
                index = _items.IndexOf(item);

                if (index < 0)
                    throw new ArgumentException(
                        "The item does not exist in the LiveList.",
                        "item");

                if (index == 0)
                    return;

                previous = _items[0];

                _items.RemoveAt(index);
                _items.Insert(0, item);
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Move,
                    item,
                    0,
                    index));

            RaiseFrontChanged(previous, item);
        }

        public void MoveToBack(T item)
        {
            int index;
            int newIndex;
            T previous;
            T value;
            T current;

            lock (_syncRoot)
            {
                index = _items.IndexOf(item);

                if (index < 0)
                    throw new ArgumentException(
                        "The item does not exist in the LiveList.",
                        "item");

                if (index == _items.Count - 1)
                    return;

                previous = _items[0];
                value = _items[index];
                newIndex = _items.Count - 1;

                _items.RemoveAt(index);
                _items.Add(value);

                current = _items[0];
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Move,
                    value,
                    newIndex,
                    index));

            if (index == 0)
                RaiseFrontChanged(previous, current);
        }

        public T Peek()
        {
            lock (_syncRoot)
            {
                if (_items.Count == 0)
                    throw new InvalidOperationException(
                        "Cannot Peek an empty LiveList.");

                return _items[0];
            }
        }

        public T PeekBack()
        {
            lock (_syncRoot)
            {
                if (_items.Count == 0)
                    throw new InvalidOperationException(
                        "Cannot PeekBack an empty LiveList.");

                return _items[_items.Count - 1];
            }
        }

        public bool Contains(T item)
        {
            lock (_syncRoot)
            {
                return _items.Contains(item);
            }
        }

        public int IndexOf(T item)
        {
            lock (_syncRoot)
            {
                return _items.IndexOf(item);
            }
        }

        public void Clear()
        {
            T previous;

            lock (_syncRoot)
            {
                if (_items.Count == 0)
                    return;

                previous = _items[0];

                _items.Clear();
            }

            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));

            RaiseFrontChanged(previous, default(T));
        }

        private T GetFrontOrDefaultUnsafe()
        {
            if (_items.Count == 0)
                return default(T);

            return _items[0];
        }

        private void RaiseFrontChanged(T previous, T current)
        {
            bool isLive;

            lock (_syncRoot)
            {
                isLive = _isLive;
            }

            if (!isLive)
                return;

            if (EqualityComparer<T>.Default.Equals(previous, current))
                return;

            FrontChangeEventHandler<T> handler =
                FrontChanged;

            if (handler == null)
                return;

            handler(
                this,
                new FrontChangedEventArgs<T>(
                    previous,
                    current));
        }

        protected virtual void OnCollectionChanged(
            NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler =
                CollectionChanged;

            if (handler == null)
                return;

            handler(this, e);
        }

        public IEnumerator<T> GetEnumerator()
        {
            List<T> snapshot;

            lock (_syncRoot)
            {
                snapshot = new List<T>(_items);
            }

            return snapshot.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}