using System;
using System.IO;
using System.Threading.Tasks;

namespace WorkingWithImages
{
    public interface ISaveChangesService
    {
        Task<bool> SaveBitmap(byte[] bitmapData, string filename);
    }
}
