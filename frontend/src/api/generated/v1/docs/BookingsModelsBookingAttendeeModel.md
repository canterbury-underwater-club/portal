# BookingsModelsBookingAttendeeModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**firstName** | **string** |  | [default to undefined]
**lastName** | **string** |  | [default to undefined]
**attendeeType** | [**NullableOfBookingAttendeeTypeModel**](NullableOfBookingAttendeeTypeModel.md) |  | [optional] [default to undefined]
**ageBracket** | [**NullableOfBookingAgeBracketModel**](NullableOfBookingAgeBracketModel.md) |  | [optional] [default to undefined]
**membershipNumber** | **number** |  | [optional] [default to undefined]

## Example

```typescript
import { BookingsModelsBookingAttendeeModel } from './api';

const instance: BookingsModelsBookingAttendeeModel = {
    firstName,
    lastName,
    attendeeType,
    ageBracket,
    membershipNumber,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
