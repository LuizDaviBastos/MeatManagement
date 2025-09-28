import http from "../http";
import { Order } from "../../types";

export const getAllOrders = async (): Promise<Order[]> => {
  const response = await http.get<Order[]>("/pedidos");
  return response.data;
};

export const getOrderById = async (id: string): Promise<Order> => {
  const response = await http.get<Order>(`/pedidos/${id}`);
  return response.data;
};

export const createOrder = async (order: Order): Promise<Order> => {
  const response = await http.post<Order>("/pedidos", order);
  return response.data;
};

export const updateOrder = async (id: string, order: Partial<Order>): Promise<Order> => {
  const response = await http.put<Order>(`/pedidos/${id}`, order);
  return response.data;
};

export const deleteOrder = async (id: string): Promise<void> => {
  await http.delete(`/pedidos/${id}`);
};
