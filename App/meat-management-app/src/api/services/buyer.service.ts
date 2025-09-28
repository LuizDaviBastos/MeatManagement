import http from "../http";
import { Buyer } from "../../types";

export const getAllBuyers = async (): Promise<Buyer[]> => {
  const response = await http.get<Buyer[]>("/compradores");
  return response.data;
};

export const getBuyerById = async (id: string): Promise<Buyer> => {
  const response = await http.get<Buyer>(`/compradores/${id}`);
  return response.data;
};

export const createBuyer = async (buyer: Buyer): Promise<Buyer> => {
  const response = await http.post<Buyer>("/compradores", buyer);
  return response.data;
};

export const updateBuyer = async (id: string, buyer: Partial<Buyer>): Promise<Buyer> => {
  const response = await http.put<Buyer>(`/compradores/${id}`, buyer);
  return response.data;
};

export const deleteBuyer = async (id: string): Promise<void> => {
  await http.delete(`/compradores/${id}`);
};
