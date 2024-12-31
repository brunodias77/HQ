namespace HQ.Application.Dtos.Donations.Responses;

public class ResponseGeneratePixDonation
{
    public string PixCode { get; set; } // Código Pix gerado
    public byte[] QrCodeBase64 { get; set; } // Imagem do QR Code em Base64
}