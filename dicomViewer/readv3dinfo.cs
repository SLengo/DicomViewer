using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace dicomViewer
{
    public class readv3dinfo
    {
        public string v3dfilename = "";
        public string v3dformat = "";
        public string v3dversion = "";
        public int v3dfilesize = 0;
        public double[] v3dscales = new double[3] { 1, 1, 1 };
        public int[] v3dsizes = new int[3] { 0, 0, 0 };
        public int v3doffset;
        public int v3dbits;
        public int v3dtime;
        public int v3dpar;
        public string info = "";

        public void getv3dinfo(string filenamedialog)
        {
            if (File.Exists(filenamedialog))
            {
                v3dfilename = filenamedialog;
                FileStream fs = File.OpenRead(v3dfilename);
                BinaryReader br = new BinaryReader(fs);

                v3dfilesize = (int)fs.Length;
                fs.Position = 0;

                byte[] byteArray = new byte[5];
                byteArray = br.ReadBytes(5);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                v3dformat = enc.GetString(byteArray);

                fs.Position = 7;
                byte[] byteArray2 = new byte[3];
                byteArray2 = br.ReadBytes(3);
                v3dversion = enc.GetString(byteArray2);

                fs.Position = 40;
                v3dsizes[0] = br.ReadInt32();
                v3dsizes[1] = br.ReadInt32();
                v3dsizes[2] = br.ReadInt32();

                v3dscales[0] = br.ReadDouble();
                v3dscales[1] = br.ReadDouble();
                v3dscales[2] = br.ReadDouble();

                fs.Position = 100;
                v3dbits = br.ReadInt32();
                if (v3dbits == 0) v3dbits = 16;
                v3dtime = br.ReadInt32();
                v3dpar = br.ReadInt32();

                v3doffset = -(v3dbits / 8) * v3dsizes[0] * v3dsizes[1] * v3dsizes[2] + v3dfilesize;

                br.Close();
                fs.Close();

                info = "File length : " + v3dfilesize + "\r\n";
                info += "File format : " + v3dformat + "\r\n";
                info += "File version : " + v3dversion + "\r\n";
                info += "Volume size : " + v3dsizes[0] + " " + v3dsizes[1] + " " + v3dsizes[2] + "\r\n";
                info += "Volume scales : " + v3dscales[0] + " " + v3dscales[1] + " " + v3dscales[2] + "\r\n";
                info += "Volume bits : " + v3dbits + "\r\n";
                info += "Volume offset : " + v3doffset + "\r\n";
            }
            else
            {
                //"File not Found";
            }
        }
    };
}
