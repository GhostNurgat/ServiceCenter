using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Files
{
    public static class GetBinCode
    {
        public static string GetCode()
        {
            string path = "code.bin";
            string code = string.Empty;

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                code = reader.ReadString();
            }

            return code;
        }
    }
}
