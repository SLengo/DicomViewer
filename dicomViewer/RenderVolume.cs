using System;
using System.Collections.Generic;
using System.Text;

namespace dicomViewer
{
    class RenderVolume
    {
        vtk.vtkRenderWindow renWin;
        vtk.vtkRenderer ren1;
        vtk.vtkRenderWindowInteractor iren = new vtk.vtkRenderWindowInteractor();
        vtk.vtkImageData VoxelData = new vtk.vtkImageData();
        vtk.vtkVolumeProperty volumeProperty;
        vtk.vtkVolume volume = new vtk.vtkVolume();


        public string info = "";
        int textmode = 0;
        int isomode = 0;
        public double isovalue = 0;
        private Form1 m_parent;

        public void setParent(Form1 frm1)
        {
            m_parent = frm1;
        }


        public void SetWindows(vtk.vtkRenderWindow renWint)
        {
            renWin = new vtk.vtkRenderWindow();
            ren1 = new vtk.vtkRenderer();
            volumeProperty = new vtk.vtkVolumeProperty();
            renWin = renWint;
        }

        public void setAlphaTransferFunction(vtk.vtkPiecewiseFunction TransferFunction)
        {
            volumeProperty.SetScalarOpacity(TransferFunction);
            renWin.Render();
        }

        public void setColorTransferFunction(vtk.vtkColorTransferFunction TransferFunction)
        {
            volumeProperty.SetColor(TransferFunction);
            renWin.Render();
        }

