import { http } from "./config.js";

export default {
  listarProdutos: () => {
    return http.get("/Pedidos");
  },

  salvar: pedido => {
    return http.post("/Pedidos", pedido);
  }
};
