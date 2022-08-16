using System;

namespace NINA.InstructionMath.Extensions {
    public static class DateTimeExtensions {
        public static double ToTimestamp(this DateTime dateTime) {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
    }
}
