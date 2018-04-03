using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicomTagParser
{
    class PrivateDICOMDictionary
    {
        static private readonly List<PrivateDICOMTagInfo> mPrivateDICOMDictionary = new List<PrivateDICOMTagInfo>
        {
            // In case you want to support private DICOM attribute names, the information has to be provided here.
            // Private attribute information has to be provided in form:
            // "Private Creator Code", "Group Number", "Element ID", "Attribute Name", "VR (Value Representation)"

            // Sample private DICOM attribute:
            { new PrivateDICOMTagInfo("YOUR PRIVATE CREATOR CODE", "300B", "ED", "My private DICOM attribute", "OB") },
        };

        static public List<PrivateDICOMTagInfo> GetPrivateDICOMDictionary()
        {
            return mPrivateDICOMDictionary;
        }
    }
    public class PrivateDICOMTagInfo
    {
        public PrivateDICOMTagInfo(string thePrivateCreatorCode, string theGroupNumber, string theElementID, string theName, string theVR)
        {
            PrivateCreatorCode = thePrivateCreatorCode;
            GroupNumber = theGroupNumber;
            ElementID = theElementID;
            Name = theName;
            VR = theVR;
        }

        public string PrivateCreatorCode { get; set; }
        public string GroupNumber { get; set; }
        public string ElementID { get; set; }
        public string Name { get; set; }
        public string VR { get; set; }
    }
}
