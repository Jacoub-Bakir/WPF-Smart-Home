using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace RoomManagement.Services
{
	public class ImageService
	{
		//private static readonly string ExecutingAssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		public static BitmapImage GetImage(byte[] image, Size imageSize)
		{
			try
			{
				
				Bitmap bitmap;
				using (var stream = new MemoryStream(image))
				{
					bitmap = new Bitmap(stream);
					bitmap = new Bitmap(bitmap, imageSize);
				}

				return Convert(bitmap);
			}
			catch
			{
				return null;
			}
		}

		private static BitmapImage Convert(object value)
		{
			if (value != null && value is Image image)
			{
				var memoryStream = new MemoryStream();
				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
				memoryStream.Seek(0, SeekOrigin.Begin);
				bitmap.StreamSource = memoryStream;
				bitmap.EndInit();

				bitmap.Freeze();

				return bitmap;
			}

			return null;
		}
	}
}
