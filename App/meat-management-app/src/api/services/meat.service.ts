import http from "../http";
import { Meat } from "../../types";

export const getAllMeats = async (): Promise<Meat[]> => {
  const response = await http.get<Meat[]>("/carnes");
  return response.data;
};

export const getMeatById = async (id: string): Promise<Meat> => {
  const response = await http.get<Meat>(`/carnes/${id}`);
  return response.data;
};

export const createMeat = async (meat: Meat): Promise<Meat> => {
  const response = await http.post<Meat>("/carnes", meat);
  return response.data;
};

export const updateMeat = async (id: string, meat: Partial<Meat>): Promise<Meat> => {
  const response = await http.put<Meat>(`/carnes/${id}`, meat);
  return response.data;
};

export const deleteMeat = async (id: string): Promise<void> => {
  await http.delete(`/carnes/${id}`);
};
