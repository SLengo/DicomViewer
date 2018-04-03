using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicomTagParser
{
    class PrivateCodeDictionary
    {
        Dictionary<string, DICOMTagInfo> mPrivateCodeDictionary = new Dictionary<string, DICOMTagInfo>();

        public void LoadPrivateCreatorCode(string theGroupNumber, string theCodeValue, string thePrivateCreatorCode)
        {
            var aPrivateCodeQuery = from entry in PrivateDICOMDictionary.GetPrivateDICOMDictionary()
                                    where entry.PrivateCreatorCode.Equals(thePrivateCreatorCode)
                                    where entry.GroupNumber.Equals(theGroupNumber)
                                    select entry;

            foreach (PrivateDICOMTagInfo aPrivateCode in aPrivateCodeQuery)
            {
                // Tag is of format '(300B,1012)'
                string aTag = string.Format("({0},{1}{2})", aPrivateCode.GroupNumber, theCodeValue, aPrivateCode.ElementID);

                mPrivateCodeDictionary[aTag] = new DICOMTagInfo(aTag, aPrivateCode.Name, aPrivateCode.VR);
            }
        }

        public void ClearPrivateCreatorCode()
        {
            mPrivateCodeDictionary.Clear();
        }

        public string GetVR(string theTagString)
        {
            // If Tag is not known, return VR value 'UN' (Unknown). 
            return mPrivateCodeDictionary.ContainsKey(theTagString) ? mPrivateCodeDictionary[theTagString].VR : "UN";
        }

        public string GetTagName(string theTagString)
        {
            // If Tag is not know, return TagName 'Not in Dictionary'. 
            return mPrivateCodeDictionary.ContainsKey(theTagString) ? mPrivateCodeDictionary[theTagString].TagName : "Not in Dictionary";
        }
    }
}
