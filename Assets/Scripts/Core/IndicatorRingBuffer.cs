using System.Collections.Generic;
using UnityEngine;

namespace VertigoCaseProject.Core
{
    /// <summary>
    /// A generic ring buffer implementation with additional functionality for managing spacing and indicator capacity.
    /// </summary>
    /// <typeparam name="T">The type of elements the buffer will hold.</typeparam>
    public class RingBuffer<T>
    {
        #region Fields

        private readonly List<T> _buffer;
        private readonly Queue<T> _availableQueue;
        private readonly float _spacing;
        private readonly int _indicatorCapacity;

        private int _currentIndex;
        private int _totalIndex;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RingBuffer{T}"/> class.
        /// </summary>
        /// <param name="buffer">The list that acts as the buffer.</param>
        /// <param name="spacing">The spacing value used for calculating the x-value.</param>
        /// <param name="indicatorCapacity">The maximum indicator capacity.</param>
        public RingBuffer(List<T> buffer, float spacing, int indicatorCapacity)
        {
            _buffer = buffer;
            _spacing = spacing;
            _indicatorCapacity = indicatorCapacity;
            _totalIndex = 0;
            _currentIndex = 0;

            _availableQueue = new Queue<T>(_buffer);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves an element from the buffer while providing its x-value and index.
        /// </summary>
        /// <param name="xValue">The calculated x-value based on spacing and total index.</param>
        /// <param name="index">The current index of the element.</param>
        /// <returns>The retrieved element of type <typeparamref name="T"/>.</returns>
        public T GetNumber(out float xValue, out int index)
        {
            T result;

            if (_availableQueue.Count > 0)
            {
                result = _availableQueue.Dequeue();
            }
            else
            {
                result = _buffer[_currentIndex];
                _currentIndex = (_currentIndex + 1) % _buffer.Count;
            }

            index = _totalIndex;
            xValue = Mathf.Clamp(_totalIndex, 0, (_indicatorCapacity + 1) / 2) * _spacing;

            _totalIndex++;
            return result;
        }

        #endregion
    }
}