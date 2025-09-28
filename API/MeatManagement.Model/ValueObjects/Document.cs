using MeatManager.Model.Enums;

namespace MeatManager.Model.ValueObjects
{
    public class Document
    {
        public string Value { get; private set; }
        public DocumentType Type { get; private set; }

        public Document(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Documento é obrigatório.");

            value = value.Replace(".", "").Replace("-", "").Replace("/", "");

            if (value.Length == 11)
                Type = DocumentType.CPF;
            else if (value.Length == 14)
                Type = DocumentType.CNPJ;
            else
                throw new ArgumentException("Documento inválido.");

            Value = value;
        }
    }
}
