namespace SocketRequest
{
    public class RequestMetaDataParser : ILineParser
    {
        public bool TryParse(string line, ref HttpRequest requestObject)
        {
            if (IsRequestMetaDataLine(line) == false)
                return false;

            Process(line, ref requestObject);
            return true;
        }

        public void Process(string line, ref HttpRequest requestObject)
        {
            var keyValue = line.Split(':');
            var key = keyValue[0].Trim();
            var value = keyValue[1].Trim();

            requestObject.RequestMetadata[key] = value;
        }
        private bool IsRequestMetaDataLine(string line)
        {
            return string.IsNullOrWhiteSpace(line) == false &&
                   line.Contains(':') == true;
        }
    }
}
