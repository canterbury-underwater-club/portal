import { Booking } from './booking'

export type DateRange = { from?: Date; to?: Date }

export interface BookingCalendarEvent {
  id: string
  title: string
  start: Date // NZ midnight (Date instant) for the booking start
  end: Date // NZ midnight (Date instant) for the booking end
  color: string
  booking: Booking
}
