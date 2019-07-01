<template>
  <div class="container">
    <div>
      <Titulo texto="Listagem de Pedidos" />
    </div>
    <b-row>
      <b-col>
        <b-nav-form style="margin-top: 15px;">
          <b-form-input v-model="filter" placeholder="Pesquisar"></b-form-input>
          <b-button variant="outline-success" :disabled="!filter" @click="filter = ''">Limpar</b-button>
        </b-nav-form>
      </b-col>
      <b-col>
        <router-link to="pedidos/cadastrar">
          <b-button variant="outline-success" class="btn-cadastrar">Cadastrar Pedido</b-button>
        </router-link>
      </b-col>
    </b-row>

    <!-- Main table element -->
    <b-table
      show-empty
      stacked="md"
      :items="pedidos"
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
      v-if="this.pedido != null"
      :id="infoModal.id"
      :title="infoModal.title"
      ok-only
      @hide="resetInfoModal"
    >
      <p class="h6">{{fields[1].label +": "+ pedido.dataPedido}}</p>
      <p class="h6">{{fields[2].label +": "+ pedido.valorTotal}}</p>
    </b-modal>
  </div>
</template>

<script>
import Titulo from "../components/titulo.vue";
import PedidoService from "../service/pedidoService";

export default {
  computed: {
    sortOptions() {
      // Create an options list from our fields
      return this.fields
        .filter(f => f.sortable)
        .map(f => {
          return { text: f.label, value: f.key };
        });
    }
  },
  mounted() {
    this.totalRows = this.pedidos.length;

    PedidoService.listarPedidos().then(resposta => {
      resposta.data.forEach(element => {
        //Formata o valor total do pedido para o formato do Real - R$ 00,00
        element.valorTotal = element.valorTotal.toLocaleString("pr-br", {
          style: "currency",
          currency: "BRL"
        });

        //Chama a função para ajustar a data no formato desejado
        element.dataPedido = this.organizarData(element.dataPedido);

        //inserindo marcara de moeda
        this.pedidos = resposta.data;
      });
    });
  },
  data() {
    return {
      pedidos: [],
      fields: [
        {
          key: "idPedido",
          label: "Id",
          sortable: true,
          sortDirection: "desc"
        },
        {
          key: "dataPedido",
          label: "Data do pedido",
          sortable: true,
          class: "text-center"
        },
        {
          key: "valorTotal",
          label: "Valor total",
          sortable: true,
          class: "text-center"
        },
        {
          key: "pedidoProdutos",
          label: "Número de produtos",
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
      pedido: null
    };
  },
  methods: {
    info(item, index, button) {
      this.pedido = item;
      //titulo do modal
      this.infoModal.title = this.pedido.idPedido;
      this.infoModal.content = JSON.stringify(item, null, 2);
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
    resetInfoModal() {
      this.infoModal.title = "";
      this.infoModal.content = "";
    },
    onFiltered(filteredItems) {
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },

    // metodo que organiza a data no formato desejado
    organizarData(d) {
      var dataSplit = String(d).split("T");
      var data = dataSplit[0];
      var time = dataSplit[1];

      var ano = String(data).split("-")[0];
      var mes = String(data).split("-")[1];
      var dia = String(data).split("-")[2];

      var hora = String(time).split(":")[0];
      var minuto = String(time).split(":")[1];

      return `${dia}/${mes}/${ano} ${hora}:${minuto}`;
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
</style>
