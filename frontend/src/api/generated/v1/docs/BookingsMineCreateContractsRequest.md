# BookingsMineCreateContractsRequest


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**checkInDate** | **string** |  | [default to undefined]
**checkOutDate** | **string** |  | [default to undefined]
**groupName** | **string** |  | [optional] [default to undefined]
**rooms** | **Array&lt;number&gt;** |  | [optional] [default to undefined]
**contractHolderId** | **string** |  | [optional] [default to undefined]
**attendees** | [**Array&lt;BookingsModelsBookingAttendeeModel&gt;**](BookingsModelsBookingAttendeeModel.md) |  | [optional] [default to undefined]

## Example

```typescript
import { BookingsMineCreateContractsRequest } from './api';

const instance: BookingsMineCreateContractsRequest = {
    checkInDate,
    checkOutDate,
    groupName,
    rooms,
    contractHolderId,
    attendees,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
