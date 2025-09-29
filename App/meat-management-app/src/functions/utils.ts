export function formatCpfCnpj(value: string): string {
    if (!value) return "";
    
    const digits = value.replace(/\D/g, ""); 
    if (digits.length <= 11) {
      return digits.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
    } else {
      return digits.replace(
        /(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/,
        "$1.$2.$3/$4-$5"
      );
    }
  }

  export const formatCurrency = (value: number) =>
    new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(value);

  export function getMeatOrigins() {
    return [
      {id: 0, name: "Bovina"},
      {id: 1, name: "Suina"},
      {id: 2, name: "Aves"},
      {id: 3, name: "Peixes"}
    ] 
  }

  export function getCurrencies() {
    return [
      { code: "BRL", name: "Real Brasileiro" },
      { code: "EUR", name: "Euro" },
      { code: "USD", name: "Dólar" },
    ];
  }

  export function formatOrigin(value: number): string {
    const origins = getMeatOrigins();
    const origin = origins.find(o => o.id === value);
    return origin ? origin.name : "Desconhecido";
  }

  export function getCurrencySymbol(currencyCode: string, locale: string = "pt-BR"): string {
    if(!currencyCode) return "";
    
    return new Intl.NumberFormat(locale, {
      style: "currency",
      currency: currencyCode,
      currencyDisplay: "narrowSymbol", // usa "R$", "$", "€"
    })
      .formatToParts(0)
      .find(part => part.type === "currency")?.value || "";
  }