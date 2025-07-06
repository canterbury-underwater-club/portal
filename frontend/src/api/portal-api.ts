import axiosInstance from './axiosInstance'
import { Configuration, MembersApi } from './generated/v1'

const isDevelopment = import.meta.env.MODE === 'development'

const basePath = isDevelopment
  ? 'http://localhost:5171'
  : `https://api.portal.canterburyunderwater.org.nz`

const buildApiConfig = async () => {
  return new Configuration({})
}

export const buildMembersApi = async () =>
  new MembersApi(await buildApiConfig(), basePath, axiosInstance)
