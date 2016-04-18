using System;

namespace FitBitPortable.Exceptions
{
    public class FitBitApiGenericException : Exception { }

    public class FitBitApiOAuth2Exception : FitBitApiGenericException { }

    public class FitBitApiDeviceDataException : FitBitApiGenericException { }

    public class FitBitApiUserProfileException : FitBitApiGenericException { }

    public class FitBitApiActivityDailySummaryException : FitBitApiGenericException { }

    public class FitBitApiHeartRateTimeSeriesException : FitBitApiGenericException { }

    public class FitBitApiHeartRateIntradayTimeSeriesException : FitBitApiGenericException { }
}
