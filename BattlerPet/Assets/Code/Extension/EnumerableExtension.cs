using System;
using System.Linq;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class EnumerableExtension
{
    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) =>
        source.Shuffle().Take(count);

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) =>
        source.OrderBy(x => Guid.NewGuid());

    public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount) =>
        list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();

    public static List<T> GetClone<T>(this List<T> source) => 
        source.GetRange(0, source.Count);
    
    public static float? SumOrNull(this IEnumerable<float> numbers) => 
        numbers.Aggregate<float, float?>(null, (current, f) => f + (current ?? 0));

    public static T FindMin<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector) where TComp : IComparable<TComp> =>
        Find(enumerable, selector, true);

    public static T FindMax<T, TComp>(this IEnumerable<T> enumerable, Func<T, TComp> selector) where TComp : IComparable<TComp> =>
        Find(enumerable, selector, false);
    
    public static T PickRandomExcluding<T>(this IEnumerable<T> collection, T excludedItem)
    {
        if (collection == null)
            return default;

        List<T> actual = collection.Where(item => !item.Equals(excludedItem)).ToList();

        return actual.Count > 0 
            ? actual[Random.Range(0, actual.Count)]
            : default;
    }
    
    public static T PickRandom<T>(this IEnumerable<T> collection)
    {
        switch (collection)
        {
            case null:
                return default;
            
            case IList<T> list:
                return list.Count != 0
                    ? list[Random.Range(0, list.Count)]
                    : default;
            
            case HashSet<T> hashset:
                return hashset.Count != 0
                    ? hashset.ElementAt(Random.Range(0, hashset.Count))
                    : default;
            default:
            {
                List<T> actual = collection.ToList();
                return actual.Count > 0
                    ? actual[Random.Range(0, actual.Count)]
                    : default;
            }
        }
    }

    private static T Find<T, TComp>(IEnumerable<T> enumerable, Func<T, TComp> selector, bool selectMin) where TComp : IComparable<TComp>
    {
        if (enumerable == null)
            return default;

        var first = true;
        var selected = default(T);
        var selectedComp = default(TComp);

        foreach (T current in enumerable)
        {
            TComp comp = selector(current);
            if (first)
            {
                first = false;
                selected = current;
                selectedComp = comp;
                continue;
            }
            int res = selectMin
                ? comp.CompareTo(selectedComp)
                : selectedComp.CompareTo(comp);

            if (res < 0)
            {
                selected = current;
                selectedComp = comp;
            }
        }
        return selected;
    }
    
    public static void AddListToDictionary<TKey, TValue>(TKey key, Dictionary<TKey, List<TValue>> dictionary, IEnumerable<TValue> values)
    {
        if (dictionary.TryGetValue(key, out List<TValue> existingList))
        {
            foreach (TValue value in values.Where(value => !existingList.Contains(value)))
                existingList.Add(value);
        }
        else
        {
            dictionary[key] = new List<TValue>(values);
        }
    }
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T obj in source)
            action(obj);
        return source;
    }
}