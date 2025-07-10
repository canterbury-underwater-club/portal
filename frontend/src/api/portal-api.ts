import { useAuth } from '@/composables/useAuth'
import { settingsService } from '@/utils/settingsService'
import axiosInstance from './axiosInstance'
import { Configuration, UsersApi } from './generated/v1'

const basePath = settingsService.isDevelopment
  ? 'http://localhost:5171'
  : `https://api.portal.canterburyunderwater.org.nz`

const buildApiConfig = async () => {
  return new Configuration({
    accessToken: await useAuth().getAccessToken(),
  })
}

export const buildUsersApi = async () =>
  new UsersApi(await buildApiConfig(), basePath, axiosInstance)
