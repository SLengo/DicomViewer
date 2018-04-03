using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    public class readVoxelData
    {
        public vtk.vtkImageData VoxelData = new vtk.vtkImageData();
        public int[] dimensions = new int[3] { 0, 0, 0 };
        public double[] spacing = new double[3];
        public double[] metaScalarRange = new double[2] { 0, 0 };
        public string info = "";

        public void ReadVTKV3D(string filename)
        {
            readv3dinfo V3Dfileinfo = new readv3dinfo();
            V3Dfileinfo.getv3dinfo(filename);

            vtk.vtkImageReader V3Dreader = new vtk.vtkImageReader();
            //V3Dreader.RemoveAllInputs();
            // all the slices are in one file
            V3Dreader.SetFileDimensionality(3);

            // image is greyscale
            V3Dreader.SetNumberOfScalarComponents(1);

            // 2 bytes per voxel
            V3Dreader.SetDataScalarTypeToUnsignedShort();

            // volume dimension sizes
            int maxx = V3Dfileinfo.v3dsizes[0] - 1;
            int maxy = V3Dfileinfo.v3dsizes[1] - 1;
            int maxz = V3Dfileinfo.v3dsizes[2] - 1;

            V3Dreader.SetDataExtent(0, maxx, 0, maxy, 0, maxz);

            //voxel spacing
            spacing = V3Dfileinfo.v3dscales;
            V3Dreader.SetDataSpacing(spacing);

            // set to zero unless you know
            double[] origing = new double[3] { 0, 0, 0 };
            V3Dreader.SetDataOrigin(origing);

            V3Dreader.SetHeaderSize((uint)V3Dfileinfo.v3doffset);

            // set filename
            V3Dreader.SetFileName(V3Dfileinfo.v3dfilename);

            V3Dreader.Update();

            vtk.vtkImageChangeInformation imageChangeInformation = new vtk.vtkImageChangeInformation();
            imageChangeInformation.SetInput(V3Dreader.GetOutput());
            imageChangeInformation.CenterImageOn(); // Centered the image here....
            imageChangeInformation.Update();

            VoxelData = imageChangeInformation.GetOutput();

            spacing = VoxelData.GetSpacing();
            int numberofpixels = VoxelData.GetNumberOfPoints();
            dimensions = VoxelData.GetDimensions();
            metaScalarRange = VoxelData.GetScalarRange();
            double[] origin = VoxelData.GetOrigin();

            info = "\r\n VTK file read, parameters: \r\n";
            info += "Volume dimensions : " + dimensions[0] + " " + dimensions[1] + " " + dimensions[2] + "\r\n";
            info += "Volume scales : " + spacing[0] + " " + spacing[1] + " " + spacing[2] + "\r\n";
            info += "Volume origin: " + origin[0] + " " + origin[1] + " " + origin[2] + "\r\n";
            info += "Scalar range : " + metaScalarRange[0] + " " + metaScalarRange[1] + "\r\n";
            info += "Number of Pixels : " + numberofpixels + "\r\n";
            GC.Collect();
        }

        public void readVTKDicom(string dirname)
        {
            
            vtk.vtkDICOMImageReader DICOMreader = new vtk.vtkDICOMImageReader();
            DICOMreader.SetDirectoryName(dirname);
            DICOMreader.SetDataOrigin(0, 0, 0);
            DICOMreader.Update();

            vtk.vtkImageChangeInformation imageChangeInformation = new vtk.vtkImageChangeInformation();
            imageChangeInformation.SetInput(DICOMreader.GetOutput());
            imageChangeInformation.CenterImageOn(); // Centered the image here....
            imageChangeInformation.Update();

            VoxelData = imageChangeInformation.GetOutput();

            spacing = VoxelData.GetSpacing();
            int numberofpixels = VoxelData.GetNumberOfPoints();
           
            dimensions = VoxelData.GetDimensions();
            metaScalarRange = VoxelData.GetScalarRange();
            double[] origin = VoxelData.GetOrigin();

            info = "\r\n VTK Dicom dir read, parameters: \r\n";
            info += "Volume dimensions : " + dimensions[0] + " " + dimensions[1] + " " + dimensions[2] + "\r\n";
            info += "Volume scales : " + spacing[0] + " " + spacing[1] + " " + spacing[2] + "\r\n";
            info += "Scalar range : " + metaScalarRange[0] + " " + metaScalarRange[1] + "\r\n";
            info += "Volume origin: " + origin[0] + " " + origin[1] + " " + origin[2] + "\r\n";
            info += "Number of Pixels : " + numberofpixels + "\r\n";
            GC.Collect();
        }
    }
}
