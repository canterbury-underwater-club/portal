# BookingsAdminBookingsModelsBookingModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**createdAt** | **string** |  | [default to undefined]
**attendees** | [**Array&lt;BookingsModelsBookingAttendeeModel&gt;**](BookingsModelsBookingAttendeeModel.md) |  | [optional] [default to undefined]
**ratePlan** | [**BookingsModelsBookingRatePlanModel**](BookingsModelsBookingRatePlanModel.md) |  | [default to undefined]
**contractHolder** | [**BookingsAdminBookingContractHoldersModelsBookingContractHolderModel**](BookingsAdminBookingContractHoldersModelsBookingContractHolderModel.md) |  | [optional] [default to undefined]
**checkInDate** | **string** |  | [default to undefined]
**checkOutDate** | **string** |  | [default to undefined]
**primaryContactId** | **string** |  | [default to undefined]
**bookingStatus** | [**BookingsModelsBookingStatusModel**](BookingsModelsBookingStatusModel.md) |  | [default to undefined]
**bondStatus** | [**BookingsModelsBookingBondStatusModel**](BookingsModelsBookingBondStatusModel.md) |  | [default to undefined]
**groupName** | **string** |  | [optional] [default to undefined]
**rooms** | **Array&lt;number&gt;** |  | [optional] [default to undefined]
**contractHolderId** | **string** |  | [optional] [default to undefined]

## Example

```typescript
import { BookingsAdminBookingsModelsBookingModel } from './api';

const instance: BookingsAdminBookingsModelsBookingModel = {
    id,
    createdAt,
    attendees,
    ratePlan,
    contractHolder,
    checkInDate,
    checkOutDate,
    primaryContactId,
    bookingStatus,
    bondStatus,
    groupName,
    rooms,
    contractHolderId,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
