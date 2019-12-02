using System;

namespace BinateCoveringProblem.App.Extensions
{
    public static class ArrayExtensions
    {
        public static TValue GetNext<TValue>(this TValue[] array, TValue value)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array), "Array is null");
            }

            var index = Array.IndexOf(array, value);

            if (index < 0)
            {
                throw new ArgumentException("Value not found", nameof(value));
            }

            if (index == array.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            
            return array[index];
        }
    }
}
