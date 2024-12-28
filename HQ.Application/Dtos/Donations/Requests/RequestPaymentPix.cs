namespace HQ.Application.Dtos.Donations.Requests;

public class RequestPaymentPix
{
    public string Key { get; set; } // Chave PIX
    public decimal? Value { get; set; } // Valor da transação
    public string MerchantName { get; set; } // Nome do destinatário ou comerciante
    public string MerchantCity { get; set; } // Cidade do destinatário ou comerciante
    public string TxId { get; set; } // Identificador único da transação (opcional)
}