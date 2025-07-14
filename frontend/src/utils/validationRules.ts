import {
  MAX_ADDRESS_LENGTH,
  MAX_NAME_LENGTH,
  MAX_OCCUPATION_LENGTH,
  MAX_PHONE_LENGTH,
} from '@/constants/validation'
import validator from 'validator'

export const emailRules = [
  (value: string | null | undefined) => {
    return (value && !validator.isEmpty(value)) || 'Email address is required.'
  },
  (value: string | null | undefined) => {
    return (value && validator.isEmail(value)) || 'Email address must be valid.'
  },
]

export const nameRules = [
  (value: string | null | undefined) =>
    (value ?? '').length <= MAX_NAME_LENGTH ||
    `Name cannot be longer than ${MAX_NAME_LENGTH} characters.`,
]

export const firstNameRules = [
  (value: string | null | undefined) => {
    return (value && !validator.isEmpty(value)) || 'First name is required.'
  },
  ...nameRules,
]

export const phoneRules = [
  (value: string | null | undefined) =>
    (value ?? '').length <= MAX_PHONE_LENGTH ||
    `Phone cannot be longer than ${MAX_PHONE_LENGTH} characters.`,
]

export const addressRules = [
  (value: string | null | undefined) =>
    (value ?? '').length <= MAX_ADDRESS_LENGTH ||
    `Address cannot be longer than ${MAX_ADDRESS_LENGTH} characters.`,
]

export const occupationRules = [
  (value: string | null | undefined) =>
    (value ?? '').length <= MAX_OCCUPATION_LENGTH ||
    `Occupation cannot be longer than ${MAX_OCCUPATION_LENGTH} characters.`,
]

export const emergencyContactNameRules = [
  (value: string | null | undefined) =>
    (value ?? '').length <= MAX_NAME_LENGTH * 2 ||
    `Name cannot be longer than ${MAX_NAME_LENGTH * 2} characters.`,
]
