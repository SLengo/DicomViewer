using System;
using System.Collections.Generic;
using System.Text;

/* **** Histogram Transfer Function Selection *****
 * 
 * Code which I use to initialize and create the histogram window: 
 * - HistogramObj.SetWindows(vtkFormsWindowControlHistogram.GetRenderWindow());
 * - HistogramObj.renderHistogram(V3Dfile.VoxelData);
 * - ConsoleText(HistogramObj.info);
 * - HistogramObj.starteventloop();
 * 
 * Code which I use to get the transfer function for the Volume rendering:
 * - RenderVolumeObj.setTransferFunction(HistogramObj.getTransferFunction());
 * 
 */
namespace dicomViewer
{
    class Histogram
    {
        vtk.vtkRenderWindow renHist = new vtk.vtkRenderWindow();
        vtk.vtkRenderer ren1 = new vtk.vtkRenderer();
        vtk.vtkImageCanvasSource2D canvas = new vtk.vtkImageCanvasSource2D();
        vtk.vtkRenderWindowInteractor iren = new vtk.vtkRenderWindowInteractor();
        vtk.vtkImageData data = new vtk.vtkImageData();

        int width = 0; int height = 0; // Width and height of window
        int numBins = 0; // Number of histogram bins
        int npoints = 0; // Number of Selected Points
        int[] pointsx = new int[50]; // Selected Points x coordinates
        int[] pointsy = new int[50]; // Selected Points y coordinates
        int[] pointsc = new int[50]; // Selected color;
        int pointsel = -1; // Point mouse select
        double rad = 5; // Size (radius) of selected points
        double[] inputRange; // Minimum and Maximum value input data 
        double[,] colortable = new double[14, 3] { { 0.00, 0.00, 0.00 }, { 1.00, 1.00, 1.00 }, { 1.00, 0.00, 0.00 }, { 1.00, 1.00, 0.00 }, { 0.00, 1.00, 0.00 }, { 0.00, 1.00, 1.00 }, { 0.00, 0.00, 1.00 }, { 1.00, 0.00, 1.00 }, { 0.50, 0.00, 0.00 }, { 0.50, 0.50, 0.00 }, { 0.00, 0.50, 0.00 }, { 0.00, 0.50, 0.50 }, { 0.00, 0.00, 0.50 }, { 0.50, 0.00, 0.50 } };

        public string info = ""; // Text for debug purposes

        // Function: Get render window from main class
        public void SetWindows(vtk.vtkRenderWindow renHistt)
        {
            renHist = renHistt;
        }

