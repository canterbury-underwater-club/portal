# BookingsMineApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1BookingsMineGet**](#v1bookingsmineget) | **GET** /v1/bookings/mine | |
|[**v1BookingsMineIdGet**](#v1bookingsmineidget) | **GET** /v1/bookings/mine/{id} | |
|[**v1BookingsMinePost**](#v1bookingsminepost) | **POST** /v1/bookings/mine | |

# **v1BookingsMineGet**
> BookingsMineListContractsResponse v1BookingsMineGet()


### Example

```typescript
import {
    BookingsMineApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsMineApi(configuration);

let from: string; // (optional) (default to undefined)
let to: string; // (optional) (default to undefined)

const { status, data } = await apiInstance.v1BookingsMineGet(
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

**BookingsMineListContractsResponse**

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

# **v1BookingsMineIdGet**
> BookingsMineGetContractsResponse v1BookingsMineIdGet()


### Example

```typescript
import {
    BookingsMineApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsMineApi(configuration);

let id: string; // (default to undefined)

const { status, data } = await apiInstance.v1BookingsMineIdGet(
    id
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **id** | [**string**] |  | defaults to undefined|


### Return type

**BookingsMineGetContractsResponse**

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

# **v1BookingsMinePost**
> BookingsMineCreateContractsResponse v1BookingsMinePost(bookingsMineCreateContractsRequest)


### Example

```typescript
import {
    BookingsMineApi,
    Configuration,
    BookingsMineCreateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsMineApi(configuration);

let bookingsMineCreateContractsRequest: BookingsMineCreateContractsRequest; //

const { status, data } = await apiInstance.v1BookingsMinePost(
    bookingsMineCreateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **bookingsMineCreateContractsRequest** | **BookingsMineCreateContractsRequest**|  | |


### Return type

**BookingsMineCreateContractsResponse**

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

