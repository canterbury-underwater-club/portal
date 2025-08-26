using System.Collections;
using System.Diagnostics;
using System.Text;

namespace CanterburyUnderwater.PortalApi.Services;

public record BookingConflict
{
    public required DateOnly Date { get; init; }
    public required int Room { get; init; }
}

public sealed class BookingConflicts : IReadOnlyCollection<BookingConflict>
{
    private readonly List<BookingConflict> _items;

    public BookingConflicts(IEnumerable<BookingConflict> items)
    {
        _items = items.OrderBy(c => c.Room).ThenBy(c => c.Date).ToList();
    }

    public bool HasConflicts => _items.Count > 0;

    public string Description
    {
        get
        {
            if (!HasConflicts) return string.Empty;

            var sb = new StringBuilder();
            var firstGroup = true;

            foreach (var kvp in ByRoom())
            {
                if (!firstGroup) sb.Append(" | ");
                firstGroup = false;

                var ranges = CollapseDateRanges(kvp.Value);
                sb.Append("Room ").Append(kvp.Key).Append(": ")
                    .Append(string.Join("; ", ranges.Select(r => r.ToString())));
            }

            return sb.ToString();
        }
    }

    public int Count => _items.Count;

    public IEnumerator<BookingConflict> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private Dictionary<int, IReadOnlyList<DateOnly>> ByRoom()
    {
        return _items.GroupBy(c => c.Room)
            .OrderBy(g => g.Key)
            .ToDictionary(
                g => g.Key, IReadOnlyList<DateOnly> (g) => g.Select(x => x.Date).Distinct().OrderBy(d => d).ToList());
    }

    private static List<DateRange> CollapseDateRanges(IReadOnlyList<DateOnly> orderedDistinctDates)
    {
        Debug.Assert(orderedDistinctDates.SequenceEqual(orderedDistinctDates.Distinct().OrderBy(d => d)),
            "Input should be ordered and distinct.");

        var ranges = new List<DateRange>();
        if (orderedDistinctDates.Count == 0) return ranges;

        var start = orderedDistinctDates[0];
        var prev = start;

        for (var i = 1; i < orderedDistinctDates.Count; i++)
        {
            var d = orderedDistinctDates[i];
            if (d == prev.AddDays(1))
            {
                prev = d;
            }
            else
            {
                ranges.Add(new DateRange(start, prev));
                start = prev = d;
            }
        }

        ranges.Add(new DateRange(start, prev));
        return ranges;
    }

    private readonly record struct DateRange(DateOnly Start, DateOnly End)
    {
        public override string ToString()
        {
            return Start == End ? Start.ToString("yyyy-MM-dd") : $"{Start:yyyy-MM-dd} – {End:yyyy-MM-dd}";
        }
    }
}