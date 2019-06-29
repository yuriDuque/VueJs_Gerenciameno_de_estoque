<template>
  <div class="container">
    <div>
      <Titulo texto="Cadastro de produtos" />
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

        <b-button type="submit" variant="success" style="margin-right: 10px;">Cadastrar</b-button>
        <b-button type="reset" variant="secondary" style="margin-right: 10px;">Limpar</b-button>
        <router-link to="/produtos">
          <b-button type="reset" variant="danger" class="btn-cadastrar">Voltar</b-button>
        </router-link>
      </b-form>
    </div>
    <ul v-if="posts && posts.length">
      <li v-for="post of posts">
        <p>
          <strong>{{post.title}}</strong>
        </p>
        <p>{{post.body}}</p>
      </li>
    </ul>
    <ul v-if="errors && errors.length">
      <li v-for="error of errors">{{error.message}}</li>
    </ul>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import ProdutoService from "../service/produtoService";

export default {
  components: {
    Titulo
  },
  data() {
    return {
      form: {
        codInterno: null,
        codBarras: null,
        descricao: "",
        valorVenda: null
      },
      posts: [],
      errors: [],
      show: true
    };
  },
  methods: {
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
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
      var produto = {
        codBarras: this.form.codBarras,
        codInterno: this.form.codInterno,
        descricao: this.form.descricao,
        valorVenda: this.form.valorVenda
      };

      console.log(produto);

      ProdutoService.salvar(produto)
        .then(resposta => {
          this.posts = response.data;
        })
        .catch(error => {
          this.errors.push(error);
        });
      console.log("teste");
      alert(errors);
      alert(posts);
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