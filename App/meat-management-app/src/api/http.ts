import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:7211",
  headers: {
    "Content-Type": "application/json",
  },
});

export default http;