<template>
  <div>
  <b-navbar type="dark" variant="dark">
    <b-navbar-nav>
      <b-navbar-brand href="">Gerenciamento</b-navbar-brand>
      <b-nav-item href="/pedidos">Pedidos</b-nav-item>
      <b-nav-item href="/produtos">Produtos</b-nav-item>
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