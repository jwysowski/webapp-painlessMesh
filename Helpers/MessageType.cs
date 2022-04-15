namespace MessageType
{
    public class MsgType
    {
        public bool IsTemperature { get; set; } = false;
        public bool InManualMode { get; set; } = false;
        public bool IsRising { get; set; } = false;

        public MsgType(bool isTemperature, bool inManualMode, bool isRising)
        {
            IsTemperature = isTemperature;
            InManualMode = inManualMode;
            IsRising = isRising;
        }

        public string GetString()
        {
            string mode = InManualMode ? "1" : "0";
            string valueType = IsTemperature ? "0" : "1";
            string isRising = IsRising ? "1" : "0";

            return mode + valueType + isRising;
        }
    }
}