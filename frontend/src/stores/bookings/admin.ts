import {
  BookingsAdminBookingsModelsBookingModel as AdminBooking,
  BookingsAdminBookingsCreateContractsRequest,
  BookingsAdminBookingsGetContractsResponse,
  BookingsAdminBookingsListContractsResponse,
  BookingsAdminBookingsUpdateContractsRequest,
} from '@/api/generated/v1'
import { buildAdminBookingsApi } from '@/api/portal-api'
import { CalendarEvent, DateRange } from '@/types'
import { fromServerDate, toServerDate } from '@/utils/dateConverter'
import { endOfMonth, endOfWeek, isWithinInterval, startOfMonth, startOfWeek } from 'date-fns'

export const useBookingsAdminStore = defineStore('bookings-admin', () => {
  const byId = ref<Record<string, AdminBooking>>({})
  const ids = ref<string[]>([])
  const loading = ref(false)
  const error = ref<unknown>(null)

  const all = computed(() => Object.values(byId.value) as AdminBooking[])

  const currentlyFiltered = computed(
    () => ids.value.map((id) => byId.value[id]).filter(Boolean) as AdminBooking[],
  )
  const get = (id: string) => byId.value[id]

  function upsert(items: AdminBooking[]) {
    for (const b of items) byId.value[b.id] = b
  }

  function reset() {
    byId.value = {}
    ids.value = []
    loading.value = false
    error.value = null
  }

  function invalidate() {
    ids.value = []
  }

  // Core fetches
  async function fetchList(params: { dateRange?: DateRange; count?: number }) {
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
      upsert(data.bookings)
      ids.value = data.bookings.map((b) => b.id)
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
    invalidate()
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
    invalidate()
  }

  // ─────────────────────────────────────────────────────────
  // Dashboard helpers

  // Build a UI event from a booking (Dates are NZ-midnight instants)
  function toEvent(b: AdminBooking): CalendarEvent<AdminBooking> {
    return {
      id: b.id,
      title: b.groupName || `Booking ${b.id.slice(0, 6)}`,
      start: fromServerDate(b.checkInDate),
      end: fromServerDate(b.checkOutDate),
      rooms: b.rooms ?? [],
      source: b,
    }
  }

  // Arrivals/departures *for the NZ day represented by `date`*
  const nextBooking = computed<AdminBooking | undefined>(() => {
    // "today" as your server DateOnly string (NZ-midnight via your helper)
    const todayServer = toServerDate(new Date())

    // filter to bookings that start today or later, pick earliest
    const upcoming = [...all.value]
      .filter((b) => b.checkInDate >= todayServer)
      .sort((a, b) => (a.checkInDate < b.checkInDate ? -1 : a.checkInDate > b.checkInDate ? 1 : 0))
      .at(0)

    return upcoming
  })

  const arrivalsOn = (date: Date) =>
    computed(() => {
      const serverDay = toServerDate(date) // UTC DateOnly string
      return currentlyFiltered.value.filter((b) => b.checkInDate === serverDay)
    })

  const departuresOn = (date: Date) =>
    computed(() => {
      const serverDay = toServerDate(date)
      return currentlyFiltered.value.filter((b) => b.checkOutDate === serverDay)
    })

  // Build calendar events that overlap the given NZ date range (inclusive)
  function eventsForRange(range: Required<DateRange>): CalendarEvent<AdminBooking>[] {
    const start = range.from
    const end = range.to
    return currentlyFiltered.value
      .filter((b) => {
        const ci = fromServerDate(b.checkInDate)
        const co = fromServerDate(b.checkOutDate)
        // overlap if either endpoint is inside, or booking fully spans window
        return (
          isWithinInterval(ci, { start, end }) ||
          isWithinInterval(co, { start, end }) ||
          (ci <= start && co >= end)
        )
      })
      .map(toEvent)
  }

  // Helpers to compute NZ date windows for calendar views (returns Dates)
  function monthWindow(anchor: Date) {
    const start = startOfWeek(startOfMonth(anchor), { weekStartsOn: 1 })
    const end = endOfWeek(endOfMonth(anchor), { weekStartsOn: 1 })
    return { from: start, to: end }
  }

  return {
    // state
    byId,
    ids,
    loading,
    error,
    // getters
    list: currentlyFiltered,
    get,
    nextBooking,
    arrivalsOn,
    departuresOn,
    eventsForRange,
    monthWindow,
    // actions
    fetchList,
    fetchById,
    create,
    patch,
    invalidate,
    reset,
  }
})
