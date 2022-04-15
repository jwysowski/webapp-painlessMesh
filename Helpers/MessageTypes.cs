using MessageType;

namespace MessageTypes
{
    public class MessageTypesLUT
    {
        private Dictionary<string, char> _types;

        public MessageTypesLUT()
        {
            _types = new Dictionary<string, char>();
            _types.Add(new MsgType(true, false, false).GetString(), '2');   // "Set temperature to auto"
            _types.Add(new MsgType(true, true, true).GetString(), '3');  // "Set temperature rising"
            _types.Add(new MsgType(true, true, false).GetString(), '4');  // "Set temperature falling"
            _types.Add(new MsgType(false, false, false).GetString(), '5');  // "Set humidity to auto"
            _types.Add(new MsgType(false, true, true).GetString(), '6');  // "Set humidity rising"
            _types.Add(new MsgType(false, true, false).GetString(), '7');  // "Set humidity falling"
        }

        public char GetMessageType(string key)
        {
            return _types[key];
        }
    }
}
