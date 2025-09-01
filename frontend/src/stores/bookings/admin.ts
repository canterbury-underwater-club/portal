import {
  BookingsAdminBookingsModelsBookingModel as AdminBooking,
  BookingsAdminBookingsCreateContractsRequest,
  BookingsAdminBookingsGetContractsResponse,
  BookingsAdminBookingsListContractsResponse,
  BookingsAdminBookingsUpdateContractsRequest,
} from '@/api/generated/v1'
import { buildAdminBookingsApi } from '@/api/portal-api'
import { DateRange } from '@/types'
import { toServerDate } from '@/utils/dateConverter'

export const useBookingsAdminStore = defineStore('bookings-admin', () => {
  const byId = ref<Record<string, AdminBooking>>({})
  const loading = ref(false)
  const error = ref<unknown>(null)

  const allBookings = computed(() => Object.values(byId.value) as AdminBooking[])
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
      return data.bookings // ⬅️ return raw list
    } catch (e) {
      error.value = e
      return []
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

  return {
    // state
    byId,
    loading,
    error,
    // getters
    allBookings,
    get,
    // actions
    fetchBookings,
    fetchById,
    create,
    patch,
  }
})
