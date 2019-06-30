<template>
  <div class="container">
    <div>
      <Titulo texto="Listagem de Produtos" />
    </div>
    <b-row>
      <b-col>
        <b-nav-form style="margin-top: 15px;">
          <b-form-input v-model="filter" placeholder="Pesquisar"></b-form-input>
          <b-button variant="outline-success" :disabled="!filter" @click="filter = ''">Limpar</b-button>
        </b-nav-form>
      </b-col>
      <b-col>
        <router-link to="produtos/cadastrar">
          <b-button variant="outline-success" class="btn-cadastrar">Cadastrar Produto</b-button>
        </router-link>
      </b-col>
    </b-row>

    <!-- Main table element -->
    <b-table
      show-empty
      stacked="md"
      :items="produtos"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
      striped
      hover
      dark
    >
      <template slot="actions" slot-scope="row">
        <b-button size="sm" @click="info(row.item, row.index, $event.target)" class="mr-1">Detalhes</b-button>
      </template>
    </b-table>

    <b-row>
      <b-col md="6" class="my-1">
        <b-pagination
          v-model="currentPage"
          :total-rows="totalRows"
          :per-page="perPage"
          class="my-0"
        ></b-pagination>
      </b-col>
    </b-row>

    <!-- Info modal -->
    <b-modal
      v-if="this.produto != null"
      :id="infoModal.id"
      :title="infoModal.title"
      ok-only
      @hide="resetInfoModal"
    >
      <p class="h6">{{fields[1].label +": "+ produto.codInterno}}</p>
      <p class="h6">{{fields[2].label +": "+ produto.codBarras}}</p>
      <p class="h6">{{fields[4].label +": "+ produto.valorVenda}}</p>

      <b-button
        variant="outline-success"
        class="btn-alterar"
        @click="alterar(produto)"
      >Alterar produto</b-button>
      <b-button
        variant="outline-danger"
        class="btn-excluir"
        @click="excluir(produto)"
      >Excluir produto</b-button>
    </b-modal>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import ProdutoService from "../service/produtoService";

export default {
  mounted() {
    this.totalRows = this.produtos.length;

    this.listar();
  },
  data() {
    return {
      produtos: [],
      fields: [
        {
          key: "idProduto",
          label: "Id",
          sortable: true,
          sortDirection: "desc"
        },
        {
          key: "codInterno",
          label: "Código Interno",
          sortable: true,
          class: "text-center"
        },
        {
          key: "codBarras",
          label: "Código Barras",
          sortable: true,
          class: "text-center"
        },
        {
          key: "descricao",
          label: "Descrição",
          sortable: true,
          class: "text-center"
        },
        {
          key: "valorVenda",
          label: "Valor de venda",
          sortable: true,
          class: "text-center"
        },
        { key: "actions", label: "Ações" }
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      filter: null,
      infoModal: {
        id: "info-modal",
        title: "",
        content: ""
      },
      produto: null
    };
  },
  methods: {
    info(item, index, button) {
      this.produto = item;
      this.infoModal.title = this.produto.descricao;
      this.infoModal.content = JSON.stringify(item, null, 2);
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
    resetInfoModal() {
      this.infoModal.title = "";
      this.infoModal.content = "";
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },

    listar() {
      ProdutoService.listarProdutos().then(resposta => {
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

    excluir(produto) {
      ProdutoService.excluirProduto(produto.idProduto).then(() => {
        alert("Produto '" + this.produto.descricao + "' excluito com sucesso!");
        this.produtos = null;
        this.listar();
      });
    },

    alterar(produto) {
      this.$router.replace({
        path: "/produtos/alterar?id=" + produto.idProduto
      });
      this.$emit("emit-produtoAlterar", produto);
    }
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
.btn-alterar {
  margin-top: 15px;
  float: left;
}

.btn-excluir {
  margin-top: 15px;
  float: right;
}
</style>