        // Function: Calculate en render Histogram in window
        public void renderHistogram(vtk.vtkImageData VoxelData)
        {
            // Get size of histogram window
            int[] size = renHist.GetSize();
            width = size[0]; height = size[1];


            //Setup parameters histogram Accumulate Filter
            numBins = width / 2;
            inputRange = VoxelData.GetScalarRange();
            double origin = inputRange[0];
            double spacing = 1.0 * (inputRange[1] - origin) / numBins;

            // Setup Accumulate filter
            vtk.vtkImageAccumulate accumulate = new vtk.vtkImageAccumulate();
            accumulate.SetInput(VoxelData); //Get input voxeldata 
            accumulate.SetComponentExtent(0, numBins - 1, 0, 0, 0, 0);
            accumulate.SetComponentOrigin(origin, 0.0, 0.0);
            accumulate.SetComponentSpacing(spacing, 1.0, 1.0);

            // Make Data object from Accumulated object 
            data = accumulate.GetOutput();
            data.Update();

            // Initialize first and last point of the transferfunction.
            npoints = 2;
            pointsx[0] = 0; pointsy[0] = 0;
            pointsx[1] = width - 1; pointsy[1] = height - 1;
            pointsc[0] = 1; pointsc[1] = 1;

            // Draw the histogram in a canvas and add the transferfunction to it 
            drawhisto();
            drawcurve();

            // Create a color table 
            vtk.vtkColorTransferFunction colorTransferFunction = new vtk.vtkColorTransferFunction();
            for (int i = 0; i < 14; i++)
            {
                colorTransferFunction.AddRGBPoint(i, colortable[i, 0], colortable[i, 1], colortable[i, 2]);
            }




            // Use the color table to color the histogram window objects
            vtk.vtkImageMapToColors sColors = new vtk.vtkImageMapToColors();
            sColors.SetInputConnection(canvas.GetOutputPort());
            sColors.SetLookupTable(colorTransferFunction);

            // Convert the histogram image to a histogram imagemap
            vtk.vtkImageMapper histmap = new vtk.vtkImageMapper();
            histmap.SetInputConnection(sColors.GetOutputPort());
            histmap.RenderToRectangleOn();
            histmap.SetColorWindow(256);
            histmap.SetColorLevel(127);

            // Create the actor needed to draw the histogram image
            vtk.vtkActor2D imageactor = new vtk.vtkActor2D();
            imageactor.SetMapper(histmap);
            imageactor.SetPosition(0, 0); // Fit the histogram to the window
            imageactor.SetPosition2(1, 1);

            // Add the Histogram Image Render to the window object
            ren1.AddActor(imageactor);
            ren1.SetViewport(0, 0, 1, 1); // Fit the histogram to the window
            renHist.AddRenderer(ren1);

            // Add a Window (mouse) interactor to the window
            iren.SetRenderWindow(renHist);

            // Add mouse events to the window
            iren.RemoveObserver((uint)vtk.EventIds.LeftButtonPressEvent);
            iren.AddObserver((uint)vtk.EventIds.LeftButtonPressEvent, new vtk.vtkDotNetCallback(LeftButtonPress));

            iren.RemoveObserver((uint)vtk.EventIds.LeftButtonReleaseEvent);
            iren.AddObserver((uint)vtk.EventIds.LeftButtonReleaseEvent, new vtk.vtkDotNetCallback(LeftButtonRelease));

            iren.RemoveObserver((uint)vtk.EventIds.MouseMoveEvent);
            iren.AddObserver((uint)vtk.EventIds.MouseMoveEvent, new vtk.vtkDotNetCallback(MouseMove));

            iren.RemoveObserver((uint)vtk.EventIds.RightButtonPressEvent);
            iren.AddObserver((uint)vtk.EventIds.RightButtonPressEvent, new vtk.vtkDotNetCallback(RightButtonPress));

            iren.RemoveObserver((uint)vtk.EventIds.MiddleButtonPressEvent);
            iren.AddObserver((uint)vtk.EventIds.MiddleButtonPressEvent, new vtk.vtkDotNetCallback(MiddleButtonPress));

            // Set the (display) update rate of the histogram
            iren.SetStillUpdateRate(5);
            iren.SetDesiredUpdateRate(5);

            // Initialize interactor and render histogram
            iren.Initialize();
            renHist.Render();

            // Debug output
            info = "Histogram is created \r\n";
        }

        // This Function converts the selected Points to a VTK transfer function
        public vtk.vtkPiecewiseFunction getAlphaTransferFunction()
        {
            vtk.vtkPiecewiseFunction TransferFunction = new vtk.vtkPiecewiseFunction();
            double colorindex = 0; double alpha = 0; double per = 0;
            for (int i = 0; i < npoints; i++)
            {
                // Calulate color index and alpha from selected points
                per = (double)pointsx[i] / (width - 1);
                colorindex = (1 - per) * inputRange[0] + per * inputRange[1];
                alpha = (double)pointsy[i] / height;
                TransferFunction.AddPoint(colorindex, alpha);
            }
            return TransferFunction;
        }

