export function uuidToColor(uuid: string): string {
  let hash = 0
  for (let i = 0; i < uuid.length; i++) {
    hash = uuid.charCodeAt(i) + ((hash << 5) - hash)
    hash |= 0 // force 32-bit
  }
  const hue = Math.abs(hash) % 360
  return `hsl(${hue}, 60%, 50%)` // saturation=60%, lightness=50%
}
