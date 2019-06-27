import Vue from "vue";
import App from "./App.vue";
import BootstrapVue from 'bootstrap-vue'
import VueRouter from 'vue-router'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(VueRouter)
Vue.use(BootstrapVue)

Vue.config.productionTip = false;

import Pedido from './pages/pedido.vue';
import Produto from './pages/produto.vue';

const routes = [
  { path: '/pedidos', component: Pedido },
  { path: '/produtos', component: Produto }
]

const router = new VueRouter({
  routes // short for `routes: routes`
})

new Vue({
  router,
  render: h => h(App)
}).$mount("#app");