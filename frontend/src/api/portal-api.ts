import { useAuth } from '@/composables/useAuth'
import { settingsService } from '@/utils/settingsService'
import axiosInstance from './axiosInstance'
import {
  BookingsAdminBookingsApi,
  BookingsAdminContractHoldersApi,
  BookingsAdminRatePlansApi,
  BookingsMineApi,
  BookingsPublicApi,
  Configuration,
  UsersApi,
} from './generated/v1'

const buildApiConfig = async () => {
  return new Configuration({
    accessToken: await useAuth().getAccessToken(),
  })
}

export const buildUsersApi = async () =>
  new UsersApi(await buildApiConfig(), settingsService.apiBasePath, axiosInstance)

export const buildPublicBookingsApi = async () =>
  new BookingsPublicApi(await buildApiConfig(), settingsService.apiBasePath, axiosInstance)

export const buildMineBookingsApi = async () =>
  new BookingsMineApi(await buildApiConfig(), settingsService.apiBasePath, axiosInstance)

export const buildAdminBookingsApi = async () =>
  new BookingsAdminBookingsApi(await buildApiConfig(), settingsService.apiBasePath, axiosInstance)

export const buildAdminRatePlansApi = async () =>
  new BookingsAdminRatePlansApi(await buildApiConfig(), settingsService.apiBasePath, axiosInstance)

export const buildAdminContractHoldersApi = async () =>
  new BookingsAdminContractHoldersApi(
    await buildApiConfig(),
    settingsService.apiBasePath,
    axiosInstance,
  )
