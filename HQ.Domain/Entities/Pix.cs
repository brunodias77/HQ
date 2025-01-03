namespace HQ.Domain.Entities;

public class Pix
{
    private const string PayloadFormatIndicator = "01";
    private const string GloballyUniqueIdentifier = "BR.GOV.BCB.PIX";
    private const string MerchantCategoryCode = "0000";
    private const string TransactionCurrency = "986";
    private const string CountryCode = "BR";
    public string Key { get; set; } // Chave PIX
    public decimal? Value { get; set; } // Valor da transação
    public string MerchantName { get; set; } // Nome do destinatário ou comerciante
    public string MerchantCity { get; set; } // Cidade do destinatário ou comerciante
    public string TxId { get; set; } 
    
    public string GeneratePixCode()
        {
            string pixCode = "";

            // Payload Format Indicator
            pixCode += $"00{PayloadFormatIndicator.Length:D2}{PayloadFormatIndicator}";

            // Merchant Account Information
            string merchantAccountInfo = $"00{GloballyUniqueIdentifier.Length:D2}{GloballyUniqueIdentifier}";
            if (!string.IsNullOrWhiteSpace(Key))
            {
                merchantAccountInfo += $"01{Key.Length:D2}{Key}";
            }
            pixCode += $"26{merchantAccountInfo.Length:D2}{merchantAccountInfo}";

            // Merchant Category Code
            pixCode += $"52{MerchantCategoryCode.Length:D2}{MerchantCategoryCode}";

            // Transaction Currency
            pixCode += $"53{TransactionCurrency.Length:D2}{TransactionCurrency}";

            // Transaction Amount
            if (Value.HasValue)
            {
                string formattedValue = Value.Value.ToString("F2").Replace(",", ".");
                pixCode += $"54{formattedValue.Length:D2}{formattedValue}";
            }

            // Country Code
            pixCode += $"58{CountryCode.Length:D2}{CountryCode}";

            // Merchant Name
            if (!string.IsNullOrWhiteSpace(MerchantName))
            {
                pixCode += $"59{MerchantName.Length:D2}{MerchantName}";
            }

            // Merchant City
            if (!string.IsNullOrWhiteSpace(MerchantCity))
            {
                pixCode += $"60{MerchantCity.Length:D2}{MerchantCity}";
            }

            // Additional Data Field Template (TxId)
            string additionalDataFieldTemplate = "";
            if (!string.IsNullOrWhiteSpace(TxId))
            {
                additionalDataFieldTemplate += $"05{TxId.Length:D2}{TxId}";
            }
            else
            {
                additionalDataFieldTemplate += "05***";
            }
            pixCode += $"62{additionalDataFieldTemplate.Length:D2}{additionalDataFieldTemplate}";

            // CRC (Checksum)
            pixCode += "6304"; // Placeholder for CRC field
            pixCode += CalculateCrc(pixCode);

            return pixCode;
        }
            
        private string CalculateCrc(string pixCode)
        {
            ushort polynomial = 0x1021;
            ushort crc = 0xFFFF;

            foreach (char c in pixCode)
            {
                crc ^= (ushort)(c << 8);

                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            return crc.ToString("X4").ToUpper();
        }

}