using System;
using System.Text;

namespace FlareApi.Config
{
    public enum DbProvider
    {
        SQLSERVER,
        MySql
    }

    public class Settings
    {
        public DbProvider Provider { get; set; } = DbProvider.MySql;
        public string ConnectionString { get; set; } = string.Empty;

        private byte[] _tokeSecret = Array.Empty<byte>();

        public string TokeSecret
        {
            set => _tokeSecret =Encoding.ASCII.GetBytes(value);
        }

        public long MaxFileBytes { get; set; } = 5_282_912L;

        public byte[] getEncodeSecret()
        {
            return _tokeSecret;
        }
    }
}