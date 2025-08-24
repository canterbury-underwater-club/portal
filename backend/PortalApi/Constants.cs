namespace CanterburyUnderwater.PortalApi;

public static class Constants
{
    public static class Validation
    {
        public const int MaxNameLength = 256;
        public const int MaxEmailAddressLength = 254;
        public const int MaxPhoneLength = 32;
        public const int MaxAddressLength = 2048;
        public const int MaxOccupationLength = 128;
        public const int MaxBookingGroupNameLength = 256;
        public const int MinContractNameLength = 2;
        public const int MaxContractNameLength = 256;
        public const int MinBookingRatePlanNameLength = 3;
        public const int MaxBookingRatePlanNameLength = 256;
        public const int MinBookingFeeNameLength = 3;
        public const int MaxBookingFeeNameLength = 256;

        public static readonly int[] RoomNumbers = [1, 2, 3, 4, 5, 6, 11, 12, 13, 14, 15];
    }
}