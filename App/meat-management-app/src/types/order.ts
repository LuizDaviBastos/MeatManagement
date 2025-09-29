import { Buyer } from "./buyer";

export interface OrderItem {
  id?: string;
  meatId: string;
  currencyCode: string;
  price: number;
  priceBRL?: number;
}

export interface Order {
  id?: string;
  buyer?: Buyer;
  total: number;
  totalBRL?: number;
  orderDate?: Date;
  items: OrderItem[];
}
