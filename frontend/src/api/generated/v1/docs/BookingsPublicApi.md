# BookingsPublicApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1BookingsPublicOccupancyGet**](#v1bookingspublicoccupancyget) | **GET** /v1/bookings/public/occupancy | |

# **v1BookingsPublicOccupancyGet**
> BookingsPublicOccupancyContractsResponse v1BookingsPublicOccupancyGet()


### Example

```typescript
import {
    BookingsPublicApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsPublicApi(configuration);

let from: string; // (default to undefined)
let to: string; // (default to undefined)

const { status, data } = await apiInstance.v1BookingsPublicOccupancyGet(
    from,
    to
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **from** | [**string**] |  | defaults to undefined|
| **to** | [**string**] |  | defaults to undefined|


### Return type

**BookingsPublicOccupancyContractsResponse**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

