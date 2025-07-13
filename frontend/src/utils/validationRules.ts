import { MAX_NAME_LENGTH, MAX_PHONE_LENGTH } from '@/constants/validation'
import validator from 'validator'

export const emailRules = [
  (value: string) => {
    return !validator.isEmpty(value) || 'Email address is required.'
  },
  (value: string) => {
    return validator.isEmail(value) || 'Email address must be valid.'
  },
]

export const nameRules = [
  (value: string) =>
    value.length <= MAX_NAME_LENGTH || `Name cannot be longer than ${MAX_NAME_LENGTH} characters.`,
]

export const firstNameRules = [
  (value: string) => {
    return !validator.isEmpty(value) || 'First name is required.'
  },
  ...nameRules,
]

export const phoneRules = [
  (value: string) =>
    value.length <= MAX_PHONE_LENGTH ||
    `Phone cannot be longer than ${MAX_PHONE_LENGTH} characters.`,
]
