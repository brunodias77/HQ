using HQ.Domain.Services;
using QRCoder;
namespace HQ.Application.Services;

public class GenerateQrCodeService : IGenerateQrCodeService
{
    public byte[] GenerateQRCode(string code)
    {
        QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        using (PngByteQRCode pngQRCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeImage = pngQRCode.GetGraphic(4); 

            return qrCodeImage;
        }    
    }
}