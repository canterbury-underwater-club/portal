# BookingsAdminRatePlansApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1BookingsAdminRatePlansGet**](#v1bookingsadminrateplansget) | **GET** /v1/bookings/admin/rate-plans | |
|[**v1BookingsAdminRatePlansIdDelete**](#v1bookingsadminrateplansiddelete) | **DELETE** /v1/bookings/admin/rate-plans/{id} | |
|[**v1BookingsAdminRatePlansIdGet**](#v1bookingsadminrateplansidget) | **GET** /v1/bookings/admin/rate-plans/{id} | |
|[**v1BookingsAdminRatePlansPost**](#v1bookingsadminrateplanspost) | **POST** /v1/bookings/admin/rate-plans | |

# **v1BookingsAdminRatePlansGet**
> BookingsAdminBookingRatePlansListContractsResponse v1BookingsAdminRatePlansGet()


### Example

```typescript
import {
    BookingsAdminRatePlansApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminRatePlansApi(configuration);

const { status, data } = await apiInstance.v1BookingsAdminRatePlansGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**BookingsAdminBookingRatePlansListContractsResponse**

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

# **v1BookingsAdminRatePlansIdDelete**
> v1BookingsAdminRatePlansIdDelete()


### Example

```typescript
import {
    BookingsAdminRatePlansApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminRatePlansApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.v1BookingsAdminRatePlansIdDelete(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**204** | No Content |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **v1BookingsAdminRatePlansIdGet**
> BookingsAdminBookingRatePlansGetContractsResponse v1BookingsAdminRatePlansIdGet()


### Example

```typescript
import {
    BookingsAdminRatePlansApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminRatePlansApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.v1BookingsAdminRatePlansIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

**BookingsAdminBookingRatePlansGetContractsResponse**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **v1BookingsAdminRatePlansPost**
> BookingsAdminBookingRatePlansCreateContractsResponse v1BookingsAdminRatePlansPost(bookingsAdminBookingRatePlansCreateContractsRequest)


### Example

```typescript
import {
    BookingsAdminRatePlansApi,
    Configuration,
    BookingsAdminBookingRatePlansCreateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminRatePlansApi(configuration);

let bookingsAdminBookingRatePlansCreateContractsRequest: BookingsAdminBookingRatePlansCreateContractsRequest; //

const { status, data } = await apiInstance.v1BookingsAdminRatePlansPost(
    bookingsAdminBookingRatePlansCreateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **bookingsAdminBookingRatePlansCreateContractsRequest** | **BookingsAdminBookingRatePlansCreateContractsRequest**|  | |


### Return type

**BookingsAdminBookingRatePlansCreateContractsResponse**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**201** | Created |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

