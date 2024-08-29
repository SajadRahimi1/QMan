using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace QMan.Infrastructure.Helpers;

public class EnumCollectionJsonValueConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
{
    public EnumCollectionJsonValueConverter() : base(
        v => JsonConvert
            .SerializeObject(v.Select(e => e.ToString()).ToList()),
        v => string.IsNullOrEmpty(v) ? new List<T>() : (JsonConvert
                .DeserializeObject<ICollection<string>>(v) ?? new List<string>())
            .Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
    {
    }
}

public class IntListConversion : ValueConverter<List<int>, string>
{
    public IntListConversion() : base(
        i => string.Join(",", i),
        s => string.IsNullOrWhiteSpace(s) ? new List<int>() : s.Split(new[] { ',' }).Select(v => int.Parse(v)).ToList())
    {
    }
}

public class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
{
    public CollectionValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>)c.ToHashSet())
    {
    }
}