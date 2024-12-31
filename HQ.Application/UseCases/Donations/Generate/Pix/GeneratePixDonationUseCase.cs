using HQ.Application.Abstractions;
using HQ.Application.Dtos.Donations.Requests;
using HQ.Application.Dtos.Donations.Responses;
using QRCoder;

namespace HQ.Application.UseCases.Donations.Generate.Pix;

public class GeneratePixDonationUseCase : IUseCase<RequestGeneratePixDonation, ResponseGeneratePixDonation>
{
    public async Task<ResponseGeneratePixDonation> Execute(RequestGeneratePixDonation request)
    {
        var pix = new Domain.Entities.Pix
        {
            Key = request.Key,
            Value = request.Value,
            MerchantName = request.MerchantName,
            MerchantCity = request.MerchantCity,
            TxId = request.TxId
        };
        string pixCode = pix.GeneratePixCode();
        var response = new ResponseGeneratePixDonation()
        {
            PixCode = pixCode,
            QrCodeBase64 = this.GenerateQRCode(pixCode)
        };
        return response;
    }
    
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