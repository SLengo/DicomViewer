using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace dicomViewer
{
    class EmptyVTK
    {
        public vtk.vtkImageData GetEmptyVolume()
        {
            // Create a pointer to a small array
            int[] managedArray = { 1, 1, 1, 1, 1, 1, 1, 1 };
            IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(managedArray[0]) * managedArray.Length);
            Marshal.Copy(managedArray, 0, pnt, managedArray.Length);

            vtk.vtkImageData emptyVolume = new vtk.vtkImageData();
            // Use the small array as data of the empty Volume
            vtk.vtkImageImport importer = new vtk.vtkImageImport();
            importer.SetWholeExtent(0, 1, 0, 1, 0, 1);
            importer.SetDataExtentToWholeExtent();
            importer.SetDataScalarTypeToUnsignedShort();
            importer.SetImportVoidPointer(pnt);
            emptyVolume = importer.GetOutput();
            return emptyVolume;
        }
    }
}
