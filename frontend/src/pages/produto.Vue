<template>
  <div class="container">
    <div>
      <Titulo texto="Listagem de produtos"/>
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
          <router-link to="produtos/cadastrar">
            <b-button variant="outline-success" class="btn-cadastrar">Cadastrar Produto</b-button>
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
  name: "produtos-list",
  data() {
    return {
      items: [
        {
          ID: 1,
          Código_Interno: "001",
          Código_Barras: "001",
          Descrição: "Tênis",
          Valor_de_venda: 10.5
        },
        {
          ID: 2,
          Código_Interno: "002",
          Código_Barras: "002",
          Descrição: "Chinelo",
          Valor_de_venda: 10.5
        },
        {
          ID: 3,
          Código_Interno: "003",
          Código_Barras: "003",
          Descrição: "Meia",
          Valor_de_venda: 10.5
        },
        {
          ID: 4,
          Código_Interno: "004",
          Código_Barras: "004",
          Descrição: "Bota",
          Valor_de_venda: 10.5
        }
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