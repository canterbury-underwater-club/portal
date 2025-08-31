# BookingsMineModelsBookingModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**createdAt** | **string** |  | [default to undefined]
**bookingStatus** | [**BookingsModelsBookingStatusModel**](BookingsModelsBookingStatusModel.md) |  | [default to undefined]
**attendees** | [**Array&lt;BookingsModelsBookingAttendeeModel&gt;**](BookingsModelsBookingAttendeeModel.md) |  | [optional] [default to undefined]
**ratePlan** | [**BookingsModelsBookingRatePlanModel**](BookingsModelsBookingRatePlanModel.md) |  | [default to undefined]
**checkInDate** | **string** |  | [default to undefined]
**checkOutDate** | **string** |  | [default to undefined]
**groupName** | **string** |  | [optional] [default to undefined]
**rooms** | **Array&lt;number&gt;** |  | [optional] [default to undefined]
**contractHolderId** | **string** |  | [optional] [default to undefined]

## Example

```typescript
import { BookingsMineModelsBookingModel } from './api';

const instance: BookingsMineModelsBookingModel = {
    id,
    createdAt,
    bookingStatus,
    attendees,
    ratePlan,
    checkInDate,
    checkOutDate,
    groupName,
    rooms,
    contractHolderId,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
