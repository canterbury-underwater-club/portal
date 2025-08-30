# BookingsModelsBookingRateModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**rateType** | [**BookingsModelsBookingRateTypeModel**](BookingsModelsBookingRateTypeModel.md) |  | [default to undefined]
**contractHolderId** | **string** |  | [optional] [default to undefined]
**attendeeType** | [**NullableOfBookingAttendeeTypeModel**](NullableOfBookingAttendeeTypeModel.md) |  | [optional] [default to undefined]
**ageBracket** | [**NullableOfBookingAgeBracketModel**](NullableOfBookingAgeBracketModel.md) |  | [optional] [default to undefined]
**unitPriceCents** | **number** |  | [default to undefined]

## Example

```typescript
import { BookingsModelsBookingRateModel } from './api';

const instance: BookingsModelsBookingRateModel = {
    rateType,
    contractHolderId,
    attendeeType,
    ageBracket,
    unitPriceCents,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