        public void VolumeRender(vtk.vtkImageData VoxelDatat)
        {

            VoxelData = VoxelDatat;
            renWin.AddRenderer(ren1);
            iren.SetRenderWindow(renWin);

            double[] inputRange = VoxelData.GetScalarRange();

            vtk.vtkPiecewiseFunction opacityTransferFunction = new vtk.vtkPiecewiseFunction();
            opacityTransferFunction.AddPoint(inputRange[0], 0);
            opacityTransferFunction.AddPoint(inputRange[1], 1);

            vtk.vtkColorTransferFunction colorTransferFunction = new vtk.vtkColorTransferFunction();
            colorTransferFunction.AddRGBPoint(inputRange[0], 1.0, 1.0, 1.0);
            colorTransferFunction.AddRGBPoint(inputRange[1], 1.0, 1.0, 1.0);

            volumeProperty.SetColor(colorTransferFunction);
            volumeProperty.SetScalarOpacity(opacityTransferFunction);
            volumeProperty.ShadeOff();
            volumeProperty.SetInterpolationTypeToLinear();

            volume.SetProperty(volumeProperty);

            setRAY();
            ren1.AddVolume(volume);
            ren1.ResetCamera();
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


        public void starteventloop()
        {
            iren.Start();
        }

        public void setisovalue(double[] metaScalarRange)
        {
            isovalue = 0.8 * (metaScalarRange[1] + metaScalarRange[0]);
        }

        public void UpdateIsoValue(double isovaluet)
        {
            isovalue = isovaluet;
            vtk.vtkPiecewiseFunction opacityIsoTransferFunction = setIsoTF();
            volumeProperty.SetScalarOpacity(opacityIsoTransferFunction);
            volume.Update();
            renWin.Render();
        }

        public vtk.vtkPiecewiseFunction setIsoTF()
        {
            double[] inputRange = VoxelData.GetScalarRange();
            vtk.vtkPiecewiseFunction opacityIsoTransferFunction = new vtk.vtkPiecewiseFunction();
            double per = ((inputRange[1] - inputRange[0]) / 100);
            if (isovalue > (inputRange[0] + per))
            {
                opacityIsoTransferFunction.AddPoint(inputRange[0], 0);
                opacityIsoTransferFunction.AddPoint(isovalue - (per), 0);
            }
            opacityIsoTransferFunction.AddPoint(isovalue, 1);
            opacityIsoTransferFunction.AddPoint(inputRange[1], 1);
            return opacityIsoTransferFunction;
        }

        public void VolumeRenderISO(vtk.vtkImageData VoxelDatat)
        {
            VoxelData = VoxelDatat;
            renWin.AddRenderer(ren1);
            iren.SetRenderWindow(renWin);

            double[] inputRange = VoxelData.GetScalarRange();

            vtk.vtkPiecewiseFunction opacityIsoTransferFunction = setIsoTF();

            vtk.vtkColorTransferFunction colorTransferFunction = new vtk.vtkColorTransferFunction();
            colorTransferFunction.AddRGBPoint(inputRange[0], 1.0, 1.0, 1.0);
            colorTransferFunction.AddRGBPoint(inputRange[1], 1.0, 1.0, 1.0);

            volumeProperty.SetColor(colorTransferFunction);
            volumeProperty.SetScalarOpacity(opacityIsoTransferFunction);
            volumeProperty.ShadeOn();
            volumeProperty.SetInterpolationTypeToLinear();
           
            volume.SetProperty(volumeProperty);

            renderisomode();

            ren1.AddVolume(volume);
            ren1.ResetCamera();
            renWin.Render();
            addBusyObservers();
            iren.Initialize(); iren.Enable();
        }

        public void renderisomode()
        {
            vtk.vtkFixedPointVolumeRayCastMapper volumeMapper = new vtk.vtkFixedPointVolumeRayCastMapper();
            volumeMapper.SetInput(VoxelData);
            if (isomode == 0) volumeMapper.AutoAdjustSampleDistancesOn();
            if (isomode == 1)
            {
                volumeMapper.AutoAdjustSampleDistancesOff();
                volumeMapper.SetSampleDistance((float)0.3);
            }
            if (isomode == 2)
            {
                volumeMapper.AutoAdjustSampleDistancesOff();
                volumeMapper.SetSampleDistance((float)0.08);
            }

            volumeMapper.Update();
            volume.SetMapper(volumeMapper);
            volume.Update();
        }

        public void setIsomode(int isomodet)
        {
            isomode = isomodet;
            renderisomode();
            renWin.Render();
        }

        public void setMIP()
        {
            vtk.vtkVolumeRayCastMIPFunction compositeFunction = new vtk.vtkVolumeRayCastMIPFunction();
            vtk.vtkVolumeRayCastMapper volumeMapper = new vtk.vtkVolumeRayCastMapper();
            volumeMapper.SetVolumeRayCastFunction(compositeFunction);
            volumeMapper.SetInput(VoxelData);
            volumeMapper.SetSampleDistance((float)0.3);
            volumeMapper.Update();
            volume.SetMapper(volumeMapper);
            volume.Update();
            renWin.Render();
        }

        public void setRAY()
        {
            vtk.vtkFixedPointVolumeRayCastMapper volumeMapper = new vtk.vtkFixedPointVolumeRayCastMapper();
            volumeMapper.SetInput(VoxelData);
            volumeMapper.SetSampleDistance((float)0.3);
            volumeMapper.Update();
            volume.SetMapper(volumeMapper);
            volume.Update();
            renWin.Render();
        }

        public void setTEXT()
        {
            vtk.vtkVolumeTextureMapper3D volumeMapper = new vtk.vtkVolumeTextureMapper3D();
            volumeMapper.SetInput(VoxelData);
            if (textmode == 1) volumeMapper.SetPreferredMethodToNVidia();
            if (textmode == 2) volumeMapper.SetPreferredMethodToFragmentProgram();
            volumeMapper.SetSampleDistance((float)0.3);
            volumeMapper.Update();
            volume.SetMapper(volumeMapper);
            volume.Update();
            renWin.Render();
        }

        public void setTextmode(int buttonid)
        {
            textmode = buttonid;
            setTEXT();
        }

        public void deleterender()
        {
            EmptyVTK eVolume = new EmptyVTK();
            VoxelData = eVolume.GetEmptyVolume(); VoxelData.Update();
            vtk.vtkFixedPointVolumeRayCastMapper volumeMapper = new vtk.vtkFixedPointVolumeRayCastMapper();
            volumeMapper.SetInput(VoxelData); volumeMapper.Update();
            volume.SetMapper(volumeMapper); volume.Update();

            iren.Disable();
        }

    }
}