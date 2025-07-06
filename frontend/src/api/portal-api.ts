import { useAuth } from '@/composables/useAuth'
import { settingsService } from '@/utils/settingsService'
import axiosInstance from './axiosInstance'
import { Configuration, MembersApi } from './generated/v1'

const basePath = settingsService.isDevelopment
  ? 'http://localhost:5171'
  : `https://api.portal.canterburyunderwater.org.nz`

const buildApiConfig = async () => {
  return new Configuration({
    accessToken: await useAuth().getAccessToken(),
  })
}

export const buildMembersApi = async () =>
  new MembersApi(await buildApiConfig(), basePath, axiosInstance)
