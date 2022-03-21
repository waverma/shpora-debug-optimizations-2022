using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace JPEG.Utilities
{
	static class IEnumerableExtensions
	{
		public static T MinOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
		{
			var minimumItem = (Value: default(T),CompareKey: int.MaxValue);
			
			foreach (var item in enumerable)
			{
				var itemKey = selector(item);
				if (minimumItem.CompareKey > itemKey) minimumItem = (item, itemKey);
			}

			return minimumItem.Value;
			
			//return enumerable.OrderBy(selector).FirstOrDefault();
		}
		
		public static unsafe (T, T) TwoMinOrDefault<T>(this T[] enumerable, Func<T, int> selector)
		{
			var minimumItem = (Value: default(T),CompareKey: int.MaxValue);
			var secondMinValue = (Value: default(T),CompareKey: int.MaxValue);

			fixed (int* input = enumerable)
			{
				
			}

			foreach (var item in enumerable)
			{
				var itemKey = selector(item);
				if (secondMinValue.CompareKey < itemKey) continue;
				if (minimumItem.CompareKey < itemKey)
				{
					secondMinValue = (item, itemKey);
					continue;
				}

				secondMinValue = minimumItem;
				minimumItem = (item, itemKey);
			}
			
			return (minimumItem.Value, secondMinValue.Value);
		}

		public static T[] Without<T>(this T[] enumerable, params T[] elements)
		{
			var ignoreFlag = false;
			
			foreach (var item in enumerable)
			{
				foreach (var ignoredElements in elements)
				{
					if (!ignoredElements.Equals(item)) continue;
					ignoreFlag = true;
					break;
				}

				if (!ignoreFlag) yield return item;
				ignoreFlag = false;
			}
			//return enumerable.Where(x => !elements.Contains(x));
		}
		
		public static IEnumerable<T> ToEnumerable<T>(this T element)
		{
			yield return element;
		}
	}
}