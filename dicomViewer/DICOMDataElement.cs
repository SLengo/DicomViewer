using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    class DICOMDataElement
    {
        public UInt16 GroupNumber { get; set; }
        public UInt16 ElementNumber { get; set; }
        public string Tag { get; set; }
        public string TagName { get; set; }
        public string VR { get; set; }
        public Int64 VM { get; set; }
        public Int64 ValueLength { get; set; }
        public string ValueField { get; set; }
        public Int64 StreamPosition { get; set; }

        public DICOMDataElement()
        {
            GroupNumber = 0;
            ElementNumber = 0;
            Tag = "";
            TagName = "";
            VR = "";
            VM = 0;
            ValueLength = -1;
            ValueField = "";
            StreamPosition = 0;
        }
    }
}