        public vtk.vtkColorTransferFunction getColorTransferFunction(bool enablealpha)
        {

            vtk.vtkColorTransferFunction TransferFunction = new vtk.vtkColorTransferFunction();
            double colorindex = 0; double alpha = 1; double per = 0;

            for (int i = 0; i < npoints; i++)
            {
                // Calulate color index and alpha from selected points
                per = (double)pointsx[i] / (width - 1);
                colorindex = (1 - per) * inputRange[0] + per * inputRange[1];
                if (enablealpha) alpha = (double)pointsy[i] / height;
                TransferFunction.AddRGBPoint(colorindex, alpha * colortable[pointsc[i], 0], alpha * colortable[pointsc[i], 1], alpha * colortable[pointsc[i], 2]);

            }
            return TransferFunction;
        }

        // Function to Start the Event Interactor Loop 
        public void starteventloop()
        {
            iren.Start();
        }

        public void deleterender()
        {
            iren.Disable();
        }

        // Function moves a selected point during mouse movement
        public void MouseMove(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata)
        {
            if (pointsel > -1) // Point selected
            {
                // Get x,y mouse position
                int[] pos = iren.GetEventPosition();
                int x = pos[0]; int y = pos[1];

                // Limit mouse position to histogram window
                if (x < 0) x = 0; else if (x > width - 1) x = width - 1;
                if (y < 0) y = 0; else if (y > height - 1) y = height - 1;

                // Set the x variable of the selected point to the mouse x
                // (only if it's not the first or last point)
                if ((pointsel > 0) && (pointsel < npoints - 1))
                {
                    if (x <= pointsx[pointsel - 1]) x = pointsx[pointsel - 1] + 1;
                    if (x >= pointsx[pointsel + 1]) x = pointsx[pointsel + 1] - 1;
                    pointsx[pointsel] = x;
                }

                // Set the y variable of the selected point
                pointsy[pointsel] = y;

                // Draw and Render the histogram and transferfunction
                drawhisto(); drawcurve();
                renHist.Render();
            }
        }

        // This function will find or create a selectedpoint
        // on the position of the left button click
        public void LeftButtonPress(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata)
        {
            // Get x,y mouse position
            int[] pos = iren.GetEventPosition();
            int x = pos[0]; int y = pos[1];

            // Calculate the distance from mouse position to the points
            // a point near the mousecursor will be selected
            double dist = 0; pointsel = -1;
            for (int i = 0; i < npoints; i++)
            {
                dist = Math.Sqrt(Math.Pow((double)(pointsx[i] - x), 2) + Math.Pow((double)(pointsy[i] - y), 2));
                if (dist < rad) pointsel = i;
            }

            // If no point beneath the mouse cursor then this code will 
            // create a point. (if the mouse is close to the curve)
            if (pointsel == -1)
            {
                double mindistv = 999; int mindistx = 0; int mindisty = 0;
                int[] linevector = makevector(); // Get an array with all curve pixels

                // Find pixel of the curve near the mouse position. 
                for (int i = 0; i < width; i++)
                {
                    dist = Math.Sqrt(Math.Pow((double)(i - x), 2) + Math.Pow((double)(linevector[i] - y), 2));
                    if (dist < mindistv)
                    {
                        mindistv = dist;
                        mindistx = i;
                        mindisty = linevector[i];
                    }
                }

                // Add a new point (if mouse is position is near the curve)
                if (mindistv < rad)
                {
                    int dp = npoints - 1;
                    // Make room for the new point in the array
                    while (mindistx < pointsx[dp])
                    {
                        pointsx[dp + 1] = pointsx[dp];
                        pointsy[dp + 1] = pointsy[dp];
                        pointsc[dp + 1] = pointsc[dp];
                        dp--;
                    }
                    pointsx[dp + 1] = mindistx;
                    pointsy[dp + 1] = mindisty;
                    pointsc[dp + 1] = 1;
                    pointsel = dp + 1;
                    npoints++;

                    // Draw and Render the histogram and transferfunction
                    drawhisto(); drawcurve();
                    renHist.Render();
                }
            }
        }

