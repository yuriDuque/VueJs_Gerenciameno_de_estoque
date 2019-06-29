import axios from "axios";

export const http = axios.create({
  baseURL: "http://localhost:51393/api"
});
