import { BookingsPublicOccupancyRoomOccupancyStatusModel } from '@/api/generated/v1'
import { buildPublicBookingsApi } from '@/api/portal-api'
import { AllRooms } from '@/constants/rooms'
import { DateRange } from '@/types'
import { fromServerDate, toServerDate } from '@/utils/dateConverter'
import { allDaysInRange } from '@/utils/dateUtils'

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
  const occupancyByLocalDate = ref(new Map<string, DailyOccupancy>())

  function getOccupancy(date: Date) {
    return occupancyByLocalDate.value.get(date.toDateString())
  }

  async function fetchOccupancy(dateRange: Required<DateRange>) {
    const { from, to } = dateRange

    const dateRangeDays = allDaysInRange(dateRange)

    const allCached = dateRangeDays.every((d) => occupancyByLocalDate.value.has(d.toDateString()))
    if (allCached) {
      return // Everything already exists locally → skip API call
    }

    loading.value = true
    error.value = null

    try {
      const api = await buildPublicBookingsApi()
      const {
        data: { occupancy },
      } = await api.v1BookingsPublicOccupancyGet(toServerDate(from), toServerDate(to))

      const responseByDay = new Map<string, Map<number, OccupancyStatus>>()

      for (const day of occupancy.days) {
        const localDate = fromServerDate(day.date)
        const roomStatuses = new Map<number, OccupancyStatus>()

        for (const r of day.rooms) {
          roomStatuses.set(r.room, toOccupancyStatus(r.status))
        }
        responseByDay.set(localDate.toDateString(), roomStatuses)
      }

      for (const day of dateRangeDays) {
        const roomsForDay = AllRooms.map((room) => {
          const status =
            responseByDay.get(day.toDateString())?.get(room) ?? OccupancyStatus.Available
          return { room, status }
        })
        occupancyByLocalDate.value.set(day.toDateString(), {
          date: day,
          rooms: roomsForDay,
        })
      }
    } catch (e) {
      error.value = e
      return []
    } finally {
      loading.value = false
    }
  }

  async function getAvailableRooms(dateRange: Required<DateRange>): Promise<number[]> {
    await fetchOccupancy(dateRange)

    const days = allDaysInRange(dateRange)
    const available = new Set<number>(AllRooms)

    for (const day of days) {
      const occupancy = occupancyByLocalDate.value.get(day.toDateString())
      if (!occupancy) {
        // If any day's data is missing (e.g., API error), conservatively return empty
        return []
      }
      for (const { room, status } of occupancy.rooms) {
        if (status !== OccupancyStatus.Available) {
          available.delete(room)
        }
      }
    }

    return Array.from(available).sort((a, b) => a - b)
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
    fetchOccupancy,
    getOccupancy,
    getAvailableRooms,
  }
})
