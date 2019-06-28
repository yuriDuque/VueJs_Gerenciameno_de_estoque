<template>
  <div class="container">
    <div>
      <Titulo texto="Listagem de pedidos"/>
    </div>
    <div>
      <b-row>
        <b-col>
          <b-nav-form style="margin-top: 15px;">
            <b-form-input class="mr-sm-2" placeholder="Pesquisar"></b-form-input>
            <b-button variant="outline-success" class="my-2 my-sm-0" type="submit">Pesquisar</b-button>
          </b-nav-form>
        </b-col>
        <b-col>
          <router-link to="pedidos/cadastrar">
            <b-button variant="outline-success" class="btn-cadastrar">Cadastrar Pedido</b-button>
          </router-link>
        </b-col>
      </b-row>
      <tr>
        <th></th>
        <th right></th>
      </tr>
      <tr></tr>
    </div>

    <div class="list">
      <b-table striped hover dark :items="items"></b-table>
    </div>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";

export default {
  components: {
    Titulo
  },
  name: "pedidos-list",
  data() {
    return {
      items: [
        { age: 40, first_name: "Dickerson", last_name: "Macdonald" },
        { age: 21, first_name: "Larsen", last_name: "Shaw" },
        { age: 89, first_name: "Geneva", last_name: "Wilson" },
        { age: 38, first_name: "Jami", last_name: "Carney" }
      ]
    };
  }
};
</script>

<style scoped>
.btn-cadastrar {
  margin: 15px;
  float: right;
}
</style>