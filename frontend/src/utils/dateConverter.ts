import { TZDate, tz } from '@date-fns/tz'
import { format } from 'date-fns'

const NZ = 'Pacific/Auckland'

/**
 * UTC DateOnly ("yyyy-MM-dd") -> NZ Date (JS Date at NZ midnight for that day)
 * Example: "2025-04-06" (UTC) -> Date representing 2025-04-06T00:00:00 in NZ,
 * DST-safe (may map to 11:00/12:00 UTC depending on NZST/NZDT).
 */
export function fromServerDate(utcDateOnly: string): Date {
  const [y, m, d] = utcDateOnly.split('-').map(Number)

  const utcMidnight = new Date(Date.UTC(y, (m ?? 1) - 1, d ?? 1, 0, 0, 0))
  const nzDayStr = format(utcMidnight, 'yyyy-MM-dd', { in: tz(NZ) })
  const [ny, nm, nd] = nzDayStr.split('-').map(Number)

  return new TZDate(ny, (nm ?? 1) - 1, nd ?? 1, 0, 0, 0, NZ)
}

/**
 * NZ Date (any time that day) -> UTC DateOnly ("yyyy-MM-dd")
 * Interprets the NZ calendar date of the given Date and returns
 * the UTC calendar day containing *NZ midnight* for that day.
 */
export function toServerDate(nzDate: Date | undefined | null): string {
  const input = nzDate ?? new Date()
  const nzDayStr = format(input, 'yyyy-MM-dd', { in: tz(NZ) })
  const [y, m, d] = nzDayStr.split('-').map(Number)

  const nzMidnight = new TZDate(y, (m ?? 1) - 1, d ?? 1, 0, 0, 0, NZ)
  return nzMidnight.toISOString().slice(0, 10)
}
