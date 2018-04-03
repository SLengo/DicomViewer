using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    class RenderMarchingCubes
    {
        public vtk.vtkMarchingCubes mc = new vtk.vtkMarchingCubes();
        vtk.vtkRenderWindowInteractor iren = new vtk.vtkRenderWindowInteractor();
        vtk.vtkRenderWindow renWin;
        vtk.vtkRenderer aRenderer;
        vtk.vtkImageData VoxelData = new vtk.vtkImageData();
        vtk.vtkPolyDataMapper mcmap = new vtk.vtkPolyDataMapper();
        vtk.vtkActor mcactor = new vtk.vtkActor();

        public string info = "";
        public double isovalue = 0;
        private Form1 m_parent;

        public void setParent(Form1 frm1)
        {
            m_parent = frm1;
        }

        public void setisovalue(double[] metaScalarRange)
        {
            isovalue = 0.8 * (metaScalarRange[1] + metaScalarRange[0]);
        }

        public void SetWindows(vtk.vtkRenderWindow renWint)
        {
            renWin = new vtk.vtkRenderWindow();
            aRenderer = new vtk.vtkRenderer();
            renWin = renWint;
        }

        public void MC_VTK(vtk.vtkImageData VoxelDatat)
        {
            VoxelData = VoxelDatat;
            mc.SetInput(VoxelData);
            mc.SetValue(0, isovalue);
            mc.ComputeGradientsOff();
            mc.ComputeScalarsOff();
            mc.ComputeNormalsOff();
            mc.ComputeNormalsOn();
            mc.Update();
            int marchingNumberOfPoints = mc.GetOutput().GetNumberOfPoints();
            int marchingNumberOfPolys = mc.GetOutput().GetNumberOfPolys();
            info += "marching number of points : " + marchingNumberOfPoints + "\r\n";
            info += "marching number of polys : " + marchingNumberOfPolys + "\r\n";
        }

        public void ShowVTKV3d()
        {
            renWin.AddRenderer(aRenderer);
            iren.SetRenderWindow(renWin);

            vtk.vtkSmoothPolyDataFilter smoother = new vtk.vtkSmoothPolyDataFilter();
            smoother.SetInputConnection(mc.GetOutputPort());
            smoother.SetRelaxationFactor(0.05);
            smoother.SetNumberOfIterations(30);

            vtk.vtkDecimatePro deci = new vtk.vtkDecimatePro();
            deci.SetInputConnection(smoother.GetOutputPort());
            deci.SetTargetReduction(0.9);
            deci.PreserveTopologyOn();
            deci.Update();

            int marchingNumberOfPoints = deci.GetOutput().GetNumberOfPoints();
            int marchingNumberOfPolys = deci.GetOutput().GetNumberOfPolys();
            info += "marching number of points : " + marchingNumberOfPoints + "\r\n";
            info += "marching number of polys : " + marchingNumberOfPolys + "\r\n";

            vtk.vtkSmoothPolyDataFilter smoother2 = new vtk.vtkSmoothPolyDataFilter();
            smoother2.SetInputConnection(deci.GetOutputPort());
            smoother2.SetRelaxationFactor(0.05);
            smoother2.SetNumberOfIterations(30);

            mcmap.SetInputConnection(smoother2.GetOutputPort());
            mcmap.ScalarVisibilityOff();
            mcactor.SetMapper(mcmap);
            mcactor.GetProperty().SetColor(1, 1, 1);

            aRenderer.AddActor(mcactor);
            aRenderer.SetBackground(0.0f, 0.0f, 0.0f);
            aRenderer.ResetCamera();
            renWin.Render();
            addBusyObservers();
            iren.Initialize(); iren.Enable();

        }

        public void addBusyObservers()
        {
            iren.AddObserver((uint)vtk.EventIds.LeftButtonPressEvent, new vtk.vtkDotNetCallback(MousePress));
            iren.AddObserver((uint)vtk.EventIds.RightButtonPressEvent, new vtk.vtkDotNetCallback(MousePress));
            iren.AddObserver((uint)vtk.EventIds.MiddleButtonPressEvent, new vtk.vtkDotNetCallback(MousePress));

            iren.AddObserver((uint)vtk.EventIds.LeftButtonReleaseEvent, new vtk.vtkDotNetCallback(MouseRelease));
            iren.AddObserver((uint)vtk.EventIds.MiddleButtonReleaseEvent, new vtk.vtkDotNetCallback(MouseRelease));
            iren.AddObserver((uint)vtk.EventIds.RightButtonReleaseEvent, new vtk.vtkDotNetCallback(MouseRelease));
        }

        public void MousePress(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata) { m_parent.ShowBusy(true); }
        public void MouseRelease(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata) { m_parent.ShowBusy(false); }


        public void UpdateIsoValue(double isovaluet)
        {
            isovalue = isovaluet;
            mc.SetValue(0, isovalue);
            mc.Update();
            renWin.Render();
        }

        void writepolyfile()
        {
            vtk.vtkPolyDataWriter polyDataWriter = new vtk.vtkPolyDataWriter();
            polyDataWriter.SetInputConnection(mc.GetOutputPort());
            string outputFileName = "c:/pietje";
            polyDataWriter.SetFileName(outputFileName);
            polyDataWriter.SetFileTypeToBinary();
            polyDataWriter.Write();
        }

        public void deleterender()
        {
            EmptyVTK eVolume = new EmptyVTK();
            isovalue = 0.5;
            MC_VTK(eVolume.GetEmptyVolume());
            mcmap.SetInputConnection(mc.GetOutputPort());
            renWin.Render();
            iren.Disable();
        }
    }
}
