# PortalApiApi

All URIs are relative to _http://localhost:5171_

| Method                                        | HTTP request             | Description |
| --------------------------------------------- | ------------------------ | ----------- |
| [**getWeatherForecast**](#getweatherforecast) | **GET** /weatherforecast |             |

# **getWeatherForecast**

> Array<WeatherForecast> getWeatherForecast()

### Example

```typescript
import { PortalApiApi, Configuration } from './api'

const configuration = new Configuration()
const apiInstance = new PortalApiApi(configuration)

const { status, data } = await apiInstance.getWeatherForecast()
```

### Parameters

This endpoint does not have any parameters.

### Return type

**Array<WeatherForecast>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details

| Status code | Description | Response headers |
| ----------- | ----------- | ---------------- |
| **200**     | OK          | -                |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
