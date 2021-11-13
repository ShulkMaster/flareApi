using System;
using System.Text;

namespace FlareApi.Config
{
    public class Settings
    {
        public string ConnectionString { get; set; } = string.Empty;

        private byte[] _tokeSecret = Array.Empty<byte>();

        public string TokenSecret
        {
            set => _tokeSecret = Encoding.ASCII.GetBytes(value);
            get => string.Empty;
        }

        public long MaxFileBytes { get; set; } = 5_282_912L;

        public byte[] GetEncodeSecret()
        {
            return _tokeSecret;
        }
    }
}