<template>
  <div class="container">
    <div>
      <Titulo texto="Cadastro de Pedidos" />
    </div>
    <div>
      <b-form @submit="salvar" @reset="onReset" v-if="show">
        <b-form-group id="input-group-1" label="Selecione os produtos:" label-for="input-1">
          <b-form-select v-model="selected" :options="options" multiple :select-size="10" required></b-form-select>
          <div class="mt-3">
            Produtos Selecionados:
            <strong>{{ selected }}</strong>
          </div>
        </b-form-group>

        <b-button type="submit" variant="success" style="margin-right: 10px;">Cadastrar</b-button>
        <b-button type="reset" variant="secondary" style="margin-right: 10px;">Limpar</b-button>
        <router-link to="/pedidos">
          <b-button type="reset" variant="danger" class="btn-cadastrar">Voltar</b-button>
        </router-link>
      </b-form>
    </div>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import PedidoService from "../service/pedidoService";
import ProdutoService from "../service/produtoService";

export default {
  data() {
    return {
      selected: [],
      options: [],
      show: true
    };
  },
  mounted() {
    ProdutoService.listarProdutos().then(resposta => {
      var prods = [];

      resposta.data.forEach(element => {
        /*element.valorVenda = element.valorVenda.toLocaleString("pr-br", {
          style: "currency",
          currency: "BRL"
        });*/

        prods.push(
          element.idProduto +
            "-" +
            element.codInterno +
            "-" +
            element.codBarras +
            "-" +
            element.descricao +
            "-R$" +
            element.valorVenda +
            ".00"
        );
      });

      this.options = prods;
    });
  },
  components: {
    Titulo
  },
  methods: {
    onReset(evt) {
      evt.preventDefault();
      this.selected = [];
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },

    salvar() {
      var produtos = [];

      this.selected.forEach(element => {
        element = String(element).trim();
        element = String(element).replace("R$", "");
        element = String(element).replace(".00", "");
        element = String(element).split("-");

        var produto = {
          idProduto: element[0],
          codInterno: element[1],
          codBarras: element[2],
          descricao: element[3],
          valorVenda: element[4]
        };

        produtos.push(produto);
      });

      PedidoService.salvar(produtos)
        .then(resposta => {
          alert("Pedido salvo com sucesso!\n" + resposta);
        })
        .catch(error => {
          alert("Erro ao salvar pedido!\n" + error);
        });
    },

    validateCodBarras() {
      if (this.form.codBarras < 0 || this.form.codBarras > 999999999999) {
        alert("Valor no código de barras é invalido!");
      }
    }
  }
};
</script>

<style scoped>
.btn-cadastrar {
  float: right;
}
</style>