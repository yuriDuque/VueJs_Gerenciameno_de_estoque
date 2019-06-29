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
      <b-table striped hover dark :items="produtos"></b-table>
    </div>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import ProdutoService from "../service/produtoService";
import ProdutoViewModel from "../viewModel/produtoViewModel";

export default {
  mounted() {
    ProdutoService.listarProdutos().then(resposta => {
      console.log(resposta.data);

      resposta.data.forEach(element => {
        element.pedidoProdutos = element.pedidoProdutos.length;

        //inserindo marcara de moeda
        element.valorVenda = element.valorVenda.toLocaleString("pr-br", {
          style: "currency",
          currency: "BRL"
        });
      });

      this.produtos = resposta.data;
    });
  },
  data() {
    return {
      produtos: []
    };
  },
  components: {
    Titulo
  }
};
</script>

<style scoped>
.btn-cadastrar {
  margin: 15px;
  float: right;
}
</style>