﻿using SMMSender.DTO;
using SMMSender.Constants;
using SMMSender.Utils.WindowsApi;
using System.Diagnostics;

namespace SMMSender.Processors
{
    internal class DesktopAppProcessorBase
    {

        protected void Run(ProcessDto dto)
        {
            Process
                .GetProcessesByName(dto.ProcessName)
                .ToList()
                .ForEach(p => p.Close()); 

            Thread.Sleep(2000);

            var process = new Process();
            process.StartInfo.FileName = dto.FilePath;
            process.StartInfo.UseShellExecute = true;
            process.Start();

            Thread.Sleep(5000);
        }


        protected void InitHandler(WindowClass windowClass, out IntPtr windowHandler)
        {
            windowHandler = WinApi.FindWindowA(windowClass.Name, null);

            if (windowHandler == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Not found program with class name \"{windowClass.Name}\"");
            }
        }

    }
}