        public void MiddleButtonPress(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata)
        {
            // Get x,y mouse position
            int[] pos = iren.GetEventPosition();
            int x = pos[0]; int y = pos[1];

            // Find the nearest point
            double dist = 0; pointsel = -1;
            for (int i = 0; i < npoints; i++)
            {
                dist = Math.Sqrt(Math.Pow((double)(pointsx[i] - x), 2) + Math.Pow((double)(pointsy[i] - y), 2));
                if (dist < rad) pointsel = i;
            }

            // When point is found
            if (pointsel > -1)
            {
                pointsc[pointsel]++; if (pointsc[pointsel] > 13) pointsc[pointsel] = 1;
                drawhisto(); drawcurve();
                renHist.Render();
            }
            pointsel = -1;
        }

        // Function will delete the point nearby on right mouse press 
        public void RightButtonPress(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata)
        {
            // Get x,y mouse position
            int[] pos = iren.GetEventPosition();
            int x = pos[0]; int y = pos[1];

            // Find the nearest point
            double dist = 0; pointsel = -1;
            for (int i = 1; i < npoints - 1; i++)
            {
                dist = Math.Sqrt(Math.Pow((double)(pointsx[i] - x), 2) + Math.Pow((double)(pointsy[i] - y), 2));
                if (dist < rad) pointsel = i;
            }

            // When point is found, delete it.
            if (pointsel > 0)
            {
                npoints--;
                for (int i = pointsel; i < npoints; i++)
                {
                    pointsx[i] = pointsx[i + 1];
                    pointsy[i] = pointsy[i + 1];
                    pointsc[i] = pointsc[i + 1];
                }

                // Draw and Render the histogram and transferfunction
                drawhisto(); drawcurve();
                renHist.Render();
            }
            pointsel = -1;
        }

        // Create a vector with all line pixels (heights)
        // (needed for new point creation)
        int[] makevector()
        {
            int[] linevector = new int[width];
            int dp = 0; double perc = 0; int y = 0;

            for (int i = 0; i < width; i++)
            {
                if (i > pointsx[dp + 1]) dp++;
                perc = ((double)(i - pointsx[dp])) / ((double)(pointsx[dp + 1] - pointsx[dp]));
                y = (int)((double)pointsy[dp] * (1 - perc) + (double)pointsy[dp + 1] * (perc));
                linevector[i] = y;
            }
            return linevector;
        }

        // Deselect point add mouse release
        public void LeftButtonRelease(vtk.vtkObject obj, uint eventId, Object data, IntPtr clientdata)
        {
            pointsel = -1;
        }

        void drawhisto()
        {
            // Initialize the canvas
            canvas.SetExtent(0, width - 1, 0, height - 1, 0, 0);
            canvas.SetDrawColor(0);
            canvas.FillBox(0, width - 1, 0, height - 1);
            canvas.SetDrawColor(2);

            // Scale the histogram max to fit the window
            double[] histRange = data.GetPointData().GetScalars().GetRange();
            double scale = 0.9 * height / Math.Log(histRange[1]);

            // Draw all histogram bins
            for (int idx = 0; idx < numBins; idx++)
            {
                double y = Math.Floor(Math.Log(data.GetScalarComponentAsDouble(idx, 0, 0, 0) + 1) * scale);
                double x = (idx * 2);
                canvas.DrawSegment((int)x, (int)0, (int)x, (int)y);
            }
        }

        // This function draws the lines and point circles in the histogram image
        void drawcurve()
        {
            for (int i = 0; i < npoints; i++)
            {
                if (i < npoints - 1)
                {
                    canvas.SetDrawColor(1);
                    canvas.DrawSegment(pointsx[i], pointsy[i], pointsx[i + 1], pointsy[i + 1]);
                }
                canvas.SetDrawColor(pointsc[i]);
                for (int j = 0; j < 9; j++)
                {
                    canvas.DrawCircle(pointsx[i], pointsy[i], (double)j * (rad / 10));
                }
                canvas.SetDrawColor(1);
                canvas.DrawCircle(pointsx[i], pointsy[i], (double)rad);

            }
        }
    }
}
