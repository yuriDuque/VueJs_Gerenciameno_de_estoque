import Vue from "vue";
import App from "./App.vue";
import BootstrapVue from "bootstrap-vue";
import VueRouter from "vue-router";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

Vue.use(VueRouter);
Vue.use(BootstrapVue);

Vue.config.productionTip = false;

import Pedido from "./pages/pedido.vue";
import Produto from "./pages/produto.vue";
import PedidoForm from "./pages/pedidoForm.vue";
import ProdutoForm from "./pages/produtoForm.vue";

const routes = [
  { path: "/pedidos", component: Pedido },
  { path: "/produtos", component: Produto },
  { path: "/pedidos/cadastrar", component: PedidoForm },
  { path: "/produtos/cadastrar", component: ProdutoForm },
  { path: "/produtos/alterar/", component: ProdutoForm }
];

const router = new VueRouter({
  routes,
  mode: "history" //remove o # da url
});

new Vue({
  router,
  el: "#app",
  render: h => h(App)
}).$mount("#app");
