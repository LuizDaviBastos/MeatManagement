import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;
const http = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  }
});

http.interceptors.response.use(
  (response) => response,
  (error) => {
    if (!error.response) {
      return Promise.reject({ message: "Erro de conexão" });
    }

    const { status, data } = error.response;
    let message = "Ocorreu um erro";

    switch (status) {
      case 404:
        message = "Recurso não encontrado";
        break;
      case 409:
        message = data?.message || "Conflito de dados";
        break;
      default:
        message = data?.message || error.message;
    }
    message = Array.isArray(data) ? data.join('\n') : data;
    return Promise.reject({ status, message });
  }
);
export default http;