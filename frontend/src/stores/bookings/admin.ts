import {
  BookingsAdminBookingsModelsBookingModel as AdminBooking,
  BookingsAdminBookingsCreateContractsRequest,
  BookingsAdminBookingsGetContractsResponse,
  BookingsAdminBookingsListContractsResponse,
  BookingsAdminBookingsUpdateContractsRequest,
} from '@/api/generated/v1'
import { buildAdminBookingsApi } from '@/api/portal-api'
import { DateRange } from '@/types'
import { Booking, BookingFromAdmin } from '@/types/booking'
import { fromServerDate, toServerDate } from '@/utils/dateConverter'
import { isWithinInterval } from 'date-fns'

export const useBookingsAdminStore = defineStore('bookings-admin', () => {
  const byId = ref<Record<string, AdminBooking>>({})
  const loading = ref(false)
  const error = ref<unknown>(null)

  const all = computed(() => Object.values(byId.value) as AdminBooking[])
  const get = (id: string) => byId.value[id]

  async function fetchBookings(params: { dateRange?: DateRange; count?: number }) {
    loading.value = true
    error.value = null
    const from = params.dateRange?.from
    const to = params.dateRange?.to
    try {
      const api = await buildAdminBookingsApi()
      const res = await api.v1BookingsAdminBookingsGet(
        from ? toServerDate(from) : undefined,
        to ? toServerDate(to) : undefined,
        params.count,
      )
      const data = res.data as BookingsAdminBookingsListContractsResponse

      for (const booking of data.bookings) byId.value[booking.id] = booking
    } catch (e) {
      error.value = e
    } finally {
      loading.value = false
    }
  }

  async function fetchById(id: string) {
    const api = await buildAdminBookingsApi()
    const res = await api.v1BookingsAdminBookingsIdGet(id)
    const data = (res.data as BookingsAdminBookingsGetContractsResponse).booking
    byId.value[id] = data
    return data
  }

  async function create(payload: BookingsAdminBookingsCreateContractsRequest) {
    const api = await buildAdminBookingsApi()
    const res = await api.v1BookingsAdminBookingsPost(payload)
    const created = res.data.booking
    byId.value[created.id] = created
    return created
  }

  async function patch(
    id: string,
    patch: Partial<BookingsAdminBookingsUpdateContractsRequest>,
    opts: { refetch?: boolean } = { refetch: true },
  ) {
    const api = await buildAdminBookingsApi()
    await api.v1BookingsAdminBookingsIdPatch(
      id,
      patch as BookingsAdminBookingsUpdateContractsRequest,
    )
    const cur = byId.value[id]
    if (cur) byId.value[id] = { ...cur, ...patch } as AdminBooking
    if (opts.refetch) await fetchById(id)
  }

  const nextUpcomingBooking = computed<Booking | undefined>(() => {
    const today = new Date()
    today.setHours(0, 0, 0, 0) // normalize to midnight

    const booking = all.value
      .filter((b) => fromServerDate(b.checkInDate) >= today)
      .sort(
        (a, b) => fromServerDate(a.checkInDate).getTime() - fromServerDate(b.checkInDate).getTime(),
      )
      .at(0)

    if (booking) return BookingFromAdmin(booking)
  })

  function bookingsForRange(range: Required<DateRange>): Booking[] {
    const start = range.from
    const end = range.to
    return all.value
      .filter((b) => {
        const checkIn = fromServerDate(b.checkInDate)
        const checkOut = fromServerDate(b.checkOutDate)
        // overlap if either endpoint is inside, or booking fully spans window
        return (
          isWithinInterval(checkIn, { start, end }) ||
          isWithinInterval(checkOut, { start, end }) ||
          (checkIn <= start && checkOut >= end)
        )
      })
      .map(BookingFromAdmin)
  }

  return {
    // state
    byId,
    loading,
    error,
    // getters
    get,
    nextUpcomingBooking,
    bookingsForRange,
    // actions
    fetchBookings,
    fetchById,
    create,
    patch,
  }
})
