﻿// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services: http://www.microsoft.com/cognitive
// 
// Microsoft Cognitive Services Github:
// https://github.com/Microsoft/Cognitive
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using FaceAPI = Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using VisionAPI = Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace LiveCameraSample
{
    // Class to hold all possible result types. 
    public class LiveCameraResult
    {
        public FaceAPI.DetectedFace[] Faces { get; set; } = null;
        public string[] CelebrityNames { get; set; } = null;
        public VisionAPI.ImageTag[] Tags { get; set; } = null;
        public ImageCaption[] Captions { get; set; } = null;
        public List<DenseCaptionOverride> DenseCaptions { get; set; } = null;

    }
    public class DenseCaptionOverride
    {
        public string denseCaptionOriginal { get; set; } = null;
        public string denseCaptionTranslate { get; set; } = null;
        public float confidenceLevel { get; set; }

        public DenseCaptionOverride(string denseCaptionOriginal1, string denseCaptionTranslate1, float confidenceLevel1)
        {
            denseCaptionOriginal = denseCaptionOriginal1;
            denseCaptionTranslate = denseCaptionTranslate1;
            confidenceLevel = confidenceLevel1;
        }

    }
}
