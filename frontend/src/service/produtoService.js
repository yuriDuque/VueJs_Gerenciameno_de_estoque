import { http } from "./config.js";

export default {
  listarProdutos: () => {
    return http.get("/produtos");
  },

  salvar: produto => {
    return http.post("/produtos", produto);
  },

  excluirProduto: id => {
    return http.delete("/produtos/" + id);
  },

  buscarProdutoPeloId: id => {
    return http.get("/produtos/" + id);
  }
};
