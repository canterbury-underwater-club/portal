import axios from 'axios'

axios.interceptors.response.use(
  function (config) {
    return config
  },
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  function (error: any) {
    const detail = error.response?.data?.detail
    if (detail) {
      throw new Error(detail)
    }
    return Promise.reject(error)
  },
)

export default axios
