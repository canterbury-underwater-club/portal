// // src/stores/bookings/mine.ts
// import {
//   BookingsMineCreateContractsRequest,
//   BookingsMineCreateContractsResponse,
//   BookingsMineGetContractsResponse,
//   BookingsMineListContractsResponse,
//   BookingsMineModelsBookingModel as MyBooking,
// } from '@/api/generated/v1'
// import { buildMineBookingsApi } from '@/api/portal-api'
// import { defineStore } from 'pinia'
// import { ref } from 'vue'
// import { makeListKey, type DateRange } from './shared'

// function mapCreateToMyBooking(res: BookingsMineCreateContractsResponse): MyBooking {
//   return {
//     id: res.id,
//     createdAt: res.createdAt,
//     bookingStatus: res.bookingStatus,
//     checkInDate: res.checkInDate,
//     checkOutDate: res.checkOutDate,
//     groupName: res.groupName,
//     rooms: res.rooms,
//     contractHolderId: res.contractHolderId ?? null,
//     attendees: res.attendees,
//   }
// }

// export const useMyBookingsStore = defineStore('bookings-mine', () => {
//   const byId = ref<Record<string, MyBooking>>({})
//   const lists = ref<Record<string, string[]>>({})
//   const loading = ref(false)
//   const error = ref<unknown>(null)

//   const get = (id: string) => byId.value[id]
//   const list = (params: DateRange = {}) => {
//     const key = makeListKey('mine', params)
//     return (lists.value[key] ?? []).map((id) => byId.value[id]).filter(Boolean) as MyBooking[]
//   }
//   function upsert(items: MyBooking[]) {
//     for (const b of items) byId.value[b.id] = b
//   }
//   function setList(params: DateRange, items: MyBooking[]) {
//     const key = makeListKey('mine', params)
//     lists.value[key] = items.map((b) => b.id)
//   }
//   function invalidateLists() {
//     for (const k of Object.keys(lists.value)) if (k.startsWith('mine')) delete lists.value[k]
//   }
//   function reset() {
//     byId.value = {}
//     lists.value = {}
//     loading.value = false
//     error.value = null
//   }

//   async function fetchList(params: DateRange = {}) {
//     loading.value = true
//     error.value = null
//     try {
//       const api = await buildMineBookingsApi()
//       const res = await api.v1BookingsMineGet(params.from, params.to)
//       // Codegen types the list as AdminBooking[], but API should return "mine" details.
//       // Cast to MyBooking[] until OpenAPI is aligned.
//       const items = (res.data as BookingsMineListContractsResponse)
//         .bookings as unknown as MyBooking[]
//       upsert(items)
//       setList(params, items)
//     } catch (e) {
//       error.value = e
//     } finally {
//       loading.value = false
//     }
//   }

//   async function fetchById(id: string) {
//     const api = await buildMineBookingsApi()
//     const res = await api.v1BookingsMineIdGet(id)
//     const data = (res.data as BookingsMineGetContractsResponse).booking
//     byId.value[id] = data
//     return data
//   }

//   async function create(payload: BookingsMineCreateContractsRequest) {
//     const api = await buildMineBookingsApi()
//     const res = await api.v1BookingsMinePost(payload)
//     const created = mapCreateToMyBooking(res.data)
//     byId.value[created.id] = created
//     invalidateLists()
//     return created
//   }

//   return {
//     // state
//     byId,
//     lists,
//     loading,
//     error,
//     // getters
//     get,
//     list,
//     // actions
//     fetchList,
//     fetchById,
//     create,
//     reset,
//   }
// })
