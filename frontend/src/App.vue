<template>
<script src="https://unpkg.com/vue/dist/vue.js"></script>
<script src="https://unpkg.com/vue-router/dist/vue-router.js"></script>
  <div id="app">
    <Navbar/>
  </div>
</template>

<script>
import Navbar from './components/navbar.vue';

export default {
  name: 'app',
  components: {
    Navbar
  }
}
</script>

