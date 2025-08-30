# BookingsAdminContractHoldersApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1BookingsAdminContractHoldersGet**](#v1bookingsadmincontractholdersget) | **GET** /v1/bookings/admin/contract-holders | |
|[**v1BookingsAdminContractHoldersPost**](#v1bookingsadmincontractholderspost) | **POST** /v1/bookings/admin/contract-holders | |

# **v1BookingsAdminContractHoldersGet**
> BookingsAdminBookingContractHoldersListContractsResponse v1BookingsAdminContractHoldersGet()


### Example

```typescript
import {
    BookingsAdminContractHoldersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminContractHoldersApi(configuration);

const { status, data } = await apiInstance.v1BookingsAdminContractHoldersGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**BookingsAdminBookingContractHoldersListContractsResponse**

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

# **v1BookingsAdminContractHoldersPost**
> BookingsAdminBookingContractHoldersCreateContractsResponse v1BookingsAdminContractHoldersPost(bookingsAdminBookingContractHoldersCreateContractsRequest)


### Example

```typescript
import {
    BookingsAdminContractHoldersApi,
    Configuration,
    BookingsAdminBookingContractHoldersCreateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new BookingsAdminContractHoldersApi(configuration);

let bookingsAdminBookingContractHoldersCreateContractsRequest: BookingsAdminBookingContractHoldersCreateContractsRequest; //

const { status, data } = await apiInstance.v1BookingsAdminContractHoldersPost(
    bookingsAdminBookingContractHoldersCreateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **bookingsAdminBookingContractHoldersCreateContractsRequest** | **BookingsAdminBookingContractHoldersCreateContractsRequest**|  | |


### Return type

**BookingsAdminBookingContractHoldersCreateContractsResponse**

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

