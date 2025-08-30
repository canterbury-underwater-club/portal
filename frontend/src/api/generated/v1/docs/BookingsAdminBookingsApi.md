# BookingsAdminBookingsApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1BookingsAdminBookingsGet**](#v1bookingsadminbookingsget) | **GET** /v1/bookings/admin/bookings | |
|[**v1BookingsAdminBookingsIdGet**](#v1bookingsadminbookingsidget) | **GET** /v1/bookings/admin/bookings/{id} | |
|[**v1BookingsAdminBookingsIdPatch**](#v1bookingsadminbookingsidpatch) | **PATCH** /v1/bookings/admin/bookings/{id} | |
|[**v1BookingsAdminBookingsPost**](#v1bookingsadminbookingspost) | **POST** /v1/bookings/admin/bookings | |

# **v1BookingsAdminBookingsGet**
> BookingsAdminBookingsListContractsResponse v1BookingsAdminBookingsGet()


### Example

```typescript
import {
    BookingsAdminBookingsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminBookingsApi(configuration);

let from: string; // (optional) (default to undefined)
let to: string; // (optional) (default to undefined)

const { status, data } = await apiInstance.v1BookingsAdminBookingsGet(
    from,
    to
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **from** | [**string**] |  | (optional) defaults to undefined|
| **to** | [**string**] |  | (optional) defaults to undefined|


### Return type

**BookingsAdminBookingsListContractsResponse**

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

# **v1BookingsAdminBookingsIdGet**
> BookingsAdminBookingsGetContractsResponse v1BookingsAdminBookingsIdGet()


### Example

```typescript
import {
    BookingsAdminBookingsApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminBookingsApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.v1BookingsAdminBookingsIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

**BookingsAdminBookingsGetContractsResponse**

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

# **v1BookingsAdminBookingsIdPatch**
> v1BookingsAdminBookingsIdPatch(bookingsAdminBookingsUpdateContractsRequest)


### Example

```typescript
import {
    BookingsAdminBookingsApi,
    Configuration,
    BookingsAdminBookingsUpdateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminBookingsApi(configuration);

let id: string; // (default to undefined)
let bookingsAdminBookingsUpdateContractsRequest: BookingsAdminBookingsUpdateContractsRequest; //

const { status, data } = await apiInstance.v1BookingsAdminBookingsIdPatch(
    id,
    bookingsAdminBookingsUpdateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **bookingsAdminBookingsUpdateContractsRequest** | **BookingsAdminBookingsUpdateContractsRequest**|  | |
| **id** | [**string**] |  | defaults to undefined|


### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**400** | Bad Request |  -  |
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **v1BookingsAdminBookingsPost**
> BookingsAdminBookingsCreateContractsResponse v1BookingsAdminBookingsPost(bookingsAdminBookingsCreateContractsRequest)


### Example

```typescript
import {
    BookingsAdminBookingsApi,
    Configuration,
    BookingsAdminBookingsCreateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminBookingsApi(configuration);

let bookingsAdminBookingsCreateContractsRequest: BookingsAdminBookingsCreateContractsRequest; //

const { status, data } = await apiInstance.v1BookingsAdminBookingsPost(
    bookingsAdminBookingsCreateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **bookingsAdminBookingsCreateContractsRequest** | **BookingsAdminBookingsCreateContractsRequest**|  | |


### Return type

**BookingsAdminBookingsCreateContractsResponse**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**201** | Created |  -  |
|**400** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

