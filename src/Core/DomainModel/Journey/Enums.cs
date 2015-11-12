namespace Core.DomainModel
{

    public enum EntryType
    {
        FacePhoto = 1,
        GpsCoordinates = 2,
        Passport = 3,
        IdentityCard = 4,
        PaperDocument = 5,
        LivenessDetection = 6,
        VideoChat = 7,
        Hologram = 8,
        Fingerprint = 9,
    }



    public enum MediaType
    {
        White = 1,
        UltraViolet = 2,
        Infrared = 3,
        RFID = 5,
        FacePhoto = 7,
        VideoStream = 9,
        Footage = 11,
        FingerPrint = 15,
    }



    public enum JourneyState
    {
        Initialized = 1,
        Started = 2,
        UnderProcess = 3,
        Finished = 4,
        Canceled = 5,
    }



    public enum JourneyStateTrigger
    {
        Start = 1,
        Process = 2,
        Finish = 3,
        Cancel = 4,
    }
}