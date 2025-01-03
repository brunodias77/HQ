namespace HQ.Domain.Services;

public interface IGenerateQrCodeService
{
    byte[] GenerateQRCode(string code);
}