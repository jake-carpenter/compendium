namespace Project;

/// <summary>
/// Sections of a UUID by the formal definition - https://datatracker.ietf.org/doc/html/rfc4122#section-3
/// </summary>
public enum UuidSection
{
    /// <summary>
    /// 4-octet section: <example>XXXXXXXX-0000-0000-0000-000000000000</example>
    /// </summary>
    TimeLow,

    /// <summary>
    /// 2-octet section: <example>00000000-XXXX-0000-0000-000000000000</example>
    /// </summary>
    TimeMid,

    /// <summary>
    /// 2-octet section: <example>00000000-0000-XXXX-0000-000000000000</example>
    /// </summary>
    TimeHighAndVersion,

    /// <summary>
    /// 1-octet section: <example>00000000-0000-0000-XX00-000000000000</example>
    /// </summary>
    ClockSeqAndReserved,

    /// <summary>
    /// 1-octet section: <example>00000000-0000-0000-00XX-000000000000</example>
    /// </summary>
    ClockSeqLow,

    /// <summary>
    /// 6-octet section: <example>00000000-0000-0000-0000-XXXXXXXXXXXX</example>
    /// </summary>
    Node
}

public enum SortPriority
{
    None,
    Primary,
    Secondary,
    Tertiary,
}

public static class UuidSectionExtensions
{
    public static SortPriority GetSortPriority(this UuidSection[] sections, int index)
    {
        for (var sectionIndex = 0; sectionIndex < sections.Length; sectionIndex++)
        {
            var section = sections[sectionIndex];
            if (!IsIndexInOctetRange(section, index))
                continue;

            return sectionIndex switch
            {
                0 => SortPriority.Primary,
                1 => SortPriority.Secondary,
                2 => SortPriority.Tertiary,
                _ => SortPriority.None
            };
        }

        return SortPriority.None;
    }

    private static bool IsIndexInOctetRange(this UuidSection section, int index)
    {
        return section switch
        {
            UuidSection.TimeLow => index < 8,
            UuidSection.TimeMid => index is > 8 and < 13,
            UuidSection.TimeHighAndVersion => index is > 13 and < 18,
            UuidSection.ClockSeqAndReserved => index is > 18 and < 21,
            UuidSection.ClockSeqLow => index is > 20 and < 23,
            UuidSection.Node => index is > 23 and < 36,
            _ => false
        };
    }
}