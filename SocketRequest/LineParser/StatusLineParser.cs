﻿using System;

namespace SocketRequest
{
    public class StatusLineParser : ILineParser
    {
        public bool TryParse(string line, ref HttpRequest requestObject)
        {
            if (IsStatusLine(line, ref requestObject) == false)
                return false;

            Process(line, ref requestObject);
            return true;
        }

        public void Process(string line, ref HttpRequest requestObject)
        {
            var statusData = line.Split(' ');

            if (statusData.Length != 3) throw new Exception("Status line format incorrect.");

            requestObject.MethodType = statusData[0].Trim();
            requestObject.Url = statusData[1].Trim();
        }
        private bool IsStatusLine(string line, ref HttpRequest requestObject)
        {
            return string.IsNullOrWhiteSpace(line) == false &&
                   line.Contains(requestObject.HttpVersion) == true;
        }
    }
}
