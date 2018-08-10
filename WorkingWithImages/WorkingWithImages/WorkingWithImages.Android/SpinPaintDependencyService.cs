﻿using System.Threading.Tasks;

using Android.Media;
using Android.OS;
using Java.IO;

using Xamarin.Forms;

// Requires android.permission.WRITE_EXTERNAL_STORAGE in AndroidManifest.xml

[assembly: Dependency(typeof(WorkingWithImages.Droid.SpinPaintDependencyService))]

namespace WorkingWithImages.Droid
{
    public class SpinPaintDependencyService : ISaveChangesService
    {
        public async Task<bool> SaveBitmap(byte[] buffer, string filename)
        {
            try
            {
                File picturesDirectory = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
                File spinPaintDirectory = new File(picturesDirectory, "SpinPaint");
                spinPaintDirectory.Mkdirs();

                using (File bitmapFile = new File(spinPaintDirectory, filename))
                {
                    bitmapFile.CreateNewFile();

                    using (FileOutputStream outputStream = new FileOutputStream(bitmapFile))
                    {
                        await outputStream.WriteAsync(buffer);
                    }

                    // Make sure it shows up in the Photos gallery promptly.
                    MediaScannerConnection.ScanFile(MainActivity.Instance,
                                                    new string[] { bitmapFile.Path },
                                                    new string[] { "image/png", "image/jpeg" }, null);
                }
            }
            catch(System.Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}