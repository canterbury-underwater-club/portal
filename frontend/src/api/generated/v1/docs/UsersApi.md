# UsersApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1UsersGet**](#v1usersget) | **GET** /v1/users | |
|[**v1UsersIdPatch**](#v1usersidpatch) | **PATCH** /v1/users/{id} | |
|[**v1UsersSignInPost**](#v1userssigninpost) | **POST** /v1/users/sign-in | |

# **v1UsersGet**
> UsersListContractsResponse v1UsersGet()


### Example

```typescript
import {
    UsersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

const { status, data } = await apiInstance.v1UsersGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**UsersListContractsResponse**

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

# **v1UsersIdPatch**
> v1UsersIdPatch(usersUpdateContractsRequest)


### Example

```typescript
import {
    UsersApi,
    Configuration,
    UsersUpdateContractsRequest
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

let id: string; // (default to undefined)
let usersUpdateContractsRequest: UsersUpdateContractsRequest; //

const { status, data } = await apiInstance.v1UsersIdPatch(
    id,
    usersUpdateContractsRequest
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **usersUpdateContractsRequest** | **UsersUpdateContractsRequest**|  | |
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
|**404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **v1UsersSignInPost**
> UsersSignInContractsResponse v1UsersSignInPost()


### Example

```typescript
import {
    UsersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new UsersApi(configuration);

const { status, data } = await apiInstance.v1UsersSignInPost();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**UsersSignInContractsResponse**

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

