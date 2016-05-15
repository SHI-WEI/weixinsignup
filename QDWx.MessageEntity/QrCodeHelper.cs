using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QDWx.MessageEntity
{
    public class QrCodeHelper
    {
        public static Image GetQrCode(string content)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode(content);

            const int moduleSizeInPixels = 4;
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize
                (moduleSizeInPixels, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            DrawingSize dSize = renderer.SizeCalculator.GetSize(qrCode.Matrix.Width);
            Image bit = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
            using (Graphics graphics = Graphics.FromImage(bit))
            {
                renderer.Draw(graphics, qrCode.Matrix);
            }
            return bit;
        }
    }
}
