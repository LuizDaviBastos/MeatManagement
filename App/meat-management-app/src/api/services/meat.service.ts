import http from "../http";
import { Meat } from "../types";

export const getAll = async (): Promise<Meat[]> => {
    return [<Meat>{name: 'test'}];
  const response = await http.get<Meat[]>("/values");
  return response.data;
};

export const getbyId = async (id: number): Promise<Meat> => {
  const response = await http.get<Meat>(`/values/${id}`);
  return response.data;
};

export const create = async (value: Meat): Promise<Meat> => {
  const response = await http.post<Meat, Meat>("/values", value);
  return response;
};

export const update = async (id: number, value: Partial<Meat>): Promise<Meat> => {
  const response = await http.put<Partial<Meat>, Meat>(`/values/${id}`, value);
  return response;
};

export const remove = async (id: number): Promise<void> => {
  await http.delete(`/values/${id}`);
};

