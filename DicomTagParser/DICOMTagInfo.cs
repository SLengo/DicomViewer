using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicomTagParser
{
    class DICOMTagInfo
    {
        public DICOMTagInfo(string theTag, string theTagName, string theVR)
        {
            Tag = theTag;
            TagName = theTagName;
            VR = theVR;
        }

        public string Tag { get; set; }
        public string TagName { get; set; }
        public string VR { get; set; }
    }
}
