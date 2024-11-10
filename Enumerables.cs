// Author: Gockner, Simon
// Created: 2020-11-20
// Copyright(c) 2020 SimonG. All Rights Reserved.

using System.Collections;
using System.Collections.ObjectModel;

namespace Lib.Tools;

public static class Enumerables
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) => new(enumerable);
    public static ObservableCollection<T> ToObservableCollectionOfType<T>(this IEnumerable enumerable) => enumerable.Cast<T>().ToObservableCollection();
    public static IEnumerable<T> NotOfType<T, TExcludedType>(this IEnumerable<T> enumerable) => enumerable.Where(i => i is not TExcludedType);
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        IEnumerable<T> list = enumerable.ToList();
        foreach (T item in list) 
            action(item);

        return list;
    }

    public static async Task<IEnumerable<T>> ForEach<T>(this IEnumerable<T> enumerable, Func<T, Task> function)
    {
        IEnumerable<T> list = enumerable.ToList();
        foreach (T item in list) 
            await function(item);

        return list;
    }

    public static IEnumerable<T> Maintain<T>(this IList<T> enumerable, IEnumerable<T> updatedEnumerable) where T : notnull
    {
        List<T> maintainingEnumerable = updatedEnumerable.ToList();
        
        bool needsMaintaining = enumerable.Count != maintainingEnumerable.Count;
        if (!needsMaintaining) 
            needsMaintaining = enumerable.Where((item, i) => !item.Equals(maintainingEnumerable[i])).Any();

        if (!needsMaintaining)
            return enumerable;

        foreach (T item in maintainingEnumerable.Where(i => !enumerable.Contains(i))) 
            enumerable.Insert(maintainingEnumerable.IndexOf(item), item);

        enumerable.RemoveAll(i => !maintainingEnumerable.Contains(i));

        return enumerable;
    }
    
    public static IEnumerable<T> RemoveAll<T>(this ICollection<T> enumerable, Func<T, bool> predicate)
    {
        foreach (T item in enumerable.Where(predicate).ToList()) 
            enumerable.Remove(item);

        return enumerable;
    }
}