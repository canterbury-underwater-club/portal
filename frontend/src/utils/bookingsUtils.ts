import { Booking } from '@/types/booking'

export function findNextUpcomingBooking(bookings: Booking[]) {
  const today = new Date()
  today.setHours(0, 0, 0, 0) // normalize to midnight

  const nextBooking = bookings
    .filter((b) => b.checkInDate >= today)
    .sort((a, b) => a.checkInDate.getTime() - b.checkOutDate.getTime())
    .at(0)

  return nextBooking
}
