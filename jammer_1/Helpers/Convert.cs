using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jammer_1.Helpers
{
    public static class Convertme
    {
        public static byte[] ToByteArray(string resource)
        {
            var assembly = typeof(Convertme).GetTypeInfo().Assembly;
            byte[] buffer = null;

            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var length = stream.Length;
                    buffer = new byte[length];
                    stream.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }
    }
}
