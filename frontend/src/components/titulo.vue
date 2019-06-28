<template>
  <h2 class="titulo">{{texto}}</h2>
</template>

<script>
export default {
  name: "Titulo",
  props: {
    texto: String
  }
};
</script>

<style scoped>
.titulo {
  text-align: center;
  padding: 20px;
}
</style>