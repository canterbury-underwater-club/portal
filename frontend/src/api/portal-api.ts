import axiosInstance from './axiosInstance'
import { Configuration, PortalApiApi } from './generated/v1'

const isDevelopment = import.meta.env.MODE === 'development'

const basePath = isDevelopment ? 'http://localhost:5171' : `api.${window.location.origin}`

const buildApiConfig = async () => {
  return new Configuration({
    apiKey: 'hello',
  })
}

export const buildWeatherApi = async () =>
  new PortalApiApi(await buildApiConfig(), basePath, axiosInstance)
