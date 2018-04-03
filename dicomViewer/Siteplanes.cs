using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    public class Siteplanes
    {
        vtk.vtkRenderWindow renwX = new vtk.vtkRenderWindow();
        vtk.vtkRenderWindow renwY = new vtk.vtkRenderWindow();
        vtk.vtkRenderWindow renwZ = new vtk.vtkRenderWindow();

        public vtk.vtkImagePlaneWidget planeWidgetX = new vtk.vtkImagePlaneWidget();
        public vtk.vtkImagePlaneWidget planeWidgetY = new vtk.vtkImagePlaneWidget();
        public vtk.vtkImagePlaneWidget planeWidgetZ = new vtk.vtkImagePlaneWidget();

        vtk.vtkRenderWindowInteractor irenX = new vtk.vtkRenderWindowInteractor();
        vtk.vtkRenderWindowInteractor irenY = new vtk.vtkRenderWindowInteractor();
        vtk.vtkRenderWindowInteractor irenZ = new vtk.vtkRenderWindowInteractor();

        vtk.vtkRenderer renX = new vtk.vtkRenderer();
        vtk.vtkRenderer renY = new vtk.vtkRenderer();
        vtk.vtkRenderer renZ = new vtk.vtkRenderer();

        int slizex = 0; int slizey = 0; int slizez = 0;
        public string info = "";
        public Boolean enabled = false;
        int firstime = 0;

        public double windowX = 0;
        public double levelX = 0;

        public double windowY = 0;
        public double levelY = 0;

        public double windowZ = 0;
        public double levelZ = 0;

        public double rangeXMin = 0;
        public double rangeXMax = 0;

        public double rangeYMin = 0;
        public double rangeYMax = 0;

        public double rangeZMin = 0;
        public double rangeZMax = 0;

        bool firstTime = true;


        public void SetWindows(vtk.vtkRenderWindow renwXt, vtk.vtkRenderWindow renwYt, vtk.vtkRenderWindow renwZt)
        {
            renwX = renwXt; renwY = renwYt; renwZ = renwZt;
        }


        public void SetParameters(int slizext, int slizeyt, int slizezt)
        {
            slizex = slizext; slizey = slizeyt; slizez = slizezt;
        }


        public void rendersiteplanes(vtk.vtkImageData VoxelData)
        {
            //vtk.vtkWin32RenderWindowInteractor

            renwX.AddRenderer(renX);
            renwY.AddRenderer(renY);
            renwZ.AddRenderer(renZ);

            irenX.SetRenderWindow(renwX);
            irenY.SetRenderWindow(renwY);
            irenZ.SetRenderWindow(renwZ);


            vtk.vtkOutlineFilter outline = new vtk.vtkOutlineFilter();
            outline.SetInput(VoxelData);

            vtk.vtkPolyDataMapper outlineMapper = new vtk.vtkPolyDataMapper();
            outlineMapper.SetInputConnection(outline.GetOutputPort());

            vtk.vtkActor outlineActor = new vtk.vtkActor();
            outlineActor.SetMapper(outlineMapper);

            vtk.vtkCellPicker picker = new vtk.vtkCellPicker();
            picker.SetTolerance(0.005);

            
            planeWidgetX.SetInteractor(irenX);
            planeWidgetX.DisplayTextOff();
            planeWidgetX.SetInput(VoxelData);
            planeWidgetX.SetPlaneOrientationToXAxes();
            planeWidgetX.SetSliceIndex(slizex);
            planeWidgetX.SetPicker(picker);
            planeWidgetX.On();
            planeWidgetX.InteractionOff();

            if (firstTime)
            {
                double[] wl = new double[2];
                vtk.vtkLookupTable lktb = new vtk.vtkLookupTable();
                lktb = planeWidgetX.GetLookupTable();
                wl = lktb.GetTableRange();
                rangeXMin = wl[0];
                rangeXMax = wl[1];

                planeWidgetX.GetWindowLevel(wl);
                windowX = wl[0];
                levelX = wl[1]; 
            }


            planeWidgetY.SetInteractor(irenY);
            planeWidgetY.DisplayTextOff();
            planeWidgetY.SetInput(VoxelData);
            planeWidgetY.SetPlaneOrientationToYAxes();
            planeWidgetY.SetSliceIndex(slizey);
            planeWidgetY.SetPicker(picker);
            planeWidgetY.On();
            planeWidgetY.InteractionOff();

            if (firstTime)
            {
                double[] wl = new double[2];
                vtk.vtkLookupTable lktb = new vtk.vtkLookupTable();
                lktb = planeWidgetY.GetLookupTable();
                wl = lktb.GetTableRange();
                rangeYMin = wl[0];
                rangeYMax = wl[1];

                planeWidgetY.GetWindowLevel(wl);
                windowY = wl[0];
                levelY = wl[1];
            }


            planeWidgetZ.SetInteractor(irenZ);
            planeWidgetZ.DisplayTextOff();
            planeWidgetZ.SetInput(VoxelData);
            planeWidgetZ.SetPlaneOrientationToZAxes();
            planeWidgetZ.SetSliceIndex(slizez);
            planeWidgetZ.SetPicker(picker);
            planeWidgetZ.On();
            planeWidgetZ.InteractionOff();

            if (firstTime)
            {
                double[] wl = new double[2];
                vtk.vtkLookupTable lktb = new vtk.vtkLookupTable();
                lktb = planeWidgetZ.GetLookupTable();
                wl = lktb.GetTableRange();
                rangeZMin = wl[0];
                rangeZMax = wl[1];

                planeWidgetZ.GetWindowLevel(wl);
                windowZ = wl[0];
                levelZ = wl[1];
            }

            firstTime = false;
            enabled = true;

            if (firstime == 1)
            {
                renX.GetActiveCamera().Roll(90);
                renX.GetActiveCamera().Azimuth(90);

                renY.GetActiveCamera().Elevation(-90);
                renX.GetActiveCamera().ParallelProjectionOff();
                renY.GetActiveCamera().ParallelProjectionOff();
                renZ.GetActiveCamera().ParallelProjectionOff();
            }

            renX.ResetCamera();
            renY.ResetCamera();
            renZ.ResetCamera();

            renX.GetActiveCamera().ParallelProjectionOn();
            renY.GetActiveCamera().ParallelProjectionOn();
            renZ.GetActiveCamera().ParallelProjectionOn();

            renX.GetActiveCamera().Azimuth(-90);
            renX.GetActiveCamera().Roll(-90);

            renY.GetActiveCamera().Elevation(90);

            firstime = 1;
            int[] dimensions = VoxelData.GetDimensions();
            double[] spacing = VoxelData.GetSpacing();
            double xmax = (double)dimensions[0] * spacing[0] * 0.5;
            double ymax = (double)dimensions[1] * spacing[1] * 0.5;
            double zmax = (double)dimensions[2] * spacing[2] * 0.5;

            double smax = xmax;
            if (ymax > smax) smax = ymax;
            if (zmax > smax) smax = zmax;

            renX.GetActiveCamera().SetParallelScale(smax);
            renY.GetActiveCamera().SetParallelScale(smax);
            renZ.GetActiveCamera().SetParallelScale(smax);

            renX.GetActiveCamera().SetClippingRange(0.1, 1000);
            renY.GetActiveCamera().SetClippingRange(0.1, 1000);
            renZ.GetActiveCamera().SetClippingRange(0.1, 1000);

            irenX.Initialize();
            irenY.Initialize();
            irenZ.Initialize();

            irenX.Disable();
            irenY.Disable();
            irenZ.Disable();

            renwX.Render();
            renwY.Render();
            renwZ.Render();
        }


        public void updateXBtightness(double level)
        {
            enabled = true;
            levelX = level;
            planeWidgetX.SetWindowLevel(windowX, level);
            renwX.Render();
        }
        public void updateXContrast(double window)
        {
            enabled = true;
            windowX = window;
            planeWidgetX.SetWindowLevel(window, levelX);
            renwX.Render();
        }

        public void updateYBtightness(double level)
        {
            enabled = true;
            levelY = level;
            planeWidgetY.SetWindowLevel(windowY, level);
            renwY.Render();
        }
        public void updateYContrast(double window)
        {
            enabled = true;
            windowY = window;
            planeWidgetY.SetWindowLevel(window, levelY);
            renwY.Render();
        }

        public void updateZBtightness(double level)
        {
            enabled = true;
            levelZ = level;
            planeWidgetZ.SetWindowLevel(windowZ, level);
            renwZ.Render();
        }
        public void updateZContrast(double window)
        {
            enabled = true;
            windowZ = window;
            planeWidgetZ.SetWindowLevel(window, levelZ);
            renwZ.Render();
        }


        public void updateRangeTableWigdet(int whichDimension)
        {
            switch (whichDimension)
            {
                case 1:
                    {
                        enabled = true;
                        vtk.vtkLookupTable lkt = new vtk.vtkLookupTable();
                        lkt.SetTableRange(new double[] { -100, 400 });
                        planeWidgetX.SetLookupTable(lkt);
                        renwX.Render();
                        break;
                    }
                case 2:
                    {
                        enabled = true;
                        vtk.vtkLookupTable lkt = new vtk.vtkLookupTable();
                        lkt.SetTableRange(new double[] { -100, 400 });
                        planeWidgetY.SetLookupTable(lkt);
                        renwY.Render();
                        break;
                    }
                case 3:
                    {
                        enabled = true;
                        vtk.vtkLookupTable lkt = new vtk.vtkLookupTable();
                        lkt.SetTableRange(new double[] {-100, 400 });
                        planeWidgetZ.SetLookupTable(lkt);
                        renwZ.Render();
                        break;
                    }
            }
        }

        public void updateX(int slizext)
        {
            enabled = true;
            slizex = slizext;
            planeWidgetX.SetSliceIndex(slizex);
            planeWidgetX.SetWindowLevel(windowX, levelX);
            renwX.Render();
        }

        public void updateY(int slizeyt)
        {
            enabled = true;
            slizey = slizeyt;
            planeWidgetY.SetSliceIndex(slizey);
            planeWidgetY.SetWindowLevel(windowY, levelY);
            renwY.Render();
        }

        public void updateZ(int slizezt)
        {
            enabled = true;
            slizez = slizezt;
            planeWidgetZ.SetSliceIndex(slizez);
            planeWidgetZ.SetWindowLevel(windowZ, levelZ);
            renwZ.Render();
        }

        public void deleterender()
        {
            if (enabled)
            {
                renwX.RemoveRenderer(renX);
                renwY.RemoveRenderer(renY);
                renwZ.RemoveRenderer(renZ);

                enabled = false;
            }
        }
    }
}
