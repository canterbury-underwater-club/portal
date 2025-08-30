/** Remove undefined so PATCH only sends intended fields */
// eslint-disable-next-line @typescript-eslint/no-explicit-any
export function sanitizePatch<T extends Record<string, any>>(patch: T): Partial<T> {
  return Object.fromEntries(Object.entries(patch).filter(([, v]) => v !== undefined)) as Partial<T>
}
