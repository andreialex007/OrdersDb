using System;

namespace OrdersDb.Domain.Wrappers
{
    public static class File
    {
        public static Action<string, byte[]> WriteAllBytesDelegate = null;

        public static void WriteAllBytes(string path, byte[] bytes)
        {
            if (WriteAllBytesDelegate != null)
            {
                WriteAllBytesDelegate(path, bytes);
                return;
            }

            System.IO.File.WriteAllBytes(path, bytes);
        }


        public static Func<string, byte[]> ReadAllBytesDelegate = null;

        public static byte[] ReadAllBytes(string path)
        {
            if (ReadAllBytesDelegate != null)
            {
                return ReadAllBytesDelegate(path);
            }

            return !System.IO.File.Exists(path) ? null : System.IO.File.ReadAllBytes(path);
        }

        public static Func<string, bool> ExistsDelegate = null;

        public static bool Exists(string path)
        {
            if (ReadAllBytesDelegate != null)
            {
                return ExistsDelegate(path);
            }

            return System.IO.File.Exists(path);
        }
    }
}
