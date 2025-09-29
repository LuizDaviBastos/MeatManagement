import http from "../http";
import { State, City } from "../../types";

export const getAllStates = async (): Promise<State[]> => {
  const response = await http.get<State[]>("/estados");
  return response.data;
};

export const getCitiesByState = async (stateId: string): Promise<City[]> => {
  const response = await http.get<City[]>(`/estados/${stateId}/cidades`);
  return response.data;
};
