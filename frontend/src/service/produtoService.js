import { http } from "./config.js";

export default {
  listarProdutos: () => {
    return http.get("/produtos");
  },

  salvar: produto => {
    return http.post("/produtos", { body: produto });
  }
};
