# UsersApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1UsersGet**](#v1usersget) | **GET** /v1/users | |
|[**v1UsersSignInPost**](#v1userssigninpost) | **POST** /v1/users/sign-in | |

# **v1UsersGet**
> Response2 v1UsersGet()


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

**Response2**

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

# **v1UsersSignInPost**
> Response v1UsersSignInPost()


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

**Response**

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

