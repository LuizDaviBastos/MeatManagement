import { Buyer } from "./buyer";

export interface OrderItem {
  id?: string;
  currencyCode: string;
  quantityKg: number;
  pricePerKg: number;
  total: number;
  totalBRL: number;
}

export interface Order {
  id?: string;
  buyer: Buyer;
  createdAt?: string;
  items: OrderItem[];
}
