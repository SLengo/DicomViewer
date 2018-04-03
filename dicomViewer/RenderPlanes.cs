using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    class RenderPlanes
    {
        vtk.vtkRenderWindow renWin;
        vtk.vtkImagePlaneWidget planeWidgetX = new vtk.vtkImagePlaneWidget();
        vtk.vtkImagePlaneWidget planeWidgetY = new vtk.vtkImagePlaneWidget();
        vtk.vtkImagePlaneWidget planeWidgetZ = new vtk.vtkImagePlaneWidget();
        vtk.vtkRenderWindowInteractor iren;
        vtk.vtkImageMapToColors sColorsX = new vtk.vtkImageMapToColors();
        vtk.vtkImageMapToColors sColorsY = new vtk.vtkImageMapToColors();
        vtk.vtkImageMapToColors sColorsZ = new vtk.vtkImageMapToColors();
        vtk.vtkRenderer ren2;
        int slizex = 0; int slizey = 0; int slizez = 0;
        public string info = "";
        private Form1 m_parent;

        public void setParent(Form1 frm1)
        {
            m_parent = frm1;
        }

        public void SetWindows(vtk.vtkRenderWindow renWint)
        {
            renWin = new vtk.vtkRenderWindow();

            iren = new vtk.vtkRenderWindowInteractor();
            renWin = renWint;
            ren2 = new vtk.vtkRenderer();
        }

        public void SetParameters(int slizext, int slizeyt, int slizezt)
        {
            slizex = slizext; slizey = slizeyt; slizez = slizezt;
        }

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public void render3Dplanes(vtk.vtkImageData VoxelData)
        {
            renWin.AddRenderer(ren2);

            iren.SetRenderWindow(renWin);

            vtk.vtkOutlineFilter outline = new vtk.vtkOutlineFilter();
            outline.SetInput(VoxelData);

            vtk.vtkPolyDataMapper outlineMapper = new vtk.vtkPolyDataMapper();
            outlineMapper.SetInputConnection(outline.GetOutputPort());

            vtk.vtkActor outlineActor = new vtk.vtkActor();
            outlineActor.SetMapper(outlineMapper);

            vtk.vtkCellPicker picker = new vtk.vtkCellPicker();
            picker.SetTolerance(0.005);

            vtk.vtkColorTransferFunction colorTransferFunction = new vtk.vtkColorTransferFunction();

            double[] inputRange = VoxelData.GetScalarRange();
            colorTransferFunction.AddRGBPoint(inputRange[0], 0.0, 0.0, 0.0);
            colorTransferFunction.AddRGBPoint(inputRange[1], 1.0, 1.0, 1.0);

            sColorsX.SetLookupTable(colorTransferFunction);
            sColorsY.SetLookupTable(colorTransferFunction);
            sColorsZ.SetLookupTable(colorTransferFunction);

            planeWidgetX.SetColorMap(sColorsX);
            planeWidgetX.SetInteractor(iren);
            planeWidgetX.DisplayTextOff();
            planeWidgetX.SetInput(VoxelData);
            planeWidgetX.SetPlaneOrientationToXAxes();
            planeWidgetX.SetSliceIndex(slizex);
            planeWidgetX.SetPicker(picker);
            planeWidgetX.On();
            planeWidgetX.InteractionOff();

            planeWidgetY.SetColorMap(sColorsY);
            planeWidgetY.SetInteractor(iren);
            planeWidgetY.DisplayTextOff();
            planeWidgetY.SetInput(VoxelData);
            planeWidgetY.SetPlaneOrientationToYAxes();
            planeWidgetY.SetSliceIndex(slizey);
            planeWidgetY.SetPicker(picker);
            planeWidgetY.On();
            planeWidgetY.InteractionOff();


            planeWidgetZ.SetColorMap(sColorsZ);
            planeWidgetZ.SetInteractor(iren);
            planeWidgetZ.DisplayTextOff();
            planeWidgetZ.SetInput(VoxelData);
            planeWidgetZ.SetPlaneOrientationToZAxes();
            planeWidgetZ.SetSliceIndex(slizez);
            planeWidgetZ.SetPicker(picker);
            planeWidgetZ.On();
            planeWidgetZ.InteractionOff();
            ren2.ResetCamera();
            renWin.Render();
            iren.Initialize(); iren.Enable();
        }

        public void setColorTransferFunction(vtk.vtkColorTransferFunction TransferFunction)
        {
            sColorsX.SetLookupTable(TransferFunction);
            sColorsY.SetLookupTable(TransferFunction);
            sColorsZ.SetLookupTable(TransferFunction);
            renWin.Render();
        }

        public void updateX(int slizext)
        {
            slizex = slizext;
            planeWidgetX.SetSliceIndex(slizex); renWin.Render();
        }

        public void updateY(int slizeyt)
        {
            slizey = slizeyt;
            planeWidgetY.SetSliceIndex(slizey); renWin.Render();
        }

        public void updateZ(int slizezt)
        {
            slizez = slizezt;
            planeWidgetZ.SetSliceIndex(slizez); renWin.Render();
        }

        public void deleterender()
        {
            EmptyVTK eVolume = new EmptyVTK();
            vtk.vtkImageData VoxelData = eVolume.GetEmptyVolume();
            planeWidgetX.SetInput(VoxelData);
            planeWidgetY.SetInput(VoxelData);
            planeWidgetZ.SetInput(VoxelData);
            planeWidgetX.Off();
            planeWidgetY.Off();
            planeWidgetZ.Off();
            renWin.Render();
        }
    }
}
