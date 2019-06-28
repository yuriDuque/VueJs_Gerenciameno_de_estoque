<template>
  <div>
    <Navbar/>
    <div id="app">
      <router-view/>
    </div>
  </div>
</template>

<script>
import Navbar from "./components/navbar.vue";

export default {
  name: "app",
  components: {
    Navbar
  }
};
</script>

