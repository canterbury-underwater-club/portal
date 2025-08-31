import {
  BookingsAdminBookingsModelsBookingModel,
  BookingsMineModelsBookingModel,
  BookingsModelsBookingAttendeeModel,
  BookingsModelsBookingFeeBasisModel,
  BookingsModelsBookingRateModel,
  NullableOfBookingAgeBracketModel,
  NullableOfBookingAttendeeTypeModel,
} from '@/api/generated/v1'
import { uuidToColor } from '@/utils/colorUtils'
import { fromServerDate } from '@/utils/dateConverter'
import { BookingCalendarEvent } from './dateTypes'

export interface AttendeeCharge {
  id: string
  attendeeName: string
  unitPriceCents: number // 0 if no matching rate
  matchedRate?: {
    unitPriceCents: number
    reason: 'contractMatch' | 'exactAttendeeMatch'
  }
}

export interface FeeCharge {
  name: string
  basis: BookingsModelsBookingFeeBasisModel
  quantity: number
  unitPriceCents: number
  totalCents: number
}

export interface BookingPriceBreakdown {
  planId?: string
  planName: string
  planEffectiveFrom: string
  usedContractHolderId?: string | null
  attendeeCharges: AttendeeCharge[]
  feeCharges: FeeCharge[]
  subtotalRatesCents: number
  subtotalFeesCents: number
  totalCents: number
  warnings: string[]
}

type AnyBooking = BookingsAdminBookingsModelsBookingModel | BookingsMineModelsBookingModel

export class Booking {
  private readonly _booking: AnyBooking

  constructor(args: { booking: AnyBooking }) {
    this._booking = args.booking
  }

  get id(): string {
    return this._booking.id
  }
  get checkInDate(): Date {
    return fromServerDate(this._booking.checkInDate)
  }
  get checkOutDate(): Date {
    return fromServerDate(this._booking.checkOutDate)
  }
  get groupName(): string | null | undefined {
    return this._booking.groupName
  }
  get rooms(): number[] {
    return this._booking.rooms ?? []
  }
  get contractHolderId(): string | null | undefined {
    return this._booking.contractHolderId ?? null
  }
  get attendees(): BookingsModelsBookingAttendeeModel[] {
    return this._booking.attendees ?? []
  }
  get title(): string {
    return this._booking.groupName || `Booking ${this._booking.id.slice(0, 6)}`
  }

