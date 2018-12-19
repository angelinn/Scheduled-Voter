using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Voter.Core
{
    public static class Extensions
    {
        public static unsafe T Decrypt<T>(this SecureString secureString, Func<string, T> action)
        {
            IntPtr insecureStringPointer = IntPtr.Zero;
            string insecureString = String.Empty;
            GCHandle gcHandle = GCHandle.Alloc(insecureString, GCHandleType.Pinned);

            try
            {
                insecureStringPointer = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                insecureString = Marshal.PtrToStringUni(insecureStringPointer);

                return action(insecureString);
            }
            finally
            {
                fixed(char* ptr = insecureString)
                {
                    for (int i = 0; i < insecureString.Length; ++i)
                        ptr[i] = '\0';

                    insecureString = null;
                    gcHandle.Free();
                    Marshal.ZeroFreeGlobalAllocUnicode(insecureStringPointer);
                }
            }
        }
    }
}
