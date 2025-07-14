# UserModel


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**id** | **string** |  | [default to undefined]
**firstName** | **string** |  | [default to undefined]
**lastName** | **string** |  | [optional] [default to undefined]
**emailAddress** | **string** |  | [default to undefined]
**homePhone** | **string** |  | [optional] [default to undefined]
**mobilePhone** | **string** |  | [optional] [default to undefined]
**photoUrl** | **string** |  | [optional] [default to undefined]
**address** | **string** |  | [optional] [default to undefined]
**dateOfBirth** | **string** |  | [optional] [default to undefined]
**occupation** | **string** |  | [optional] [default to undefined]
**emergencyContactName** | **string** |  | [optional] [default to undefined]
**emergencyContactPhone** | **string** |  | [optional] [default to undefined]
**membershipStatus** | [**MembershipStatusModel**](MembershipStatusModel.md) |  | [default to undefined]
**membershipStartDate** | **string** |  | [optional] [default to undefined]
**membershipEndDate** | **string** |  | [optional] [default to undefined]
**roles** | **Array&lt;string&gt;** |  | [default to undefined]

## Example

```typescript
import { UserModel } from './api';

const instance: UserModel = {
    id,
    firstName,
    lastName,
    emailAddress,
    homePhone,
    mobilePhone,
    photoUrl,
    address,
    dateOfBirth,
    occupation,
    emergencyContactName,
    emergencyContactPhone,
    membershipStatus,
    membershipStartDate,
    membershipEndDate,
    roles,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
