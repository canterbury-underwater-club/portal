# BookingsModelsBookingRatePlanModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [optional] [default to undefined]
**name** | **string** |  | [default to undefined]
**effectiveFrom** | **string** |  | [default to undefined]
**rates** | [**Array&lt;BookingsModelsBookingRateModel&gt;**](BookingsModelsBookingRateModel.md) |  | [default to undefined]
**fees** | [**Array&lt;BookingsModelsBookingFeeModel&gt;**](BookingsModelsBookingFeeModel.md) |  | [default to undefined]
**isCurrent** | **boolean** |  | [default to undefined]

## Example

```typescript
import { BookingsModelsBookingRatePlanModel } from './api';

const instance: BookingsModelsBookingRatePlanModel = {
    id,
    name,
    effectiveFrom,
    rates,
    fees,
    isCurrent,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
