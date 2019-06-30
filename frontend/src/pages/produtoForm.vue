<template>
  <div class="container">
    <div>
      <Titulo v-if="path === '/produtos/cadastrar' " texto="Cadastro de produtos" />
      <Titulo v-if="path === '/produtos/alterar'" texto="Alterar de produto" />
    </div>
    <div>
      <b-form @submit="salvar" @reset="onReset" v-if="show">
        <b-form-group id="input-group-1" label="Código Interno:" label-for="input-1">
          <b-form-input id="codInterno" v-model="form.codInterno" type="number" required></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Código de barras:" label-for="input-1">
          <b-form-input v-model="form.codBarras" type="number" required @change="validateCodBarras"></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-2" label="Descrição:" label-for="input-2">
          <b-form-input v-model="form.descricao" required></b-form-input>
        </b-form-group>

        <b-form-group id="input-group-1" label="Valor de venda:" label-for="input-1">
          <b-form-input v-model="form.valorVenda" type="number" required></b-form-input>
        </b-form-group>

        <b-button
          @click="salvar()"
          v-if="path === '/produtos/cadastrar'"
          variant="success"
          style="margin-right: 10px;"
        >Cadastrar</b-button>
        <b-button
          @click="salvar()"
          v-if="path === '/produtos/alterar'"
          variant="success"
          style="margin-right: 10px;"
        >Alterar</b-button>
        <b-button
          v-if="path === '/produtos/cadastrar' "
          type="reset"
          variant="secondary"
          style="margin-right: 10px;"
        >Limpar</b-button>
        <router-link to="/produtos">
          <b-button type="reset" variant="danger" class="btn-cadastrar">Voltar</b-button>
        </router-link>
      </b-form>
    </div>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import ProdutoService from "../service/produtoService";
import produtoService from "../service/produtoService";

export default {
  components: {
    Titulo
  },
  data() {
    return {
      form: {
        idProduto: null,
        codInterno: null,
        codBarras: null,
        descricao: "",
        valorVenda: null
      },
      produto: null,
      path: null,
      show: true
    };
  },
  mounted() {
    this.path = window.location.pathname;
    this.getProduto();
  },
  methods: {
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.form.idProduto = null;
      this.form.codInterno = null;
      this.form.codBarras = null;
      this.form.descricao = "";
      this.form.valorVenda = null;
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },

    salvar() {
      if (this.form.idProduto == null) {
        ProdutoService.salvar(this.form)
          .then(() => {
            alert("Produto salvo com sucesso!");
            this.$router.replace({ path: "/produtos" });
          })
          .catch(error => {
            alert("Erro ao salvar produto!\n" + error);
            this.$router.replace({ path: "/produtos" });
          });
      } else {
        ProdutoService.alterar(this.form)
          .then(() => {
            alert("Produto alterado com sucesso!");
            this.$router.replace({ path: "/produtos" });
          })
          .catch(error => {
            alert("Erro ao alterar produto!\n" + error);
            this.$router.replace({ path: "/produtos" });
          });
      }
    },

    getProduto() {
      var idProduto = window.location.href.split("=")[1];
      produtoService.buscarProdutoPeloId(idProduto).then(resposta => {
        this.form.idProduto = resposta.data.idProduto;
        this.form.codInterno = resposta.data.codInterno;
        this.form.codBarras = resposta.data.codBarras;
        this.form.descricao = resposta.data.descricao;
        this.form.valorVenda = resposta.data.valorVenda;
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