<template>
  <div id="navbar">
    <b-navbar type="dark" variant="dark">
      <b-navbar-nav>
        <b-navbar-brand>
          <router-link class="link" to="/pedidos">Gerenciamento</router-link>
        </b-navbar-brand>
        <b-nav-item>
          <router-link class="link" to="/pedidos">Pedidos</router-link>
        </b-nav-item>
        <b-nav-item>
          <router-link class="link" to="/produtos">Produtos</router-link>
        </b-nav-item>
      </b-navbar-nav>
    </b-navbar>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  props: {
    msg: String
  }
};
</script>

<style scoped>
.link {
  color: white;
  text-decoration: none;
}

.link:hover {
  color: gray;
}
</style>