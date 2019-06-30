import { http } from "./config.js";

export default {
  listarPedidos: () => {
    return http.get("/Pedidos");
  },

  salvar: pedido => {
    return http.post("/Pedidos", pedido);
  }
};
