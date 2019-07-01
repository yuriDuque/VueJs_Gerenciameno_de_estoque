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

      // Metodos necessarios para remover da string os caracteres indesejados,
      // e com isso poder montar um JSON que possa ser convertido diretamente na API
      this.selected.forEach(element => {
        element = String(element).trim();
        element = String(element).replace("R$", "");
        element = String(element).replace(".00", "");
        element = String(element).split("-");

        //Instancia o produto para depois converte-lo em JSON
        var produto = {
          idProduto: element[0],
          codInterno: element[1],
          codBarras: element[2],
          descricao: element[3],
          valorVenda: element[4]
        };

        produtos.push(produto);
      });

      // Tive dificuldades com o retorno de sucesso da API, sempre retornava erro
      PedidoService.salvar(produtos)
        .then(resposta => {
          alert("Pedido salvo com sucesso!\n" + resposta);
          this.$router.replace({ path: "/pedidos" });
        })
        .catch(error => {
          alert("Erro ao salvar pedido!\n" + error);
          //Acidionado o redirecionamento aqui para corrigir rapidamente o erro de retorno da API
          this.$router.replace({ path: "/pedidos" });
        });
    }
  }
};
</script>

<style scoped>
.btn-cadastrar {
  float: right;
}
</style>