  calculatePrice(): BookingPriceBreakdown {
    const warnings: string[] = []

    if (!this._booking.ratePlan.rates || !this._booking.ratePlan.fees) {
      warnings.push('Rate plan missing rates and/or fees. All charges will be zero.')
      return this.emptyBreakdown(warnings)
    }

    // Soft warning if plan begins after check-in
    if (Booking.isDateAfter(this._booking.ratePlan.effectiveFrom, this._booking.checkInDate)) {
      warnings.push(
        `Rate plan "${this._booking.ratePlan.name}" (effective ${this._booking.ratePlan.effectiveFrom}) starts after check-in (${this._booking.checkInDate}).`,
      )
    }

    // --- Contract rate uniqueness enforcement ---
    const chId = (this.contractHolderId ?? '').toLowerCase()
    let contractRate: BookingsModelsBookingRateModel | undefined
    if (chId) {
      const matches = this._booking.ratePlan.rates.filter(
        (r) => (r.contractHolderId ?? '').toLowerCase() === chId,
      )
      if (matches.length > 1) {
        // Client-side error: server should guarantee uniqueness
        const details = matches.map((r) => `${r.contractHolderId}:${r.unitPriceCents}`).join(', ')
        throw new Error(
          `Invariant violation: multiple BookingRates found for ContractHolderId=${this.contractHolderId}. ` +
            `Server should ensure uniqueness. Matches: [${details}]`,
        )
      }
      contractRate = matches[0]
    }

    // --- Build attendee charges ---
    const attendeeCharges: AttendeeCharge[] = this.attendees.map((a) => {
      // Rule A: If contract rate exists, use it for ALL attendees
      if (contractRate) {
        return {
          id: a.id,
          attendeeName: `${a.firstName} ${a.lastName ?? ''}`.trim(),
          unitPriceCents: contractRate.unitPriceCents,
          matchedRate: { unitPriceCents: contractRate.unitPriceCents, reason: 'contractMatch' },
        }
      }

      // Rule B: exact attendeeType + ageBracket match
      const exact = this._booking.ratePlan.rates.find((r) =>
        Booking.exactAttendeeMatch(r, a.attendeeType ?? null, a.ageBracket ?? null),
      )

      if (exact) {
        return {
          id: a.id,
          attendeeName: `${a.firstName} ${a.lastName ?? ''}`.trim(),
          unitPriceCents: exact.unitPriceCents,
          matchedRate: { unitPriceCents: exact.unitPriceCents, reason: 'exactAttendeeMatch' },
        }
      }

      warnings.push(`No rate matched attendee "${a.firstName} ${a.lastName ?? ''}". Using 0.`)
      return {
        id: a.id,
        attendeeName: `${a.firstName} ${a.lastName ?? ''}`.trim(),
        unitPriceCents: 0,
      }
    })

    const subtotalRatesCents = attendeeCharges.reduce((s, x) => s + x.unitPriceCents, 0)

    // --- Fees
    const feeCharges: FeeCharge[] = (this._booking.ratePlan.fees ?? []).map((f) => {
      const qty =
        f.basis === BookingsModelsBookingFeeBasisModel.PerAttendeePerBooking
          ? this.attendees.length
          : 1
      return {
        name: f.name,
        basis: f.basis,
        quantity: qty,
        unitPriceCents: f.unitPriceCents,
        totalCents: qty * f.unitPriceCents,
      }
    })

    const subtotalFeesCents = feeCharges.reduce((s, f) => s + f.totalCents, 0)
    const totalCents = subtotalRatesCents + subtotalFeesCents

    return {
      planId: this._booking.ratePlan.id,
      planName: this._booking.ratePlan.name,
      planEffectiveFrom: this._booking.ratePlan.effectiveFrom,
      usedContractHolderId: this.contractHolderId ?? null,
      attendeeCharges,
      feeCharges,
      subtotalRatesCents,
      subtotalFeesCents,
      totalCents,
      warnings,
    }
  }

  toCalendarEvents(): BookingCalendarEvent[] {
    const events: BookingCalendarEvent[] = []

    const cur = new Date(this.checkInDate)
    const end = this.checkOutDate
    const color = uuidToColor(this.id)

    while (cur <= end) {
      const day = new Date(cur)
      events.push({
        id: this.id,
        title: this.title,
        start: day,
        end: day,
        color,
        booking: this,
      })
      cur.setDate(cur.getDate() + 1)
    }

    return events
  }

  private static exactAttendeeMatch(
    rate: BookingsModelsBookingRateModel,
    attendeeType: NullableOfBookingAttendeeTypeModel | null,
    ageBracket: NullableOfBookingAgeBracketModel | null,
  ): boolean {
    if (attendeeType === null || ageBracket === null) return false
    return (rate.attendeeType ?? null) === attendeeType && (rate.ageBracket ?? null) === ageBracket
  }

  private static isDateAfter(aISO: string, bISO: string): boolean {
    const aNZ = fromServerDate(aISO)
    const bNZ = fromServerDate(bISO)
    return aNZ.getTime() > bNZ.getTime()
  }

  private emptyBreakdown(warnings: string[]): BookingPriceBreakdown {
    return {
      planId: this._booking.ratePlan.id,
      planName: this._booking.ratePlan.name,
      planEffectiveFrom: this._booking.ratePlan.effectiveFrom,
      usedContractHolderId: this.contractHolderId,
      attendeeCharges: [],
      feeCharges: [],
      subtotalRatesCents: 0,
      subtotalFeesCents: 0,
      totalCents: 0,
      warnings,
    }
  }
}

export const BookingFromAdmin = (booking: BookingsAdminBookingsModelsBookingModel) =>
  new Booking({ booking })

export const BookingFromMine = (booking: BookingsMineModelsBookingModel) => new Booking({ booking })
