# MembersApi

All URIs are relative to *http://localhost:5171*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**v1MembersGet**](#v1membersget) | **GET** /v1/members | |

# **v1MembersGet**
> Response v1MembersGet()


### Example

```typescript
import {
    MembersApi,
    Configuration
} from './api';

const configuration = new Configuration();
const apiInstance = new MembersApi(configuration);

const { status, data } = await apiInstance.v1MembersGet();
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

