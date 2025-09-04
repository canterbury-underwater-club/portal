import { DateRange } from '@/types'

export function allDaysInRange(dateRange: Required<DateRange>) {
  const { from, to } = dateRange
  const days: Date[] = []
  for (let d = new Date(from); d <= to; d.setDate(d.getDate() + 1)) {
    days.push(new Date(d))
  }
  return days
}
