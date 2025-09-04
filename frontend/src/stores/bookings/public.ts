import { BookingsPublicOccupancyRoomOccupancyStatusModel } from '@/api/generated/v1'
import { buildPublicBookingsApi } from '@/api/portal-api'
import { DateRange } from '@/types'
import { fromServerDate, toServerDate } from '@/utils/dateConverter'

export enum OccupancyStatus {
  Available,
  Booked,
  Pending,
}

export interface DailyOccupancy {
  date: Date
  rooms: { room: number; status: OccupancyStatus }[]
}

export const useBookingsPublicStore = defineStore('bookings-public', () => {
  const loading = ref(false)
  const error = ref<unknown>(null)
  const occupancyByLocalDate = ref<Record<string, DailyOccupancy>>({})

  async function fetchOccupancy(dateRange: Required<DateRange>) {
    loading.value = true
    error.value = null
    const { from, to } = dateRange
    try {
      const api = await buildPublicBookingsApi()
      const {
        data: { occupancy },
      } = await api.v1BookingsPublicOccupancyGet(toServerDate(from), toServerDate(to))

      for (const day of occupancy.days) {
        const localDate = fromServerDate(day.date)
        occupancyByLocalDate.value[localDate.toDateString()] = {
          date: localDate,
          rooms: day.rooms.map((r) => ({ room: r.room, status: toOccupancyStatus(r.status) })),
        }
      }
    } catch (e) {
      error.value = e
      return []
    } finally {
      loading.value = false
    }
  }

  function toOccupancyStatus(model: BookingsPublicOccupancyRoomOccupancyStatusModel) {
    switch (model) {
      case BookingsPublicOccupancyRoomOccupancyStatusModel.Booked:
        return OccupancyStatus.Booked
      case BookingsPublicOccupancyRoomOccupancyStatusModel.Pending:
        return OccupancyStatus.Pending
    }
  }

  return {
    loading,
    occupancyByLocalDate,
    fetchOccupancy,
  }
})
