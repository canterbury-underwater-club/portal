export type DateRange = { from?: Date; to?: Date }

export interface CalendarEvent<T> {
  id: string
  title: string
  start: Date // NZ midnight (Date instant) for the booking start
  end: Date // NZ midnight (Date instant) for the booking end
  rooms: number[]
  source: T
}
